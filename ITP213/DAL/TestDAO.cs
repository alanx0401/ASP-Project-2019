using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class TestDAO
    {
        public static int insertTestAnswers(int tripID,string studentName, string adminNo, string qOneAnswer, string qTwoAnswer, string qThreeAnswer, string qFourAnswer, string qFiveAnswer)
        {
            //Get connection string from webvconfig
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            string sqlstr =
                "INSERT INTO test(tripID,studentName, adminNo,qOneAnswer,qTwoAnswer,qThreeAnswer,qFourAnswer,qFiveAnswer)VALUES(@tripID,@studentName,@adminNo,@qOneAnswer,@qTwoAnswer,@qThreeAnswer,@qFourAnswer,@qFiveAnswer)";
            /*INSERT INTO studentBlog(title,content,studentName,adminNo,country, blogtime)VALUES('Hi','Hi','hi','171846z','SG', GETDATE())*/

            Test obj = new Test();

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, myConn);
            cmd.Parameters.AddWithValue("tripID", tripID);
            cmd.Parameters.AddWithValue("adminNo", adminNo);
            cmd.Parameters.AddWithValue("studentName", studentName);
            cmd.Parameters.AddWithValue("qOneAnswer", qOneAnswer);
            cmd.Parameters.AddWithValue("qTwoAnswer", qTwoAnswer);
            cmd.Parameters.AddWithValue("qThreeAnswer", qThreeAnswer);
            cmd.Parameters.AddWithValue("qFourAnswer", qFourAnswer);
            cmd.Parameters.AddWithValue("qFiveAnswer", qFiveAnswer);


            int result = cmd.ExecuteNonQuery();
            return result;

        }
        public static List<Test> getAllTests()
        {
            List<Test> resultList = new List<Test>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            //WRITE SQL Statement to retrieve all columns from Customer by customer Id using query parameter
            string sqlStr = "SELECT * FROM test";

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            //            da.SelectCommand.Parameters.AddWithValue("paraCustId", custId);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["resultTable"].Rows)
                {
                    Test obj = new Test();   // create a blog instance
                    obj.tripID = Convert.ToInt32(row["tripId"]);
                    obj.studentName = row["studentName"].ToString();
                    obj.adminNo = row["adminNo"].ToString();
                    obj.qOneAnswer = row["qOneAnswer"].ToString();
                    obj.qTwoAnswer = row["qTwoAnswer"].ToString();
                    obj.qThreeAnswer = row["qThreeAnswer"].ToString();
                    obj.qFourAnswer = row["qFourAnswer"].ToString();
                    obj.qFiveAnswer = row["qFiveAnswer"].ToString();
                    obj.testID = Convert.ToInt32(row["testID"]);

                    resultList.Add(obj);

                }
            }
            return resultList;
        }

        public static Test getTestAnswersById(int testID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            //WRITE SQL Statement to retrieve all columns from Customer by customer Id using query parameter
            string sqlStr = "SELECT * FROM test WHERE testID = @testID";

            // change custId in where clause to custId1 or 
            // change connection string in web config to a wrong file name  

            Test obj = new Test();   // create a customer instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("testID", testID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["resultTable"].Rows[0];
                obj.testID = Convert.ToInt32(row["testId"]);
                obj.adminNo = row["adminNo"].ToString();
                obj.qOneAnswer = row["qOneAnswer"].ToString();
                obj.qTwoAnswer = row["qTwoAnswer"].ToString();
                obj.qThreeAnswer = row["qThreeAnswer"].ToString();
                obj.qFourAnswer = row["qFourAnswer"].ToString();
                obj.qFiveAnswer = row["qFiveAnswer"].ToString();

            }
            else
            {
                obj = null;
            }

            return obj;
        }
    }
}