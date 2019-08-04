using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


namespace ITP213
{
    public partial class changePassword1 : System.Web.UI.Page
    {
        string uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["uuid"] != null)
            {
                uid = Request.QueryString["uuid"].ToString();
            }
            if (Session["UUID"] != null)
            {
                uid = Session["UUID"].ToString();
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            changePasswordFunction1(tbPassword1.Text.Trim());
        }

        //=============================================================================================================================================================
        // Feature:
        private void changePasswordFunction1(string pwd)
        {
            string finalHash;
            string salt;

            try
            {
                // hashing + salting password
                string password = string.Empty;
                password = pwd.Trim();
                var getHashingAndSaltingPwd = hashingAndSaltingPassword1(password);

                finalHash = string.Empty;
                finalHash = getHashingAndSaltingPwd.Item1;

                salt = string.Empty;
                salt = getHashingAndSaltingPwd.Item2;

                int result = DAL.ChangePassword.updatePasswordIntoAccountTable(uid, finalHash, salt); // account table
                if (result == 1)
                {
                    // success
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
        public Tuple<string, string> hashingAndSaltingPassword1(string pwd)
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
        protected void CVPassword1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;

            string password = tbPassword1.Text.Trim();
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

            string filename = "DAL\\Functions\\Validations\\Data\\dictionary.txt"; ;
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
    }
}