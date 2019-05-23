using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ViewBlog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                GridView2.DataSource = BlogDAO.getAllBlog();
                GridView2.DataBind();
            }
            else
            {
                Response.Redirect("/login.aspx");
            }

            

        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string id = GridView2.Rows[index].Cells[0].Text;
                Response.Redirect("BlogDetails.aspx?blogID=" + id);
            }
        }

    }
}