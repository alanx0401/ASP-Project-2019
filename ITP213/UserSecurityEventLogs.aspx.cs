using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITP213.DAL;
namespace ITP213
{
    public partial class UserSecurityEventLogs : System.Web.UI.Page
    {
        userSecurityEventLog obj = new userSecurityEventLog();
        string UUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UUID"] != null && Session["name"] != null && !IsPostBack)
            {
                lbUser.Text = Session["name"].ToString();
                UUID = Session["UUID"].ToString();
                bind();
            }
            else
            {
                Response.Redirect("login.aspx", false);
            }
        }
        protected void bind()
        {
            UUID = Session["UUID"].ToString();
            List<userSecurityEventLog> eventsList = new List<userSecurityEventLog>();
            eventsList = obj.getEventDesc(UUID);
            GVUserSecurityEventLogs.DataSource = eventsList;
            GVUserSecurityEventLogs.DataBind();

        }
    }
}