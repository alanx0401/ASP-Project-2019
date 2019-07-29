﻿using ITP213.Email;
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
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ITP213.DAL.Peishan_Function
{
    /// <summary>
    /// ******** Note: SMS feature is incomplete!
    /// </summary>
    public class EmailAndPhoneValidation
    {
        // For phone verification
        public static string otp()
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
        public static Tuple<Boolean, string> SendOTP(string email)
        {
            Boolean verdict = false;
            string otpPassword = string.Empty;

            string finalHash;
            string salt;

            try
            {
                DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(email);
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
                        // ************
                        // sendSMSForPhoneVerification(otpPassword, tbContactNumber.Text); // send an sms to the user --> costs $0.05 per sms

                        //lblError.Text = "Your otp password: " + otpPassword; // ****temp
                        verdict = true;
                    }

                    else
                    {
                        /*lblError.Text = "Sorry! An error has occurred!";
                        lblError.ForeColor = System.Drawing.Color.Red;*/
                    }

                }
                else // not the first time the user has verified their phone number
                {
                    // ****===> move this to resend email? When user request to resend otp password 
                    // check if datetimeSend passed a day
                    var currentDateTime = DateTime.Now;
                    var otpDateTimeSend = verifyPhoneOTPObj.dateTimeSend;
                    var diff = currentDateTime.Subtract(otpDateTimeSend);

                    if (diff.Minutes < 1) // if the minute difference is less than 1, password is still valid
                    {
                        // false
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
                                // ************
                                //sendSMSForPhoneVerification(otpPassword, tbContactNumber.Text);
                                //lblError.Text = "Resending otp as it expires: Your otp password: " + otpPassword; // ****temp
                                verdict = true;

                            }

                            else
                            {
                                /*lblError.Text = "Sorry! An error has occurred!";
                                lblError.ForeColor = System.Drawing.Color.Red;*/
                            }
                        }
                        else
                        {
                            /*lblError.Text = "Sorry! An error has occurred!";
                            lblError.ForeColor = System.Drawing.Color.Red;*/
                        }
                    }
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                
            }
            return Tuple.Create(verdict, otpPassword);
        }
        
        public static Tuple<string, string> hashingAndSaltingPassword(string pwd)
        {
            //hashing & salting pwd
            string finalHash;
            string salt;

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

            /*RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;*/

            return Tuple.Create(finalHash, salt);
        }
        public static Boolean phoneVerification(string userpassword, string email)
        {
            Boolean verdict = false;
            try
            {
                
                DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(email);
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
                                    verdict = true;
                                    //Response.Redirect("/EditAccount.aspx"); // Successful!
                                }
                                else
                                {
                                    /*lblError.Text = "Sorry! An error has occurred!";
                                    lblError.ForeColor = System.Drawing.Color.Red;*/
                                }
                            }
                            else
                            {
                                /*lblError.Text = "Sorry! An error has occurred!";
                                lblError.ForeColor = System.Drawing.Color.Red;*/
                            }
                        }
                        else
                        {
                            /*lblError.Text = "Sorry! Password is not valid!";
                            lblError.ForeColor = System.Drawing.Color.Red;*/
                        }
                    }
                    else // ******************send a new otp // if the minute difference is more than 1, password is invalid; Hence, there's a need to generate new password
                    {
                        /*lblError.Text = "Sorry! OTP has expired, we'll send you a new one.";
                        lblError.ForeColor = System.Drawing.Color.Red;*/

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
                            /*lblError.Text = "Your otp password has expired: " + otpPassword;*/

                        }

                        else
                        {
                            /*lblError.Text = "Sorry! An error has occurred!";
                            lblError.ForeColor = System.Drawing.Color.Red;*/
                        }
                    }

                }
                else
                {
                    /*lblError.Text = "Sorry! An error has occurred!";
                    lblError.ForeColor = System.Drawing.Color.Red;*/
                }
            }
            catch (Exception)
            {

            }
            return verdict;
        }
        // For email verification
        public static async Task<SendEmailResponse> Execute(string displayName, string email, string randomToken, string title, string bodyMessage)
        {
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_API_KEY"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("nyptravel2019@gmail.com", "NYP Travel");
            var subject = title;

            var to = new EmailAddress(email, displayName);
            var plainTextContent = "Confirm your account";


            /*Uri uri = HttpContext.Current.Request.Url;
            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;*/
            var htmlContent = bodyMessage;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);



            return new SendEmailResponse();
        }
        public static Tuple<Boolean, string> sendingEmailVerification(string email)
        {
            Boolean verdict = false;
            string lblError = String.Empty;

            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(email);
            string UUID = loginObj.UUID; // retriving UUID from loginObj
            // check if verification email has been sent before
            if (RegisterDAO.checkEmailVerificationTable(UUID) == null) // nth in the verification table
            {
                string randomToken = Guid.NewGuid().ToString(); // email Token
                string encodeRandomToken = EncodeToken(randomToken);

                // insert
                int result = RegisterDAO.insertIntoVerifyEmail(UUID, randomToken);
                if (result == 1)
                {
                    // send an email to the old email about the changing into new email
                    string title = "NYP Travel - Email Confirmation";
                    Uri uri = HttpContext.Current.Request.Url;
                    string host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                    var htmlContent = "Hi, " + loginObj.name + ". Please confirm your account by clicking <strong><a href=\"" + host + "/ConfirmEmail.aspx/?x=" + randomToken + "\" + >here</a></strong>";

                    DAL.Peishan_Function.EmailAndPhoneValidation.Execute(loginObj.name, email, encodeRandomToken, title, htmlContent);
                }
                else
                {
                    lblError = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                }
            }
            else
            {
                // ****===> move this to resend email? When user request to resend email verification
                DAL.Register obj = DAL.RegisterDAO.checkEmailVerificationTable(UUID);

                // check if the verification email has expired
                var currentDateTime = DateTime.Now;
                var emailDateTimeSend = obj.dateTimeSend;
                var diff = currentDateTime.Subtract(emailDateTimeSend);

                if (diff.Hours < 24) // token is still valid
                {
                    lblError = "Please verify your email.";
                }
                else
                {
                    int result = DAL.RegisterDAO.deleteVerifyEmailOTPTable(UUID); // delete current expired token
                    if (result == 1)
                    {
                        string randomToken = Guid.NewGuid().ToString(); // email Token
                        string encodeRandomToken = EncodeToken(randomToken); // not used yet

                        // insert
                        int result2 = RegisterDAO.insertIntoVerifyEmail(UUID, randomToken);
                        if (result2 == 1)
                        {
                            // send an email to the old email about the changing into new email
                            string title = "NYP Travel - Email Confirmation";
                            Uri uri = HttpContext.Current.Request.Url;
                            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                            var htmlContent = "Hi, " + loginObj.name + ". Please confirm your account by clicking <strong><a href=\"" + host + "/ConfirmEmail.aspx/?y=" + randomToken + "\" + >here</a></strong>";

                            DAL.Peishan_Function.EmailAndPhoneValidation.Execute(loginObj.name, email, encodeRandomToken, title, htmlContent);
                            lblError = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                        }
                        else
                        {
                            lblError= "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                        }
                    }
                    else
                    {
                        lblError = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                    }
                }
            }
            return Tuple.Create(verdict, lblError);
        }
        public static string EncodeToken(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }
    }
}