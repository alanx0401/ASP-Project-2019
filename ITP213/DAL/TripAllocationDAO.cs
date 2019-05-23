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
    public class TripAllocationDAO
    {
        // TripAllocation.aspx
        public static List<TripAllocation> getStudentName(string course)
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            SELECT adminNo, name
            FROM student
            INNER JOIN account on student.accountID = account.accountID
            WHERE course='C85'
             */
            string sqlStr = "SELECT * FROM student INNER JOIN account on student.accountID = account.accountID WHERE course=@sCourse";

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("sCourse", course);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.name = row["name"].ToString();
                    obj.adminNo = row["adminNo"].ToString();
                    resultList.Add(obj);

                }
            }

            return resultList;

        }

        public static List<TripAllocation> getLecturerName(string school, string staffID)
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            SELECT * FROM lecturer INNER JOIN account on lecturer.accountID = account.accountID WHERE school='SIT';
             */
            string sqlStr = "SELECT * FROM lecturer INNER JOIN account on lecturer.accountID = account.accountID WHERE school=@lSchool and staffID!=@staffID;";

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("lSchool", school);
            da.SelectCommand.Parameters.AddWithValue("staffID", staffID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.name = row["name"].ToString();
                    obj.staffID = row["staffID"].ToString();
                    resultList.Add(obj);

                }
            }

            return resultList;

        }

        public static int insertTrip(int tripCost,string arrivalDate, string departureDate, int noOfStudents, int noOfLecturers, string tripType, string tripName, string country, string companyName)
          {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /*
            INSERT INTO overseasTrip (overseasTripStatus, arrivalDate, departureDate, noOfStudents, noOfLecturers, tripType, tripName, country, adminNo, staffID)
            VALUES ('ONGOING', '10/29/2018', '10/30/2018', 20, 4, 'Immersion Trip', '2018 - Japan Immersion Trip' ,'Japan', '171846Z', 'johnny_appleseed')
             */

            string sqlStr =
                "INSERT INTO overseasTrip (overseasTripStatus,tripCost, arrivalDate, departureDate, noOfStudents, noOfLecturers, tripType, tripName, country, companyName) " +
                "VALUES('PENDING',@tripCost, @oArrivalDate, @oDepartureDate, @oNoOfStudents, @oNoOfLecturers, @oTripType, @oTripName, @oCountry, @companyName)";


            TripAllocation obj = new TripAllocation();   // create a tripAllocation instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            // DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
            cmd.Parameters.AddWithValue("tripCost", Convert.ToInt32(tripCost));
            cmd.Parameters.AddWithValue("oArrivalDate", DateTime.ParseExact(arrivalDate,"MM/dd/yyyy", null));
            cmd.Parameters.AddWithValue("oDepartureDate", DateTime.ParseExact(departureDate, "MM/dd/yyyy", null));
            cmd.Parameters.AddWithValue("oNoOfStudents", Convert.ToInt32(noOfStudents));
            cmd.Parameters.AddWithValue("oNoOfLecturers", Convert.ToInt32(noOfLecturers));
            cmd.Parameters.AddWithValue("oTripType", tripType);
            cmd.Parameters.AddWithValue("oTripName", tripName);
            cmd.Parameters.AddWithValue("oCountry", country);
            cmd.Parameters.AddWithValue("companyName", companyName);

            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static TripAllocation getTripIDByTripNameDepartureDateAndArrivalDate(string tripName, string departureDate, string arrivalDate, string studyTrip)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            //WRITE SQL Statement to retrieve tripID
            /*
             SELECT * FROM overseasTrip
             WHERE tripName='Jap Trip' and departureDate='2018-12-29' and arrivalDate='2018-12-31' and tripType='Study Trip';
             */
            string sqlStr = "SELECT * FROM overseasTrip WHERE tripName = @oTripName and departureDate = @oDepartureDate and arrivalDate = @oArrivalDate and tripType = @oStudyTrip";

            TripAllocation obj = new TripAllocation();   // create a tripAllocation instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("oTripName", tripName);
            da.SelectCommand.Parameters.AddWithValue("oDepartureDate", DateTime.ParseExact(departureDate, "MM/dd/yyyy", null));
            da.SelectCommand.Parameters.AddWithValue("oArrivalDate", DateTime.ParseExact(arrivalDate, "MM/dd/yyyy", null));
            da.SelectCommand.Parameters.AddWithValue("oStudyTrip", studyTrip);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["resultTable"].Rows[0];  // Sql command returns only one record
                obj.tripID = Convert.ToInt32(row["tripID"]);
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public static int insertEnrolledStudent(int tripID, string adminID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            //sql: INSERT INTO overseasEnrolledStudent Values(23,'171846Z');
            string sqlStr = "INSERT INTO overseasEnrolledStudent Values(@oTripID, @oAdminNo)";

            TripAllocation obj = new TripAllocation();

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("oTripID", tripID);
            cmd.Parameters.AddWithValue("oAdminNo", adminID);

            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int insertEnrolledLecturer(int tripID, string staffID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            //sql: INSERT INTO overseasEnrolledStudent Values(23,'171846Z');
            string sqlStr = "INSERT INTO overseasEnrolledLecturer Values(@oTripID, @ostaffID)";

            TripAllocation obj = new TripAllocation();

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("oTripID", tripID);
            cmd.Parameters.AddWithValue("ostaffID", staffID);

            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static TripAllocation getTripByTripID(int tripID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            SELECT * FROM overseasTrip INNER JOIN overseasEnrolledStudent on overseasTrip.tripID = overseasEnrolledStudent.tripID WHERE overseasTrip.tripID = @tripID; SELECT staffID FROM overseasTrip INNER JOIN overseasEnrolledLecturer on overseasTrip.tripID = overseasEnrolledLecturer.tripID WHERE overseasTrip.tripID = @tripID;
             */
            string sqlStr = "SELECT * FROM overseasTrip WHERE overseasTrip.tripID = @tripID;";


            TripAllocation obj = new TripAllocation();   

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("tripID", tripID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["resultTable"].Rows[0];  // Sql command returns only one record
                obj.departureDate = row["departureDate"].ToString();
                obj.arrivalDate = row["arrivalDate"].ToString();
                obj.tripType = row["tripType"].ToString();
                obj.tripName = row["tripName"].ToString();
                obj.country = row["country"].ToString();
                obj.tripCost = row["tripCost"].ToString();
                obj.companyName = row["companyName"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public static List<TripAllocation> getStudentNameByTripID(int tripID)
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            SELECT * FROM overseasEnrolledStudent INNER JOIN student on student.adminNo = overseasEnrolledStudent.adminNo INNER JOIN account on account.accountID = student.accountID where tripID=30
             */
            string sqlStr = "SELECT * FROM overseasEnrolledStudent INNER JOIN student on student.adminNo = overseasEnrolledStudent.adminNo INNER JOIN account on account.accountID = student.accountID where tripID = @tripID";

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("tripID", tripID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.name = row["name"].ToString();
                    obj.adminNo = row["adminNo"].ToString();
                    resultList.Add(obj);

                }
            }

            return resultList;

        }

        public static List<TripAllocation> getLecturerNameByTripID(int tripID, string staffID)
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            //SELECT * FROM overseasEnrolledLecturer INNER JOIN lecturer on lecturer.staffID = overseasEnrolledLecturer.staffID INNER JOIN account on account.accountID = lecturer.accountID where tripID=30;
             */
            string sqlStr = "SELECT * FROM overseasEnrolledLecturer INNER JOIN lecturer on lecturer.staffID = overseasEnrolledLecturer.staffID INNER JOIN account on account.accountID = lecturer.accountID where tripID=@tripID and overseasEnrolledLecturer.staffID!=@staffID;";

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("tripID", tripID);
            da.SelectCommand.Parameters.AddWithValue("staffID", staffID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.name = row["name"].ToString();
                    obj.staffID = row["staffID"].ToString();
                    resultList.Add(obj);

                }
            }

            return resultList;

        }
        //===============================================================================================
        // ViewAllocatedTrip.aspx : Study Trip & Immersion Trip & Internship
        // Do not display trips that approved withdrawTripRequest
        public static List<TripAllocation> displayTripsBasedOnAdminNo(string adminNo, string tripType) // goal: wants to display tripName & tripID & before arrivalDate
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            select * from overseasTrip
            INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID
            where 
            STR(overseasTrip.tripID)+'.'+'171846Z' 
            not in (select STR(tripID)+'.'+adminNo from withdrawTripRequest where withdrawalTripRequestStatus='Approved')
            and adminNo='171846Z'
            and tripType='Study Trip'
            and arrivalDate>=GETDATE()
            ORDER BY arrivalDate DESC;
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("select * from overseasTrip");
            sqlStr.AppendLine("INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID");
            sqlStr.AppendLine("where");
            sqlStr.AppendLine("STR(overseasTrip.tripID)+'.'+ @sAdminNo");
            sqlStr.AppendLine("not in (select STR(tripID)+'.'+adminNo from withdrawTripRequest where withdrawalTripRequestStatus='Approved')");
            sqlStr.AppendLine("and adminNo=@sAdminNo");
            sqlStr.AppendLine("and tripType=@tripType");
            sqlStr.AppendLine("and overseasTripStatus!='ENDED'");
            sqlStr.AppendLine("ORDER BY arrivalDate DESC;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("sAdminNo", adminNo);
            da.SelectCommand.Parameters.AddWithValue("tripType", tripType);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.tripID = Convert.ToInt32(row["tripID"].ToString());
                    obj.tripName = row["tripName"].ToString();
                    obj.tripType = row["tripType"].ToString();
                    obj.overseasTripStatus = row["overseasTripStatus"].ToString();
                    obj.departureDate = row["departureDate"].ToString();
                    obj.arrivalDate = row["arrivalDate"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;

        }

        public static List<TripAllocation> displayPastTripsBasedOnAdminNo(string adminNo, string tripType) // goal: wants to display tripName & tripID & after arrivalDate
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*
            Select * From overseasTrip
            INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID 
            WHERE adminNo = '171846z and tripType='Immersion Trip and arrivalDate<=GETDATE();'';
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Select * From overseasTrip");
            sqlStr.AppendLine("INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID");
            sqlStr.AppendLine("WHERE adminNo = @sAdminNo and tripType=@tripType and overseasTripStatus='ENDED'");
            sqlStr.AppendLine("ORDER BY arrivalDate DESC;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("sAdminNo", adminNo);
            da.SelectCommand.Parameters.AddWithValue("tripType", tripType);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.tripID = Convert.ToInt32(row["tripID"].ToString());
                    obj.tripName = row["tripName"].ToString();
                    obj.tripType = row["tripType"].ToString();
                    obj.overseasTripStatus = row["overseasTripStatus"].ToString();
                    obj.departureDate = row["departureDate"].ToString();
                    obj.arrivalDate = row["arrivalDate"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;

        }

        public static List<TripAllocation> displayTripsBasedOnStaffID(string staffID, string tripType) // goal: wants to display tripName & tripID & beforeArrivalDate
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            /*Select * From overseasTrip INNER JOIN overseasEnrolledLecturer ON overseasTrip.tripID = overseasEnrolledLecturer.tripID WHERE staffID='morris_b';*/
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Select * From overseasTrip");
            sqlStr.AppendLine("INNER JOIN overseasEnrolledLecturer ON overseasTrip.tripID = overseasEnrolledLecturer.tripID");
            sqlStr.AppendLine("WHERE staffID=@lStaffID and tripType=@tripType and overseasTripStatus!='ENDED'");
            sqlStr.AppendLine("ORDER BY arrivalDate DESC;");

            /*string sqlStr = "Select * From overseasTrip 
             * INNER JOIN overseasEnrolledLecturer ON overseasTrip.tripID = overseasEnrolledLecturer.tripID 
             * WHERE staffID=@lStaffID and tripType='Study Trip' and arrivalDate>=GETDATE();";*/

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("lStaffID", staffID);
            da.SelectCommand.Parameters.AddWithValue("tripType", tripType);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.tripID = Convert.ToInt32(row["tripID"].ToString());
                    obj.tripName = row["tripName"].ToString();
                    obj.tripType = row["tripType"].ToString();
                    obj.overseasTripStatus = row["overseasTripStatus"].ToString();
                    obj.departureDate = row["departureDate"].ToString();
                    obj.arrivalDate = row["arrivalDate"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }

        public static List<TripAllocation> displayPastTripsBasedOnStaffID(string staffID, string tripType) // goal: wants to display tripName & tripID & afterArrivalDate
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Select * From overseasTrip");
            sqlStr.AppendLine("INNER JOIN overseasEnrolledLecturer ON overseasTrip.tripID = overseasEnrolledLecturer.tripID");
            sqlStr.AppendLine("WHERE staffID=@lStaffID and tripType=@tripType and overseasTripStatus='ENDED'");
            sqlStr.AppendLine("ORDER BY arrivalDate DESC;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("lStaffID", staffID);
            da.SelectCommand.Parameters.AddWithValue("tripType", tripType);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.tripID = Convert.ToInt32(row["tripID"].ToString());
                    obj.tripName = row["tripName"].ToString();
                    obj.tripType = row["tripType"].ToString();
                    obj.overseasTripStatus = row["overseasTripStatus"].ToString();
                    obj.departureDate = row["departureDate"].ToString();
                    obj.arrivalDate = row["arrivalDate"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }
        //==========================================================================================================

        /*
        DELETE FROM withdrawTripRequest WHERE tripID = 25;
        DELETE FROM overseasEnrolledLecturer where tripID = 25;
        DELETE FROM overseasEnrolledStudent where tripID = 25;
        DELETE FROM overseasTrip WHERE tripID = 25;
        */
        public static int deleteById(int tripID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            StringBuilder sqlStr = new StringBuilder();

            sqlStr.AppendLine("DELETE FROM withdrawTripRequest WHERE tripID = @tripID;");
            sqlStr.AppendLine("DELETE FROM overseasEnrolledLecturer where tripID = @tripID;");
            sqlStr.AppendLine("DELETE FROM overseasEnrolledStudent where tripID = @tripID;");
            sqlStr.AppendLine("DELETE FROM announcement where tripID=@tripID;");
            sqlStr.AppendLine("DELETE FROM injuryReport WHERE tripID = @tripID;");
            sqlStr.AppendLine("DELETE FROM announcement WHERE tripID = @tripID");

            // delete this last
            sqlStr.AppendLine("DELETE FROM overseasTrip WHERE tripID = @tripID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("tripID", tripID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int updateTripByTripID(int tripID, string departureDate, string arrivalDate, int noOfStudents, int noOfLecturers, string tripType, string tripName, string country)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            /*
            Update overseasTrip
            Set departureDate=GETDATE(), 
            arrivalDate=GETDATE(),
            noOfStudents=1, 
            noOfLecturers=2, 
            tripType='Study Trip', 
            tripName='Testing', 
            country='Japan'
            WHERE tripID=37;
             */
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Update overseasTrip ");
            sqlStr.AppendLine("Set departureDate=@departureDate, ");
            sqlStr.AppendLine("arrivalDate=@arrivalDate,");
            sqlStr.AppendLine("noOfStudents=@noOfStudents, ");
            sqlStr.AppendLine("noOfLecturers=@noOfLecturers,");
            sqlStr.AppendLine("tripType=@tripType,");
            sqlStr.AppendLine("tripName=@tripName, ");
            sqlStr.AppendLine("country=@country");
            sqlStr.AppendLine("WHERE tripID=@tripID;");

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
            cmd.Parameters.AddWithValue("departureDate", DateTime.ParseExact(departureDate, "MM/dd/yyyy", null));
            cmd.Parameters.AddWithValue("arrivalDate", DateTime.ParseExact(arrivalDate, "MM/dd/yyyy", null));
            cmd.Parameters.AddWithValue("noOfStudents", noOfStudents);
            cmd.Parameters.AddWithValue("noOfLecturers", noOfLecturers);
            cmd.Parameters.AddWithValue("tripType", tripType);
            cmd.Parameters.AddWithValue("tripName", tripName);
            cmd.Parameters.AddWithValue("country", country);
            cmd.Parameters.AddWithValue("tripID", tripID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int deleteEnrolledStudentsByTripID(int tripID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            string sqlStr = "DELETE FROM overseasEnrolledStudent WHERE tripID=@tripID;";

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("tripID", tripID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int deleteEnrolledLecturerByTripID(int tripID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            string sqlStr = "DELETE FROM overseasEnrolledLecturer WHERE tripID=@tripID;";

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, myConn);
            cmd.Parameters.AddWithValue("tripID", tripID);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        // ViewIndividualTrip.aspx
        public static List<TripAllocation> getTripDetailsByTripIDForViewInividualTrip(int tripID)
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("Select CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType, departureDate, arrivalDate, overseasTripStatus, noOfStudents,noOfLecturers, country From overseasTrip");
            sqlStr.AppendLine("WHERE tripID=@tripID;");
            //Create Adapter

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("tripID", tripID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.tripName = row["tripNameAndTripType"].ToString();
                    obj.departureDate = row["departureDate"].ToString();
                    obj.arrivalDate = row["arrivalDate"].ToString();
                    obj.overseasTripStatus = row["overseasTripStatus"].ToString();
                    obj.noOfStudents = Convert.ToInt32(row["noOfStudents"]);
                    obj.noOfLecturers = Convert.ToInt32(row["noOfLecturers"]);
                    obj.country = row["country"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }
        public static List<TripAllocation> getEnrolledStudentNameByTripIDForViewInividualTrip(int tripID)
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * FROM overseasEnrolledStudent");
            sqlStr.AppendLine("INNER JOIN student ON student.adminNo = overseasEnrolledStudent.adminNo");
            sqlStr.AppendLine("INNER JOIN account ON student.accountID =  account.accountID");
            sqlStr.AppendLine("WHERE tripID=@tripID;");
            //Create Adapter

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("tripID", tripID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.name = row["name"].ToString();
                    resultList.Add(obj);
                }
            }
            else
            {
                resultList = null;
            }
            return resultList;
        }
        public static List<TripAllocation> getEnrolledLecturerNameByTripIDForViewInividualTrip(int tripID)
        {
            List<TripAllocation> resultList = new List<TripAllocation>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * FROM overseasEnrolledLecturer");
            sqlStr.AppendLine("INNER JOIN lecturer ON lecturer.staffID = overseasEnrolledLecturer.staffID");
            sqlStr.AppendLine("INNER JOIN account ON lecturer.accountID =  account.accountID");
            sqlStr.AppendLine("WHERE tripID=@tripID;");
            //Create Adapter

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("tripID", tripID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    TripAllocation obj = new TripAllocation();
                    obj.name = row["name"].ToString();
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