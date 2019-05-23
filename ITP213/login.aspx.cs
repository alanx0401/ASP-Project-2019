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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string password = "";
            byte[] data = UTF8Encoding.UTF8.GetBytes(tbPassword.Text);
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
            }

        }
    }
}