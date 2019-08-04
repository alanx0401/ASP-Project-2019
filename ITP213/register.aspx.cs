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
            Boolean validateAdminNumberResult = DAL.Functions.Validations.RegisterValidation.checkIfAdminNumberExist(adminNo);
            Boolean validateEmailResult = DAL.Functions.Validations.RegisterValidation.checkIfEmailExist(email);

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
                    var hi = DAL.Functions.Validations.EmailAndPhoneValidation.SendOTP(tbEmail.Text.Trim(), tbContactNumber.Text.Trim());
                    if (hi.Item1 == true) // successfully send OTP
                    {
                        lblError.Text = "OTP Password: " + hi.Item2.ToString();
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
            
            bool result = DAL.Functions.Validations.EmailAndPhoneValidation.phoneVerification(tbVerifyPassword.Text.Trim(), tbEmail.Text.Trim());
            if (result == true)
            {
                newDeviceLogin();


                // Sending email verification
                string sendEmail = ConfigurationManager.AppSettings["SendEmail"];
                if (sendEmail.ToLower() == "true")
                {
                    sendingEmailVerification();
                }

                string email = tbEmail.Text.Trim();
                email = DAL.Functions.Validations.EmailAndPhoneValidation.EncodeToken(email);
                Response.Redirect("/Login.aspx?x="+email);
            }
            else
            {
                lblError.Text = "Password entered is either incorrect or expired.";
                lblError.ForeColor = System.Drawing.Color.Red;
            }

        }
        // ===========================> Validations
        // --> Panel 1
        protected void CVAdminNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            string adminNo = tbAdminNo.Text.Trim();
            Boolean result = DAL.Functions.Validations.RegisterValidation.checkIfAdminNumberExist(adminNo);
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
            Boolean result = DAL.Functions.Validations.RegisterValidation.checkIfEmailExist(email);

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

            string filename = "DAL\\Functions\\Validations\\Data\\dictionary.txt";
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
                    btnResendPhoneVerification.Enabled = false;
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
        // Part 2: Send OTP
        // Part 3: Phone Verification
        public void sendingEmailVerification()
        {

            var result = DAL.Functions.Validations.EmailAndPhoneValidation.sendingEmailVerification(tbEmail.Text);
            if (result.Item1 == true)
            {

            }
            else
            {
                lblError.Text = result.Item2.ToString();
            }
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

        /*private void insertIntoNewDeviceLoginTable()
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
        }*/
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
        public static Tuple<Boolean, string> checkDeviceLogin(string email)
        {
            string lblError = string.Empty;
            Boolean verdict = false;

            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(email);
            string UUID = loginObj.UUID; // retriving UUID from loginObj

            string country = DAL.Peishan_Function.NewDeviceLogin.GetCountrybyip();
            string macAddress = DAL.Peishan_Function.NewDeviceLogin.GetMACAddress();
            string publicIP = getExternalIp();

            DAL.Login obj = DAL.Peishan_Function.NewDeviceLogin.getMacAddressFromNewDeviceLogin(UUID, macAddress);
            if (obj != null) // macAddress exist in this user
            {
                // update
                int result = DAL.Peishan_Function.NewDeviceLogin.updateIntoNewDeviceLoginTable(UUID, DAL.Peishan_Function.NewDeviceLogin.GetMACAddress(), publicIP);
                if (result == 1)
                {
                    verdict = true;
                }
                else
                {
                    lblError = "Sorry! An error has occurred! Please try again later!";
                }
            }
            else
            {
                // insert
                int result = DAL.RegisterDAO.insertIntoNewDeviceLogin(DAL.Peishan_Function.NewDeviceLogin.GetMACAddress(), country, getExternalIp(), DateTime.Now, UUID);

                if (result == 1)
                {
                    // ***** send an Email to alert user

                    string title = "Security Alert - New Device Login";

                    var htmlContent = "Hi, " + loginObj.name + ". An unknown device login is found. Country: " + country + ", IP address: " + getExternalIp() + ".";

                    verdict = true;
                    DAL.Functions.Validations.EmailAndPhoneValidation.Execute(loginObj.name, email, "Hi", title, htmlContent);
                    //=========================================
                    verdict = true;
                }
                else
                {
                    lblError = "Sorry! An error has occurred! Please try again later!";
                }
            }

            return Tuple.Create(verdict, lblError);

        }

        protected void btnResendPhoneVerification_Click(object sender, EventArgs e)
        {
            DAL.Login obj = DAL.LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            // check password
            var resendPhone = DAL.Functions.Validations.EmailAndPhoneValidation.resendPhoneVerification(obj.email, obj.mobile);
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
            int seconds = int.Parse(Label2.Text);
            if (seconds > 0)
            {
                Label2.Text = (seconds - 1).ToString();
                Label2.Visible = true;
            }
            else
            {
                Label2.Visible = false;
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
                var total = (diff.Days * 24 * 60 * 60) + (diff.Hours * 60 * 60) + (diff.Minutes * 60) + diff.Seconds;

                if (total < 25)
                {
                    var time = 25 - total;
                    btnResendPhoneVerification.Enabled = false;
                    Label2.Text = time.ToString();
                    Label2.Visible = true;
                }
                else
                {
                    Label2.Text = "0";
                    Label2.Visible = false;
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