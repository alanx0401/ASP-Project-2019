using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ITP213.DAL
{
    public class WithdrawalRequestDAO
    {
        // StudentWithdrawalRequest.aspx
        public static List<WithdrawalRequest> displayAllocatedTrips(string adminNo)
        {
            List<WithdrawalRequest> resultList = new List<WithdrawalRequest>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT overseasTrip.tripID, country, tripName, CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType");
            sqlStr.AppendLine("FROM overseasTrip ");
            sqlStr.AppendLine("INNER JOIN overseasEnrolledStudent on overseasTrip.tripID = overseasEnrolledStudent.tripID");
            sqlStr.AppendLine("WHERE adminNo=@adminNo;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("adminNo", adminNo);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    WithdrawalRequest obj = new WithdrawalRequest();
                    obj.tripID = Convert.ToInt32(row["tripID"]);
                    obj.tripName = row["tripName"].ToString();
                    //obj.tripType = row["tripType"].ToString();
                    obj.country = row["country"].ToString();
                    obj.tripNameAndTripType = row["tripNameAndTripType"].ToString();
                    resultList.Add(obj);

                }
            }

            return resultList;

        }

        // for studentWithdrawalRequest.aspx
        public static List<WithdrawalRequest> displayAllocatedPendingTrips(string adminNo)
        {
            List<WithdrawalRequest> resultList = new List<WithdrawalRequest>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT overseasTrip.tripID, tripName, CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType");
            sqlStr.AppendLine("FROM overseasTrip ");
            sqlStr.AppendLine("INNER JOIN overseasEnrolledStudent on overseasTrip.tripID = overseasEnrolledStudent.tripID");
            sqlStr.AppendLine("WHERE adminNo=@adminNo and overseasTripStatus='PENDING';");

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("adminNo", adminNo);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    WithdrawalRequest obj = new WithdrawalRequest();
                    obj.tripID = Convert.ToInt32(row["tripID"]);
                    obj.tripName = row["tripName"].ToString();
                    //obj.tripType = row["tripType"].ToString();
                    obj.tripNameAndTripType = row["tripNameAndTripType"].ToString();
                    resultList.Add(obj);

                }
            }

            return resultList;

        }

        public static int insert(string withdrawalReason, string adminNo, int tripID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;


            /*
             INSERT INTO withdrawTripRequest(withdrawalTripRequestStatus, withdrawalReason, adminNo, tripID, createdOn) VALUES('ONGOING', 'Testing 1 2 3','171846Z',28, GETDATE());
             */
            string sqlStr = "INSERT INTO withdrawTripRequest(withdrawalTripRequestStatus, withdrawalReason, adminNo, tripID, createdOn) VALUES('PENDING', @withdrawalReason,@adminNo,@tripID, GETDATE());";

            WithdrawalRequest obj = new WithdrawalRequest();
            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("withdrawalReason", withdrawalReason);
            cmd.Parameters.AddWithValue("adminNo", adminNo);
            cmd.Parameters.AddWithValue("tripID", Convert.ToInt32(tripID));

            int result = cmd.ExecuteNonQuery();
            return result;
        }

        // LecturerWithdrawalRequest.aspx
        public static List<WithdrawalRequest> displayWithdrawalRequest(string staffID)
        {
            List<WithdrawalRequest> resultList = new List<WithdrawalRequest>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            Select overseasTrip.tripID, withdrawTripRequestID, adminNo, departureDate, arrivalDate ,withdrawalReason, CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType  FROM withdrawTripRequest INNER JOIN overseasEnrolledLecturer ON withdrawTripRequest.tripID = overseasEnrolledLecturer.tripID INNER JOIN overseasTrip ON withdrawTripRequest.tripID = overseasTrip.tripID WHERE staffID='susie_waters';
             */
            string sqlStr = "Select withdrawalTripRequestStatus, createdOn, overseasTrip.tripID, withdrawTripRequestID, adminNo, departureDate, arrivalDate ,withdrawalReason, CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType  FROM withdrawTripRequest INNER JOIN overseasEnrolledLecturer ON withdrawTripRequest.tripID = overseasEnrolledLecturer.tripID INNER JOIN overseasTrip ON withdrawTripRequest.tripID = overseasTrip.tripID WHERE staffID=@staffID ORDER BY createdOn DESC;";

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("staffID", staffID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    WithdrawalRequest obj = new WithdrawalRequest();
                    obj.tripID = Convert.ToInt32(row["tripID"]);
                    obj.withdrawTripRequestID = Convert.ToInt32(row["withdrawTripRequestID"]);
                    obj.adminNo = row["adminNo"].ToString();
                    obj.departureDate = row["departureDate"].ToString();
                    obj.arrivalDate = row["arrivalDate"].ToString();
                    obj.withdrawalReason = row["withdrawalReason"].ToString();
                    obj.tripNameAndTripType = row["tripNameAndTripType"].ToString();
                    obj.createdOn = row["createdOn"].ToString();
                    obj.withdrawalTripRequestStatus = row["withdrawalTripRequestStatus"].ToString();
                    resultList.Add(obj);

                }
            }
            else
            {
                resultList = null;
            }

            return resultList;

        }

        // StudentWithdrawalResult.aspx
        public static List<WithdrawalRequest> displayStudentWithdrawalRequest(string adminNo)
        {
            List<WithdrawalRequest> resultList = new List<WithdrawalRequest>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            Select createdOn, withdrawTripRequest.tripID, withdrawTripRequestID, withdrawalTripRequestStatus, departureDate, arrivalDate ,withdrawalReason, CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType  
            FROM withdrawTripRequest 
            INNER JOIN overseasTrip ON withdrawTripRequest.tripID = overseasTrip.tripID 
            WHERE adminNo='171846z';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Select createdOn, withdrawTripRequest.tripID, withdrawTripRequestID, withdrawalTripRequestStatus, departureDate, arrivalDate ,withdrawalReason, CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType");
            sqlStr.AppendLine("FROM withdrawTripRequest");
            sqlStr.AppendLine("INNER JOIN overseasTrip ON withdrawTripRequest.tripID = overseasTrip.tripID ");
            sqlStr.AppendLine("WHERE adminNo=@adminNo ORDER BY createdOn DESC;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("adminNo", adminNo);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    WithdrawalRequest obj = new WithdrawalRequest();
                    obj.tripID = Convert.ToInt32(row["tripID"]);
                    obj.withdrawTripRequestID = Convert.ToInt32(row["withdrawTripRequestID"]);
                    obj.departureDate = row["departureDate"].ToString();
                    obj.arrivalDate = row["arrivalDate"].ToString();
                    obj.withdrawalReason = row["withdrawalReason"].ToString();
                    obj.tripNameAndTripType = row["tripNameAndTripType"].ToString();
                    obj.createdOn = row["createdOn"].ToString();
                    obj.withdrawalTripRequestStatus = row["withdrawalTripRequestStatus"].ToString();
                    resultList.Add(obj);

                }
            }
            else
            {
                resultList = null;
            }

            return resultList;

        }

        // ViewAnnouncement.aspx
        public static int approveTripRequestByWithdrawTripRequestID(int withdrawTripRequestID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /*
            UPDATE WITHDRAWTRIPREQUEST
            SET withdrawalTripRequestStatus = 'Approved'
            WHERE withdrawTripRequestID = 12;
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE WITHDRAWTRIPREQUEST SET ");
            sqlStr.AppendLine("withdrawalTripRequestStatus = 'Approved'");
            sqlStr.AppendLine("WHERE withdrawTripRequestID = @withdrawTripRequestID;");



            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("withdrawTripRequestID", withdrawTripRequestID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int rejectTripRequestByWithdrawTripRequestID(int withdrawTripRequestID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /*
            UPDATE WITHDRAWTRIPREQUEST
            SET withdrawalTripRequestStatus = 'Approved'
            WHERE withdrawTripRequestID = 12;
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("UPDATE WITHDRAWTRIPREQUEST SET ");
            sqlStr.AppendLine("withdrawalTripRequestStatus = 'Rejected'");
            sqlStr.AppendLine("WHERE withdrawTripRequestID = @withdrawTripRequestID;");



            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("withdrawTripRequestID", withdrawTripRequestID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}