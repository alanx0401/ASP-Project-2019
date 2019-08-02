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
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
            }
        }
        protected void bind()
        {
            string UUID = Session["UUID"].ToString();
            List<SecurityEventLog> eventsList = new List<SecurityEventLog>();
            eventsList = obj.GetEvents();
            GVEventLogs.DataSource = eventsList;
            GVEventLogs.DataBind();
            List<SecurityEventLog> auditLog = new List<SecurityEventLog>();
            auditLog = obj.getEventBasedonAccountType(UUID);
            GVAuditLogs.DataSource = auditLog;
            GVAuditLogs.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            DDLSearch.SelectedValue = "0";
            tbStartDate.Text = "";
            tbEndDate.Text = "";
            PanelEvents.Visible = true;
            PanelSearchFilter.Visible = false;
            PanelEventDateRange.Visible = false;
            GVEventDateRange.Visible = false;
            PanelAuditLogs.Visible = false;
        }

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSearch.SelectedValue== "0")
            {
                PanelEvents.Visible = true;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
                PanelAuditLogs.Visible = false;

            }
            else if (DDLSearch.SelectedValue== "1")
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = true;
                PanelEventDateRange.Visible = false;
                PanelAuditLogs.Visible = false;
            }
            else if (DDLSearch.SelectedValue == "2")
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = true;
                PanelAuditLogs.Visible = false;
            }
            else
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
                PanelAuditLogs.Visible = true;
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

        protected void btnUUID_Click(object sender, EventArgs e)
        {
            GVEventLogsInUsername.Visible = false;
        }

        protected void btnUsername_Click(object sender, EventArgs e)
        {
            GVEventLogs.Visible = false;
        }
    }
}