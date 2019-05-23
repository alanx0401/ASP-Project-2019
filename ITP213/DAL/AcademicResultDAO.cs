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
    public class AcademicResultDAO
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        public AcademicResult getStudentByAdminNo(string adminNo)
        {
            //Get connection string from web.config
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("Select * from academicResult where");
            sqlCommand.AppendLine("adminNo = @paraAdminNo");

            AcademicResult obj = new AcademicResult();

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("paraAdminNo", adminNo);
            // fill dataset
            da.Fill(ds, "studentTable");
            int rec_cnt = ds.Tables["studentTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["studentTable"].Rows[0];  // Sql command returns only one record
                obj.academicResultID = row["academicResultID"].ToString();
                obj.adminNo = row["adminNo"].ToString();
                obj.cumulativeGPA = row["cumulativeGPA"].ToString();
                obj.sem1Year1GPA = row["sem1Year1GPA"].ToString();
                obj.sem2Year1GPA = row["sem2Year1GPA"].ToString();
                obj.sem1Year2GPA = row["sem1Year2GPA"].ToString();
                obj.sem2Year2GPA = row["sem2Year2GPA"].ToString();
                obj.sem1Year3GPA = row["sem1Year3GPA"].ToString();
                obj.sem2Year3GPA = row["sem2Year3GPA"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
        public List<AcademicResult> getTDbyAdminNo()
        {
            // Step 2 : declare a list to hold collection of customer's timeDeposit
            //           DataSet instance and dataTable instance 

            List<AcademicResult> tdList = new List<AcademicResult>();
            DataSet ds = new DataSet();
            DataTable tdData = new DataTable();
            //
            // Step 3 :Create SQLcommand to select all columns from TDMaster by parameterised customer id
            //          where TD is not matured yet

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * From academicResult");

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
                    AcademicResult myTD = new AcademicResult();

                    // Step 8 Set attribute of timeDeposit instance for each row of record in TableTD

                    myTD.academicResultID = row["academicResultID"].ToString();
                    myTD.adminNo = row["adminNo"].ToString();
                    myTD.cumulativeGPA = row["cumulativeGPA"].ToString();
                   
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