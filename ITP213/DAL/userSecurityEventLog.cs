using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;

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
            List<userSecurityEventLog> eventDescList = new List<userSecurityEventLog>();
            string queryStr = "SELECT eventDesc, dateTimeDetails FROM Eventlogs WHERE UUID = @UUID";
            SqlConnection conn = new SqlConnection(_conn);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                eventDesc = dr["eventDesc"].ToString();
                dateTimeDetails = Convert.ToDateTime(dr["dateTimeDetails"].ToString());
                userSecurityEventLog eventDescObj = new userSecurityEventLog(eventDesc, dateTimeDetails);
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
    }
}