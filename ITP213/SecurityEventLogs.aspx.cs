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
        EventLog obj = new EventLog();
        string _conn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
                PanelEvents.Visible = true;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
                PanelUUID.Visible = false;
            }
        }
        protected void bind()
        {
            List<EventLog> eventsList = new List<EventLog>();
            eventsList = obj.GetEvents();
            GVEventLogs.DataSource = eventsList;
            GVEventLogs.DataBind();
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
            PanelUUID.Visible = false;
        }

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSearch.SelectedValue== "0")
            {
                PanelEvents.Visible = true;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
                PanelUUID.Visible = false;
            }
            else if (DDLSearch.SelectedValue== "1")
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = true;
                PanelEventDateRange.Visible = false;
                PanelUUID.Visible = false;
            }
            else if (DDLSearch.SelectedValue == "2")
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = true;
                PanelUUID.Visible = false;
            }
            else
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDateRange.Visible = false;
                PanelUUID.Visible = true;
            }

        }

        protected void DDLEventDesc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(tbStartDate.Text);
            DateTime endDate = Convert.ToDateTime(tbEndDate.Text);
            List<EventLog> eventsList = new List<EventLog>();
            //eventsList = obj.searchEventLogDate(startDate, endDate);
            eventsList = obj.searchEventLogDate(startDate, endDate);
            GVEventDateRange.DataSource = eventsList;
            GVEventDateRange.DataBind();
            //GetDataChart();
        }

        protected void GetDataChart()
        {
       
            DateTime startDate = Convert.ToDateTime(tbStartDate.Text);
            DateTime endDate = Convert.ToDateTime(tbEndDate.Text);
            string queryStr = "SELECT eventDesc, COUNT(*) AS countEvent FROM Eventlogs  WHERE dateTimeDetails BETWEEN @startDate AND @endDate GROUP BY eventDesc";
            using (SqlConnection con = new SqlConnection(_conn))
            {
                SqlCommand cmd = new SqlCommand(queryStr, con);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                Series series = chartEvent.Series["Series1"];
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.XValueMember = reader["countEvent"].ToString();
                    series.YValueMembers = reader["eventDesc"].ToString();
       
                }
                chartEvent.DataSource = reader;
                chartEvent.DataBind();
                con.Close();
                reader.Close();
                reader.Dispose();
            }
        }
    }
}