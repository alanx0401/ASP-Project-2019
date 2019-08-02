using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class changePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UUID"] != null)
            {

            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }


        //=============================================================================================================================================================
        // Feature:
        private void changePasswordFunction(string pwd)
        {
            if (Page.IsValid)
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

                    int result = DAL.ChangePassword.updatePasswordIntoAccountTable(Session["UUID"].ToString(), finalHash, salt); // account table
                    if (result == 1)
                    {
                        // success
                        EventLog eventObj = new EventLog();
                        eventObj.EventInsert("Change Password", DateTime.Now, Session["UUID"].ToString());
                        Response.Redirect("/Default.aspx");
                    }
                    else
                    {
                        lblError.Text = "Whoops, something went wrong. Please try again.";
                    }
                }
                catch (Exception)
                {

                }
            }
                
        }
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

        protected void CVPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;

            Settings SettingsObj = SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            DAL.Login LoginObj = LoginDAO.getStudentTableByAccountID(Session["UUID"].ToString());
            // Password should not contain user's name or admin number
            string password = tbPassword.Text.Trim().ToLower();
            string adminNo = LoginObj.adminNo.ToString();

            string realName = SettingsObj.name.ToString();

            bool adminNoOk = !AllPartsOfLength(adminNo, 3)
                .Any(part => password.Contains(part));
            bool realNameOk = !AllPartsOfLength(realName, 3)
                .Any(part => password.Contains(part));
            //lblError.Text = "username: " + adminNoOk + ", realName" + realNameOk; // for checking purposes
            Boolean currentPassword = checkCurrentPassword(password);

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

        protected void btnChangePassword_Click1(object sender, EventArgs e)
        {
            Settings SettingsObj = SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            DAL.Login LoginObj = LoginDAO.getStudentTableByAccountID(Session["UUID"].ToString());

            string password = tbPassword.Text.Trim();
            bool adminNoOk = !AllPartsOfLength(LoginObj.aNum, 3)
                .Any(part => password.Contains(part));
            bool realNameOk = !AllPartsOfLength(SettingsObj.name, 3)
                .Any(part => password.Contains(part));

            if (adminNoOk == true && realNameOk == true)
            {
                changePasswordFunction(tbPassword.Text.Trim());
            }
        }

        protected void CVCurrentPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;

            string password = tbPassword.Text.Trim();
            Boolean result = checkCurrentPassword(password);
            if (!Page.IsValid)
            {
                if (result == true) 
                {
                    args.IsValid = true;
                }
            }
            else
            {
                if (result == true) 
                {
                    args.IsValid = true;
                }
            }
        }
        private bool checkCurrentPassword(string password)
        {

            Boolean result = false;

            string pwd = tbCurrentPassword.Text.ToString().Trim();
            SHA512Managed hashing = new SHA512Managed();
            Settings obj = SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());    
            string dbHash = obj.password;
            string dbSalt = obj.passwordSalt;

            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);
                    if (userHash == dbHash) // password matches
                    {
                        result = true;
                    }
                }
            }
            catch
            {

            }


            return result;
        }
    }
}