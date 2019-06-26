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
            PanelEvents.Visible = true;
            DDLSearch.SelectedValue = "Please Select";
            PanelSearchFilter.Visible = false;
        }

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLSearch.SelectedValue== "Please Select")
            {
                PanelEvents.Visible = true;
                PanelSearchFilter.Visible = false;
            }
            else
            {
                PanelEvents.Visible = false;
                PanelSearchFilter.Visible = true;
            }
        }

        protected void DDLEventDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventCount eventCountObj = new EventCount();
            List<EventCount> eventCountList = new List<EventCount>();
            eventCountList = eventCountObj.GetEventsCount(DDLEventDesc.SelectedValue);
            GVEventOccured.DataSource = eventCountList;
            GVEventOccured.DataBind();
        }
    }
}