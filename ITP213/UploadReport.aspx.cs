using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class UploadReport : System.Web.UI.Page
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "student")
                {
                }
                else
                {
                    Response.Redirect("/UnauthorizedErrorPage.aspx");
                }
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.FileName == null)
            {

            }
            else
            {
                SqlConnection myConn = new SqlConnection(DBConnect);
                string strSql = "insert into [report] ([fileName],[fileSize],[fileType],[adminNo]) values(@parafilename,@parafilesize,@parafiletype,@paraadminno)";
                SqlCommand cmd = new SqlCommand(strSql, myConn);
                FileInfo fi = new FileInfo(FileUpload1.FileName);
                string ext = fi.Extension;
                cmd.Parameters.AddWithValue("@parafilename", FileUpload1.FileName);
                cmd.Parameters.AddWithValue("@parafilesize", FileUpload1.FileBytes.Length.ToString());
                cmd.Parameters.AddWithValue("@parafileType", GetFileTypeByExtension(fi.Extension));
                cmd.Parameters.AddWithValue("@paraadminno", Session["adminNo"].ToString());
                myConn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Report/") + FileUpload1.FileName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    myConn.Close();
                    Label1.Text = "File Uploaded Successfully!";
                }
            }         
        }
        private string GetFileTypeByExtension(string extension)
        {
            switch (extension.ToLower())
            {
                case ".doc":
                case ".docx":
                    return "Microsoft Word Document";
                case ".xlsx":
                case ".xls":
                    return "Microsoft Excel Document";
                case ".txt":
                    return "Text Document";
                case ".pdf":
                    return "PDF Document";
                default:
                    return "Unknown";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.End();
            }
        }
    }
}