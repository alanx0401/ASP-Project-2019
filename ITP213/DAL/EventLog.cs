using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;
namespace ITP213.DAL
{
    public class EventLog
    {
        string _conn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        private int _eventID;
        private string _eventDesc = "";
        private DateTime _dateTimeDetails; // this is another way to specify empty string
        private string _UUID = "";
        // Default constructor
        public EventLog()
        {
        }

        // Constructor that take in all data required to build a EventLogs object
        public EventLog(int eventID, string eventDesc, DateTime dateTimeDetails, string UUID)
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

        public List<EventLog> GetEvents()
        {
            List<EventLog> prodList = new List<EventLog>();
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
                EventLog obj = new EventLog(eventID, eventDesc, dateTimeDetails, UUID);
                prodList.Add(obj);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return prodList;
        }

        /*public int ProductInsert()
        {
            string msg = null;
            int result = 0;
            string queryStr = "INSERT INTO Products(Product_ID, Product_Name, Product_Desc, Unit_Price, Product_Image, Stock_Level)" +
            "values(@Product_ID, @Product_Name, @Product_Desc, @Unit_Price, @Product_Image, @Stock_Level)";
            try
            {
                SqlConnection conn = new SqlConnection(_connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@Product_ID", this.Product_ID);
                cmd.Parameters.AddWithValue("@Product_Name", this.Product_Name);
                cmd.Parameters.AddWithValue("@Product_Desc", this.Product_Desc);
                cmd.Parameters.AddWithValue("@Unit_Price", this.Unit_Price);
                cmd.Parameters.AddWithValue("@Product_Image", this.Product_Image);
                cmd.Parameters.AddWithValue("@Stock_Level", this.Stock_Level);
                conn.Open();
                result += cmd.ExecuteNonQuery();
                conn.Close();

                return result;
            }
            catch (SqlException ex)
            {
                return 0;
            }
        }*/
        //end Insert
    }
}