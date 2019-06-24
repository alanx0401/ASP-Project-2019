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
                GVParticularEvent.Visible = false;
                lb_SearchFilter.Visible = false;
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
            GVEventLogs.Visible = true;
            GVParticularEvent.Visible = false;
            lb_SearchFilter.Visible = false;
        }

        protected void DDLEventDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            GVEventLogs.Visible = false;
            GVParticularEvent.Visible = true;
            lb_SearchFilter.Visible = true;
        }
    }
}