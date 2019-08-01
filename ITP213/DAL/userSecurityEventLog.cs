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
        string _eventDesc = null;
        DateTime _dateTimeDetails;

        public string eventDesc
        {
            get { return _eventDesc; }
            set { _eventDesc = value; }
        }
        public DateTime dateTimeDetails
        {
            get { return _dateTimeDetails; }
            set { _dateTimeDetails = value; }
        }

        public userSecurityEventLog()
        {

        }
        public userSecurityEventLog(string eventDesc, DateTime dateTimeDetails)
        {
            _eventDesc = eventDesc;
            _dateTimeDetails = dateTimeDetails;
        }
        //To display contents in GVEventLogs based on EventDesc 
        public List<userSecurityEventLog> getEventDesc(string UUID)
        {
            List<userSecurityEventLog> resultList = new List<userSecurityEventLog>();

            //string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT eventDesc, dateTimeDetails FROM Eventlogs WHERE UUID = @UUID");
            //Create Adapter

            SqlConnection myConn = new SqlConnection(_conn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
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
        /*public List<userSecurityEventLog> searchEventLogDate(DateTime startDate, DateTime endDate, string UUID)
        {
            List<userSecurityEventLog> resultList = new List<userSecurityEventLog>();
            //Get connection string from web.config
            //string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT eventDesc,dateTimeDetails FROM EventLogs WHERE dateTimeDetails BETWEEN @startDate AND @endDate AND UUID = @UUID Order By dateTimeDetails DESC");
            //Create Adapter

            SqlConnection myConn = new SqlConnection(_conn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
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
        }*/

    }
}