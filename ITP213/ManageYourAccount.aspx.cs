using ITP213.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ManageYourAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnOTP.Attributes.Add("onclick", "return false;");
            if (Session["UUID"] != null)
            {
                if (!IsPostBack)
                {
                    HyperLinkChangePassword.NavigateUrl = "/changePassword.aspx";
                    DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
                    if (obj != null)
                    {

                        lblPhoneNumber.Text = obj.mobile;
                        HyperLinkPhoneNum.NavigateUrl = "/EditPhoneNumber.aspx";
                        lblVerifiedPhoneStatus.Text = obj.phoneVerified;

                        if (obj.phoneVerified == "Yes")
                        {
                            lblVerifiedPhoneStatus.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblVerifiedPhoneStatus.ForeColor = System.Drawing.Color.Red;
                        }

                        lblEmail.Text = obj.email;
                        HyperLinkEmail.NavigateUrl = "/EditEmail.aspx";
                        lblVerifiedEmailStatus.Text = obj.emailVerified;
                        if (obj.emailVerified == "Yes")
                        {
                            lblVerifiedEmailStatus.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblVerifiedEmailStatus.ForeColor = System.Drawing.Color.Red;
                        }

                        if (obj.googleAuthEnabled == "Yes")
                        {
                            lblGoogleAuth.Text = "Enabled";
                            btnGoogleAuth.Visible = true;
                            btnGoogleAuth.Text = "Disable";
                        }
                        else
                        {
                            lblGoogleAuth.Text = "Not enabled";
                            btnGoogleAuth.Text = "Add phone";
                            btnGoogleAuth.Attributes.Add("href", "/GoogleAuth.aspx");
                        }
                        if (obj.phoneVerified == "Yes") // only allow user to enable OTP 2FA when the phone has been verified
                        {
                            Panel1.Visible = true;
                            if (obj.otpEnabled == "Yes")
                            {
                                lblOTP.Text = "Enabled";
                                btnOTP.Text = "Disable"; // change number and also change number from db
                            }
                            else
                            {
                                lblOTP.Text = "Not enabled";
                                btnOTP.Text = "Enable"; // use the database's number and add it.
                            }

                        }
                        else
                        {
                            Panel1.Visible = false;
                            //lblResult.Text = "To enable OTP, please verify your phone";
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
            
            
        }
        protected void btnOTP_Click(object sender, EventArgs e)
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            if (obj != null)
            {
                if (obj.otpEnabled == "Yes")
                {

                    if (PanelCaptcha.Visible == true)
                    {
                        if (IsReCaptchValid() == true)
                        {
                            int result = DAL.SettingsDAO.updateOTPEnabledInAccount(Session["UUID"].ToString(), "No");
                            if (result == 1)
                            {
                                lblResult.Text = "You have successfully disabled OTP";
                                lblOTP.Text = "Not enabled";
                                btnOTP.Text = "Enable"; // use the database's number and add it.

                                PanelCaptcha.Visible = false;
                            }
                            else
                            {
                                lblResult.Text = "Sorry! An error has ocurred. Please try again later.";
                            }
                        }
                        else
                        {
                            lblResult.Text = "Please select CAPTCHA";
                        }
                    }
                    else
                    {
                        PanelCaptcha.Visible = true;
                    }
                }
                else
                {
                    if (PanelCaptcha.Visible == true)
                    {
                        if (IsReCaptchValid() == true)
                        {
                            int result = DAL.SettingsDAO.updateOTPEnabledInAccount(Session["UUID"].ToString(), "Yes");
                            if (result == 1)
                            {
                                lblResult.Text = "You have successfully enabled OTP";
                                lblOTP.Text = "Enabled";
                                btnOTP.Text = "Disable"; // change number and also change number from db

                                PanelCaptcha.Visible = false;
                            }
                            else
                            {
                                lblResult.Text = "Sorry! An error has ocurred. Please try again later.";
                            }
                        }
                        else
                        {
                            lblResult.Text = "Please select CAPTCHA";
                        }
                    }
                    else
                    {
                        PanelCaptcha.Visible = true;
                    }
                }
            }
        }
        protected void btnGoogleAuth_Click(object sender, EventArgs e)
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            if (lblGoogleAuth.Text == "Not enabled")
            {
                Response.Redirect("/GoogleAuth.aspx");
            }
            else if (lblGoogleAuth.Text == "Enabled") // must remove googleAuthEnabled
            {

                int result = DAL.SettingsDAO.updateGoogleAuthEnabledAndSecretKeyInAccount(Session["UUID"].ToString(), "", "No", "", "");

                if (result == 1)
                {
                    lblResult.Text = "You have successfully disabled OTP";
                    lblGoogleAuth.Text = "Not enabled";
                    btnGoogleAuth.Text = "Add phone";
                }
                else
                {
                    lblResult.Text = "Sorry! An error has ocurred. Please try again later.";
                }
            }
        }
        public bool IsReCaptchValid() // CAPTCHA
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = Environment.GetEnvironmentVariable("SecretKey");
            //var secretKey = ConfigurationManager.AppSettings["SecretKey"];
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }
    }
}