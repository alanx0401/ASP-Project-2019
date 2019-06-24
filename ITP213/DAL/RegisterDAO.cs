using ITP213.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using Twilio.Types;
using System.Net;
using Twilio.Rest.Api.V2010.Account;
using System.Data;

namespace ITP213.DAL
{
    public class RegisterDAO
    {
        /// <summary>
        /// Store Register Part 1's Content
        /// -- Part 1
        /// -- Name
        /// -- Email
        /// -- Password
        /// -- Confirm Password
        /// 
        /// Update Register Part 2's Content
        /// -- Part 2
        /// --- Date of birth
        /// --- Contact Number
        /// ---- Phone verification feature
        ///     ---> Select (Check if the data alr existed;)
        ///         ---> If they do not exist (INSERT)
        ///         ---> If they did, check dateTimeSend(cannot be more than a minute) 
        ///             --> If dateTimeSend is more than 1 minute (UPDATE VerifyPhoneOTP table --> Resend OTP)
        ///             
        /// OTP to verify phone's number
        /// -- Part 3
        /// ---- Phone verification feature (??? What if user changed their number?)
        ///     ---> Select (Check if the data alr existed;)
        ///         ---> If they do not exist (Display error msg cos they should be inserted by now)
        ///         ---> If they did, check dateTimeSend(cannot be more than a minute) 
        ///             --> If dateTimeSend is more than 1 minute (UPDATE VerifyPhoneOTP table --> Resend OTP)
        ///             --> If dateTimeSend is less than 1 minute (UPDATE Account Table(Set phoneVerified='Yes') && (Delete verifyPhoneOTP)
        /// </summary>
        // Part 1
        public static int insertIntoAccountTable(string UUID, string name, string email, string passwordHash, string passwordSalt)
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
        // Part 2: Phone Verification (Insert OTP password in VerifyPhoneOTPTable)
        public static int insertIntoVerifyPhoneOTP(string UUID, string passwordHash, string passwordSalt)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /* 
            INSERT INTO VerifyPhoneOTP
            VALUES ('{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}', 'VlK6ox52iMWXlmxmW3/Pm2+a7bFavBMLNNaEud4w/BdVmnUIWXylV2xKZoBEGD7kJu5K7mbBs5KN5UMJPdD4NA==', 'trPajFhUaIs=', GETDATE());
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("INSERT INTO VerifyPhoneOTP");
            sqlStr.AppendLine("VALUES (@UUID, @passwordHash, @passwordSalt, GETDATE());");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
            cmd.Parameters.AddWithValue("@passwordSalt", passwordSalt);

            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int updateVerifyPhoneOTP(string UUID, string passwordHash, string passwordSalt) // UPDATE (if they do exist; resend) / (if the dateTimeSend is passed a day)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE VerifyPhoneOTP
            SET passwordHash='VlK6ox52iMWXlmxmW3/Pm2+a7bFavBMLNNaEud4w/BdVmnUIWXylV2xKZoBEGD7kJu5K7mbBs5KN5UMJPdD4NA==', passwordSalt='trPajFhUaIs=', dateTimeSend=GETDATE()
            WHERE UUID='{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE VerifyPhoneOTP");
            sqlStr.AppendLine("SET passwordHash=@passwordHash, passwordSalt=@passwordSalt, dateTimeSend=GETDATE()");
            sqlStr.AppendLine("WHERE UUID=@UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
            cmd.Parameters.AddWithValue("@passwordSalt", passwordSalt);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        // Part 2 & 3: Phone Verification (Check if the data has alr existed)
        public static DAL.Register checkVerifyPhoneOTP(string UUID) // (Check if the data has alr existed; dateTimeSend)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            /*
            SELECT dateTimeSend, passwordHash, passwordSalt
            FROM VerifyPhoneOTP
            Where UUID='{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
            */

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT dateTimeSend, passwordHash, passwordSalt");
            sqlStr.AppendLine("FROM VerifyPhoneOTP");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            DAL.Register obj = new DAL.Register();

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@UUID", UUID);
            // fill dataset
            da.Fill(ds, "VerifyPhoneOTP");
            int rec_cnt = ds.Tables["VerifyPhoneOTP"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["VerifyPhoneOTP"].Rows[0];  // Sql command returns only one record
                obj.dateTimeSend = Convert.ToDateTime(row["dateTimeSend"].ToString());
                obj.passwordHash = row["passwordHash"].ToString();
                obj.passwordSalt = row["passwordSalt"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        // Part 3: Phone Verification (If the phone is verified)
        public static int updatePhoneVerifiedInAccountTable(string UUID) // UPDATE (if they do exist; resend) / (if the dateTimeSend is passed a day)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE account
            SET phoneVerified='Yes'
            WHERE UUID='{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account");
            sqlStr.AppendLine("SET phoneVerified='Yes'");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int deleteVerifyPhoneOTPTable(string UUID) // DELETE verifyPhoneOTP
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /*
            Delete from verifyPhoneOTP
            WHERE UUID='{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Delete from verifyPhoneOTP");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        // Part 3: Email Verification
        public static int insertIntoVerifyEmail(string UUID, string verificationToken) //****** NOT secure!!! INSERT Verification Email
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /* 
            INSERT INTO VerifyEmail
            VALUES ('{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}', 'trPajFhUaIs=', GETDATE());
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("INSERT INTO VerifyEmail");
            sqlStr.AppendLine("VALUES (@UUID, @verificationToken, GETDATE());");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@verificationToken", verificationToken);

            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static DAL.Register checkEmailVerificationTable(string UUID) // SELECT (to compare if the verification token is the same AND dateTimeSend must be before 24 hour) WHERE UUID = 
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            /*
            SELECT verificationToken, dateTimeSend
            FROM VerifyEmail
            WHERE UUID = '{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
            */

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT dateTimeSend");
            sqlStr.AppendLine("FROM VerifyEmail");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            DAL.Register obj = new DAL.Register();

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@UUID", UUID);
            // fill dataset
            da.Fill(ds, "VerifyPhoneOTP");
            int rec_cnt = ds.Tables["VerifyPhoneOTP"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["VerifyPhoneOTP"].Rows[0];  // Sql command returns only one record
                //obj.verificationToken = row["verificationToken"].ToString();
                obj.dateTimeSend = Convert.ToDateTime(row["dateTimeSend"].ToString());
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        public static DAL.Register checkTokenInEmailVerificationTable(string verificationToken) // SELECT (to compare if the verification token is the same AND dateTimeSend must be before 24 hour) WHERE UUID = 
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            /*
            SELECT verificationToken, dateTimeSend
            FROM VerifyEmail
            WHERE UUID = '{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
            */

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT dateTimeSend, UUID");
            sqlStr.AppendLine("FROM VerifyEmail");
            sqlStr.AppendLine("WHERE verificationToken = @verificationToken;");

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
                obj.dateTimeSend = Convert.ToDateTime(row["dateTimeSend"].ToString());
                obj.UUID = row["UUID"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        public static int deleteVerifyEmailOTPTable(string UUID) // Once verified, DELETE the verification token || DELETE the verification token if it's after 24 hours
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /*
            DELETE from VerifyEmail
            WHERE UUID = '{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Delete from verifyEmail");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int updateEmailVerifiedInAccountTable(string UUID) //Once verified, UPDATE accountTable --> emailVerified
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE account
            SET emailVerified='Yes'
            WHERE UUID='{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account");
            sqlStr.AppendLine("SET emailVerified='Yes'");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int updateVerificationTokenFromVerifyEmail(string UUID, string verificationToken) // UPDATE (dateTimeSend is after 24 hour)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            UPDATE VerifyEmail
            SET verificationToken = 'trPajFhUaIs=', dateTimeSend = GETDATE()
            WHERE UUID='{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE VerifyEmail");
            sqlStr.AppendLine("SET verificationToken = @verificationToken, dateTimeSend = GETDATE()");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@verificationToken", verificationToken);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int insertIntoNewDeviceLogin(string macAddress, string Location, string PublicIPAddress, DateTime LastLogin,string UUID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /* 
            INSERT INTO NewDeviceLogin
            VALUES ('00FFE61D21D4', 'SG', '219.74.70.95', GETDATE(), '{0x3adb3b4d,0xf5ee,0x4684,{0xb5,0xd7,0x06,0xa0,0x61,0xd2,0x85,0x83}}');
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("INSERT INTO NewDeviceLogin");
            sqlStr.AppendLine("VALUES (@macAddress, @Location, @PublicIPAddress, GETDATE(), @UUID);");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@macAddress", macAddress);
            cmd.Parameters.AddWithValue("@Location", Location);
            cmd.Parameters.AddWithValue("@PublicIPAddress", PublicIPAddress);
            cmd.Parameters.AddWithValue("@UUID", UUID);

            int result = cmd.ExecuteNonQuery();
            return result;
        }

        //========================================================================
        public static Login getLoginByUUID(string UUID)
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from account by email & password using query parameter
            string sqlString = "SELECT * FROM account WHERE UUID=@UUID";

            Login obj = new Login(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("@UUID", UUID);
            // fill dataset
            da.Fill(ds, "accountTable");
            int rec_cnt = ds.Tables["accountTable"].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables["accountTable"].Rows[0]; // Sql command only returns only 1 record
                obj.name = row["name"].ToString();
                obj.email = row["email"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }

    }
}