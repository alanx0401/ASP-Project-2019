using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ViewAllocatedTrip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (!IsPostBack) // first load of the page
                {
                    // upload trip status
                    //Updating Trip Status
                    //Get connection string from web.config
                    string DBConnect = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

                    StringBuilder sqlStr = new StringBuilder();
                    // overseasTripStatus = 'Ongoing'
                    sqlStr.AppendLine("UPDATE overseasTrip");
                    sqlStr.AppendLine("SET overseasTripStatus='ONGOING'");
                    sqlStr.AppendLine("WHERE departureDate<= GETDATE() AND arrivalDate>=GETDATE();");
                    // overseasTripStatus = 'Ended'
                    sqlStr.AppendLine("UPDATE overseasTrip");
                    sqlStr.AppendLine("SET overseasTripStatus='ENDED'");
                    sqlStr.AppendLine("WHERE departureDate<= GETDATE() AND arrivalDate<=GETDATE();");

                    SqlConnection myConn = new SqlConnection(DBConnect);
                    myConn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr.ToString(), myConn);
                    int result = cmd.ExecuteNonQuery();
                    myConn.Close();
                    //--Updating Trip Status

                    // ==== upload trip status
                    string CreateTripStatus = Request.QueryString["CreateTripStatus"];

                    if (CreateTripStatus != null)
                    {
                        panelAlert.Visible = true;
                    }
                    else
                    {
                        panelAlert.Visible = false;
                    }
                    if (Session["accountType"].ToString() == "student" || Session["accountType"].ToString() == "parent")
                    {
                        // Study Trip
                        if (TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Study Trip") == null)
                        {
                            PanelStudyTrips.Visible = true;
                        }
                        else
                        {
                            RepeaterStudyTrips.DataSource = TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Study Trip");
                            RepeaterStudyTrips.DataBind();
                        }
                        if (TripAllocationDAO.displayPastTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Study Trip") == null)
                        {
                            lblPastStudyTripMsg.Visible = true;
                        }
                        else
                        {
                            lblPastStudyTripMsg.Visible = false;
                            RepeaterPastStudyTrips.DataSource = TripAllocationDAO.displayPastTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Study Trip");
                            RepeaterPastStudyTrips.DataBind();
                        }
                        // Immersion Trip
                        if (TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Immersion Trip") == null)
                        {
                            PanelImmersionTrips.Visible = true;
                        }
                        else
                        {
                            RepeaterImmersionTrips.DataSource = TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Immersion Trip");
                            RepeaterImmersionTrips.DataBind();
                        }
                        if (TripAllocationDAO.displayPastTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Immersion Trip") == null)
                        {
                            lblPastImmersionTripMsg.Visible = true;
                        }
                        else
                        {
                            lblPastImmersionTripMsg.Visible = false;
                            RepeaterPastImmersionTrips.DataSource = TripAllocationDAO.displayPastTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Immersion Trip");
                            RepeaterPastImmersionTrips.DataBind();
                        }
                        // Internship
                        if (TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Internship") == null)
                        {
                            PanelInternship.Visible = true;
                        }
                        else
                        {
                            RepeaterInternship.DataSource = TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Internship");
                            RepeaterInternship.DataBind();
                        }
                        if (TripAllocationDAO.displayPastTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Internship") == null)
                        {
                            lblPastInternship.Visible = true;
                        }
                        else
                        {
                            lblPastInternship.Visible = false;
                            RepeaterPastInternship.DataSource = TripAllocationDAO.displayPastTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Internship");
                            RepeaterPastInternship.DataBind();
                        }
                    }
                    else if (Session["accountType"].ToString() == "lecturer")
                    {
                        // Study Trip
                        if (TripAllocationDAO.displayTripsBasedOnStaffID(Session["staffID"].ToString(), "Study Trip") == null)
                        {
                            PanelStudyTrips.Visible = true;
                        }
                        else
                        {
                            RepeaterStudyTrips.DataSource = TripAllocationDAO.displayTripsBasedOnStaffID(Session["staffID"].ToString(), "Study Trip");
                            RepeaterStudyTrips.DataBind();
                        }
                        if (TripAllocationDAO.displayPastTripsBasedOnStaffID(Session["staffID"].ToString(), "Study Trip") == null)
                        {
                            lblPastStudyTripMsg.Visible = true;
                        }
                        else
                        {
                            lblPastStudyTripMsg.Visible = false;
                            RepeaterPastStudyTrips.DataSource = TripAllocationDAO.displayPastTripsBasedOnStaffID(Session["staffID"].ToString(), "Study Trip");
                            RepeaterPastStudyTrips.DataBind();
                        }
                        // Immersion Trip
                        if (TripAllocationDAO.displayTripsBasedOnStaffID(Session["staffID"].ToString(), "Immersion Trip") == null)
                        {
                            PanelImmersionTrips.Visible = true;
                        }
                        else
                        {
                            RepeaterImmersionTrips.DataSource = TripAllocationDAO.displayTripsBasedOnStaffID(Session["staffID"].ToString(), "Immersion Trip");
                            RepeaterImmersionTrips.DataBind();
                        }
                        if (TripAllocationDAO.displayPastTripsBasedOnStaffID(Session["staffID"].ToString(), "Immersion Trip") == null)
                        {
                            lblPastImmersionTripMsg.Visible = true;
                        }
                        else
                        {
                            lblPastImmersionTripMsg.Visible = false;
                            RepeaterPastImmersionTrips.DataSource = TripAllocationDAO.displayPastTripsBasedOnStaffID(Session["staffID"].ToString(), "Immersion Trip");
                            RepeaterPastImmersionTrips.DataBind();
                        }
                        // Internship
                        if (TripAllocationDAO.displayTripsBasedOnStaffID(Session["staffID"].ToString(), "Internship") == null)
                        {
                            PanelInternship.Visible = true;
                        }
                        else
                        {
                            RepeaterInternship.DataSource = TripAllocationDAO.displayTripsBasedOnStaffID(Session["staffID"].ToString(), "Internship");
                            RepeaterInternship.DataBind();
                        }
                        if (TripAllocationDAO.displayPastTripsBasedOnStaffID(Session["staffID"].ToString(), "Internship") == null)
                        {
                            lblPastInternship.Visible = true;
                        }
                        else
                        {
                            lblPastInternship.Visible = false;
                            RepeaterPastInternship.DataSource = TripAllocationDAO.displayPastTripsBasedOnStaffID(Session["staffID"].ToString(), "Internship");
                            RepeaterPastInternship.DataBind();
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
            
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e) // for repeater study trip
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "trips_Click")
                {
                    string name = e.CommandArgument.ToString();
                    lblTesting.Text = name;
                    // **** to redirect to test
                    //if (Session["accountType"].ToString() == "lecturer") 
                    //{
                    //Response.Redirect("/lec.aspx?tripID=" + name);
                    //}

                    //******************* shang ji: TEST
                    /*if (Session["accountType"].ToString() == "student")
                    {
                        Response.Redirect("/stud.aspx?tripID=" + name); // do test
                    }*/
                    Response.Redirect("/ViewIndividualTrip.aspx?tripID=" + name);
                        
                }
            }
        }

        protected void btnStudyTripsWithdraw_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnStudyTrips_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                lblTesting.Text = name;
                Response.Redirect("/StudentWithdrawalRequest.aspx?tripID="+name);
            }
        }

        protected void EditTrip_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                lblTesting.Text = name;
                Response.Redirect("/TripAllocation.aspx?tripID=" + name);
            }
        }

        protected void delete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                lblTesting.Text = name;
                DAL.TripAllocationDAO.deleteById(Convert.ToInt32(name));
                Response.Redirect("/ViewAllocatedTrip.aspx");
            }
        }

        protected void CreateTest_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                lblTesting.Text = name;
                //***** shang ji: TEST
                Response.Redirect("/test.aspx?tripID=" + name);
            }
        }


        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }

        protected void RepeaterStudyTrip_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            // parent, student
            Button withdrawB = (Button)item.FindControl("btnStudyTripsWithdraw");
            // lecturer
            Button editB = (Button)item.FindControl("btnStudyTripsEdit");
            Button deleteB = (Button)item.FindControl("btnStudyTripsDelete");
            Label tripStatus = (Label)item.FindControl("lblTripStatus");

            if ( Session["accountType"].ToString() == "student")
            {
                withdrawB.Visible = false;
                editB.Visible = false;
                deleteB.Visible = false;
                if (tripStatus.Text == "PENDING")
                {
                    withdrawB.Visible = true;
                }
            }
            else if (Session["accountType"].ToString() == "parent")
            {
                withdrawB.Visible = false;
                editB.Visible = false;
                deleteB.Visible = false;
            }
            else
            {
                withdrawB.Visible = false;
                deleteB.Visible = false; 
            }
        }

        protected void RepeaterImmersionTrip_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            // parent, student
            Button withdrawB = (Button)item.FindControl("btnImmersionTripsWithdraw");
            // lecturer
            Button editB = (Button)item.FindControl("btnImmersionTripsEdit");
            Button deleteB = (Button)item.FindControl("btnImmersionTripsDelete");

            if (Session["accountType"].ToString() == "student")
            {
                withdrawB.Visible = true;
                editB.Visible = false;
                deleteB.Visible = false;
            }
            else if (Session["accountType"].ToString() == "parent")
            {
                withdrawB.Visible = false;
                editB.Visible = false;
                deleteB.Visible = false;
            }
            else
            {
                withdrawB.Visible = false;
                editB.Visible = true;
                deleteB.Visible = false;
            }
        }

        protected void RepeaterPastStudyTrips_Command(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "trips_Click")
                {
                    string name = e.CommandArgument.ToString();
                    lblTesting.Text = name;


                    // **** to redirect to test
                    //if (Session["accountType"].ToString() == "lecturer") 
                    //{
                    //Response.Redirect("/lec.aspx?tripID=" + name);
                    //}

                    //******************* shang ji: TEST
                    /*if (Session["accountType"].ToString() == "student")
                    {
                        Response.Redirect("/stud.aspx?tripID=" + name); // do test
                    }*/
                    Response.Redirect("/ViewIndividualTrip.aspx?tripID=" + name);

                }
            }
        }
    }
}