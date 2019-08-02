using ITP213.DAL;
using System;
using System.Collections.Generic;
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

        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            changePasswordFunction(tbPassword.Text.Trim());
        }

        //=============================================================================================================================================================
        // Feature:
        private void changePasswordFunction(string pwd)
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
    }
}