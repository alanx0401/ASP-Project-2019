using Google.Authenticator;
using ITP213.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ITP213
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) // First Load of the page
            {
                if (Request.Browser.Cookies) // check if browser supports cookies
                {
                    if (Request.QueryString["CheckCookie"] == null)
                    {
                        HttpCookie cookie = new HttpCookie("TestCookie", "1");
                        Response.Cookies.Add(cookie);
                        Response.Redirect("~/login.aspx?CheckCookie=1");
                    }
                    else
                    {
                        HttpCookie cookie = Request.Cookies["TestCookie"];
                        if (cookie == null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "alert('We have detected that the cookies are disabled on your browser. Please enable them.');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "alert('Browser don't support cookies. Please install one of the modern browser.');", true);
                }
            }

            // added in Year 3 Sem 1
            // disable autocomplete form        
            tbEmail.Attributes.Add("autocomplete", "off");
            tbPassword.Attributes.Add("autocomplete", "off");
            tb2FAPin.Attributes.Add("autocomplete", "off");

            // check recaptcha verification if fail count is more than or equal to 6
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            if (loginObj != null) // check if user has alr inputted their email in the textbox.
            {
                DAL.Login AccountFailedAttemptObj = LoginDAO.getAccountFailedAttemptByUUID(loginObj.UUID);
                if (AccountFailedAttemptObj != null) 
                {
                    int failCount = AccountFailedAttemptObj.AccountFailedAttemptCounter;
                    if (failCount >= 9) // ban account if failCount is more than or equals to 9.
                    {
                        lblError.Text = "Sorry! Your account is temporarily ban. Please try again later.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (failCount >= 5) // implement reCAPTCHA
                    {
                        ReCaptchContainer.Visible = true;
                    }
                    else
                    {
                        ReCaptchContainer.Visible = false;
                    }
                }
            }

            // temporary
            /*DAL.Login obj = LoginDAO.getLoginByEmailAndPassword("linpeishann@gmail.com");

            Session["UUID"] = obj.UUID;
            // **** Find ways to remove the session below
            Session["accountID"] = obj.UUID;
            Session["accountType"] = obj.accountType;
            Session["name"] = obj.name;
            Session["email"] = "linpeishann@gmail.com";
            Session["mobile"] = obj.mobile;
            Session["dateOfBirth"] = obj.dateOfBirth;

            if (Session["accountType"].ToString() == "student")
            {
                // student table
                DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(obj.UUID);
                Session["adminNo"] = studentObj.adminNo;
                Session["school"] = studentObj.studentSchool;
                Session["course"] = studentObj.course;
                Session["allergies"] = studentObj.allergies;
                Session["dietaryNeeds"] = studentObj.dietaryNeeds;

            }

            Response.Redirect("Default.aspx");*/
            // ------ temp
        }
        protected void btnPanel1_Click(object sender, EventArgs e) // Submit login form and checks if pwd and email matches. 
        {
            // If it matches, check if 2FA is enabled.
            // --> If 2FA is enabled, check what is enabled - googleAuth || OTP
            // --> If 2FA is not enabled, just login.
            // If it doesn't, check for numberOfFailedAttempts. 
            // --> If attempt is more than/ equal to 6, CAPTCHA. 
            // --> If attempt is more than 10, ban account msg

            // fix error exception msg!
            Panel1();
        }
        protected void btnPanel2_Click(object sender, EventArgs e) // Part 2 --> Part 3
        {
            PanelPart2.Visible = false;
            PanelPart3.Visible = true;

            lblTitle.Text = "Enter verification code";

            Panel2();
        }

        protected void btnPanel3_Click(object sender, EventArgs e) // Part 3 --> Part 2
        {
            //-- Check if the password is crt, if it is, then find the UUID to retrieve personal datas

            // Assumming password is checked/verified in Part 1, now, you have to verify if 2FA value is correct.
            // If user selects googleAuth
            // --> check if the code is valid
            // ----> if it is valid, allow user to access /default.aspx
            // ----> If it is not valid, failedAttemptCounter gets increased by 1
            // Else if user selects 2FA --> check if the password expires
            // ---> If it did, send a password
            // ---> If it didn't, validate if it matches the one in the database
            // ------> If it did, allow user to access /default.aspx
            // ------> Else, failedAttemptCounter gets increased by 1.
            // delete account failed attempts
            /*DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            string UUID = loginObj.UUID;
            DAL.Login AccountFailedAttemptObj = getAccountFailedAttemptByUUID(UUID);

            if (AccountFailedAttemptObj != null) // Delete failed attempt(s)
            {

                int result = deleteAccountFailedAttemptTableByUUID(UUID);
                if (result == 1) { }
                else
                {
                    lblError.Text = "An error has occured. Please try again.";
                }

            }*/
            //-- If password is wrong,
            /*else // if login fails
            {*/
            Panel3();
            /*DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            string UUID = loginObj.UUID;
            DAL.Login AccountFailedAttemptObj = LoginDAO.getAccountFailedAttemptByUUID(UUID);

            if (AccountFailedAttemptObj != null) // update
            {
                int failCount = AccountFailedAttemptObj.AccountFailedAttemptCounter;
                if (failCount == 10)
                {
                    int result = LoginDAO.updateAccountStatusToBanByUUID(UUID);
                    if (result == 1) { }
                    else
                    {
                        lblError.Text = "An error has occured. Please try again.";
                    }
                }
                else
                {
                    failCount += 1;

                    int result = LoginDAO.updateAccountFailedAttemptTableByUUID(UUID, failCount);
                    if (result == 1) { }
                    else
                    {
                        lblError.Text = "An error has occured. Please try again.";
                    }
                }

            }
            else // insert
            {
                int result = LoginDAO.insertAccountFailedAttemptTable(UUID);
                if (result == 1) { }
                else
                {
                    lblError.Text = "An error has occured. Please try again.";
                }
            }


            lblError.Text = "Please check your email or password!";
            lblError.ForeColor = System.Drawing.Color.Red;
            //}
            // Password Expiration: cannot be more than a year --> Based on UUID
            DAL.Login p = LoginDAO.getChangePasswordDateByEmailAndPasswordHash("{0x39d609a5,0x10eb,0x40a8,{0x86,0xf1,0x06,0x0b,0xfe,0xe7,0xc1,0x82}}");

            DateTime changePasswordDate = Convert.ToDateTime(p.changePasswordDate.ToString());

            if ((DateTime.Now - changePasswordDate).TotalDays > 365)// if bigger than one year
            {
                Response.Redirect("changePassword.aspx");
            }
            string PCName = Dns.GetHostEntry(Request.ServerVariables["REMOTE_ADDR"]).HostName;

            Response.Redirect("Default.aspx");*/
        }

        protected void btnBack2_Click(object sender, EventArgs e) 
        {
            PanelPart2.Visible = true;
            PanelPart3.Visible = false;

            lblTitle.Text = "Send verification code";
        }
        // =================================================================================================================
        // Summary for Panel 1
        private void Panel1()
        {
            string pwd = tbPassword.Text.ToString().Trim();
            string email = tbEmail.Text.Trim().ToString();
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = LoginDAO.getDBHash(email);
            string dbSalt = LoginDAO.getDBSalt(email);

            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    if (CheckBanAccount() == true) // check ban account
                    {
                        lblError.Text = "Sorry! Your account is temporarily ban. Please try again later.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                    else // if account is not ban, continue verifying the user
                    {
                        DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
                        string UUID = loginObj.UUID;

                        if (userHash == dbHash) // password matches
                        {
                            if (ReCaptchContainer.Visible == true) // accountFailedAttempt is equal or more than 6 times --> To check the CAPTCHA value
                            {
                                if (IsReCaptchValid() == true) // CAPTCHA is correct
                                {
                                    moveToPanel2();
                                }
                                else
                                {
                                    lblError.Text = "Please check your email or password or CAPTCHA value!";
                                    lblError.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else // accountFailedAttempt is less than 6 times
                            {
                                moveToPanel2();
                            }
                        }
                        else // if login fails *****problem with counting
                        {
                            DAL.Login AccountFailedAttemptObj = LoginDAO.getAccountFailedAttemptByUUID(UUID);

                            if (AccountFailedAttemptObj != null) // update failed attempt
                            {
                                int failCount = AccountFailedAttemptObj.AccountFailedAttemptCounter;
                                if (failCount >= 9) // change account status from 'Not ban' to 'Ban' when fail count hits 10 in db ; #problem: doesn't update to 10
                                {
                                    int result2 = LoginDAO.updateAccountStatusToBanByUUID(UUID);
                                    if (result2 == 1)
                                    {
                                        // Kai Ming's function
                                        SecurityEventLog eventObj = new SecurityEventLog();
                                        int result3 = eventObj.EventInsert("Failed Login", DateTime.Now, UUID);
                                    }
                                    else
                                    {
                                        lblError.Text = "An error has occured. Please try again.";
                                    }

                                }
                                else
                                {
                                    failCount += 1;

                                    int result2 = LoginDAO.updateAccountFailedAttemptTableByUUID(UUID, failCount);
                                    if (result2 == 1)
                                    {
                                        // Kai Ming's function
                                        SecurityEventLog eventObj = new SecurityEventLog();
                                        int result3 = eventObj.EventInsert("Failed Login", DateTime.Now, UUID);
                                    }
                                    else
                                    {
                                        lblError.Text = "An error has occured. Please try again.";
                                    }
                                }

                            }
                            else // insert failed attempt
                            {
                                int result2 = LoginDAO.insertAccountFailedAttemptTable(UUID);
                                if (result2 == 1)
                                {
                                    // Kai Ming's function
                                    SecurityEventLog eventObj = new SecurityEventLog();
                                    int result3 = eventObj.EventInsert("Failed Login", DateTime.Now, UUID);
                                }
                                else
                                {
                                    lblError.Text = "An error has occured. Please try again.";
                                }
                            }


                            lblError.Text = "Please check your email or password!";
                            lblError.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else
                {
                    lblError.Text = "Please check your email or password!";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.ToString();
                //throw new Exception(ex.ToString());
                lblError.Text = "An error has occured. Please try again.";
            }
            finally { }
        }
        // send 2FA stuffs
        private void Panel2()
        {
            DAL.Login obj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);

            if (obj != null)
            {
                // Google Auth and 2FA feature here
                if (rb2FATypes.SelectedItem.Text == "Google Authenticator")
                {
                    // lblError.Text = "You clicked Google Authenticator";
                }
                else if (rb2FATypes.SelectedItem.Text == "OTP")
                {
                    // send the otp()
                    var result = DAL.Peishan_Function.EmailAndPhoneValidation.SendOTP(tbEmail.Text.Trim(), obj.mobile.ToString());
                    if (result.Item1 == true)
                    {
                        lblError.Text = "OTP: " + result.Item2.ToString();
                    }
                    else
                    {
                        lblError.Text = "An error has occured. Please try again.";
                    }
                    //lblError.Text = "You clicked One Time Password";
                }
            }
            else // It meant that user has not entered an email and somehow it managed to bypass to this portion.
            {
                lblError.Text = "Sorry, an error has occurred. Please try again later.";
            }
        }
        // verify 2FA stuffs; then login;
        private void Panel3()
        {
            DAL.Login obj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);

            if (obj != null)
            {
                // Google Auth and 2FA feature here
                if (rb2FATypes.SelectedItem.Text == "Google Authenticator")
                {
                    // verify google auth
                    VerifyGoogleAuth();
                }
                else if (rb2FATypes.SelectedItem.Text == "OTP")
                {
                    // verify OTP password
                    //VerifyOTP(tb2FAPin.Text.Trim());
                    bool result = DAL.Peishan_Function.EmailAndPhoneValidation.phoneVerification(tb2FAPin.Text.Trim(), tbEmail.Text);
                    if (result == true)
                    {
                        LogIn();
                    }
                    else
                    {
                        lblError.Text = "Password entered is either incorrect or expired.";
                    }
                }
            }
            else // It meant that user has not entered an email and somehow it managed to bypass to this portion.
            {
                lblError.Text = "Sorry, an error has occurred. Please try again later.";
            }
        }
        // =================================================================================================================
        /// <summary>
        /// Check if user enabled google auth or otp
        /// </summary>
        protected void moveToPanel2()
        {
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            if (loginObj != null)
            {

                if (loginObj.googleAuthEnabled == "Yes" || loginObj.otpEnabled == "Yes") // move to Panel 2
                {
                    PanelPart1.Visible = false;
                    PanelPart2.Visible = true;
                    lblTitle.Text = "Send verification code";

                    rb2FATypes.Items.Clear();
                    if (loginObj.googleAuthEnabled == "Yes")
                    {
                        rb2FATypes.Items.Add(new ListItem("Google Authenticator", "0"));
                    }
                    if (loginObj.otpEnabled == "Yes")
                    {
                        rb2FATypes.Items.Add(new ListItem("OTP", "1"));
                    }
                }
                else // if no 2FA
                {
                    LogIn();
                }
            }
        }
        protected void LogIn() // creating Session & deleteFailedAttempt
        {
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            //Set Session to only last 20 min
            Session.Timeout = 20;

            //create a new GUID and save into the session
            string guid = Guid.NewGuid().ToString();
            Session["AuthToken"] = guid;
            
            //now create a new cookie with this guid value
            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

            Session["UUID"] = loginObj.UUID;
            // **** Find ways to remove the session below
            Session["accountID"] = loginObj.UUID;
            Session["accountType"] = loginObj.accountType;
            Session["name"] = loginObj.name;
            Session["email"] = tbEmail.Text;
            Session["mobile"] = loginObj.mobile;
            Session["dateOfBirth"] = loginObj.dateOfBirth;

            DAL.Login AccountFailedAttemptObj = LoginDAO.getAccountFailedAttemptByUUID(loginObj.UUID);

            if (AccountFailedAttemptObj != null) // Delete failed attempt(s)
            {

                int result = LoginDAO.deleteAccountFailedAttemptTableByUUID(loginObj.UUID);
                if (result == 1)
                {
                    // ******
                    
                }
                else
                {
                    lblError.Text = "An error has occured. Please try again.";
                }

            }

            // Kai Ming's function
            SecurityEventLog eventObj = new SecurityEventLog();
            int result3 = eventObj.EventInsert("Successful Login", DateTime.Now, loginObj.UUID);
            //newDeviceLogin();

            if (Session["accountType"].ToString() == "student")
            {
                // student table
                DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(loginObj.UUID);
                Session["adminNo"] = studentObj.adminNo;
                Session["school"] = studentObj.studentSchool;
                Session["course"] = studentObj.course;
                Session["allergies"] = studentObj.allergies;
                Session["dietaryNeeds"] = studentObj.dietaryNeeds;
                //Session["parentID"] = studentObj.parentID;

            }
            else if (Session["accountType"].ToString() == "lecturer")
            {
                // lecturer table
                DAL.Login lecturerObj = LoginDAO.getLecturerTableByAccountID(loginObj.UUID);
                Session["staffID"] = lecturerObj.staffID;
                Session["school"] = lecturerObj.lecturerSchool;
                Session["staffRole"] = lecturerObj.staffRole;
            }
            /*else if (Session["accountType"].ToString() == "parent")
            {
                // parent table
                DAL.Login parentObj = LoginDAO.getParentTableByAccountID(loginObj.accountID);
                Session["parentID"] = parentObj.parentID;
                Session["adminNo"] = parentObj.adminNo;
            }*/

            // Password Expiration: cannot be more than a year
            checkPasswordExpiration();

            Response.Redirect("Default.aspx");
        }
        /// <summary>
        /// Feature: Check Ban Account
        /// </summary>
        protected Boolean CheckBanAccount()
        {
            Boolean verdict = true;
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            if (loginObj != null)
            {
                if (loginObj.accountStatus == "Not ban") // account is not ban 
                {
                    verdict = false;
                }
                else // Any other stuffs mention in the db meant account is ban
                {

                    // Compare the banAccountDateTime
                    var currentDateTime = DateTime.Now;
                    var banAccountDateTime = loginObj.banAccountDateTime;
                    var diff = currentDateTime.Subtract(banAccountDateTime);

                    var total = (diff.Days * 24 * 60 * 60) + (diff.Hours * 60 * 60) + (diff.Minutes * 60) + (diff.Seconds);
                    if (total < 18000) // if the hour difference is less than 5, account is ban
                    {
                        verdict = true;
                    }
                    else // need to update the account cos the ban is over.
                    {
                        string UUID = loginObj.UUID;
                        int result = LoginDAO.updateAccountStatusToNotBanByUUID(UUID);
                        if (result == 1)
                        {
                            verdict = false;
                        }
                        else
                        {
                            lblError.Text = "An error has occured. Please try again.";
                            lblError.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            else
            {
                lblError.Text = "An error has occured. Please try again.";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            return verdict;
        }
        /// <summary>
        /// Check CAPTCHA value
        /// </summary>
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

        // Part 2 --> Part 3
        // If user selects OTP: repetitive!! Copied from register page. Need to improve on this code.
        public Tuple<string, string> hashingAndSaltingPassword(string pwd)
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
       
        // If user selects GoogleAuth, needs to get secretKey from account Table.
        private void VerifyGoogleAuth()
        {
            DAL.Login obj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            if (obj != null)
            {
                if (obj.secretKey != null)
                {

                    string user_enter = tb2FAPin.Text; // store this password in db when it loads
                    byte[] secretKeyByte = Convert.FromBase64String(obj.secretKey);
                    string secretKey = decryptData(secretKeyByte);

                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                    bool isCorrectPIN = tfa.ValidateTwoFactorPIN(secretKey, user_enter);

                    if (isCorrectPIN == true)
                    {
                        // lblError.Text = "Yes";
                        LogIn();
                    }
                    else // **************** improve this code
                    {

                        lblError.Text = "Incorrect 2FA password. Please try again";
                        lblError.ForeColor = System.Drawing.Color.Red;
                        // To be improved. Repetitive error counter && changePasswordDate && NewDeviceLogin
                        string UUID = obj.UUID;
                        DAL.Login AccountFailedAttemptObj = LoginDAO.getAccountFailedAttemptByUUID(UUID);

                        if (AccountFailedAttemptObj != null) // update
                        {
                            int failCount = AccountFailedAttemptObj.AccountFailedAttemptCounter;
                            if (failCount == 10)
                            {
                                int result = LoginDAO.updateAccountStatusToBanByUUID(UUID);
                                if (result == 1) { }
                                else
                                {
                                    lblError.Text = "An error has occured. Please try again.";
                                }
                            }
                            else
                            {
                                failCount += 1;

                                int result = LoginDAO.updateAccountFailedAttemptTableByUUID(UUID, failCount);
                                if (result == 1) { }
                                else
                                {
                                    lblError.Text = "An error has occured. Please try again.";
                                }
                            }

                        }
                        else // insert
                        {
                            int result = LoginDAO.insertAccountFailedAttemptTable(UUID);
                            if (result == 1) { }
                            else
                            {
                                lblError.Text = "An error has occured. Please try again.";
                            }
                        }
                        
                    }
                }
                else // by right secretKey should already been stored cos the googleAuth has already been enabled.
                {
                    lblError.Text = "Sorry! An error has occurred!";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            else // by right secretKey should already been stored cos the googleAuth has already been enabled.
            {
                lblError.Text = "Sorry! An error has occurred!";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected string decryptData(byte[] cipherText)
        {
            string plainText = null;

            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                DAL.Login obj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
                if (obj != null)
                {
                    cipher.IV = Convert.FromBase64String(obj.IV);
                    cipher.Key = Convert.FromBase64String(obj.Key);

                    // Create a decryptor to perform the stream transform
                    ICryptoTransform decryptTransform = cipher.CreateDecryptor();

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptTransform, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream and place them in a string
                                plainText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    lblError.Text = "Sorry! An error has occurred!";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
            finally
            { }

            return plainText;
        }
        private void VerifyOTP(string userpassword)
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
                                    LogIn();
                                    //Response.Redirect("/login.aspx"); // Successful!
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
                        otpPassword = DAL.Peishan_Function.EmailAndPhoneValidation.otp().Trim();

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
        // *********************************Check Password Expiration function
        public void checkPasswordExpiration()
        {
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);

            DateTime changePasswordDate = Convert.ToDateTime(loginObj.changePasswordDate.ToString());

            if ((DateTime.Now - changePasswordDate).TotalDays > 365)// if bigger than one year
            {
                Response.Redirect("changePassword.aspx");
            }
        }

        // **********************************New Device Login
        private void newDeviceLogin()
        {
            // select statement to retrieve the lists of macAddresses stored into the newDeviceLogin table
            // Check if the list of macAddresses matches with the current macAddress
            // ---> If it does, redirect them to new device login alert
            // ---> Else, insert them into newDeviceLogin && sendEmail to giive the user an option on whether to remove the device
            // ---> (EXTRA): Take note of this macAddress with this UUID, if it tries to login again, prevent them from doing so.
            Boolean result = DAL.Peishan_Function.NewDeviceLogin.checkDeviceLogin(tbEmail.Text.Trim()).Item1;
            if (result == true) // insert and update success
            {

            }
            else
            {
                lblError.Text = DAL.Peishan_Function.NewDeviceLogin.checkDeviceLogin(tbEmail.Text.Trim()).Item2;
            }
        }
    }
}