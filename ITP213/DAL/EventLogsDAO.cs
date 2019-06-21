using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
/// <summary>
/// Summary description for EventLogsDAO
/// </summary>
namespace ITP213.DAL
{
    public class EventLogsDAO
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        public List<EventLogs> GetAllEventLogs()
        {
            List<EventLogs> eventList = new List<EventLogs>();
            int eventLogID;
            string eventDesc,UUID;
            DateTime dateTimeEvent;
            string query = "SELECT * FROM EventLogs";
            SqlConnection conn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                eventLogID = int.Parse(dr["eventID"].ToString());
                eventDesc = dr["eventDesc"].ToString();
                dateTimeEvent = Convert.ToDateTime(dr["dateTimeEvent"].ToString());
                UUID = dr["UUID"].ToString();
                EventLogs e = new EventLogs(eventLogID,eventDesc,dateTimeEvent,UUID);
                eventList.Add(e);
            }
            conn.Close();
            dr.Close();
            return eventList;
        }
    }
}