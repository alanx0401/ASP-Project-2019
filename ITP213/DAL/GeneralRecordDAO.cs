using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace MyFunctions.DAL
{
    public class GeneralRecordDAO
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        public GeneralRecord getStudentByAdminNo(string adminNo)
        {
            //Get connection string from web.config
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("Select * from generalRecord where");
            sqlCommand.AppendLine("adminNo = @paraAdminNo");

            GeneralRecord obj = new GeneralRecord();

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("paraAdminNo", adminNo);
            // fill dataset
            da.Fill(ds, "studentTable");
            int rec_cnt = ds.Tables["studentTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["studentTable"].Rows[0];  // Sql command returns only one record
                obj.generalRecordID = row["generalRecordID"].ToString();
                obj.adminNo = row["adminNo"].ToString();
                obj.studentName = row["studentName"].ToString();
                obj.school = row["school"].ToString();
                obj.course = row["course"].ToString();
                obj.email = row["email"].ToString();
                obj.dateOfBirth = row["dateOfBirth"].ToString();
                obj.dietaryNeeds = row["dietaryNeeds"].ToString();
                obj.spokenLanguage = row["spokenLanguage"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        public List<GeneralRecord> getTDbyAdminNo()
        {
            // Step 2 : declare a list to hold collection of customer's timeDeposit
            //           DataSet instance and dataTable instance 

            List<GeneralRecord> tdList = new List<GeneralRecord>();
            DataSet ds = new DataSet();
            DataTable tdData = new DataTable();
            //
            // Step 3 :Create SQLcommand to select all columns from TDMaster by parameterised customer id
            //          where TD is not matured yet

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT generalRecordID, adminNo, studentName, dietaryNeeds From generalRecord");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 6: fill dataset
            da.Fill(ds, "TableTD");

            // Step 7: Iterate the rows from TableTD above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableTD"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["TableTD"].Rows)
                {
                    GeneralRecord myTD = new GeneralRecord();

                    // Step 8 Set attribute of timeDeposit instance for each row of record in TableTD

                    myTD.generalRecordID = row["generalRecordID"].ToString();
                    myTD.adminNo = row["adminNo"].ToString();
                    myTD.studentName = row["studentName"].ToString();
                    myTD.dietaryNeeds = row["dietaryNeeds"].ToString();

                    //  Step 9: Add each timeDeposit instance to array list
                    tdList.Add(myTD);
                }
            }
            else
            {
                tdList = null;
            }
            return tdList;
        }

    }
}
