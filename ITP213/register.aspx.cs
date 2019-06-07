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
    public partial class register : System.Web.UI.Page
    {
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            /*string password;
            if (IsPostBack)
            {
            
            if (!(String.IsNullOrEmpty(tbPassword.Text.Trim())))
                {
                    tbPassword.Attributes["value"] = tbPassword.Text;
                    password = Request.Form["tbPassword"].ToString();
                }
            }*/
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            PanelPart1.Visible = false;
            btnNext.Visible = false;
            PanelPart2.Visible = true;
            btnBack.Visible = true;
            btnRegister.Visible = false;
            Label1.Text = "More information details";
            lblLogin.Visible = false;
            btnNext1.Visible = true;
        }
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            PanelPart1.Visible = false;
            btnNext.Visible = false;
            btnNext1.Visible = false;
            PanelPart2.Visible = false;
            btnBack.Visible = true;
            btnRegister.Visible = true;
            Label1.Text = "Verify your phone number";
            lblLogin.Visible = false;
            PanelPart3.Visible = true;
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
            string password = "";
            password = tbPassword.Text;

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

            LoginDAO.insert(tbName.Text, tbEmail.Text, tbContactNumber.Text, tbDateOfBirth.Text, finalHash, salt);
            Response.Redirect("/login.aspx");
        }

    }
}