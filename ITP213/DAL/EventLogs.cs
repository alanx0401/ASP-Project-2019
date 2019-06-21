using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ITP213.DAL
{
    public class EventLogs
    {
        //Default Constructor 
        public EventLogs()
        {

        }

        public EventLogs(int eventID, string eventDesc, DateTime dateTimeDetail, string UUID)
        {
            _eventID = eventID;
            _eventDesc = eventDesc;
            _dateTimeDetail = dateTimeDetail;
            _UUID = UUID;
        }

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
        public DateTime dateTimeDetail
        {
            get { return _dateTimeDetail; }
            set { _dateTimeDetail = value; }
        }
        public string UUID
        {
            get { return _UUID; }
            set { _UUID = value; }
        }

        public List<EventLogs> GetEventLogs()
        {
            List<EventLogs> eventLogList = new List<EventLogs>();
            string eventDesc, UUID;
            DateTime dateTimeDetail;
            int eventID;
            string query = "SELECT * FROM EventLogs";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                eventID = int.Parse(dr["eventID"].ToString());
                eventDesc = dr["eventDesc"].ToString();
                dateTimeDetail = Convert.ToDateTime(dr["dateTimeDetails"].ToString());
                UUID = dr["UUID"].ToString();

                EventLogs eventLogs = new EventLogs(eventID, eventDesc, dateTimeDetail, UUID);
                eventLogList.Add(eventLogs);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return eventLogList;
        }

    }

}