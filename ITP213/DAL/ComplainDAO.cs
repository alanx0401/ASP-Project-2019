using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ITP213.DAL
{
    public class ComplainDAO
    {
        public static int insertComplain(string subject, string comments, string studentName, string adminNo, string trip,string image,string complainType)
        {
            //Get connection string from webvconfig
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            string sqlstr =
                "INSERT INTO complain(subject,comments,studentName,adminNo,trip,image,complainType,complainStatus)VALUES(@subject,@comments,@studentName,@adminNo,@trip,@image,@complainType,'true')";
            /*INSERT INTO studentBlog(title,content,studentName,adminNo,country, blogtime)VALUES('Hi','Hi','hi','171846z','SG', GETDATE())*/

            Complain obj = new Complain();

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, myConn);
            cmd.Parameters.AddWithValue("subject", subject);
            cmd.Parameters.AddWithValue("comments", comments);
            cmd.Parameters.AddWithValue("studentName", studentName);
            cmd.Parameters.AddWithValue("adminNo", adminNo);
            cmd.Parameters.AddWithValue("trip", trip);
            cmd.Parameters.AddWithValue("image", image);
            cmd.Parameters.AddWithValue("complainType", complainType);

            int result = cmd.ExecuteNonQuery();
            return result;

        }
        public static int updateByComplainID(int complainID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            StringBuilder sqlStr = new StringBuilder();
            /*
             
             */
            sqlStr.AppendLine("UPDATE complain SET ");
            sqlStr.AppendLine("complainStatus = 'false'");
            sqlStr.AppendLine("WHERE complainID = @complainID");



            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("complainID", complainID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}