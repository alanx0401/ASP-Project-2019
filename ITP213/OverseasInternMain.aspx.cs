using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class OverseasInternMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                ddlTripName.DataSource = TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Internship");
                ddlTripName.Items.Insert(0, new ListItem("--Select Trip", "0"));
                ddlTripName.AppendDataBoundItems = true;
                ddlTripName.DataTextField = "tripName";
                ddlTripName.DataValueField = "tripID";
                ddlTripName.DataBind();
            }*/
        }
        
    }
}