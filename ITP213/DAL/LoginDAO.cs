using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class LoginDAO
    {
        public static Login getLoginByEmailAndPassword(string email, string password) {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from account by email & password using query parameter
            string sqlString = "SELECT * FROM account WHERE email = @aEmail AND passwordHash = @aPassword";

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("aEmail", email);
            da.SelectCommand.Parameters.AddWithValue("aPassword", password);
            // fill dataset
            da.Fill(ds, "accountTable");
            int rec_cnt = ds.Tables["accountTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["accountTable"].Rows[0]; // Sql command only returns only 1 record
                obj.accountID = Convert.ToInt32(row["accountID"]);
                obj.accountType = row["accountType"].ToString();
                obj.name = row["name"].ToString();
                obj.email = row["email"].ToString();
                obj.mobile = row["mobile"].ToString();
                obj.dateOfBirth = Convert.ToDateTime(row["dateOfBirth"]);
                obj.password = row["passwordHash"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        // to retrieve student's table info
        public static Login getStudentTableByAccountID(int accountID) {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from student by accountID using query parameter
            string sqlString = "SELECT * FROM student WHERE accountID = @sAccountID";

            //SELECT * FROM student where accountID=1;

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("sAccountID", accountID);
            // fill dataset
            da.Fill(ds, "studentTable");
            int rec_cnt = ds.Tables["studentTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["studentTable"].Rows[0]; // Sql command only returns only 1 record
                obj.adminNo = row["adminNo"].ToString();
                obj.studentSchool = row["school"].ToString();
                obj.course = row["course"].ToString();
                obj.allergies = row["allergies"].ToString();
                obj.dietaryNeeds = row["dietaryNeeds"].ToString();
                obj.parentID = Convert.ToInt32(row["parentID"]);
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        // to retrieve parent's table info
        public static Login getParentTableByAccountID(int accountID)
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
        }
        // to retrieve lecturer's table info
        public static Login getLecturerTableByAccountID(int accountID)
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from lecturer by accountID using query parameter
            string sqlString = "SELECT * FROM lecturer WHERE accountID = @lAccountID";

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("lAccountID", accountID);
            // fill dataset
            da.Fill(ds, "lecturerTable");
            int rec_cnt = ds.Tables["lecturerTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["lecturerTable"].Rows[0]; // Sql command only returns only 1 record
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
        // insert
        public static int insert(string name, string email, string mobile, string dateOfBirth, string passwordHash, string passwordSalt)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /* 
             * "INSERT INTO account(name, accountType, accountStatus, email, mobile, dateOfBirth, passwordHash, passwordSalt, emailVerified, phoneVerified, banAccountDateTime, googleAuthEnabled, otpEnabled, changePasswordDate)
             VALUES('Lin Peishan1111', 'student', 'Not ban', 'lin@email.com', '81849020', '1/31/2018', 'ewewfewfw', 'anything', 'No', 'No', '','No', 'No', GETDATE())"
             * 
             */
            string sqlStr =
                "INSERT INTO account(name, accountType, accountStatus, email, mobile, dateOfBirth, passwordHash, passwordSalt, emailVerified, phoneVerified, banAccountDateTime, googleAuthEnabled, otpEnabled, changePasswordDate) VALUES(@name, 'student', 'Not ban', @email, @mobile, @dateOfBirth, @passwordHash, @passwordSalt, 'No', 'No', '', 'No', 'No', GETDATE())";
                //"INSERT INTO Person (FullName,Gender,PersonRole)VALUES(@pFullName,@pGender,@pPersonRole)";

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@mobile", mobile);
            // DateTime.ParseExact(arrivalDate,"MM/dd/yyyy", null)
            cmd.Parameters.AddWithValue("@dateOfBirth", DateTime.ParseExact(dateOfBirth, "MM/dd/yyyy", null));
            cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
            cmd.Parameters.AddWithValue("@passwordSalt", passwordSalt);

            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}