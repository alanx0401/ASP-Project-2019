using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class EditEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbEmail.Attributes.Add("autocomplete", "off");
            tbPasswordEmail.Attributes.Add("autocomplete", "off");
            if (!IsPostBack)
            {
                DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
                if (obj != null)
                {
                    tbEmail.Text = obj.email;

                    if (obj.emailVerified == "Yes")
                    {
                        btnResendEmailVerification.Visible = false;
                    }
                    else
                    {
                        btnResendEmailVerification.Visible = true;
                    }
                }
            }
        }

        protected void btnConfirmUpdate_Click(object sender, EventArgs e)
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            if (obj != null)
            {
                // Email
                if (tbEmail.Text != obj.email)  // ****** improvement: inform the previous email owner about the change in email. They can click on the link. If they didn't request for it, they can change back to the original email
                {
                    if (tbPasswordEmail.Visible == true && lblPasswordEmail.Visible == true)
                    {


                        Boolean passwordResult = checkPassword(tbPasswordEmail.Text.Trim());
                        if (passwordResult == true) // password
                        {
                            string newEmail = tbEmail.Text.Trim();
                            string previousEmail = obj.email; // email has not been changed yet

                            // decide if user should store old email in the database (only store them if email has been verified!)
                            if (obj.emailVerified == "Yes")
                            {
                                string randomToken = Guid.NewGuid().ToString(); // token in case email is not changed by user
                                string encodeRandomToken = DAL.Peishan_Function.EmailAndPhoneValidation.EncodeToken(randomToken);
                                DateTime changedDate = DateTime.Now;

                                int result2 = EditAccountDAO.insertOldEmailByUUID(previousEmail, Session["UUID"].ToString(), randomToken, DateTime.Now);
                                if (result2 == 1)
                                {
                                    // send an email to the old email about the changing into new email
                                    string title = "NYP Travel - Security Alert";
                                    Uri uri = HttpContext.Current.Request.Url;
                                    string host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                                    var htmlContent = "Hi, " + obj.name + ". You have requested to change your email to " + newEmail + " at "+ changedDate.ToString() + ". If you did not change your email, please click <strong><a href=\"" + host + "/ConfirmEmail.aspx/?y=" + encodeRandomToken + "\" + >here</a></strong>. Thank you!";
                                    DAL.Peishan_Function.EmailAndPhoneValidation.Execute(obj.name, previousEmail, encodeRandomToken, title, htmlContent);

                                    int result = EditAccountDAO.updateEmailByUUID(newEmail, Session["UUID"].ToString());

                                    if (result == 1)
                                    {
                                        // send out email: *******
                                        DAL.Peishan_Function.EmailAndPhoneValidation.sendingEmailVerification(newEmail);

                                        Response.Redirect("/ManageYourAccount.aspx");
                                    }
                                    else
                                    {
                                        lblError.Text = "Sorry! An error has occurred.";
                                    }
                                }
                                else
                                {
                                    lblError.Text = "Sorry! An error has occurred.";
                                }
                            }
                            else // does not need to send out security alert email cos it has not been verified yet
                            {
                                int result = EditAccountDAO.updateEmailByUUID(newEmail, Session["UUID"].ToString());
                                if (result == 1)
                                {
                                    // send out email: *******
                                    DAL.Peishan_Function.EmailAndPhoneValidation.sendingEmailVerification(newEmail);


                                }
                                else
                                {
                                    lblError.Text = "Sorry! An error has occurred.";
                                }
                            }
                        }
                        else
                        {
                            lblError.Text = "You have entered the wrong password.";
                        }
                    }
                    else
                    {
                        tbPasswordEmail.Visible = true;
                        lblPasswordEmail.Visible = true;
                        lblError.Text = "Please enter your password to continue";
                    }
                    
                }
                else
                {
                    lblError.Text = "Please change something to update.";
                }
            }
        }

        protected Boolean checkPassword(string pwd)
        {
            //string pwd = tbPasswordPhoneNumber.Text.Trim();
            Boolean verdict = false;
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = SettingsDAO.getDBHash(Session["UUID"].ToString());
            string dbSalt = SettingsDAO.getDBSalt(Session["UUID"].ToString());

            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    if (userHash == dbHash) // password matches
                    {
                        verdict = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.ToString();
                //throw new Exception(ex.ToString());
                lblError.Text = "An error has occured. Please try again.";
            }
            finally { }

            return verdict;
        }

        protected void btnResendEmailVerification_Click(object sender, EventArgs e)
        {
            var resendEmail = DAL.Peishan_Function.EmailAndPhoneValidation.resendEmailVerification(tbEmail.Text.Trim());
            if (resendEmail.Item1 == true)
            {
                //btnResendEmailVerification.Text = false;
            }
            else
            {
                lblError.Text = resendEmail.Item2.ToString();
            }
        }
    }
}