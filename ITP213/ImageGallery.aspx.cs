using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITP213.DAL;

namespace ITP213
{
    public partial class ImageGallery : System.Web.UI.Page
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadImage.FileName == null)
            {
            }
            else
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                string s = "Images/" + FileUploadImage.FileName;
                string strSql = "insert into [image] ([title],[image],[user],[location],[tags]) values(@paraTitle,@paraImage,@paraUser,@paraLocation,@paraTags)";
                SqlCommand cmd = new SqlCommand(strSql, myConn);
                cmd.Parameters.AddWithValue("@paraTitle", TextBoxTitle.Text);
                cmd.Parameters.AddWithValue("@paraImage", s);
                cmd.Parameters.AddWithValue("@paraUser", Session["name"].ToString());
                cmd.Parameters.AddWithValue("@paraLocation", DropDownListLocation.SelectedItem.ToString());
                List<string> selectedValues = CheckBoxListTags.Items.Cast<ListItem>()
                    .Where(li => li.Selected)
                    .Select(li => li.Value)
                    .ToList();
                string combinedTags = String.Join("", selectedValues.ToArray());
                cmd.Parameters.AddWithValue("@paraTags", combinedTags);
                myConn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    FileUploadImage.SaveAs(Server.MapPath("~/Images/") + FileUploadImage.FileName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    myConn.Close();
                    TextBoxTitle.Text = "";
                    Response.Redirect("ImageGallery.aspx");
                }
            }
        }
    }
}