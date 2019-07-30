using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;

namespace ITP213.DAL
{
    public class userEventLog
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


        public userEventLog()
        {

        }

        public userEventLog(string eventDesc, DateTime dateTimeDetails)
        {
            _eventDesc = eventDesc;
            _dateTimeDetails = dateTimeDetails;
        }

        public List<userEventLog> getIndividualUserLog(string UUID)
        {
            List<userEventLog> prodList = new List<userEventLog>();
            string eventDesc;
            DateTime dateTimeDetails;
            string query = "SELECT eventDesc, dateTimeDetails FROM EventLogs Where UUID =@UUID Order By eventID DESC";
            SqlConnection conn = new SqlConnection(_conn);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UUID", UUID);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                eventDesc = dr["eventID"].ToString();
                dateTimeDetails = Convert.ToDateTime(dr["dateTimeDetails"].ToString());
                userEventLog obj = new userEventLog(eventDesc, dateTimeDetails);
                prodList.Add(obj);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return prodList;
        }

    }
}