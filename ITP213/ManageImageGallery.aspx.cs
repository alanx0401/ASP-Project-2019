using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITP213.DAL;
using System.Data.SqlClient;
using System.Configuration;

namespace ITP213
{
    public partial class ManageImageGallery : System.Web.UI.Page
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "lecturer")
                {
                    ImageDAO imageDAO = new ImageDAO();
                    List<UploadedImage> tdList = new List<UploadedImage>();
                    tdList = imageDAO.getImage();
                    GridViewImages.DataSource = tdList;
                    GridViewImages.DataBind();
                }
                else
                {
                    Response.Redirect("/UnauthorizedErrorPage.aspx");
                }
            }
        }

        protected void GridViewImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label l1 = GridViewImages.Rows[e.RowIndex].FindControl("LabelImageID") as Label;
            SqlConnection myConn = new SqlConnection(DBConnect);
            string strSql = "delete from [image] where [imageID]= @id";
            SqlCommand cmd = new SqlCommand(strSql, myConn);

            cmd.Parameters.AddWithValue("@id", l1.Text);

            myConn.Open();
            cmd.ExecuteNonQuery();
            myConn.Close();
            Response.Redirect("ManageImageGallery.aspx");
        }
    }
}