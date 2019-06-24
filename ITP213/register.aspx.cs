using ITP213.DAL;
using ITP213.Email;
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
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ITP213
{
    public partial class register : System.Web.UI.Page
    {
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // disable autocomplete form        
            tbName.Attributes.Add("autocomplete", "off");
            tbAdminNo.Attributes.Add("autocomplete", "off");
            tbEmail.Attributes.Add("autocomplete", "off");
            tbPassword.Attributes.Add("autocomplete", "off");
            tbConfirmPassword.Attributes.Add("autocomplete", "off");
            tbDateOfBirth.Attributes.Add("autocomplete", "off");
            tbContactNumber.Attributes.Add("autocomplete", "off");
            tbVerifyPassword.Attributes.Add("autocomplete", "off");

            /*
            if (IsPostBack)
            {
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

            registeringAccount(tbPassword.Text); // register
            
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
                int result = RegisterDAO.updateById(tbContactNumber.Text, tbDateOfBirth.Text, tbAdminNo.Text); // Based on admin no
                if (result == 1) // contactNumber and dateOfBirth are successfully updated
                {
                    SendOTP();
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
            // Verify email
            string sendEmail = ConfigurationManager.AppSettings["SendEmail"];
            if (sendEmail.ToLower() == "true")
            {
                //var emailVerificatonCode = mUserManger.Gener
                //Execute("Hi","www.localhost");
                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                //Execute().Wait();

                //Execute(tbName.Text, tbEmail.Text);

                sendingEmailVerification();

            }

            // Verify phone code
            phoneVerification(tbVerifyPassword.Text);
        }

        //=============================================================================================================================================================
        // hashing + salting password method
        public Tuple<string, string> hashingAndSaltingPassword(string pwd)
        {
            //hashing & salting pwd
            string finalHash;
            string salt;
            byte[] Key;
            byte[] IV;

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

            return Tuple.Create(finalHash, salt);
        }

        private void registeringAccount(string pwd)
        {
            string finalHash;
            string salt;

            try
            {
                // hashing + salting password
                string password = string.Empty;
                password = pwd.Trim();
                var getHashingAndSaltingPwd = hashingAndSaltingPassword(password);

                finalHash = string.Empty;
                finalHash = getHashingAndSaltingPwd.Item1;

                salt = string.Empty;
                salt = getHashingAndSaltingPwd.Item2;

                string UUID = Guid.NewGuid().ToString("X"); // generate random UUID
                int result = RegisterDAO.insertIntoAccountTable(UUID, tbName.Text, tbEmail.Text, finalHash, salt); // account table
                int result2 = RegisterDAO.insertaAdminNoInAdminTable(tbAdminNo.Text, UUID); // student table
                if (result == 1 && result2 == 1)
                {
                    btnBack.Visible = false;
                }
            }
            catch (Exception)
            {

            }
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
        public void sendSMSForPhoneVerification(string password, string verifyNumber)
        {
            // sms
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            Twilio.TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber(verifyNumber); // Verifying number
            var from = new PhoneNumber("+12565703020"); // Twilio num

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                | SecurityProtocolType.Tls11
                                                | SecurityProtocolType.Tls12
                                                | SecurityProtocolType.Ssl3;
            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Your OTP for phone verification is " + password);
        }
        public void SendOTP() // Part 2  
        {
            string otpPassword;
            string finalHash;
            string salt;

            try
            {
                DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
                string UUID = loginObj.UUID; // retriving UUID from loginObj

                // 1) Check if the data has alr existed
                DAL.Register verifyPhoneOTPObj = RegisterDAO.checkVerifyPhoneOTP(UUID);
                if (verifyPhoneOTPObj == null) // first time the user has verified their phone number
                {
                    otpPassword = string.Empty;
                    otpPassword = otp().Trim(); // Generate random 6 digit OTP

                    var getHashingAndSaltingPwd = hashingAndSaltingPassword(otpPassword);

                    finalHash = string.Empty;
                    finalHash = getHashingAndSaltingPwd.Item1;

                    salt = string.Empty;
                    salt = getHashingAndSaltingPwd.Item2;

                    int result = RegisterDAO.insertIntoVerifyPhoneOTP(UUID, finalHash, salt); // insert the otp as they do not exist

                    if (result == 1)
                    {
                        // sendSMSForPhoneVerification(otpPassword, tbContactNumber.Text); // send an sms to the user --> costs $0.05 per sms
                        lblError.Text = "Your otp password: "+otpPassword; // ****temp
                    }

                    else
                    {
                        lblError.Text = "Sorry! An error has occurred!";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else // not the first time the user has verified their phone number
                {
                    // check if datetimeSend passed a day
                    var currentDateTime = DateTime.Now;
                    var otpDateTimeSend = verifyPhoneOTPObj.dateTimeSend;
                    var diff = currentDateTime.Subtract(otpDateTimeSend);

                    if (diff.Minutes < 1) // if the minute difference is less than 1, password is still valid
                    {

                    }
                    else // ******************send a new otp // if the minute difference is more than 1, password is invalid; Hence, there's a need to generate new password
                    {
                        
                        int result = RegisterDAO.deleteVerifyPhoneOTPTable(UUID); // delete the otpValue
                        if (result == 1)
                        {
                            otpPassword = string.Empty;
                            otpPassword = otp().Trim(); // Generate random 6 digit OTP

                            var getHashingAndSaltingPwd2 = hashingAndSaltingPassword(otpPassword);

                            finalHash = string.Empty;
                            finalHash = getHashingAndSaltingPwd2.Item1;

                            salt = string.Empty;
                            salt = getHashingAndSaltingPwd2.Item2;

                            int result2 = RegisterDAO.insertIntoVerifyPhoneOTP(UUID, finalHash, salt); // insert new otp

                            if (result2 == 1)
                            {
                                //sendSMSForPhoneVerification(otpPassword, tbContactNumber.Text);
                                lblError.Text = "Resending otp as it expires: Your otp password: " + otpPassword; // ****temp
                            }

                            else
                            {
                                lblError.Text = "Sorry! An error has occurred!";
                                lblError.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lblError.Text = "Sorry! An error has occurred!";
                            lblError.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                
            }
            catch (Exception)
            {

            }

           
        }
        public void phoneVerification(string userpassword) // Part 3
        {
            try
            {
                DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
                string UUID = loginObj.UUID;

                // 1) Check if the data has alr existed
                DAL.Register verifyPhoneOTPObj = RegisterDAO.checkVerifyPhoneOTP(UUID);

                if (verifyPhoneOTPObj != null) // they should receive an sms by now; hence, verifyPhoneOTPObj shouldn't be null
                {
                    // check if datetimeSend passed a day
                    var currentDateTime = DateTime.Now;
                    var otpDateTimeSend = verifyPhoneOTPObj.dateTimeSend;
                    var diff = currentDateTime.Subtract(otpDateTimeSend);

                    if (diff.Minutes < 1) // if the minute difference is less than 1, password is still valid
                    {
                        // ********** get db hash & db salt
                        SHA512Managed hashing = new SHA512Managed();
                        string dbHash = verifyPhoneOTPObj.passwordHash;
                        string dbSalt = verifyPhoneOTPObj.passwordSalt;

                        // validating password
                        string pwdWithSalt = userpassword + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);

                        if (userHash.Equals(dbHash)) // MATCH; UPDATE ACCOUNT
                        {
                            int result = RegisterDAO.updatePhoneVerifiedInAccountTable(UUID);
                            if (result == 1)
                            {
                                int result2 = RegisterDAO.deleteVerifyPhoneOTPTable(UUID); // delete OTP table
                                if (result2 == 1)
                                {
                                    Response.Redirect("/login.aspx"); // Successful!
                                }
                                else
                                {
                                    lblError.Text = "Sorry! An error has occurred!";
                                    lblError.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                lblError.Text = "Sorry! An error has occurred!";
                                lblError.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else 
                        {
                            lblError.Text = "Sorry! Password is not valid!";
                            lblError.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else // ******************send a new otp // if the minute difference is more than 1, password is invalid; Hence, there's a need to generate new password
                    {
                        lblError.Text = "Sorry! OTP has expired, we'll send you a new one.";
                        lblError.ForeColor = System.Drawing.Color.Red;

                        // sending a new otp
                        string otpPassword = string.Empty;
                        otpPassword = otp().Trim();

                        var getHashingAndSaltingPwd2 = hashingAndSaltingPassword(otpPassword);

                        string finalHash = string.Empty;
                        finalHash = getHashingAndSaltingPwd2.Item1;

                        string salt = string.Empty;
                        salt = getHashingAndSaltingPwd2.Item2;

                        int result = RegisterDAO.insertIntoVerifyPhoneOTP(UUID, finalHash, salt); // insert the otp as they do not exist

                        if (result == 1)
                        {
                            //sendSMSForPhoneVerification(otpPassword, tbContactNumber.Text);
                            lblError.Text = "Your otp password has expired: " + otpPassword;
                        }

                        else
                        {
                            lblError.Text = "Sorry! An error has occurred!";
                            lblError.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                        
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

        public static async Task<SendEmailResponse> Execute(string displayName, string email, string randomToken)
        {
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_API_KEY"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("nyptravel2019@gmail.com", "NYP Travel");
            var subject = "NYP Travel - Email Confirmation";

            var to = new EmailAddress(email, displayName);
            var plainTextContent = "Confirm your account";

            
            Uri uri = HttpContext.Current.Request.Url;
            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
            var htmlContent = "Hi, " + displayName + ". Please confirm your account by clicking <strong><a href=\"" + host + "/ConfirmEmail.aspx/?x=" + randomToken +"\" + >here</a></strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            

            return new SendEmailResponse();
        }

        public void sendingEmailVerification()
        {
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            string UUID = loginObj.UUID; // retriving UUID from loginObj
            // check if verification email has been sent before
            if (RegisterDAO.checkEmailVerificationTable(UUID) == null) // nth in the verification table
            {
                string randomToken = Guid.NewGuid().ToString(); // email Token
                string encodeRandomToken = EncodeServerName(randomToken); // not used 

                // insert
                int result = RegisterDAO.insertIntoVerifyEmail(UUID, randomToken);
                if (result == 1)
                {
                    Execute(tbName.Text, tbEmail.Text, randomToken);
                }
                else
                {
                    lblError.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                DAL.Register obj = DAL.RegisterDAO.checkEmailVerificationTable(UUID);

                // check if the verification email has expired
                var currentDateTime = DateTime.Now;
                var emailDateTimeSend = obj.dateTimeSend;
                var diff = currentDateTime.Subtract(emailDateTimeSend);

                if (diff.Hours < 24) // token is still valid
                {
                    lblError.Text = "Please verify your email.";
                }
                else
                {
                    int result = DAL.RegisterDAO.deleteVerifyEmailOTPTable(UUID); // delete current expired token
                    if (result == 1)
                    {
                        string randomToken = Guid.NewGuid().ToString(); // email Token
                        string encodeRandomToken = EncodeServerName(randomToken); // not used 

                        // insert
                        int result2 = RegisterDAO.insertIntoVerifyEmail(UUID, randomToken);
                        if (result2 == 1)
                        {
                            
                            Execute(tbName.Text, tbEmail.Text, randomToken);
                            lblError.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                            lblError.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lblError.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                            lblError.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblError.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        public static string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public static string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }

    }
}