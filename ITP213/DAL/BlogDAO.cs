using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class BlogDAO
    {
        public static int insertBlog(string title, string content, string studentName, string adminNo, string country)
        {
            //Get connection string from webvconfig
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            string sqlstr =
                "INSERT INTO studentBlog(title,content,studentName,adminNo,country, blogtime)VALUES(@title,@content,@studentName,@adminNo,@country, GETDATE())";
            /*INSERT INTO studentBlog(title,content,studentName,adminNo,country, blogtime)VALUES('Hi','Hi','hi','171846z','SG', GETDATE())*/

            Blog obj = new Blog();

            SqlConnection myConn = new SqlConnection(DBConnect);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, myConn);
            cmd.Parameters.AddWithValue("title", title);
            cmd.Parameters.AddWithValue("content", content);
            cmd.Parameters.AddWithValue("studentName", studentName);
            cmd.Parameters.AddWithValue("adminNo", adminNo);
            cmd.Parameters.AddWithValue("country", country);

            int result = cmd.ExecuteNonQuery();
            return result;

        }
        public static List<Blog> getAllBlog()
        {
            List<Blog> resultList = new List<Blog>();
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            //WRITE SQL Statement to retrieve all columns from Customer by customer Id using query parameter
            string sqlStr = "SELECT * FROM studentBlog";

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
                    Blog obj = new Blog();   // create a blog instance
                    obj.blogID = Convert.ToInt32(row["blogId"]);
                    obj.studentName = row["studentName"].ToString();
                    obj.title = row["title"].ToString();
                    obj.country = row["country"].ToString();
                    obj.content = row["content"].ToString();
                    obj.blogTime = row["blogTime"].ToString();

                    resultList.Add(obj);

                }
            }

            return resultList;

        }
        public static Blog getBlogById(int blogID)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            //WRITE SQL Statement to retrieve all columns from Customer by customer Id using query parameter
            string sqlStr = "SELECT * FROM studentBlog WHERE blogID = @blogID";

            // change custId in where clause to custId1 or 
            // change connection string in web config to a wrong file name  

            Blog obj = new Blog();   // create a customer instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlStr, myConn);
            da.SelectCommand.Parameters.AddWithValue("blogID", blogID);
            // fill dataset
            da.Fill(ds, "resultTable");
            int rec_cnt = ds.Tables["resultTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["resultTable"].Rows[0];
                obj.blogID = Convert.ToInt32(row["blogId"]);
                obj.studentName = row["studentName"].ToString();
                obj.title = row["title"].ToString();
                obj.country = row["country"].ToString();
                obj.content = row["content"].ToString();
                obj.blogTime = row["blogTime"].ToString();// Sql command returns only one record

            }
            else
            {
                obj = null;
            }

            return obj;
        }
    }
}