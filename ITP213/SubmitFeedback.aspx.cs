using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class SubmitFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)// first time coming to page
            {
                if (Session["accountType"] != null)
                {
                    if (Session["accountType"].ToString() == "student" || Session["accountType"].ToString() == "parent")
                    {
                        ddlTripName.DataSource = WithdrawalRequestDAO.displayAllocatedPendingTrips(Session["adminNo"].ToString());
                        ddlTripName.Items.Insert(0, new ListItem("--Select Trip--", "0"));
                        ddlTripName.AppendDataBoundItems = true;
                        ddlTripName.DataTextField = "tripNameAndTripType";
                        ddlTripName.DataValueField = "tripID";
                        ddlTripName.DataBind();
                    }
                    /*==========================================
                    if (Session["accountType"]!=)
                    ddlTripName.DataSource = TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Internship");
                    ddlTripName.Items.Insert(0, new ListItem("---Select Trip---", "0"));
                    ddlTripName.AppendDataBoundItems = true;
                    ddlTripName.DataTextField = "tripName";
                    ddlTripName.DataValueField = "tripID";
                    ddlTripName.DataBind();*/
                    else
                    {
                        Response.Redirect("/UnauthorizedErrorPage.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // ddlTripName.SelectedValue.ToString();
            string enjoy = EnjoyDropDown.SelectedItem.ToString();
            string lodging = LodgingDropDown.SelectedItem.ToString();
            string affordable = AffordableDropDown.SelectedItem.ToString();
            string interaction = InteractDropDown.SelectedItem.ToString();
            string companyVisit = CompanyVisitDropDown.SelectedItem.ToString();
            string transport = TransportDropDown.SelectedItem.ToString(); // string
            string improvement = tbImprove.Text;
            string tripID = ddlTripName.SelectedValue.ToString(); //num

            FeedbackDAO.insert(enjoy, lodging, affordable, interaction, companyVisit, transport, improvement, tripID, Session["adminNo"].ToString());
            Response.Redirect("Default.aspx");
        }
    }
}