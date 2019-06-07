using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class login : System.Web.UI.Page
    {
        string hash = @"foxle@rn";
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (IsPostBack)
            {
                if (!(String.IsNullOrEmpty(tbPassword.Text.Trim())))
                {
                    tbPassword.Attributes["value"] = tbPassword.Text.Trim();
                    string strDate = Request.Form["tbPassword"].Trim().ToString();
                }
            }*/
                    
            tbEmail.Attributes.Add("autocomplete", "off");
            tbPassword.Attributes.Add("autocomplete", "off");
            tb2FAPin.Attributes.Add("autocomplete", "off");
            if (!IsPostBack) // First Load of the page
            {
                if (Request.Browser.Cookies)
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
                            //lblError.Text = "We have detected that the cookies are disabled on your browser. Please enable them.";
                            //lblError.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "alert('Browser don't support cookies. Please install one of the modern browser.');", true);
                    //lblError.Text = "Browser don't support cookies. Please install one of the modern browser.";
                    //lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            if (Request.Cookies["authcookie"] != null) {
                DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(Request.Cookies["authcookie"]["email"], Request.Cookies["authcookie"]["password"]);
                if (loginObj != null) {
                    // account Table
                    Session["accountID"] = loginObj.accountID;
                    Session["accountType"] = loginObj.accountType;
                    Session["name"] = loginObj.name;
                    Session["email"] = loginObj.email;
                    Session["mobile"] = loginObj.mobile;
                    Session["dateOfBirth"] = loginObj.dateOfBirth;
                    
                    //lblError.Text = "Yayy! Succeeded";
                    if (Session["accountType"].ToString() == "student")
                    {
                        // student table
                        DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(loginObj.accountID);
                        Session["adminNo"] = studentObj.adminNo;
                        Session["school"] = studentObj.studentSchool;
                        Session["course"] = studentObj.course;
                        Session["allergies"] = studentObj.allergies;
                        Session["dietaryNeeds"] = studentObj.dietaryNeeds;
                        Session["parentID"] = studentObj.parentID;

                    }
                    else if (Session["accountType"].ToString() == "parent")
                    {
                        // parent table
                        DAL.Login parentObj = LoginDAO.getParentTableByAccountID(loginObj.accountID);
                        Session["parentID"] = parentObj.parentID;
                        Session["adminNo"] = parentObj.adminNo;
                    }
                    else if (Session["accountType"].ToString() == "lecturer")
                    {
                        // lecturer table
                        DAL.Login lecturerObj = LoginDAO.getLecturerTableByAccountID(loginObj.accountID);
                        Session["staffID"] = lecturerObj.staffID;
                        Session["school"] = lecturerObj.lecturerSchool;
                        Session["staffRole"] = lecturerObj.staffRole;
                    }
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
        protected void btnCheckFor2FA_Click(object sender, EventArgs e)
        {
            PanelPart1.Visible = false;
            PanelPart2.Visible = true;
            lblTitle.Text = "Send verification code";
        }
        protected void btnSubmitChoice_Click(object sender, EventArgs e)
        {
            PanelPart2.Visible = false;
            PanelPart3.Visible = true;
            lblTitle.Text = "Enter verification code";
        }
        protected void btnBack1_Click(object sender, EventArgs e)
        {
            PanelPart1.Visible = true;
            PanelPart2.Visible = false;
            lblTitle.Text = "Login";
        }
        protected void btnBack2_Click(object sender, EventArgs e)
        {
            PanelPart3.Visible = false;
            PanelPart2.Visible = true;
            lblTitle.Text = "Send verification code";
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //string password = "";
            string pwd = tbPassword.Text.ToString().Trim();
            string email = tbEmail.Text.Trim().ToString();
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(email);
            string dbSalt = getDBSalt(email);

            /*try
            {*/
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    if (userHash == dbHash)
                    {
                        //Will edit in the future==============================
                        DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text, userHash);
                        if (loginObj != null)
                        {
                            // account Table
                            // **storing almost all columns except for password
                            Session["accountID"] = loginObj.accountID;
                            Session["accountType"] = loginObj.accountType;
                            Session["name"] = loginObj.name;
                            Session["email"] = tbEmail.Text;
                            Session["mobile"] = loginObj.mobile;
                            Session["dateOfBirth"] = loginObj.dateOfBirth;

                            if (Session["accountType"].ToString() == "student")
                            {
                                // student table
                                DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(1);
                                Session["adminNo"] = studentObj.adminNo;
                                Session["school"] = studentObj.studentSchool;
                                Session["course"] = studentObj.course;
                                Session["allergies"] = studentObj.allergies;
                                Session["dietaryNeeds"] = studentObj.dietaryNeeds;
                                Session["parentID"] = studentObj.parentID;

                            }
                            else if (Session["accountType"].ToString() == "parent")
                            {
                                // parent table
                                DAL.Login parentObj = LoginDAO.getParentTableByAccountID(loginObj.accountID);
                                Session["parentID"] = parentObj.parentID;
                                Session["adminNo"] = parentObj.adminNo;
                            }
                            else if (Session["accountType"].ToString() == "lecturer")
                            {
                                // lecturer table
                                DAL.Login lecturerObj = LoginDAO.getLecturerTableByAccountID(loginObj.accountID);
                                Session["staffID"] = lecturerObj.staffID;
                                Session["school"] = lecturerObj.lecturerSchool;
                                Session["staffRole"] = lecturerObj.staffRole;
                            }

                        //Response.Redirect("Default.aspx");
                        Response.Redirect("changePassword.aspx");
                    }
                        //=====================================================
                        //Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        lblError.Text = "Please check your email or password!";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
            else
            {
                lblError.Text = "Please check your email or password!";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            //}
            /*catch (Exception ex)
            {
            throw new Exception(ex.ToString());
            }
            finally { }*/


            //===========================================================================
            /* byte[] data = UTF8Encoding.UTF8.GetBytes(tbPassword.Text.Trim());
             using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
             {
                 byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                 using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                 {
                     ICryptoTransform transform = tripleDes.CreateEncryptor();
                     byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                     password = Convert.ToBase64String(results);
                 }
             }
             //lblError.Text = password;
             DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text, password);
             if (loginObj != null)
             {
                 // account Table
                 // **storing almost all columns except for password
                 Session["accountID"] = loginObj.accountID;
                 Session["accountType"] = loginObj.accountType;
                 Session["name"] = loginObj.name;
                 Session["email"] = tbEmail.Text;
                 Session["mobile"] = loginObj.mobile;
                 Session["dateOfBirth"] = loginObj.dateOfBirth;

                 if (Session["accountType"].ToString() == "student")
                 {
                     // student table
                     DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(loginObj.accountID);
                     Session["adminNo"] = studentObj.adminNo;
                     Session["school"] = studentObj.studentSchool;
                     Session["course"] = studentObj.course;
                     Session["allergies"] = studentObj.allergies;
                     Session["dietaryNeeds"] = studentObj.dietaryNeeds;
                     Session["parentID"] = studentObj.parentID;

                 }
                 else if (Session["accountType"].ToString() == "parent")
                 {
                     // parent table
                     DAL.Login parentObj = LoginDAO.getParentTableByAccountID(loginObj.accountID);
                     Session["parentID"] = parentObj.parentID;
                     Session["adminNo"] = parentObj.adminNo;
                 }
                 else if (Session["accountType"].ToString() == "lecturer")
                 {
                     // lecturer table
                     DAL.Login lecturerObj = LoginDAO.getLecturerTableByAccountID(loginObj.accountID);
                     Session["staffID"] = lecturerObj.staffID;
                     Session["school"] = lecturerObj.lecturerSchool;
                     Session["staffRole"] = lecturerObj.staffRole;
                 }

                 if (cbRememberMe.Checked) {
                     Response.Cookies["authcookie"]["email"] = tbEmail.Text;
                     Response.Cookies["authcookie"]["password"] = password;
                     Response.Cookies["authcookie"].Expires = DateTime.Now.AddDays(2);
                 }
                 Response.Redirect("Default.aspx");
             }
             else
             {
                 lblError.Text = "Please check your email or password!";
                 lblError.ForeColor = System.Drawing.Color.Red;
             }*/

        }

        // getDBHash
        protected string getDBHash(string email)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            string h = null;
            SqlConnection connection = new SqlConnection(DBConnect);
            string sql = "SELECT passwordHash from account where email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["passwordHash"] != null)
                        {
                            if (reader["passwordHash"] != DBNull.Value)
                            {
                                h = reader["passwordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally { connection.Close(); }
            return h;
        }

        // getDBSakt
        protected string getDBSalt(string email)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            string s = null;
            SqlConnection connection = new SqlConnection(DBConnect);
            string sql = "SELECT passwordSalt from account where email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["passwordSalt"] != null)
                        {
                            if (reader["passwordSalt"] != DBNull.Value)
                            {
                                s = reader["passwordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally { connection.Close(); }
            return s;
        }
    }
}