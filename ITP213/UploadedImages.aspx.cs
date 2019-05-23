using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class UploadedImages : System.Web.UI.Page
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {

            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }
        protected void delete_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "delete_Click2")
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                string strSql = "delete from [image] where [imageID]= @id";
                SqlCommand cmd = new SqlCommand(strSql, myConn);

                string id = e.CommandArgument.ToString();
                cmd.Parameters.AddWithValue("@id", id);

                myConn.Open();
                cmd.ExecuteNonQuery();
                myConn.Close();
                Response.Redirect("UploadedImages.aspx");
            }
        }
    }
}