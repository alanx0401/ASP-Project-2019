using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Web.SessionState;

namespace ITP213.DAL
{
    public class SecurityDAO
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        

        public string check_account_type(string uuid)
        {
            string accountType;
            //Use this method to check the account type of the user
            string queryStr = "SELECT accountType FROM account WHERE UUID = @uuid";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@uuid", uuid);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Check if there are any resultsets
            if (dr.Read())
            {
                accountType = dr["accountType"].ToString();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("User not found");
                accountType = null;
            }
            conn.Close();
            dr.Close();
            dr.Dispose();
            return accountType;
        }

        public Boolean check_account_admin(string uuid)
        {
            string accountType;
            Boolean returnValue;
            //Use this method to check the account type of the user
            string queryStr = "SELECT accountType FROM account WHERE UUID = @uuid";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@uuid", uuid);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Check if there are any resultsets
            if (dr.Read())
            {
                accountType = dr["accountType"].ToString();
                if (accountType.Equals("admin"))
                {
                    returnValue = true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("User not admin");
                    returnValue = false;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("User not found");
                returnValue = false;
            }
            conn.Close();
            dr.Close();
            dr.Dispose();
            
            return returnValue;
            
        }

        public Boolean check_account_student(string uuid)
        {
            string accountType;
            Boolean returnValue;
            //Use this method to check the account type of the user
            string queryStr = "SELECT accountType FROM account WHERE UUID = @uuid";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@uuid", uuid);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //Check if there are any resultsets
                if (dr.Read())
                {
                    accountType = dr["accountType"].ToString();
                    if (accountType.Equals("student"))
                    {
                        returnValue = true;
                        System.Diagnostics.Debug.WriteLine("user is student");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("User not admin");
                        returnValue = false;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("User not found");
                    returnValue = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            

            return returnValue;

        }
        public void apply_session_fixation_patch(HttpSessionState session, HttpRequest request, HttpResponse response)
        {
            if (session["UUID"] != null && session["AuthToken"] != null && request.Cookies["AuthToken"] != null)
            {
                if (!session["AuthToken"].ToString().Equals(request.Cookies["AuthToken"].Value))
                {
                    //TODO: Need to Change
                    response.Redirect("authnotequaltocookieauth.aspx");
                    
                }
                
            }
            else
            {
                Exception ex = new Exception("User id or cookieauth is empty");
                //TODO: Need to Change
                response.Redirect("uidorauthandcookieauthempty.aspx");
                
            }
            //return response;
        }

        public void check_accounts_expired()
        {
            string queryStr = "UPDATE account SET accountStatus= 'Ban' WHERE (DATEDIFF(year,accountCreated,GETDATE()) >= 3)";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            } catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        public string HTML_Encoding(string text)
        {
            string encodedString;
            encodedString = HttpUtility.HtmlEncode(text);
            return encodedString;
        }

        public string HTML_Decoding(string text)
        {
            string decodedString = HttpUtility.HtmlDecode(text);
            return decodedString;
        }
    }
}