using ITP213.DAL;
using ITP213.Email;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ITP213
{
    public partial class EditAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UUID"] != null)
                {
                    DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
                    if (obj != null)
                    {
                        tbEmail.Text = obj.email;
                        tbPhoneNumber.Text = obj.mobile;

                        if (obj.emailVerified == "Yes")
                        {
                            btnResendEmailVerification.Visible = false;
                        }
                        else
                        {
                            btnResendEmailVerification.Visible = true;
                        }
                        if (obj.phoneVerified == "Yes")
                        {
                            btnResendPhoneVerification.Visible = false;
                        }
                        else
                        {
                            btnResendPhoneVerification.Visible = true;
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
        }

        protected void btnConfirmUpdate_Click(object sender, EventArgs e) // update email & phone number
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            if (obj != null)
            {
                // Phone
                if (tbPhoneNumber.Text != obj.mobile) // user has changed their phone number
                {
                    tbPasswordPhoneNumber.Visible = true;
                    lblPasswordPhoneNumber.Visible = true;

                    // ===============================================================

                    //===============================================================
                    Boolean passwordResult = checkPassword(tbPasswordPhoneNumber.Text.Trim());
                    if (passwordResult == true) //************ password matches db password
                    {
                        string mobile = tbPhoneNumber.Text.Trim();
                        int result = EditAccountDAO.updateMobileByUUID(mobile, Session["UUID"].ToString());
                        if (result == 1)
                        {
                            // ********show Panel1 & send out one time password --> verify password
                            Panel1.Visible = true;
                            string email = tbEmail.Text.Trim();
                            var sendOTPResult = DAL.Peishan_Function.EmailAndPhoneValidation.SendOTP(email);

                            Boolean verdict = sendOTPResult.Item1;
                            if (verdict == true)
                            {
                                string otpPassword = sendOTPResult.Item2;

                                lblError.Text = "OTP Password: " + otpPassword;
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
                }
                
                // Email
                if (tbEmail.Text != obj.email)  // ****** improvement: inform the previous email owner about the change in email. They can click on the link. If they didn't request for it, they can change back to the original email
                {
                    tbPasswordEmail.Visible = true;
                    lblPasswordEmail.Visible = true;

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

                            int result2 = EditAccountDAO.insertOldEmailByUUID(previousEmail, Session["UUID"].ToString(), encodeRandomToken);
                            if (result2 == 1)
                            {
                                // send an email to the old email about the changing into new email
                                string title = "NYP Travel - Security Alert";
                                Uri uri = HttpContext.Current.Request.Url;
                                string host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                                var htmlContent = "Hi, " + obj.name + ". You have requested to change your email to " + newEmail + ". If you did not change your email, please click <strong><a href=\"" + host + "/ConfirmEmail.aspx/?y=" + randomToken + "\" + >here</a></strong>. Thank you!";
                                DAL.Peishan_Function.EmailAndPhoneValidation.Execute(obj.name, previousEmail, encodeRandomToken, title, htmlContent);

                                int result = EditAccountDAO.updateEmailByUUID(newEmail, Session["UUID"].ToString());
                                if (result == 1)
                                {
                                    // send out email
                                    DAL.Peishan_Function.EmailAndPhoneValidation.sendingEmailVerification(newEmail);


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
                    }
                }
            }
            else
            {
                lblError.Text = "Please change something to update.";
            }
        }

        protected void BtnConfirmOTP_Click(object sender, EventArgs e)
        {
            string password = tbOneTimePassword.Text.Trim();
            string email = tbEmail.Text.Trim();
            Boolean verdict = DAL.Peishan_Function.EmailAndPhoneValidation.phoneVerification(password, email);
            if (verdict == true)
            {
                Response.Redirect("/ManageYourAccount.aspx");
            }
            else
            {
                lblError.Text = "Sorry, password is either invalid or expired. If it is expired, we'll send you a new one.";
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
    }
}