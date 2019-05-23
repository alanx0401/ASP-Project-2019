using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
namespace ITP213.DAL
{
    public class ResponseDAO
    {
        public static int insertAnnouncement(string announcementTitle, string announcementMessage)
        {
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            int result = 0;
            //StringBuilder strSql = new StringBuilder();

            //strSql.AppendLine("INSERT INTO announcement (annnouncementTitle, announcementMessage)");
            //strSql.AppendLine("VALUES (@aAnnouncementTitle', '@aAnnouncementMessage)");
            string strSql = "INSERT INTO announcement (announcementTitle, announcementMessage, adminNo) VALUES(@aAnnouncementTitle, @aAnnouncementMessage, @aAdminNo); ";

            // Announcement obj = new Announcement(); // create an announcement instance
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(strSql, myConn);

            cmd.Parameters.AddWithValue("@aAnnouncementTitle", announcementTitle);
            cmd.Parameters.AddWithValue("@aAnnouncementMessage", announcementMessage);
            cmd.Parameters.AddWithValue("@aAdminNo", "171846Z"); // hardcoding it becauuse login page is not crxted yet

            myConn.Open();
            result = cmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public static List<Announcement> getAllAnnouncement()
        {
            List<Announcement> resultList = new List<Announcement>();
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create Adapter
            string sqlStr = "SELECT announcementID, announcementTitle, announcementMessage FROM announcement";

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);

            // Fill dataset
            da.Fill(ds, "announcementTable");
            int rec_cnt = ds.Tables["announcementTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["announcementTable"].Rows)
                {
                    Announcement obj = new Announcement(); // create announcement instance
                    obj.announcementID = Convert.ToInt32(row["announcementID"]);
                    obj.announcementTitle = row["announcementTitle"].ToString();
                    obj.announcementMessage = row["announcementMessage"].ToString();

                    resultList.Add(obj);
                }
            }
            return resultList;
        }

        public static Announcement getAllAnnouncement1()
        {
            Announcement obj = new Announcement();   // create a customer instance
            // Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create Adapter
            string sqlStr = "SELECT announcementID, announcementTitle, announcementMessage FROM announcement";

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);

            // Fill dataset
            da.Fill(ds, "announcementTable");
            int rec_cnt = ds.Tables["announcementTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                //Announcement obj = new Announcement(); // create announcement instance
                DataRow row = ds.Tables["announcementTable"].Rows[0];
                obj.announcementID = Convert.ToInt32(row["announcementID"]);
                obj.announcementTitle = row["announcementTitle"].ToString();
                obj.announcementMessage = row["announcementMessage"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        public static Announcement getAnnouncementByAdminNo(int announcementID)
        {
            // Get connection string from web.config
            /*string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            // Create Adapter
            string sqlStr = "SELECT * FROM announcement where adminNo = @aAdminNo";

            Announcement obj = new Announcement(); // create announcement instance
            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("aAdminNo", adminNo);

            // fill dataset
            da.Fill(ds, "announcementTable");
            int rec_cnt = ds.Tables["announcementTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["announcementTable"].Rows[0];
                obj.announcementID = Convert.ToInt32(row["announcementID"]);
                obj.announcementTitle = row["announcementTitle"].ToString();
                obj.announcementMessage = row["announcementMessage"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }*/

            //===============================================================================
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            //WRITE SQL Statement to retrieve all columns from Customer by customer Id using query parameter
            string sqlStr = "SELECT * FROM announcement where announcementID = @aAnnouncementID";

            // change custId in where clause to custId1 or 
            // change connection string in web config to a wrong file name  

            Announcement obj = new Announcement();   // create a customer instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("aAnnouncementID", announcementID);
            // fill dataset
            da.Fill(ds, "announcementTable");
            int rec_cnt = ds.Tables["announcementTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["announcementTable"].Rows[0];  // Sql command returns only one record
                obj.announcementID = Convert.ToInt32(row["announcementID"]);
                obj.announcementMessage = row["announcementMessage"].ToString();
                obj.announcementTitle = row["announcementTitle"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public static int deleteById(int announcementID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            string sqlStr = "DELETE announcement WHERE announcementID = @pAnnouncementID";


            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("pAnnouncementID", announcementID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int updateById(int announcementID, string announcementTitle, string announcementMessage)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            /*UPDATE announcement SET 
            announcementTitle='Updated',announcementMessage='Updated'
            where announcementID=21;*/
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE announcement SET ");
            sqlStr.AppendLine("announcementTitle = @aAnnouncementTitle,");
            sqlStr.AppendLine("announcementMessage=@aAnnouncementMessage");
            sqlStr.AppendLine("WHERE announcementID=@aAnnouncementID");



            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("aAnnouncementID", announcementID);
            cmd.Parameters.AddWithValue("aAnnouncementTitle", announcementTitle);
            cmd.Parameters.AddWithValue("aAnnouncementMessage", announcementMessage);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}