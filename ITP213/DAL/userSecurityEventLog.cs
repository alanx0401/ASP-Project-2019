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
    public class userSecurityEventLog
    {
        string _conn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        // Get/Set the attributes of the EventLogs object.
        public int eventID { get; set; }
        public string eventDesc { get; set; }
        public DateTime dateTimeDetails { get; set; }
        public string UUID { get; set; }

        //To display contents in GVEventLogs based on UUID 
        public List<userSecurityEventLog> GetSecurityEventLogsBYUUID(string UUID)
        {
            List<userSecurityEventLog> resultList = new List<userSecurityEventLog>();
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT eventDesc, dateTimeDetails FROM Eventlogs WHERE UUID = @UUID");
            //Create Adapter

            SqlConnection con = new SqlConnection(_conn);
            da = new SqlDataAdapter(sqlStr.ToString(), con);
            da.SelectCommand.Parameters.AddWithValue("@UUID", UUID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    userSecurityEventLog obj = new userSecurityEventLog();
                    obj.eventDesc = row["eventDesc"].ToString();
                    obj.dateTimeDetails = Convert.ToDateTime(row["dateTimeDetails"].ToString());
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }

        //To display contents in GVEventLogs based on start date, end date and UUID 
        public List<userSecurityEventLog> GetSecurityEventLogsByDate(DateTime startDate, DateTime endDate, string UUID)
        {
            List<userSecurityEventLog> resultList = new List<userSecurityEventLog>();
 
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT eventDesc,dateTimeDetails FROM EventLogs WHERE dateTimeDetails BETWEEN @startDate AND @endDate AND UUID = @UUID Order By dateTimeDetails DESC");
            //Create Adapter

            SqlConnection con = new SqlConnection(_conn);
            da = new SqlDataAdapter(sqlStr.ToString(), con);
            da.SelectCommand.Parameters.AddWithValue("@startDate", startDate);
            da.SelectCommand.Parameters.AddWithValue("@endDate", endDate);
            da.SelectCommand.Parameters.AddWithValue("@UUID", UUID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    userSecurityEventLog obj = new userSecurityEventLog();
                    obj.eventDesc = row["eventDesc"].ToString();
                    obj.dateTimeDetails = Convert.ToDateTime(row["dateTimeDetails"].ToString());
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }
    }

}