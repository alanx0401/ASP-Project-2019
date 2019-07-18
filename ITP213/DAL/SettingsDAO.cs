﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ITP213.DAL
{
    public class SettingsDAO
    {
        public static Settings getAccountTableByUUID(string UUID)
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create adapter 
            // Write SQL Statement to retrieve all columns from account by email & password using query parameter
            string sqlString = "SELECT * FROM account WHERE UUID = @UUID";

            DAL.Settings obj = new DAL.Settings(); // create a login instance;

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlString, myConn);
            da.SelectCommand.Parameters.AddWithValue("UUID", UUID);
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
                obj.phoneVerified = row["phoneVerified"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        public static int updateGoogleAuthEnabledAndSecretKeyInAccount(string UUID, string secretKey) // change status from 'Not ban' to 'Ban'
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            --Update secret Key
            UPDATE account
            SET secretKey = 'Yu@235eR'
            WHERE UUID='{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account");
            sqlStr.AppendLine("SET secretKey = @secretKey, googleAuthEnabled='Yes'");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@secretKey", secretKey);
            int result = cmd.ExecuteNonQuery();

            return result;
        }

        public static int updateOTPEnabledInAccount(string UUID, string otpEnabled) // change status from 'Not ban' to 'Ban'
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*
            --Update secret Key
            UPDATE account
            SET secretKey = 'Yu@235eR'
            WHERE UUID='{0x52e09610,0x3746,0x44fa,{0x99,0x4c,0xe4,0x9a,0xa4,0x06,0xc0,0xa8}}';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE account");
            sqlStr.AppendLine("SET otpEnabled=@otpEnabled");
            sqlStr.AppendLine("WHERE UUID = @UUID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            cmd.Parameters.AddWithValue("@otpEnabled", otpEnabled);
            int result = cmd.ExecuteNonQuery();

            return result;
        }
    }
}