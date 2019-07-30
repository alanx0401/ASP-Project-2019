using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;
using ITP213.DAL;
using System.Web.UI.DataVisualization.Charting;

namespace ITP213
{
    public partial class UserSecurityEventLogs : System.Web.UI.Page
    {
        userSecurityEventLog obj = new userSecurityEventLog();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountID"] != null && Session["name"] != null)
            {
                if (!IsPostBack)
                {
                    lbUser.Text = Session["name"].ToString();
                    lbUUID.Text = Session["accountID"].ToString();
                    //lbUser.Text = Session["name"].ToString() + Session["accountID"].ToString();
                    bind();
                    PanelEvents.Visible = true;
                    PanelSearchFilter.Visible = false;
                }  
            }
            else
            {
                Response.Redirect("login.aspx", false);
            }
        }
        protected void bind()
        {
            string UUID;
            UUID = Session["accountID"].ToString();
            List<userSecurityEventLog> eventsList = new List<userSecurityEventLog>();
            eventsList = obj.getEventDesc(UUID);
            GVEventLogs.DataSource = eventsList;
            GVEventLogs.DataBind();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            DDLSearch.SelectedValue = "0";
            PanelEvents.Visible = true;
            PanelSearchFilter.Visible = false;
        }

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSearch.SelectedValue == "0") //Please Select
            {
                PanelEvents.Visible = true;
                PanelSearchFilter.Visible = false;
            }
            else//Search by Event Description
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = true;
            }
        }
    }
}