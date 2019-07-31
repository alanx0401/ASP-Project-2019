using ITP213.DAL;
using ITP213.Email;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ConfirmEmail : System.Web.UI.Page
    {
        // for encryption & decryption
        protected void Page_Load(object sender, EventArgs e)
        {
            // 2 things: retrieve the token from QUERY STRING & token from db
            // both needs to be decrypted
            if (!IsPostBack) // first load
            {
                string emailToken = Request.QueryString["x"];
                string changeBackToOriginalEmail = Request.QueryString["y"];

                if (changeBackToOriginalEmail == null && emailToken == null)
                {
                    lblResult.Text = "We're sorry, the link you've submitted is invalid, expired, or has already been used.";
                }
                else
                {
                    if (changeBackToOriginalEmail != null)
                    {
                        changeBackToOriginalEmail = Request.QueryString["y"].ToString();
                        changeBackToOriginalEmail = DecodeToken(changeBackToOriginalEmail);
                        // search for oldEmailToken to find the old email
                        Register obj = checkOldEmailTokenInOldEmail(changeBackToOriginalEmail);
                        if (obj != null)
                        {

                            string oldEmail = obj.oldEmail;
                            string UUID = obj.UUID;
                            // update old email into account table
                            int result = updateOldEmailInAccountTable(UUID, oldEmail);
                            if (result == 1)
                            {
                                // delete oldEmail based on oldEmail
                                int result2 = deleteOldEmail(changeBackToOriginalEmail);
                                if (result2 == 1)
                                {
                                    int result3 = RegisterDAO.deleteVerifyEmailOTPTable(UUID);
                                    
                                    lblResult.Text = "We have changed back the email to your old email.";
                                }
                                else
                                {
                                    lblResult.Text = "We're sorry, the link you've submitted is invalid, expired, or has already been used.";
                                }
                            }
                            else
                            {
                                lblResult.Text = "We're sorry, the link you've submitted is invalid, expired, or has already been used.";
                            }
                        }
                        else
                        {
                            lblResult.Text = "We're sorry, the link you've submitted is invalid, expired, or has already been used.";
                        }
                    }

                    if (emailToken != null)
                    {
                        // decryting 
                        emailToken = Request.QueryString["x"].ToString();
                        emailToken = DecodeToken(emailToken);
                        // SELECT
                        Register obj = RegisterDAO.checkTokenInEmailVerificationTable(emailToken); // if emailToken exist
                        if (obj != null) // token exists
                        {
                            // check if the verification email has expired
                            var currentDateTime = DateTime.Now;
                            var emailDateTimeSend = obj.dateTimeSend;
                            var diff = currentDateTime.Subtract(emailDateTimeSend);

                            string UUID = obj.UUID;

                            if (diff.Hours < 24) // token is still valid, change account verification
                            {
                                int result = RegisterDAO.deleteVerifyEmailOTPTable(UUID);
                                if (result == 1)
                                {
                                    int result2 = RegisterDAO.updateEmailVerifiedInAccountTable(UUID);
                                    if (result2 == 1)
                                    {
                                        lblResult.Text = "Email is successfully verified.";
                                    }
                                    else
                                    {
                                        lblResult.Text = "Sorry! An error has occurred!";
                                    }
                                }
                            }
                            else // token expired
                            {
                                lblResult.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                                /*int result = DAL.RegisterDAO.deleteVerifyEmailOTPTable(UUID); // delete current expired token
                                if (result == 1)
                                {
                                    string randomToken = Guid.NewGuid().ToString(); // email Token
                                    string encodeRandomToken = EncodeToken(randomToken);

                                    // insert
                                    int result2 = RegisterDAO.insertIntoVerifyEmail(UUID, randomToken);
                                    if (result2 == 1)
                                    {
                                        // get account Table
                                        DAL.Login LoginObj = RegisterDAO.getLoginByUUID(UUID);

                                        Execute(LoginObj.name, LoginObj.email, encodeRandomToken);
                                        lblResult.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                                    }
                                    else
                                    {
                                        lblResult.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                                    }


                                }*/
                            }
                        }
                        else // token does not exist
                        {
                            lblResult.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                        }
                    }
                }
                
            }
        
        }
        
        public static string EncodeToken(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }


        public static string DecodeToken(string encodedServername)
        {
            string result = "";
            try
            {
                result = Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
            }
            catch (Exception)
            {
                result = encodedServername;
            }
            finally { }
            return result;
        }

        public static DAL.Register checkOldEmailTokenInOldEmail(string verificationToken) 
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            /*
            SELECT *
            FROM OldEmail
            WHERE oldEmailToken='a8c41c6c-1379-4fdf-aa4c-4d03e4d78d6c';
            */

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT *");
            sqlStr.AppendLine("FROM OldEmail");
            sqlStr.AppendLine("WHERE oldEmailToken=@verificationToken;");

            DAL.Register obj = new DAL.Register();

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@verificationToken", verificationToken);
            // fill dataset
            da.Fill(ds, "VerifyPhoneOTP");
            int rec_cnt = ds.Tables["VerifyPhoneOTP"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["VerifyPhoneOTP"].Rows[0];  // Sql command returns only one record
                obj.oldEmail = row["oldEmail"].ToString();
                obj.UUID = row["UUID"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public static int updateOldEmailInAccountTable(string UUID, string email) //Once verified, UPDATE accountTable --> emailVerified
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE account
            SET email='linpeishann@gmail.com'
            WHERE UUID='{0x66b75693,0x0472,0x4e03,{0xbc,0x17,0x79,0xfc,0xb2,0xaa,0x4f,0x12}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account");
            sqlStr.AppendLine("SET email=@email, emailVerified='Yes'");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@email", email);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int deleteOldEmail(string token) // DELETE verifyPhoneOTP
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Delete from OldEmail");
            sqlStr.AppendLine("WHERE oldEmailToken=@oldEmailToken;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@oldEmailToken", token);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}