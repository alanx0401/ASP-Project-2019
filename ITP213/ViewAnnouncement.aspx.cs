using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ViewAnnouncement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["accountType"] != null)
                {
                    if (Session["accountType"].ToString() == "lecturer")
                    {
                        // get all announcements by staffID
                        if (AnnouncementDAO.getAnnouncementsByStaffID(Session["staffID"].ToString()) == null)
                        {
                            PanelStudyTrips.Visible = true;
                        }
                        else
                        {
                            RepeaterStudyTrips.DataSource = AnnouncementDAO.getAnnouncementsByStaffID(Session["staffID"].ToString());
                            RepeaterStudyTrips.DataBind();

                        }

                    }
                    else if (Session["accountType"].ToString() == "student")
                    {
                        // get all announcements by adminNo
                        if (AnnouncementDAO.getAnnouncementsByAdminNo(Session["adminNo"].ToString()) == null)
                        {
                            PanelStudyTrips.Visible = true;
                        }
                        else
                        {
                            RepeaterStudyTrips.DataSource = AnnouncementDAO.getAnnouncementsByAdminNo(Session["adminNo"].ToString());
                            RepeaterStudyTrips.DataBind();


                        }

                    }
                    else
                    {
                        Response.Redirect("/UnauthorizedErrorPage.aspx");
                    }
                }else
                {
                    Response.Redirect("/login.aspx");
                }   
                
            }
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnEditTrip_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string announcementID = e.CommandArgument.ToString();
                Response.Redirect("CreateAnnouncement.aspx?AnnouncementID=" + announcementID);
            }
        }

        protected void btnStudyTrips_Command(object sender, CommandEventArgs e) // for deletion
        {
            if (e.CommandName == "trips_Click")
            {
                string announcementID = e.CommandArgument.ToString();
                AnnouncementDAO.deleteById(Convert.ToInt32(announcementID));
                Response.Redirect("/ViewAnnouncement.aspx");
            }
        }

        protected void RepeaterStudyTrips_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                //Response.Redirect("ViewIndividualAnnouncement.aspx?AnnouncementID=" + index);
            }
        }

        protected void RepeaterImmersionTrips_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                //Response.Redirect("ViewIndividualAnnouncement.aspx?AnnouncementID=" + index);
            }
        }

        protected void RepeaterWithdrawalRequest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "trips_Click")
                {
                    string name = e.CommandArgument.ToString();
                }
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {

        }

        protected void btnRejected_Click(object sender, EventArgs e)
        {

        }

        protected void btnApproved_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                WithdrawalRequestDAO.approveTripRequestByWithdrawTripRequestID(Convert.ToInt32(name));
            }
        }

        protected void btnRejected_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                WithdrawalRequestDAO.rejectTripRequestByWithdrawTripRequestID(Convert.ToInt32(name));
            }
        }

        protected void cbSelector_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        protected void cbRead_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;;
        }

        protected void RepeaterStudyTrips_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // check if lecturer has the right to edit the ennouncement
            if (Session["accountType"].ToString() == "lecturer")
            {
                RepeaterItem item = e.Item;
                Label lblStaffID = (Label)item.FindControl("lblStaffID");
                string createdBy = lblStaffID.Text;
                string currentID = Session["staffID"].ToString();

                Button editB = (Button)item.FindControl("btnStudyTripsEdit");
                Button deleteB = (Button)item.FindControl("btnStudyTripsDelete");
                CheckBox cb = (CheckBox)item.FindControl("cbRead");

                int i = 0;

                if (createdBy == currentID)
                {
                    editB.Visible = true;
                    deleteB.Visible = true;
                    cb.Visible = false;
                }
                else
                {
                    editB.Visible = false;
                    deleteB.Visible = false;
                    cb.Visible = true;
                }
            }
        }

        protected void RepeaterImmersionTrips_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // check if lecturer has the right to edit the ennouncement
            if (Session["accountType"].ToString() == "lecturer")
            {
                RepeaterItem item = e.Item;
                Label lblStaffID = (Label)item.FindControl("lblStaffID");
                string createdBy = lblStaffID.Text;
                string currentID = Session["staffID"].ToString();

                Button editB = (Button)item.FindControl("btnImmersionTripsEdit");
                Button deleteB = (Button)item.FindControl("btnImmersionTripsDelete");
                CheckBox cb = (CheckBox)item.FindControl("cbRead");

                int i = 0;

                if (createdBy == currentID)
                {
                    editB.Visible = true;
                    deleteB.Visible = true;
                    cb.Visible = false;
                }
                else
                {
                    editB.Visible = false;
                    deleteB.Visible = false;
                    cb.Visible = true;
                }
            }
        }

        protected void cbRead_CheckedChanged1(object sender, EventArgs e)
        {

            var chk = (CheckBox)sender;
            var announcementID = chk.Attributes["CommandName"];
            if (Session["accountType"].ToString() == "lecturer")
            {
                AnnouncementDAO.insertLecturerAnnReadWithAnnouncementIDAndStaffID(Convert.ToInt32(announcementID), Session["staffID"].ToString());
                // AnnouncementDAO.updateAnnouncementVisibleByAnnouncementIDAndStaffID(Convert.ToInt32(announcementID), Session["staffID"].ToString());
            }
            else if (Session["accountType"].ToString() == "student")
            {
                //chk.Visible = false;
                AnnouncementDAO.insertStudentAnnReadWithAnnouncementIDAndStudentID(Convert.ToInt32(announcementID), Session["adminNo"].ToString());
            }
            Response.Redirect("/ViewAnnouncement.aspx");
        }
    }
}