using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ITP213
{
    public partial class register : System.Web.UI.Page
    {
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            phoneVerification(); // --> costs $0.05 per sms
            /*string password;
            if (IsPostBack)
            {
            
            if (!(String.IsNullOrEmpty(tbPassword.Text.Trim())))
                {
                    tbPassword.Attributes["value"] = tbPassword.Text;
                    password = Request.Form["tbPassword"].ToString();
                }
            }*/
        }
        protected void btnNext_Click(object sender, EventArgs e) // Panel 1
        {
            PanelPart1.Visible = false;
            btnNext.Visible = false;
            PanelPart2.Visible = true;
            btnBack.Visible = true;
            btnRegister.Visible = false;
            Label1.Text = "More information details";
            lblLogin.Visible = false;
            btnNext1.Visible = true;

            // -------------------
            hashingAndSaltingPassword(tbPassword.Text);

            try
            {
                string UUID = Guid.NewGuid().ToString("X");
                int result = LoginDAO.insertIntoAccountTable(UUID, tbName.Text, tbEmail.Text, finalHash, salt); // account table
                int result2 = LoginDAO.insertaAdminNoInAdminTable(tbAdminNo.Text, UUID); // student table
                if (result == 1 && result2 == 1)
                {
                    btnBack.Visible = false;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnNext1_Click(object sender, EventArgs e) // Panel 2
        {
            PanelPart1.Visible = false;
            btnNext.Visible = false;
            btnNext1.Visible = false;
            PanelPart2.Visible = false;
            btnBack.Visible = false;
            btnBack1.Visible = true;
            btnRegister.Visible = true;
            Label1.Text = "Verify your phone number";
            lblLogin.Visible = false;
            PanelPart3.Visible = true;

            try
            {
                int result = LoginDAO.updateById(tbContactNumber.Text, tbDateOfBirth.Text, tbAdminNo.Text);
                if (result == 1)
                {

                }
                else
                {
                    lblError.Text = "Sorry! An error has occurred!";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            PanelPart1.Visible = true;
            btnNext.Visible = true;
            btnNext1.Visible = false;
            PanelPart2.Visible = false;
            PanelPart3.Visible = false;
            btnBack.Visible = false;
            btnRegister.Visible = false;
            Label1.Text = "Register";
            lblLogin.Visible = true;
        }
        protected void btnBack1_Click(object sender, EventArgs e)
        {
            PanelPart1.Visible = false;
            PanelPart2.Visible = true;
            PanelPart3.Visible = false;
            btnNext.Visible = true;
            btnNext1.Visible = false;
            btnBack.Visible = false;
            btnRegister.Visible = false;
            Label1.Text = "More information details";
            lblLogin.Visible = true;
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("/login.aspx");
        }
        //=============================================================================================================================================================
        public void phoneVerification()
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

            lblError.Text = otp;
            /*string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber("+6583997254"); // Verifying number
            var from = new PhoneNumber("+12565703020"); // Twilio num

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                | SecurityProtocolType.Tls11
                                                | SecurityProtocolType.Tls12
                                                | SecurityProtocolType.Ssl3;
            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "My first sms");*/

        }
        // hashing + salting password
        private void hashingAndSaltingPassword(string pwd)
        {
            string password = pwd.Trim();

            // Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            // Fills array of bytes with a cryptographically strong sequence of random values;
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            string pwdWithSalt = password + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(password));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

            finalHash = Convert.ToBase64String(hashWithSalt);

            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;
        }
    }
}