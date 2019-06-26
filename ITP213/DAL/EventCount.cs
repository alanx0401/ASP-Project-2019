using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace ITP213.DAL
{
    public class EventCount
    {
        string _conn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        private string _UUID = "";
        private int _NoOfOccurance;

        // Default constructor
        public EventCount()
        {
        }

        public EventCount(string UUID, int NoOfOccurance)
        {
            _UUID = UUID;
            _NoOfOccurance = NoOfOccurance;
        }

        // Get/Set the attributes of the EventLogsCount object.
        public string UUID
        {
            get { return _UUID; }
            set { _UUID = value; }
        }

        public int NoOfOccurance
        {
            get { return _NoOfOccurance; }
            set { _NoOfOccurance = value; }
        }

        public List<EventCount> GetEventsCount(string eventD)
        {
            List<EventCount> eventLogCount = new List<EventCount>();
            string UUID;
            //int eventOccured;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SELECT COUNT(eventDesc) as EventCount, UUID");
            stringBuilder.AppendLine("FROM Eventlogs");
            stringBuilder.AppendLine("WHERE eventDesc=@eventDesc");
            stringBuilder.AppendLine("GROUP BY UUID;");
            SqlConnection conn = new SqlConnection(_conn);
            SqlCommand cmd = new SqlCommand(stringBuilder.ToString(), conn);
            cmd.Parameters.AddWithValue("@eventDesc", eventD);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UUID = dr["UUID"].ToString();
                NoOfOccurance = int.Parse(dr["EventCount"].ToString());
                EventCount obj = new EventCount(UUID, NoOfOccurance);
                eventLogCount.Add(obj);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return eventLogCount;
        }
    }
}