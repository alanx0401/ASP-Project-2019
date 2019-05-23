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
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["name"] != null)
                {
                    lblName.Text = Session["name"].ToString();
                    if (Session["accountType"].ToString() == "lecturer")
                    {
                        if (AnnouncementDAO.getUnreadAnnouncementCountByStaffID(Session["staffID"].ToString()) == null)
                        {
                            PanelAnnouncement.Visible = false;
                            badgeUnreadCount.Visible = false;
                        }
                        else
                        {
                            PanelAnnouncement.Visible = true;
                            badgeUnreadCount.Visible = true;
                            RepeaterUnreadCount.DataSource = AnnouncementDAO.getUnreadAnnouncementCountByStaffID(Session["staffID"].ToString());
                            RepeaterUnreadCount.DataBind();
                        }
                    }
                    else if (Session["accountType"].ToString() == "student")
                    {
                        if (AnnouncementDAO.getUnreadAnnouncementCountByAdminNo(Session["adminNo"].ToString()) == null)
                        {
                            PanelAnnouncement.Visible = false;
                            badgeUnreadCount.Visible = false;
                        }
                        else
                        {
                            PanelAnnouncement.Visible = true;
                            badgeUnreadCount.Visible = true;
                            RepeaterUnreadCount.DataSource = AnnouncementDAO.getUnreadAnnouncementCountByAdminNo(Session["adminNo"].ToString());
                            RepeaterUnreadCount.DataBind();
                        }
                    }
                    else if (Session["accountType"].ToString() == "parent")
                    {
                        RepeaterUnreadCount.DataSource = AnnouncementDAO.getUnreadAnnouncementCountByAdminNo(Session["adminNo"].ToString());
                        RepeaterUnreadCount.DataBind();
                    }

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
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
                
            }
           
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }

        protected void RepeaterUnreadMsg_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Session["accountType"].ToString() == "lecturer")
            {
                if (AnnouncementDAO.getUnreadAnnouncementCountByStaffID(Session["staffID"].ToString()) == null)
                {
                    PanelAnnouncement.Visible = false;
                    badgeUnreadCount.Visible = false;
                }
                else
                {
                    PanelAnnouncement.Visible = true;
                    badgeUnreadCount.Visible = true;
                }
            }
            else if (Session["accountType"].ToString() == "student")
            {
                if (AnnouncementDAO.getUnreadAnnouncementCountByAdminNo(Session["adminNo"].ToString()) == null)
                {
                    PanelAnnouncement.Visible = false;
                    badgeUnreadCount.Visible = false;
                }
                else
                {
                    PanelAnnouncement.Visible = true;
                    badgeUnreadCount.Visible = true;
                }
            }
            else if (Session["accountType"].ToString() == "parent")
            {
                if (AnnouncementDAO.getUnreadAnnouncementCountByAdminNo(Session["adminNo"].ToString()) == null)
                {
                    PanelAnnouncement.Visible = false;
                    badgeUnreadCount.Visible = false;
                }
                else
                {
                    PanelAnnouncement.Visible = true;
                    badgeUnreadCount.Visible = true;
                }
            }
        }

        protected void RepeaterUnreadCount_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}