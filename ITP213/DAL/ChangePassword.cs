using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ITP213.DAL
{
    public class ChangePassword
    {
        // Part 1
        public static int updatePasswordIntoAccountTable(string UUID, string passwordHash, string passwordSalt)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /* 
             
            UPDATE account
            SET passwordHash='Hi', passwordSalt='kdmakdkdk', changePasswordDate=GETDATE()
            WHERE UUID='{0x3adb3b4d,0xf5ee,0x4684,{0xb5,0xd7,0x06,0xa0,0x61,0xd2,0x85,0x83}}'
             * 
             */

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account");
            sqlStr.AppendLine("SET passwordHash=@passwordHash, passwordSalt=@passwordSalt, changePasswordDate=GETDATE()");
            sqlStr.AppendLine("WHERE UUID=@UUID");
           
            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
            cmd.Parameters.AddWithValue("@passwordSalt", passwordSalt);

            int result = cmd.ExecuteNonQuery();

            return result;
        }
    }
}