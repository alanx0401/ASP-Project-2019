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
            if (Session["UUID"] != null && Session["name"] != null)
            {
                if (!IsPostBack)
                {
                    lbUser.Text = Session["name"].ToString();
                    lbUUID.Text = Session["UUID"].ToString();
                    //lbUser.Text = Session["name"].ToString() + Session["UUID"].ToString();
                    bind();
                    PanelEvents.Visible = true;
                    PanelSearchFilter.Visible = false;
                    PanelEventDateRange.Visible = false;
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
            UUID = Session["UUID"].ToString();
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
            PanelEventDateRange.Visible = false;
        }

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSearch.SelectedValue == "0") //Please Select
            {
                PanelEvents.Visible = true;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
            }
            else if (DDLSearch.SelectedValue == "1") //Search by Event Description
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = true;
                PanelEventDateRange.Visible = false;
            }
            else //Search Security Event Based on Date Range
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string UUID = Session["UUID"].ToString();
            DateTime startDate = Convert.ToDateTime(tbStartDate.Text);
            DateTime endDate = Convert.ToDateTime(tbEndDate.Text);
            List<userSecurityEventLog> eventsList = new List<userSecurityEventLog>();
            eventsList = obj.searchEventLogDate(startDate, endDate, UUID);
            GVEventDateRange.DataSource = eventsList;
            GVEventDateRange.DataBind();
        }
    }
}