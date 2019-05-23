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
    public class CCARecordDAO
    {
        public CCARecord getStudentByAdminNo(string adminNo)
        {
            //Get connection string from web.config
            string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SqlDataAdapter da;
            DataSet ds = new DataSet();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("Select * from ccaRecord where");
            sqlCommand.AppendLine("adminNo = @paraAdminNo");

            CCARecord obj = new CCARecord();

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("paraAdminNo", adminNo);
            // fill dataset
            da.Fill(ds, "studentTable");
            int rec_cnt = ds.Tables["studentTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["studentTable"].Rows[0];  // Sql command returns only one record
                obj.ccaRecordID = row["ccaRecordID"].ToString();
                obj.adminNo = row["adminNo"].ToString();
                obj.classification = row["classification"].ToString();
                obj.achievementTitle = row["achievementTitle"].ToString();
                obj.date = row["date"].ToString();
                obj.ccaPoints = row["ccaPoints"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }
    }
}