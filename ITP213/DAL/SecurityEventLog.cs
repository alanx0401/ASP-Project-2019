using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;


namespace ITP213.DAL
{
    public class SecurityEventLog
    {
        string _conn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        
        // Get/Set the attributes of the EventLogs object.
        public int eventID { get; set; }
        public string eventDesc { get; set; }
        public DateTime dateTimeDetails { get; set; }
        public string UUID { get; set; }
        public string accountName { get; set; }

        public List<SecurityEventLog> GetSecurityEventLogs()
        {
            List<SecurityEventLog> resultList = new List<SecurityEventLog>();
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * FROM Eventlogs");

            SqlConnection con = new SqlConnection(_conn);
            da = new SqlDataAdapter(sqlStr.ToString(), con);

            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    SecurityEventLog obj = new SecurityEventLog();

                    obj.eventID = Convert.ToInt32(row["eventID"].ToString());
                    obj.dateTimeDetails = Convert.ToDateTime(row["dateTimeDetails"].ToString());
                    obj.eventDesc = row["eventDesc"].ToString();
                    obj.UUID = row["UUID"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }

        /*public List<SecurityEventLog> GetSecurityEventLogsbyUsername()
        {
            List<SecurityEventLog> resultList = new List<SecurityEventLog>();
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT eventID,eventDesc,dateTimeDetails, account.name FROM Eventlogs INNER JOIN account on account.UUID = EventLogs.UUID");

            SqlConnection con = new SqlConnection(_conn);
            da = new SqlDataAdapter(sqlStr.ToString(), con);

            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    SecurityEventLog obj = new SecurityEventLog();

                    obj.eventID = Convert.ToInt32(row["eventID"].ToString());
                    obj.dateTimeDetails = Convert.ToDateTime(row["dateTimeDetails"].ToString());
                    obj.eventDesc = row["eventDesc"].ToString();
                    obj.accountName = row["account.name"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }*/

        //To display contents in GVEventLogs based on EventDesc 
        public List<SecurityEventLog> GetParticularEvents(string eventDesc)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT eventID, eventDesc, dateTimeDetails, account.name As Users  FROM Eventlogs Inner Join account on account.UUID = EventLogs.UUID WHERE eventDesc = @eventDesc");
       
            List<SecurityEventLog> resultList = new List<SecurityEventLog>();

            SqlConnection con = new SqlConnection(_conn);
            da = new SqlDataAdapter(sqlCommand.ToString(), con);
            da.SelectCommand.Parameters.AddWithValue("@eventDesc", eventDesc);
            
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
              if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    SecurityEventLog obj = new SecurityEventLog();
                    obj.eventID = Convert.ToInt32(row["eventID"].ToString());
                    obj.dateTimeDetails = Convert.ToDateTime(row["dateTimeDetails"].ToString());
                    obj.eventDesc = row["eventDesc"].ToString();
                    obj.accountName = row["Users"].ToString();
                    //obj.UUID = row["UUID"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }

        //To display audit logs in GVEventLogs based on Admin Account Type 
        public List<SecurityEventLog> auditLog(string accountType)
        {
            List<SecurityEventLog> resultList = new List<SecurityEventLog>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT eventID, eventDesc, dateTimeDetails, Eventlogs.UUID FROM Eventlogs INNER JOIN account on account.UUID = Eventlogs.UUID WHERE accountType=@accountType");
            //Create Adapter

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@accountType", accountType);
            da.SelectCommand.Parameters.AddWithValue("@dateTimeDetails", DateTime.Today);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    SecurityEventLog obj = new SecurityEventLog();

                    //obj.eventID = Convert.ToInt32(row["eventID"].ToString());
                    obj.dateTimeDetails = Convert.ToDateTime(row["dateTimeDetails"].ToString());
                    obj.eventDesc = row["eventDesc"].ToString();
                    //obj.UUID = row["UUID"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }

        //To display contents in GVEventLogs based on start date an end date 
        public List<SecurityEventLog> searchEventLogDate(DateTime startDate, DateTime endDate)
        {
            List<SecurityEventLog> resultList = new List<SecurityEventLog>();

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            //sqlStr.AppendLine("SELECT eventDesc, dateTimeDetails FROM EventLogs WHERE dateTimeDetails >= @startDate AND dateTimeDetails < @endDate ORDER BY dateTimeDetails DESC");
            sqlStr.AppendLine("SELECT eventID, eventDesc, dateTimeDetails FROM EventLogs WHERE dateTimeDetails BETWEEN @startDate AND @endDate ORDER BY dateTimeDetails DESC");
            //Create Adapter

            SqlConnection con = new SqlConnection(_conn);
            da = new SqlDataAdapter(sqlStr.ToString(), con);
            da.SelectCommand.Parameters.AddWithValue("@startDate", startDate);
            da.SelectCommand.Parameters.AddWithValue("@endDate", endDate);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    SecurityEventLog obj = new SecurityEventLog();
                    obj.eventID = Convert.ToInt32(row["eventID"].ToString());
                    obj.dateTimeDetails = Convert.ToDateTime(row["dateTimeDetails"].ToString());
                    obj.eventDesc = row["eventDesc"].ToString();
                    //obj.UUID = row["UUID"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }

        // For Wycliff's Password Reset and Pei Shan's Login
        public int EventInsert(string eventDesc, DateTime dateTimeDetails, string UUID)
        {

            int result = 0;
            string queryStr = "INSERT INTO EventLogs(eventDesc, dateTimeDetails, UUID)" +
            "values(@eventDesc, @dateTimeDetails, @UUID)";
            try
            {
                SqlConnection conn = new SqlConnection(_conn);
                SqlCommand cmd = new SqlCommand(queryStr, conn);

                cmd.Parameters.AddWithValue("@eventDesc", eventDesc);
                cmd.Parameters.AddWithValue("@dateTimeDetails", dateTimeDetails);
                cmd.Parameters.AddWithValue("@UUID", UUID);
                conn.Open();
                result += cmd.ExecuteNonQuery();
                conn.Close();

                return result;
            }
            catch (SqlException ex)
            {
                return 0;
            }
        }
        //end Insert

    }
}