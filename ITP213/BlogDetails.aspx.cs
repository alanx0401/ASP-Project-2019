using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class BlogDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                string id = Request.QueryString["blogID"];
                Blog b = BlogDAO.getBlogById(Convert.ToInt32(id));
                LabelName.Text = b.studentName;
                LabelTitle.Text = b.title;
                LabelContent.Text = b.content;
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }
    }
}