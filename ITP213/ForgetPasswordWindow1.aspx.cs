using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Net;
using System.Net.Mail;

using System.Security.Cryptography;
using System.Text;
//test
namespace ITP213
{
    public partial class ForgetPasswordWindow1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

            /* RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;*/

            return Tuple.Create(finalHash, salt);
        }

        /*public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }*/

        protected void btnResetpw_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select email,UUID from account where email=@email";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@email", tbEmail.Text);
            sqlconn.Open();
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                string username = sdr["email"].ToString();
                string uid = sdr["UUID"].ToString();
                //string password = sdr["password"].ToString();



                try
                {
                    MailMessage mm = new MailMessage("wycliff1999@gmail.com", tbEmail.Text);
                    mm.Subject = "Your new password";
                    mm.Body = string.Format("Hello : <h1>{0}</h1>, your password is link is http://localhost:16452/changePassword1.aspx?uuid={1}", username,uid);
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    NetworkCredential nc = new NetworkCredential();
                    //NetworkCredential loginInfo = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["wycliff1999@gmail.com"]), Convert.ToString(ConfigurationManager.AppSettings["wywy12345"]));
                    nc.UserName = "wycliff1999@gmail.com";
                    nc.Password = "wywy12345";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Send(mm);
                    LabelMsg.Text = "Your password has been sent to " + tbEmail.Text;
                    LabelMsg.ForeColor = Color.Green;
                }
                catch (Exception ex) { throw new Exception(ex.ToString()); }
            }
            else
            {
                LabelMsg.Text = "Your password has been sent to " + tbEmail.Text; //Message for deception
                LabelMsg.ForeColor = Color.Green;
            }
            
            
            
        }
    }
}