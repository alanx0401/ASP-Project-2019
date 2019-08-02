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
    public partial class SecurityEventLogs : System.Web.UI.Page
    {
        SecurityEventLog obj = new SecurityEventLog();
        string _conn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
                PanelEvents.Visible = true;
                PanelAuditLogs.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
            }
        }
        protected void bind()
        {
            List<SecurityEventLog> eventsList = new List<SecurityEventLog>();
            eventsList = obj.GetEvents();
            GVEventLogs.DataSource = eventsList;
            GVEventLogs.DataBind();

            string accountType = "admin";
            List<SecurityEventLog> auditLogList = new List<SecurityEventLog>();
            auditLogList = obj.auditLog(accountType);
            GVEventLogs.DataSource = eventsList;
            GVEventLogs.DataBind();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            DDLSearch.SelectedValue = "0";
            tbStartDate.Text = "";
            tbEndDate.Text = "";
            PanelEvents.Visible = true;
            PanelAuditLogs.Visible = false;
            PanelSearchFilter.Visible = false;
            PanelEventDateRange.Visible = false;
            GVEventDateRange.Visible = false;
            btnUUID.Visible = true;
            btnusername.Visible = true;
        }

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSearch.SelectedValue== "0")
            {
                PanelEvents.Visible = true;
                PanelAuditLogs.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
                
            }
            else if (DDLSearch.SelectedValue== "1")
            {
                PanelEvents.Visible = false;
                PanelAuditLogs.Visible = true;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
            }
            else if (DDLSearch.SelectedValue == "2")
            {
                PanelEvents.Visible = false;
                PanelAuditLogs.Visible = false;
                PanelSearchFilter.Visible = true;
                PanelEventDateRange.Visible = false;
            }
            else
            {
                PanelEvents.Visible = false;
                PanelAuditLogs.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = true;
            }

        }

        protected void DDLEventDesc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(tbStartDate.Text);
            DateTime endDate = Convert.ToDateTime(tbEndDate.Text);
            List<SecurityEventLog> eventsList = new List<SecurityEventLog>();
            eventsList = obj.searchEventLogDate(startDate, endDate);
            GVEventDateRange.DataSource = eventsList;
            GVEventDateRange.DataBind();
        }
        protected void SqlDataSourceGVUserMode_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void btnUUID_Click(object sender, EventArgs e)
        {
            GVEventLogs.Visible = true;
        }

        protected void btnusername_Click(object sender, EventArgs e)
        {
            GVEventLogs.Visible = false;
            GVEventsByUsername.Visible = true;
        }
    }
}