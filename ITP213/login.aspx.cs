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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // disable autocomplete form        
            tbEmail.Attributes.Add("autocomplete", "off");
            tbPassword.Attributes.Add("autocomplete", "off");
            tb2FAPin.Attributes.Add("autocomplete", "off"); 

            if (!IsPostBack) // First Load of the page
            {
                if (Request.Browser.Cookies) // check if browser supports cookies
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
                }

                
            }

            // check recaptcha verification if fail count is more than or equal to 6
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            if (loginObj != null)
            {
                DAL.Login AccountFailedAttemptObj = getAccountFailedAttemptByUUID(loginObj.UUID);
                if (AccountFailedAttemptObj != null)
                {
                    int failCount = AccountFailedAttemptObj.AccountFailedAttemptCounter;
                    if (failCount >= 5) // implement reCAPTCHA
                    {
                        ReCaptchContainer.Visible = true;
                    }
                    else
                    {
                        ReCaptchContainer.Visible = false;
                    }
                }

            }
        }
        protected void btnCheckFor2FA_Click(object sender, EventArgs e) // fix error exception msg!
        {
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
                    
                    if (CheckBanAccount() == true) 
                    { }
                    else // if account is not ban, continue verifying the user
                    {
                        DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
                        string UUID = loginObj.UUID;

                        int result = updateAccountStatusToNotBanByUUID(UUID); // change from 'Ban' to 'Not ban'
                        if (result == 1) { }
                        else
                        {
                            lblError.Text = "An error has occured. Please try again.";
                        }


                        if (userHash == dbHash) // password matches
                        {
                            if (ReCaptchContainer.Visible == true) // accountFailedAttempt is equal or more than 6 times
                            {
                                if (IsReCaptchValid() == true) // CAPTCHA is crt
                                {
                                    LoginAuthentication();
                                }
                                else
                                {
                                    lblError.Text = "Please check your email or password!";
                                    lblError.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else // accountFailedAttempt is less than 6 times
                            {
                                LoginAuthentication();
                            }
                        }
                        else // if login fails
                        {
                            DAL.Login AccountFailedAttemptObj = getAccountFailedAttemptByUUID(UUID);

                            if (AccountFailedAttemptObj != null) // update failed attempt
                            {
                                int failCount = AccountFailedAttemptObj.AccountFailedAttemptCounter;
                                if (failCount >= 9) // change account status from 'Not ban' to 'Ban' when fail count hits 10 in db ; #problem: doesn't update to 10
                                {
                                    int result2 = updateAccountStatusToBanByUUID(UUID);
                                    if (result2 == 1) { }
                                    else
                                    {
                                        lblError.Text = "An error has occured. Please try again.";
                                    }

                                }
                                else
                                {
                                    failCount += 1;

                                    int result2 = updateAccountFailedAttemptTableByUUID(UUID, failCount);
                                    if (result2 == 1) { }
                                    else
                                    {
                                        lblError.Text = "An error has occured. Please try again.";
                                    }
                                }

                            }
                            else // insert failed attempt
                            {
                                int result2 = insertAccountFailedAttemptTable(UUID);
                                if (result2 == 1) { }
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
            /*}
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
                //throw new Exception(ex.ToString());
            }
            finally { }*/

        }
        protected void btnSubmitChoice_Click(object sender, EventArgs e)
        {
            PanelPart2.Visible = false;
            PanelPart3.Visible = true;
            lblTitle.Text = "Enter verification code";
            // Google Auth and 2FA feature here
        }

        protected void btnBack2_Click(object sender, EventArgs e)
        {
            PanelPart3.Visible = false;
            PanelPart2.Visible = true;
            lblTitle.Text = "Send verification code";
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //-- Check if the password is crt, if it is, then find the UUID to retrieve personal datas


            // delete account failed attempts
            /*DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            string UUID = loginObj.UUID;
            DAL.Login AccountFailedAttemptObj = getAccountFailedAttemptByUUID(UUID);

            if (AccountFailedAttemptObj != null) // Delete failed attempt(s)
            {

                int result = deleteAccountFailedAttemptTableByUUID(UUID);
                if (result == 1) { }
                else
                {
                    lblError.Text = "An error has occured. Please try again.";
                }

            }*/
            //-- If password is wrong,
            /*else // if login fails
            {*/
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            string UUID = loginObj.UUID;
            DAL.Login AccountFailedAttemptObj = getAccountFailedAttemptByUUID(UUID);

            if (AccountFailedAttemptObj != null) // update
            {
                int failCount = AccountFailedAttemptObj.AccountFailedAttemptCounter;
                if (failCount == 10)
                {
                    int result = updateAccountStatusToBanByUUID(UUID);
                    if (result == 1) { }
                    else
                    {
                        lblError.Text = "An error has occured. Please try again.";
                    }
                }
                else
                {
                    failCount += 1;

                    int result = updateAccountFailedAttemptTableByUUID(UUID, failCount);
                    if (result == 1) { }
                    else
                    {
                        lblError.Text = "An error has occured. Please try again.";
                    }
                }

            }
            else // insert
            {
                int result = insertAccountFailedAttemptTable(UUID);
                if (result == 1) { }
                else
                {
                    lblError.Text = "An error has occured. Please try again.";
                }
            }


            lblError.Text = "Please check your email or password!";
            lblError.ForeColor = System.Drawing.Color.Red;
            //}
            // Password Expiration: cannot be more than a year --> Based on UUID
            DAL.Login p = getChangePasswordDateByEmailAndPasswordHash("{0x39d609a5,0x10eb,0x40a8,{0x86,0xf1,0x06,0x0b,0xfe,0xe7,0xc1,0x82}}");

            DateTime changePasswordDate = Convert.ToDateTime(p.changePasswordDate.ToString());

            if ((DateTime.Now - changePasswordDate).TotalDays > 365)// if bigger than one year
            {
                Response.Redirect("changePassword.aspx");
            }
            string PCName = Dns.GetHostEntry(Request.ServerVariables["REMOTE_ADDR"]).HostName;

            Response.Redirect("Default.aspx");
        }
        // =================================================================================================================
        /// <summary>
        /// Verify authentic user
        /// </summary>
        protected void LoginAuthentication()
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
                    Session["UUID"] = loginObj.UUID;
                    Session["accountID"] = loginObj.UUID;
                    Session["accountType"] = loginObj.accountType;
                    Session["name"] = loginObj.name;
                    Session["email"] = tbEmail.Text;
                    Session["mobile"] = loginObj.mobile;
                    Session["dateOfBirth"] = loginObj.dateOfBirth;

                    DAL.Login AccountFailedAttemptObj = getAccountFailedAttemptByUUID(loginObj.UUID);

                    if (AccountFailedAttemptObj != null) // Delete failed attempt(s)
                    {

                        int result = deleteAccountFailedAttemptTableByUUID(loginObj.UUID);
                        if (result == 1) { }
                        else
                        {
                            lblError.Text = "An error has occured. Please try again.";
                        }

                    }

                    if (Session["accountType"].ToString() == "student")
                    {
                        // student table
                        DAL.Login studentObj = LoginDAO.getStudentTableByAccountID(loginObj.UUID);
                        Session["adminNo"] = studentObj.adminNo;
                        Session["school"] = studentObj.studentSchool;
                        Session["course"] = studentObj.course;
                        Session["allergies"] = studentObj.allergies;
                        Session["dietaryNeeds"] = studentObj.dietaryNeeds;
                        //Session["parentID"] = studentObj.parentID;

                    }
                    /*else if (Session["accountType"].ToString() == "parent")
                    {
                        // parent table
                        DAL.Login parentObj = LoginDAO.getParentTableByAccountID(loginObj.accountID);
                        Session["parentID"] = parentObj.parentID;
                        Session["adminNo"] = parentObj.adminNo;
                    }*/
                    else if (Session["accountType"].ToString() == "lecturer")
                    {
                        // lecturer table
                        DAL.Login lecturerObj = LoginDAO.getLecturerTableByAccountID(loginObj.UUID);
                        Session["staffID"] = lecturerObj.staffID;
                        Session["school"] = lecturerObj.lecturerSchool;
                        Session["staffRole"] = lecturerObj.staffRole;
                    }

                    // Password Expiration: cannot be more than a year

                    DateTime changePasswordDate = Convert.ToDateTime(loginObj.changePasswordDate.ToString());

                    if ((DateTime.Now - changePasswordDate).TotalDays > 365)// if bigger than one year
                    {
                        Response.Redirect("changePassword.aspx");
                    }
                    

                    Response.Redirect("Default.aspx");
                }
            }
        }
        /// <summary>
        /// Feature: Check Ban Account
        /// </summary>
        protected Boolean CheckBanAccount()
        {
            Boolean verdict = false;
            DAL.Login loginObj = LoginDAO.getLoginByEmailAndPassword(tbEmail.Text);
            if (loginObj != null)
            {
                if (loginObj.accountStatus == "Ban")
                {
                    // Compare the banAccountDateTime
                    
                    var currentDateTime = DateTime.Now;
                    var banAccountDateTime = loginObj.banAccountDateTime;
                    var diff = currentDateTime.Subtract(banAccountDateTime);
                    //var res = String.Format("{0}:{1}:{2}", diff.Hours, diff.Minutes, diff.Seconds);
                    //lblError.Text = res;
                    if (diff.Hours < 5) // if the hour difference is less than 5, account is ban
                    {
                        verdict = true;
                        lblError.Text = "Sorry! Your account is temporarily ban. Please try again later.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                    else // account is not ban 
                    { }
                }
                else // account is not ban
                {

                }
            }
            return verdict;
        }
        /// <summary>
        /// Password Hashing
        /// </summary>
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

        /// <summary>
        /// Feature: Password Expiration
        /// </summary>
        // Retrieve changePasswordDate to see if the change password date is after a year.
        public static DAL.Login getChangePasswordDateByEmailAndPasswordHash(string UUID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            /*
            SELECT changePasswordDate
            FROM account
            WHERE UUID = '{0x39d609a5,0x10eb,0x40a8,{0x86,0xf1,0x06,0x0b,0xfe,0xe7,0xc1,0x82}}';
            */

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT *");
            sqlStr.AppendLine("FROM account");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            DAL.Login obj = new DAL.Login();

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@UUID", UUID);
            // fill dataset
            da.Fill(ds, "account");
            int rec_cnt = ds.Tables["account"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["account"].Rows[0];  // Sql command returns only one record
                obj.changePasswordDate = row["changePasswordDate"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        /// <summary>
        /// Feature: Ban Account
        /// </summary>
        public static DAL.Login getAccountFailedAttemptByUUID(string UUID) // select
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT *");
            sqlStr.AppendLine("FROM AccountFailedAttempt");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            DAL.Login obj = new DAL.Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("UUID", UUID);
            // fill dataset
            da.Fill(ds, "AccountFailedAttemptTable");
            int rec_cnt = ds.Tables["AccountFailedAttemptTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["AccountFailedAttemptTable"].Rows[0]; // Sql command only returns only 1 record
                obj.UUID = row["UUID"].ToString();
                obj.AccountFailedAttemptCounter = Convert.ToInt32(row["AccountFailedAttemptCounter"].ToString());
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        public static int insertAccountFailedAttemptTable(string UUID)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /* 
            INSERT INTO AccountFailedAttempt
            VALUES('{0x39d609a5,0x10eb,0x40a8,{0x86,0xf1,0x06,0x0b,0xfe,0xe7,0xc1,0x82}}', 1);
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("INSERT INTO AccountFailedAttempt");
            sqlStr.AppendLine("VALUES(@UUID, 1);");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);

            int result = cmd.ExecuteNonQuery();

            return result;
        }
        public static int updateAccountFailedAttemptTableByUUID(string UUID, int AccountFailedAttemptCounter)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE AccountFailedAttempt
            SET AccountFailedAttemptCounter=2
            WHERE UUID = '{0x39d609a5,0x10eb,0x40a8,{0x86,0xf1,0x06,0x0b,0xfe,0xe7,0xc1,0x82}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE AccountFailedAttempt");
            sqlStr.AppendLine("SET AccountFailedAttemptCounter = @AccountFailedAttemptCounter");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@AccountFailedAttemptCounter", AccountFailedAttemptCounter);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int deleteAccountFailedAttemptTableByUUID(string UUID) // delete: when login successfully
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /*
            DELETE AccountFailedAttempt 
            WHERE UUID = '{0x39d609a5,0x10eb,0x40a8,{0x86,0xf1,0x06,0x0b,0xfe,0xe7,0xc1,0x82}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("DELETE AccountFailedAttempt");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int updateAccountStatusToBanByUUID(string UUID) // change status from 'Not ban' to 'Ban'
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE account
            SET accountStatus = 'Ban'
            WHERE UUID = '{0x39d609a5,0x10eb,0x40a8,{0x86,0xf1,0x06,0x0b,0xfe,0xe7,0xc1,0x82}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account");
            sqlStr.AppendLine("SET accountStatus = 'Ban', banAccountDateTime=GETDATE()");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int updateAccountStatusToNotBanByUUID(string UUID) // change status from 'Ban' to 'Not ban'
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE account
            SET accountStatus = 'Not ban'
            WHERE UUID = '{0x39d609a5,0x10eb,0x40a8,{0x86,0xf1,0x06,0x0b,0xfe,0xe7,0xc1,0x82}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account");
            sqlStr.AppendLine("SET accountStatus = 'Not ban', banAccountDateTime=''");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public bool IsReCaptchValid() // CAPTCHA
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = ConfigurationManager.AppSettings["SecretKey"];
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
    }
}