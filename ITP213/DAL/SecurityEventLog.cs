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
        private int _eventID;
        private string _eventDesc = "";
        private DateTime _dateTimeDetails; // this is another way to specify empty string
        private string _UUID = "";

        //Default Constructor
        public SecurityEventLog()
        {

        }

        // Constructor that take in all data required to build a EventLogs object
        public SecurityEventLog(int eventID, string eventDesc, DateTime dateTimeDetails, string UUID)
        {
            _eventID = eventID;
            _eventDesc = eventDesc;
            _dateTimeDetails = dateTimeDetails;
            _UUID = UUID;
        }

        // Get/Set the attributes of the EventLogs object.
        public int eventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }
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

        public string UUID
        {
            get { return _UUID; }
            set { _UUID = value; }
        }

        public List<SecurityEventLog> GetEvents()
        {
            List<SecurityEventLog> prodList = new List<SecurityEventLog>();
            int eventID;
            string eventDesc;
            DateTime dateTimeDetails;
            string UUID;
            string query = "SELECT * FROM EventLogs Order By eventID DESC";
            SqlConnection conn = new SqlConnection(_conn);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                eventID = int.Parse(dr["eventID"].ToString());
                eventDesc = dr["eventDesc"].ToString();
                dateTimeDetails = Convert.ToDateTime(dr["dateTimeDetails"].ToString());
                UUID = dr["UUID"].ToString();
                SecurityEventLog obj = new SecurityEventLog(eventID, eventDesc, dateTimeDetails, UUID);
                prodList.Add(obj);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return prodList;
        }

        //To display contents in GVEventLogs based on EventDesc 
        public List<SecurityEventLog> getEventDesc(string eventDesc)
        {
            List<SecurityEventLog> eventDescList = new List<SecurityEventLog>();
            int eventID;
            DateTime dateTimeDetails;
            string UUID;
            string queryStr = "SELECT * FROM Eventlogs WHERE eventDesc = @eventDesc";
            SqlConnection conn = new SqlConnection(_conn);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@eventDesc", eventDesc);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                eventID = int.Parse(dr["eventID"].ToString());
                dateTimeDetails = Convert.ToDateTime(dr["dateTimeDetails"].ToString());
                UUID = dr["UUID"].ToString();
                SecurityEventLog eventDescObj = new SecurityEventLog(eventID, eventDesc, dateTimeDetails, UUID);
                eventDescList.Add(eventDescObj);
            }
            else
            {
                eventDescList = null;
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return eventDescList;
        }

        //To display contents in GVEventLogs based on start date an end date 
        public List<SecurityEventLog> searchEventLogDate(DateTime startDate, DateTime endDate)
        {
            List<SecurityEventLog> resultList = new List<SecurityEventLog>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * FROM EventLogs WHERE dateTimeDetails BETWEEN @startDate AND @endDate Order By dateTimeDetails DESC");
            //Create Adapter

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
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