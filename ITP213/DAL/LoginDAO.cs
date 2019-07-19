using ITP213.Email;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ITP213.DAL
{
    public class LoginDAO
    {
        public static Login getLoginByEmailAndPassword(string email) {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from account by email & password using query parameter
            string sqlString = "SELECT * FROM account WHERE email = @aEmail";

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("aEmail", email);
            // fill dataset
            da.Fill(ds, "accountTable");
            int rec_cnt = ds.Tables["accountTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["accountTable"].Rows[0]; // Sql command only returns only 1 record
                obj.UUID = row["UUID"].ToString();
                obj.accountType = row["accountType"].ToString();
                obj.name = row["name"].ToString();
                obj.email = row["email"].ToString();
                obj.mobile = row["mobile"].ToString();
                obj.dateOfBirth = Convert.ToDateTime(row["dateOfBirth"]);
                obj.password = row["passwordHash"].ToString();
                obj.googleAuthEnabled = row["googleAuthEnabled"].ToString();
                obj.otpEnabled = row["otpEnabled"].ToString();
                obj.changePasswordDate = row["changePasswordDate"].ToString();
                obj.accountStatus = row["accountStatus"].ToString();
                obj.banAccountDateTime = Convert.ToDateTime(row["banAccountDateTime"].ToString());
                obj.secretKey = row["secretKey"].ToString();
                obj.Key = row["Key"].ToString();
                obj.IV = row["IV"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        // to retrieve student's table info
        public static Login getStudentTableByAccountID(string UUID) {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from student by accountID using query parameter
            string sqlString = "SELECT * FROM student WHERE UUID = @UUID";

            //SELECT * FROM student where accountID=1;

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("UUID", UUID);
            // fill dataset
            da.Fill(ds, "studentTable");
            int rec_cnt = ds.Tables["studentTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["studentTable"].Rows[0]; // Sql command only returns only 1 record
                obj.adminNo = row["UUID"].ToString();
                obj.studentSchool = row["school"].ToString();
                obj.course = row["course"].ToString();
                obj.allergies = row["allergies"].ToString();
                obj.dietaryNeeds = row["dietaryNeeds"].ToString();
                //obj.parentID = Convert.ToInt32(row["parentID"]);
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        // to retrieve parent's table info
        /*public static Login getParentTableByAccountID(int accountID)
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from parent by accountID using query parameter
            string sqlString = "SELECT * FROM parent WHERE accountID = @pAccountID";

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("pAccountID", accountID);
            // fill dataset
            da.Fill(ds, "parentTable");
            int rec_cnt = ds.Tables["parentTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["parentTable"].Rows[0]; // Sql command only returns only 1 record
                obj.parentID = Convert.ToInt32(row["parentID"]);
                obj.adminNo = row["adminNo"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }*/
        // to retrieve lecturer's table info
        public static Login getLecturerTableByAccountID(string UUID)
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            string sqlString = "SELECT * FROM lecturer WHERE UUID = @UUID";

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("UUID", UUID);
            // fill dataset
            da.Fill(ds, "studentTable");
            int rec_cnt = ds.Tables["studentTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["studentTable"].Rows[0]; // Sql command only returns only 1 record
                obj.staffID = row["staffID"].ToString();
                obj.lecturerSchool = row["school"].ToString();
                obj.staffRole = row["staffRole"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        //=========================================================================================================
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
        // temp: have not choose whether to keep it
        public static string getDBHash(string email)
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
        public static string getDBSalt(string email)
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