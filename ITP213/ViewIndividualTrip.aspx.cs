using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ViewIndividualTrip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["tripID"] != null)
                    {
                        int tripID = Convert.ToInt32(Request.QueryString["tripID"]);
                        DAL.TripAllocation obj = TripAllocationDAO.getTripByTripID(tripID);
                        lblTripName.Text = obj.tripName;
                        lblTripName2.Text = obj.tripName;

                        RepeaterIndividualTrip.DataSource = TripAllocationDAO.getTripDetailsByTripIDForViewInividualTrip(tripID);
                        RepeaterIndividualTrip.DataBind();

                        RepeaterStudentName.DataSource = TripAllocationDAO.getEnrolledStudentNameByTripIDForViewInividualTrip(tripID);
                        RepeaterStudentName.DataBind();

                        RepeaterStaffName.DataSource = TripAllocationDAO.getEnrolledLecturerNameByTripIDForViewInividualTrip(tripID);
                        RepeaterStaffName.DataBind();
                    }

                    if (Session["accountType"].ToString() == "student")
                    {
                        PanelStudent.Visible = true;
                    }
                    else
                    {
                        PanelStudent.Visible = false;
                    }
                    if (Session["accountType"].ToString() == "lecturer")
                    {
                        PanelLecturer.Visible = true;
                    }
                    else
                    {
                        PanelLecturer.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
            
        }

        protected void btnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["tripID"] != null)
            {
                int tripID = Convert.ToInt32(Request.QueryString["tripID"]);
                Response.Redirect("/CreateTest.aspx?tripID=" + tripID);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ViewAnnouncement.aspx");
        }
    }
}