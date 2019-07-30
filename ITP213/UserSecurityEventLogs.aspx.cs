using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
<<<<<<< HEAD
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
=======

using ITP213.DAL;

namespace ITP213
{
    public partial class IndividualUserEventLog : System.Web.UI.Page
    {
        string UUID;
        string name;
        userEventLog userEventLog = new userEventLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountID"]!= null && !IsPostBack)
            {
                if (Session["name"] !=null && !IsPostBack)
                {
                    bind();
                }
                else
                {
                   Response.Redirect("login.aspx", false);
                }
          
>>>>>>> TechnicalReview2
            }
            else
            {
                Response.Redirect("login.aspx", false);
            }
        }
        protected void bind()
        {
<<<<<<< HEAD
            UUID = Session["UUID"].ToString();
            List<userSecurityEventLog> eventsList = new List<userSecurityEventLog>();
            eventsList = obj.getEventDesc(UUID);
            GVUserSecurityEventLogs.DataSource = eventsList;
            GVUserSecurityEventLogs.DataBind();
=======
            name = Session["name"].ToString();
            UUID = Session["accountID"].ToString();
            lblUser.Text = name + " with AccountID of " + UUID;
            List<userEventLog> userEventList = new List<userEventLog>();
            userEventList = userEventLog.getIndividualUserLog(UUID);
            GVEventLogs.DataSource = userEventList;
            GVEventLogs.DataBind();
>>>>>>> TechnicalReview2

        }
    }
}