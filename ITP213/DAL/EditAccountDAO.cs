using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ITP213.DAL
{
    public class EditAccountDAO
    {
        public static int updateMobileByUUID(string mobile, string UUID)
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
            sqlStr.AppendLine("SET mobile = @mobile, phoneVerified='No'");
            sqlStr.AppendLine("FROM account");
            sqlStr.AppendLine("WHERE UUID=@UUID;");


            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@mobile", mobile);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int updateEmailByUUID(string email, string UUID)
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
            sqlStr.AppendLine("SET email = @email, emailVerified='No'");
            sqlStr.AppendLine("FROM account");
            sqlStr.AppendLine("WHERE UUID=@UUID;");


            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int insertOldEmailByUUID(string email, string UUID, string oldEmailToken, DateTime changedDate)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            INSERT INTO table_name
            VALUES (value1, value2, value3, ...);
            oldEmailToken
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Insert into OldEmail ");
            sqlStr.AppendLine("VALUES (@UUID, @email, @oldEmailToken, @changedDate);");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@oldEmailToken", oldEmailToken);
            cmd.Parameters.AddWithValue("@changedDate", changedDate);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}