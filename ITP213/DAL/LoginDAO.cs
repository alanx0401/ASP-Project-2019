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
            // Write SQL Statement to retrieve all columns from lecturer by accountID using query parameter
            string sqlString = "SELECT * FROM lecturer WHERE UUID = @UUID";

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("UUID", UUID);
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
        /// <summary>
        /// Store Register Part 1's Content
        /// -- Part 1
        /// -- Name
        /// -- Email
        /// -- Password
        /// -- Confirm Password
        /// </summary>
        public static int insertIntoAccountTable(string UUID,string name, string email, string passwordHash, string passwordSalt)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /* 
             * "INSERT INTO account(name, accountType, accountStatus, email, mobile, dateOfBirth, passwordHash, passwordSalt, emailVerified, phoneVerified, banAccountDateTime, googleAuthEnabled, otpEnabled, changePasswordDate)
             VALUES('Lin Peishan1111', 'student', 'Not ban', 'lin@email.com', '81849020', '1/31/2018', 'ewewfewfw', 'anything', 'No', 'No', '','No', 'No', GETDATE())"
             * 
             */
            
            string sqlStr =
                "INSERT INTO account(UUID, name, accountType, accountStatus, email, mobile, dateOfBirth, passwordHash, passwordSalt, emailVerified, phoneVerified, banAccountDateTime, googleAuthEnabled, otpEnabled, changePasswordDate) VALUES(@UUID, @name, 'student', 'Not ban', @email, '', '', @passwordHash, @passwordSalt, 'No', 'No', '', 'No', 'No', GETDATE())";
                //"INSERT INTO Person (FullName,Gender,PersonRole)VALUES(@pFullName,@pGender,@pPersonRole)";

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            // cmd.Parameters.AddWithValue("@mobile", mobile);
            // DateTime.ParseExact(arrivalDate,"MM/dd/yyyy", null)
            // cmd.Parameters.AddWithValue("@dateOfBirth", DateTime.ParseExact(dateOfBirth, "MM/dd/yyyy", null));
            cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
            cmd.Parameters.AddWithValue("@passwordSalt", passwordSalt);

            int result = cmd.ExecuteNonQuery();
            /*string sendEmail = ConfigurationManager.AppSettings["SendEmail"];
            if (sendEmail.ToLower() == "true")
            {
                SendEmail("Yayy!");
            }*/
            string sendEmail = ConfigurationManager.AppSettings["SendEmail"];
            if (sendEmail.ToLower() == "true")
            {
                //var emailVerificatonCode = mUserManger.Gener
                //Execute("Hi","www.localhost");
                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                
                
            }
            //Execute().Wait();
            string MyNewGuid = Guid.NewGuid().ToString(); // email Token
            Execute("Peishan", MyNewGuid);
            return result;
        }
        // Part 1: Store adminNo in student table
        public static int insertaAdminNoInAdminTable(string adminNo, string UUID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /* 
             * INSERT INTO student(adminNo, UUID) VALUES('171846Z','{0x0ba085b1,0x4ad8,0x480a,{0xa0,0xf9,0x4b,0x5b,0xa1,0x3c,0xa9,0xe9}}'
             * 
             */
            string sqlStr =
                "INSERT INTO student(adminNo, UUID) VALUES(@adminNo,@UUID);";

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@adminNo", adminNo);

            int result = cmd.ExecuteNonQuery();

            return result;
        }
        // Part 2: Update mobile & dateOfBirth in accountTable through adminNo from student table.
        public static int updateById(string mobile, string dateOfBirth, string adminNo)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE account
            SET mobile = '81234567', dateOfBirth='01-31-2000'
            FROM account
            INNER JOIN student
            ON account.UUID = student.UUID
            WHERE adminNo='171846z';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account ");
            sqlStr.AppendLine("SET mobile = @mobile, dateOfBirth = @dateOfBirth");
            sqlStr.AppendLine("FROM account");
            sqlStr.AppendLine("INNER JOIN student");
            sqlStr.AppendLine("ON account.UUID = student.UUID");
            sqlStr.AppendLine("WHERE adminNo=@adminNo;");


            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@mobile", mobile);
            cmd.Parameters.AddWithValue("@dateOfBirth", DateTime.ParseExact(dateOfBirth, "MM/dd/yyyy", null));
            cmd.Parameters.AddWithValue("@adminNo", adminNo);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        // send grid
        //public static async Task<SendEmailResponse> Execute(SendEmailDetails details)
        public static async Task<SendEmailResponse> Execute(string displayName, string randomToken)
        {
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_API_KEY"];
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("nyptravel2019@gmail.com", "NYP Travel");
            var subject = "NYP Travel - Email Confirmation";
            var to = new EmailAddress("linpeishann@gmail.com", "Peishan");
            var plainTextContent = "Confirm your account";

            string encodeRandomToken = EncodeServerName(randomToken);
            var htmlContent = "Hi, "+displayName+ ". Please confirm your account by clicking this link: <strong><a href=\"" + encodeRandomToken + "\">link</a></strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return new SendEmailResponse();
        }

        public static string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public static string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }

    }
}