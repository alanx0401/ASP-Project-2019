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
    public class ImageDAO
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        public List<UploadedImage> getImage()
        {
            List<UploadedImage> tdList = new List<UploadedImage>();
            DataSet ds = new DataSet();
            DataTable tdData = new DataTable();
            //
            // Step 3 :Create SQLcommand to select all columns from TDMaster by parameterised customer id
            //          where TD is not matured yet

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * from image");

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
                    UploadedImage myImg = new UploadedImage();

                    // Step 8 Set attribute of timeDeposit instance for each row of record in TableTD

                    myImg.imageID = row["imageID"].ToString();
                    myImg.location = row["location"].ToString();
                    myImg.title = row["title"].ToString();
                    myImg.image = row["image"].ToString();
                    myImg.user = row["user"].ToString();
                    myImg.tags = row["tags"].ToString();

                    //  Step 9: Add each timeDeposit instance to array list
                    tdList.Add(myImg);
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