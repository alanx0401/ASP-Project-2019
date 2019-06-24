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
    }
}