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
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace ITP213
{
    public partial class register : System.Web.UI.Page
    {
        // for encryption & decryption
        byte[] Key;
        byte[] IV;

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

            CVDateOfBirth1.ValueToCompare = DateTime.Now.ToShortDateString();
        }
        protected void btnNext_Click(object sender, EventArgs e) // Panel 1: Inserting data in account table
        {
            string adminNo = tbAdminNo.Text.Trim();
            string email = tbEmail.Text.Trim();
            string password = tbPassword.Text.Trim();
            string realName = tbName.Text.Trim();
            Boolean validateAdminNumberResult = DAL.Validation.RegisterValidation.checkIfAdminNumberExist(adminNo);
            Boolean validateEmailResult = DAL.Validation.RegisterValidation.checkIfEmailExist(email);

            bool adminNoOk = !AllPartsOfLength(adminNo, 3)
                .Any(part => password.Contains(part));
            bool realNameOk = !AllPartsOfLength(realName, 3)
                .Any(part => password.Contains(part));

            Boolean validatePasswordInDictionary = checkPasswordFoundInDictionary(password);

            if (validateAdminNumberResult != true && validateEmailResult != true && adminNoOk == true && realNameOk == true && validatePasswordInDictionary != true) // these validations needs be true such that user can move to the next panel.
            {
                // Disable Panel 1's Content
                PanelPart1.Visible = false;
                btnNext.Visible = false;
                btnRegister.Visible = false;
                lblLogin.Visible = false;

                // Enable Panel 2's Content
                PanelPart2.Visible = true;
                Label1.Text = "More information details";
                btnNext1.Visible = true;

            
                registeringAccount(tbPassword.Text); // Inserting data in account table
            }

        }
        protected void btnNext1_Click(object sender, EventArgs e) // Panel 2: Update contact number, date of birth and adminNo
        {
            // Disable Panel 1's Content: Optional
            PanelPart1.Visible = false;
            btnNext.Visible = false;
            btnRegister.Visible = false;
            lblLogin.Visible = false;


            // Disable Panel 2's Content
            PanelPart2.Visible = false;
            btnNext1.Visible = false;

            // Enable Panel 3's Content
            PanelPart3.Visible = true;
            btnBack1.Visible = true;
            btnRegister.Visible = true;
            Label1.Text = "Verify your phone number";
            
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
                lblError.Text = "Sorry! An error has occurred!";
            }
        }

        protected void btnBack1_Click(object sender, EventArgs e) // To enable Panel 2's Content
        {
            // Disable Panel 1's Content: Optional
            PanelPart1.Visible = false;
            btnNext.Visible = false;
            btnRegister.Visible = false;
            lblLogin.Visible = false;


            // Disable Panel 2's Content
            PanelPart2.Visible = true;
            btnNext1.Visible = true;
            Label1.Text = "More information details";

            // Disable Panel 3's Content
            PanelPart3.Visible = false;
            btnBack1.Visible = false;
            btnRegister.Visible = false;
        }
        protected void btnRegister_Click(object sender, EventArgs e) // Verify Phone & Sending Email Verification
        {
            // Assuming that email verification has been sent & user verified their phone code, 
            // we need to insert the current device details now
            insertIntoNewDeviceLoginTable();

            // Sending email verification
            string sendEmail = ConfigurationManager.AppSettings["SendEmail"];
            if (sendEmail.ToLower() == "true")
            {
                sendingEmailVerification();
            }

            // Verify phone code
            phoneVerification(tbVerifyPassword.Text);

        }
        // ===========================> Validations
        // --> Panel 1
        protected void CVAdminNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            string adminNo = tbAdminNo.Text.Trim();
            Boolean result = DAL.Validation.RegisterValidation.checkIfAdminNumberExist(adminNo);
            if (!Page.IsValid)
            {

                if (result != true) // admin number does not exists in db
                {
                    args.IsValid = true;
                }
            }
            else
            {

                if (result != true) // admin number does not exists in db
                {
                    args.IsValid = true;
                }
            }
        }
        protected void CVEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            string email = tbEmail.Text.Trim();
            Boolean result = DAL.Validation.RegisterValidation.checkIfEmailExist(email);

            if (!Page.IsValid)
            {
                if (result != true) // email does not exists in db
                {
                    args.IsValid = true;
                }
            }
            else
            {
                if (result != true) // email does not exists in db
                {
                    args.IsValid = true;
                }
            }
        }
        protected void CVPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;

            // Password should not contain user's name or admin number
            string password = tbPassword.Text.Trim().ToLower();
            string adminNo = tbAdminNo.Text.Trim().ToLower();

            string realName = tbName.Text.Trim().ToLower();

            bool adminNoOk = !AllPartsOfLength(adminNo, 3)
                .Any(part => password.Contains(part));
            bool realNameOk = !AllPartsOfLength(realName, 3)
                .Any(part => password.Contains(part));
            //lblError.Text = "username: " + adminNoOk + ", realName" + realNameOk; // for checking purposes

            if (!Page.IsValid)
            {
                if (adminNoOk == true && realNameOk == true) // password does not contain adminNo(3 letters) & realName
                {
                    args.IsValid = true;
                }
            }
            else
            {
                if (adminNoOk == true && realNameOk == true) // password does not contain adminNo(3 letters) & realName
                {
                    args.IsValid = true;
                }
            }
        }
        public IEnumerable<string> AllPartsOfLength(string value, int length)
        {
            for (int startPos = 0; startPos <= value.Length - length; startPos++)
            {
                yield return value.Substring(startPos, length);
            }
            yield break;
        }
        protected void CVPassword1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;

            string password = tbPassword.Text.Trim();
            Boolean result = checkPasswordFoundInDictionary(password);
            if (!Page.IsValid)
            {
                if (result != true) // password does not exists in db
                {
                    args.IsValid = true;
                }
            }
            else
            {
                if (result != true) // password does not exists in db
                {
                    args.IsValid = true;
                }
            }
        }
        private bool checkPasswordFoundInDictionary(string password)
        {

            Boolean result = false;

            string filename = "DAL\\Validation\\Data\\dictionary.txt";
            string FILEPATH = HttpRuntime.AppDomainAppPath + filename;
            // temp
            try
            {   // Open the text file using a stream reader.

                foreach (string line in System.IO.File.ReadAllLines(FILEPATH))
                {
                    if (line.Contains(password)) // password is found in the dictionary
                        result = true;
                }
            }
            catch (IOException ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }
        //=============================================================================================================================================================
        // Panel 1's stuff:
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
                int result2 = RegisterDAO.insertaAdminNoInAdminTable(tbAdminNo.Text, UUID); // student table: assuming the registered users are all users
                if (result == 1 && result2 == 1)
                {
                    // success
                }
            }
            catch (Exception)
            {

            }
        }

        // Panel 3's stuff:
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
                string encodeRandomToken = EncodeToken(randomToken); 

                // insert
                int result = RegisterDAO.insertIntoVerifyEmail(UUID, randomToken);
                if (result == 1)
                {
                    Execute(tbName.Text, tbEmail.Text, encodeRandomToken);
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
                        string encodeRandomToken = EncodeToken(randomToken); // not used yet

                        // insert
                        int result2 = RegisterDAO.insertIntoVerifyEmail(UUID, randomToken);
                        if (result2 == 1)
                        {
                            
                            Execute(tbName.Text, tbEmail.Text, encodeRandomToken);
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

        
        public static string EncodeToken(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }
        // Part 3: NewDeviceLogin
        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty) // only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }

            }
            return sMacAddress;
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

        private void insertIntoNewDeviceLoginTable()
        {
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            string UUID = loginObj.UUID; // retriving UUID from loginObj

            string country = GetCountrybyip();

            int result = DAL.RegisterDAO.insertIntoNewDeviceLogin(GetMACAddress(), country, getExternalIp(), DateTime.Now, UUID);

            if (result == 1)
            {
            }
            else
            {
                lblError.Text = "Sorry! An error has occurred! Please try again later!";
            }
        }
    }
}