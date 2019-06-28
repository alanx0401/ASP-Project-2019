using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ITP213.DAL;
namespace ITP213
{
    public partial class SecurityEventLogs : System.Web.UI.Page
    {
        EventLog obj = new EventLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
                PanelEvents.Visible = true;
                PanelSearchFilter.Visible = false;
                PanelEventDuration.Visible = false;
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
            PanelEvents.Visible = true;
            PanelSearchFilter.Visible = false;
            PanelEventDuration.Visible = false;
            PanelUUID.Visible = false;
        }

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSearch.SelectedValue== "0")
            {
                PanelEvents.Visible = true;
                PanelSearchFilter.Visible = false;
                PanelEventDuration.Visible = false;
                PanelUUID.Visible = false;
            }
            else if (DDLSearch.SelectedValue== "1")
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = true;
                PanelEventDuration.Visible = false;
                PanelUUID.Visible = false;
            }
            else if (DDLSearch.SelectedValue == "2")
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDuration.Visible = true;
                PanelUUID.Visible = false;
            }
            else
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = false;
                PanelEventDuration.Visible = false;
                PanelUUID.Visible = true;
            }

        }

        protected void DDLEventDesc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GVeventDuration_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}