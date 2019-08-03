using Google.Authenticator;
using ITP213.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class GoogleAuth : System.Web.UI.Page
    {
        // for encryption & decryption
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                TextBox1.Attributes.Add("autocomplete", "off");
                if (Session["UUID"] != null)
                {
                    DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
                    if (obj != null)
                    {
                        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();// settings
                                                                                    // secret key --> UUID
                        string UUID = Session["UUID"].ToString().Substring(1, 10); // 10 digits from UUID
                        string secretKey = UUID + otp(); // 10 digits from UUID + otp()
                        Session["sk"] = secretKey;

                        var setupInfo = tfa.GenerateSetupCode("NYP Travel", obj.email, secretKey, 200, 200); //the width and height of the Qr Code);

                        string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl; //  assigning the Qr code information + URL to string
                        string manualEntrySetupCode = setupInfo.ManualEntryKey; // show the Manual Entry Key for the users that don't have app or phone
                        Image1.ImageUrl = qrCodeImageUrl;// showing the qr code on the page "linking the string to image element"
                        Label1.Text = manualEntrySetupCode; // showing the manual Entry setup code for the users that can not use their phone
                    }
                    else
                    {
                        Label1.Text = "Sorry, an error has occurred.";
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (PanelCaptcha.Visible == true)
            {
                verifyGoogleAuth();
            }
            else
            {
                PanelCaptcha.Visible = true;
            }
        }

        public void verifyGoogleAuth()
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());

            if (obj != null)
            {
                string user_enter = TextBox1.Text; // store this password in db when it loads

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

                if (Session["sk"] != null)
                {
                    if (IsReCaptchValid() == true) // CAPTCHA is correct
                    {
                        bool isCorrectPIN = tfa.ValidateTwoFactorPIN(Session["sk"].ToString(), user_enter);

                        /*string[] allPINS = tfa.GetCurrentPINs(Session["sk"].ToString());

                        for (int i = 0; i < allPINS.Length; i++)
                        {
                            Label2.Text += i.ToString() + " :" + allPINS[i] + ", <br>";
                        }*/

                        if (isCorrectPIN == true)
                        {

                            string secretKey = Session["sk"].ToString();
                            secretKey = Convert.ToBase64String(encryptData(secretKey));
                            int result = DAL.SettingsDAO.updateGoogleAuthEnabledAndSecretKeyInAccount(Session["UUID"].ToString(), secretKey, "Yes", Convert.ToBase64String(IV), Convert.ToBase64String(Key));

                            if (result == 1)
                            {
                                Label2.Text = "Done! You're all set!";
                                Label2.ForeColor = System.Drawing.Color.Green;
                                PanelCaptcha.Visible = false;
                            }
                            else
                            {
                                Label2.Text = "Sorry! An error has ocurred!";
                            }

                        }
                        else
                        {

                            Label2.Text = "Incorrect password. Please try again";
                        }
                    }
                    else
                    {
                        Label2.Text = "Please check your CAPTCHA.";
                        Label2.ForeColor = System.Drawing.Color.Red;
                    }
                    
                }
                else
                {
                    Label2.Text = "Sorry! An error has ocurred!";
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

        protected byte[] encryptData(string data)
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;

            byte[] cipherText = null;
            try
            {
                //RijndaelManaged cipher = new RijndaelManaged();
                cipher.Key = Key;
                cipher.IV = IV;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }

            return cipherText;
        }

        public string otp()
        {
            // Generate OTP
            string num = "0123456789";
            int len = num.Length;
            string otp = string.Empty;

            // How many digits otp you want to mention
            int otpdigit = 6;
            string finalDigit;
            int getindex;
            for (int i = 0; i < otpdigit; i++)
            {
                do
                {
                    getindex = new Random().Next(0, len);
                    finalDigit = num.ToCharArray()[getindex].ToString();
                }
                while (otp.IndexOf(finalDigit) != -1);
                otp += finalDigit;
            }

            return otp;
        }
    }
}