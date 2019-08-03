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
    public partial class EditPhoneNumber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbPhoneNumber.Attributes.Add("autocomplete", "off");
            tbPasswordPhoneNumber.Attributes.Add("autocomplete", "off");
            tbOneTimePassword.Attributes.Add("autocomplete", "off");

            if (Session["UUID"] != null)
            {
                if (!IsPostBack)
                {
                    DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
                    if (obj != null)
                    {
                        tbPhoneNumber.Text = obj.mobile;
                        if (obj.phoneVerified == "Yes")
                        {
                            btnResendPhoneVerification.Visible = false;
                            PanelOTP.Visible = false;
                        }
                        else
                        {
                            btnResendPhoneVerification.Visible = true;
                            PanelOTP.Visible = true;

                            checkVerificationTime();
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }

        protected void BtnConfirmOTP_Click(object sender, EventArgs e)
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());

            string password = tbOneTimePassword.Text.Trim();
            string email = obj.email;
            Boolean verdict = DAL.Functions.Validations.EmailAndPhoneValidation.phoneVerification(password, email);
            if (verdict == true)
            {
                Response.Redirect("/ManageYourAccount.aspx");
            }
            else
            {
                lblError.Text = "Sorry, password is either invalid or expired. ";
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

        protected void btnResendPhoneVerification_Click(object sender, EventArgs e)
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            // check password
            var resendPhone = DAL.Functions.Validations.EmailAndPhoneValidation.resendPhoneVerification(obj.email, tbPhoneNumber.Text.Trim());
            if (resendPhone.Item1 == true)
            {
                lblError.Text = resendPhone.Item2.ToString();
                PanelOTP.Visible = true;
                checkVerificationTime();
            }
            else
            {
                lblError.Text = resendPhone.Item3.ToString();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int seconds = int.Parse(Label1.Text);
            if (seconds > 0)
            {
                Label1.Text = (seconds - 1).ToString();
                Label1.Visible = true;
            }
            else
            {
                Label1.Visible = false;
                btnResendPhoneVerification.Enabled = true;

            }
        }

        protected void btnConfirmUpdate_Click(object sender, EventArgs e)
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            if (obj != null)
            {
                // Phone
                if (tbPhoneNumber.Text != obj.mobile) // user has changed their phone number || phone has not been verified
                {
                    PanelOTP.Visible = false;

                    if (PanelEnterPasswordToChangePhoneNo.Visible == true)
                    {
                        Boolean passwordResult = checkPassword(tbPasswordPhoneNumber.Text.Trim());
                        if (passwordResult == true) //************ password matches db password
                        {
                            btnConfirmUpdate.Visible = false;
                            string mobile = tbPhoneNumber.Text.Trim();
                            int result = EditAccountDAO.updateMobileByUUID(mobile, Session["UUID"].ToString());
                            if (result == 1)
                            {
                                // ********show Panel1 & send out one time password --> verify password
                                PanelOTP.Visible = true;
                                string email = obj.email;
                                var sendOTPResult = DAL.Functions.Validations.EmailAndPhoneValidation.SendOTP(email, mobile);

                                Boolean verdict = sendOTPResult.Item1;
                                if (verdict == true)
                                {
                                    string otpPassword = sendOTPResult.Item2;
                                    PanelEnterPasswordToChangePhoneNo.Visible = false;
                                    lblError.Text = "OTP Password: " + otpPassword;
                                    lblError.ForeColor = System.Drawing.Color.Green;

                                    checkVerificationTime();

                                    //btnConfirmUpdate.Visible = false;
                                }
                                else
                                {
                                    lblError.Text = "Sorry! An error has occurred.";
                                    lblError.ForeColor = System.Drawing.Color.Red;
                                }

                            }
                            else
                            {
                                lblError.Text = "Sorry! An error has occurred.";
                            }
                        }
                        else
                        {
                            lblError.Text = "Password invalid. Please enter again.";
                        }
                    }
                    else
                    {
                        PanelEnterPasswordToChangePhoneNo.Visible = true;
                        lblError.Text = "Please enter password to continue";

                    }

                }
                else
                {
                    lblError.Text = "Please change something to update.";
                }
            }
        }
        private void checkVerificationTime()
        {
            DAL.Register verificationObj = DAL.RegisterDAO.checkVerifyPhoneOTP(Session["UUID"].ToString());

            if (verificationObj != null)
            {
                btnResendPhoneVerification.Visible = true;
                // check if the verification email has expired
                var currentDateTime = DateTime.Now;
                var phoneDateTimeSend = verificationObj.dateTimeSend;
                var diff = currentDateTime.Subtract(phoneDateTimeSend);
                var total = (diff.Days * 24 * 60 * 60) + (diff.Hours * 60 * 60) + (diff.Minutes * 60) + diff.Seconds;

                if (total < 25)
                {
                    var time = 25 - total;
                    btnResendPhoneVerification.Enabled = false;
                    Label1.Text = time.ToString();
                    Label1.Visible = true;
                }
                else
                {
                    Label1.Text = "0";
                    Label1.Visible = false;
                    btnResendPhoneVerification.Enabled = true;
                }
            }
            else
            {
                btnResendPhoneVerification.Visible = false;
            }
        }
    }
}