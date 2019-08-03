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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
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
                /*if (Request.Browser.Cookies) // check if browser supports cookies
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
                }*/
                // check if user just registered their account and redirect to login page
                string email = Request.QueryString["x"];
                if (email != null)
                {
                    email = Request.QueryString["x"].ToString();
                    string verifiedEmail = DAL.Peishan_Function.EmailAndPhoneValidation.DecodeToken(email);
                    if (verifiedEmail != email)
                    {
                        lblError.Text = "We've sent a verification to your email address: " + verifiedEmail + "<br>If this email is incorrect, please <a href=\"/changeEmail.aspx\">click here.</a>";
                        lblError.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }

            // added in Year 3 Sem 1
            // disable autocomplete form        
            tbEmail.Attributes.Add("autocomplete", "off");
            tbPassword.Attributes.Add("autocomplete", "off");
            tb2FAPin.Attributes.Add("autocomplete", "off");

            tripStatus(); // not sure if it's working
            
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
            /*DAL.Login obj = LoginDAO.getLoginByEmailAndPassword("lecturer_dummy@nyp.edu.sg");

            Session["UUID"] = obj.UUID;
            // **** Find ways to remove the session below
            Session["accountID"] = obj.UUID;
            Session["accountType"] = obj.accountType;
            Session["name"] = obj.name;
            Session["email"] = "lecturer_dummy@nyp.edu.sg";
            Session["mobile"] = obj.mobile;
            Session["dateOfBirth"] = obj.dateOfBirth;*/

            /*if (Session["accountType"].ToString() == "student")
            {
                // student table
                DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(obj.UUID);
                Session["adminNo"] = studentObj.adminNo;
                Session["school"] = studentObj.studentSchool;
                Session["course"] = studentObj.course;
                Session["allergies"] = studentObj.allergies;
                Session["dietaryNeeds"] = studentObj.dietaryNeeds;

            }*/
            /*if (Session["accountType"].ToString() == "lecturer")
            {
                // lecturer table
                DAL.Login lecturerObj = LoginDAO.getLecturerTableByAccountID(obj.UUID);
                Session["staffID"] = lecturerObj.staffID;
                Session["school"] = lecturerObj.lecturerSchool;
                Session["staffRole"] = lecturerObj.staffRole;
            }
            Response.Redirect("Default.aspx");*/
            // ------ temp
        }
        protected void btnPanel1_Click(object sender, EventArgs e) // Submit login form and checks if pwd and email matches. 
        {
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
            Panel3();
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
                        btnResendPhoneVerification.Enabled = false;
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

            DAL.Settings settingsObj = SettingsDAO.getAccountTableByUUID(loginObj.UUID);

            if (settingsObj.emailVerified == "Yes" && settingsObj.phoneVerified == "Yes")
            {
                string countryUserShouldBeIn = checkCountry();
                string countryUserIsCurrentlyIn = GetCountrybyip();
                if (countryUserShouldBeIn == countryUserIsCurrentlyIn)
                {
                    //=======================================
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
                    newDeviceLogin();

                    if (Session["accountType"].ToString() == "student")
                    {
                        // student table
                        DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(loginObj.UUID);
                        Session["adminNo"] = studentObj.aNum;
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
                else
                {
                    lblError.Text = "Sorry, you are not allow to log in.";
                }
            }
            else
            {
                lblError.Text = "Please ensure your phone number and email are verified.";
            }  
            
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

                string password = tb2FAPin.Text.Trim();
                string email = loginObj.email;
                Boolean verdict = DAL.Peishan_Function.EmailAndPhoneValidation.phoneVerification(password, email);
                if (verdict == true)
                {
                    LogIn();
                }
                else
                {
                    lblError.Text = "Sorry, password is either invalid or expired. ";
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
            Boolean result = DAL.Peishan_Function.NewDeviceLogin.checkDeviceLogin(tbEmail.Text.Trim()).Item1;
            if (result == true) // insert and update success
            {
                
            }
            else
            {
                lblError.Text = DAL.Peishan_Function.NewDeviceLogin.checkDeviceLogin(tbEmail.Text.Trim()).Item2;
            }
        }

        protected void btnResendPhoneVerification_Click(object sender, EventArgs e)
        {
            DAL.Login obj = DAL.LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            // check password
            var resendPhone = DAL.Peishan_Function.EmailAndPhoneValidation.resendPhoneVerification(obj.email, obj.mobile);
            if (resendPhone.Item1 == true)
            {
                lblError.Text = resendPhone.Item2.ToString();
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
        private void checkVerificationTime()
        {
            DAL.Login obj = DAL.LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            DAL.Register verificationObj = DAL.RegisterDAO.checkVerifyPhoneOTP(obj.UUID);

            if (verificationObj != null)
            {
                btnResendPhoneVerification.Visible = true;
                // check if the verification email has expired
                var currentDateTime = DateTime.Now;
                var phoneDateTimeSend = verificationObj.dateTimeSend;
                var diff = currentDateTime.Subtract(phoneDateTimeSend);
                var total = (diff.Hours * 60 * 60) + (diff.Minutes * 60) + diff.Seconds;

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
        private void tripStatus()
        {
            try
            {
                string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

                StringBuilder sqlStr = new StringBuilder();
                // overseasTripStatus = 'Ongoing'
                sqlStr.AppendLine("UPDATE overseasTrip");
                sqlStr.AppendLine("SET overseasTripStatus='ONGOING'");
                sqlStr.AppendLine("WHERE departureDate<= GETDATE() AND arrivalDate>=GETDATE();");
                // overseasTripStatus = 'Ended'
                sqlStr.AppendLine("UPDATE overseasTrip");
                sqlStr.AppendLine("SET overseasTripStatus='ENDED'");
                sqlStr.AppendLine("WHERE departureDate<= GETDATE() AND arrivalDate<=GETDATE();");

                SqlConnection myConn = new SqlConnection(DBConnect);
                myConn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
                int result = cmd.ExecuteNonQuery();
                myConn.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "Sorry, something went wrong. Please try again.";
            }
            finally {}
        }
        public static DAL.TripAllocation displayTripCountryBasedOnAdminNo(string adminNo) // check if user is inside ongoing trip
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from account by email & password using query parameter
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("select * from overseasTrip");
            sqlStr.AppendLine("INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID");
            sqlStr.AppendLine("where");
            sqlStr.AppendLine("STR(overseasTrip.tripID)+'.'+ @sAdminNo");
            sqlStr.AppendLine("not in (select STR(tripID)+'.'+adminNo from withdrawTripRequest where withdrawalTripRequestStatus='Approved')");
            sqlStr.AppendLine("and adminNo=@sAdminNo");
            sqlStr.AppendLine("and overseasTripStatus='ONGOING'");

            DAL.TripAllocation obj = new DAL.TripAllocation(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@sAdminNo", adminNo);
            // fill dataset
            da.Fill(ds, "accountTable");
            int rec_cnt = ds.Tables["accountTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["accountTable"].Rows[0]; // Sql command only returns only 1 record
                obj.country = row["country"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;

        }
        public static DAL.TripAllocation displayTripCountryBasedOnStaffID(string staffID) // goal: wants to display tripName & tripID & beforeArrivalDate
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from account by email & password using query parameter
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Select * From overseasTrip");
            sqlStr.AppendLine("INNER JOIN overseasEnrolledLecturer ON overseasTrip.tripID = overseasEnrolledLecturer.tripID");
            sqlStr.AppendLine("WHERE staffID=@lStaffID and overseasTripStatus='ONGOING'");
            sqlStr.AppendLine("ORDER BY arrivalDate DESC;");

            DAL.TripAllocation obj = new DAL.TripAllocation(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("lStaffID", staffID);
            // fill dataset
            da.Fill(ds, "accountTable");
            int rec_cnt = ds.Tables["accountTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["accountTable"].Rows[0]; // Sql command only returns only 1 record
                obj.country= row["country"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }

        private string checkCountry()
        {
            string country = string.Empty;

            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);

            // check country
            if (loginObj.accountType == "student")
            {
                DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(loginObj.UUID);

                DAL.TripAllocation tripObj = displayTripCountryBasedOnAdminNo(studentObj.aNum);

                if (tripObj != null)
                {
                    // trip is ongoing
                    country = tripObj.country;

                    if (country == "Canada")
                    {
                        country = "CA";
                    }
                    else if (country == "Hong Kong")
                    {
                        country = "HK";
                    }
                    else if (country == "United States")
                    {
                        country = "US";
                    }
                }
                else
                {
                    // user must be in singapore
                    country = "SG";
                }
            }
            else if (loginObj.accountType == "lecturer")
            {
                DAL.Login lecturerObj = LoginDAO.getLecturerTableByAccountID(loginObj.UUID);

                DAL.TripAllocation tripObj = displayTripCountryBasedOnStaffID(lecturerObj.staffID);

                if (tripObj != null)
                {
                    // trip is ongoing
                    country = tripObj.country;

                    if (country == "Canada")
                    {
                        country = "CA";
                    }
                    else if (country == "Hong Kong")
                    {
                        country = "HK";
                    }
                    else if (country == "United States")
                    {
                        country = "US";
                    }
                }
                else
                {
                    // user must be in singapore
                    country = "SG";
                }
            }
            else if (loginObj.accountType == "admin")
            {
                country = "SG";
            }
            return country;
        }

        public string GetCountrybyip()
        {
            string ipaddress = getExternalIp();
            string strreturnvalue = string.Empty;
            string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + ipaddress);
            XmlDocument ipInfixml = new XmlDocument();
            ipInfixml.LoadXml(ipResponse);
            XmlNodeList responseXML = ipInfixml.GetElementsByTagName("query");
            string returnvalue = responseXML.Item(0).ChildNodes[2].InnerText.ToString();

            return returnvalue;

        }

        public static string getExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }
        public string IPRequestHelper(string url)
        {
            HttpWebRequest objrequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objresponse = (HttpWebResponse)objrequest.GetResponse();
            StreamReader responsereader = new StreamReader(objresponse.GetResponseStream());
            string responseread = responsereader.ReadToEnd();
            responsereader.Close();
            responsereader.Dispose();
            return responseread;
        }
    }
}