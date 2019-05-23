using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class LecturerWithdrawalRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["accountType"] != null)
                {
                    if (Session["accountType"].ToString() == "lecturer")
                    {
                        // for withdrawal request
                        if (WithdrawalRequestDAO.displayWithdrawalRequest(Session["staffID"].ToString()) == null)
                        {
                            PanelEmptyWithdrawalRequest.Visible = true;
                        }
                        else
                        {
                            PanelEmptyWithdrawalRequest.Visible = false;
                            RepeaterWithdrawalRequest.DataSource = WithdrawalRequestDAO.displayWithdrawalRequest(Session["staffID"].ToString());
                            RepeaterWithdrawalRequest.DataBind();

                        }
                    }
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

        protected void RepeaterWithdrawalRequest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "trips_Click")
                {
                    string name = e.CommandArgument.ToString();
                    lblTesting.Text = name;
                }
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {

        }

        protected void btnApproved_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                lblTesting.Text = name;
                WithdrawalRequestDAO.approveTripRequestByWithdrawTripRequestID(Convert.ToInt32(name));
                Response.Redirect("/LecturerWithdrawalRequest.aspx");
            }
        }

        protected void btnRejected_Click(object sender, EventArgs e)
        {

        }

        protected void btnRejected_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                lblTesting.Text = name;
                WithdrawalRequestDAO.rejectTripRequestByWithdrawTripRequestID(Convert.ToInt32(name));
                Response.Redirect("/LecturerWithdrawalRequest.aspx");
            }

        }

        protected void RepeaterWithdrawalRequest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            Label tripStatus = (Label)item.FindControl("lblTripStatus");
            Label tripStatus2 = (Label)item.FindControl("lblTripStatus2");
            Button approvedB = (Button)item.FindControl("btnApproved");
            Button rejectedB = (Button)item.FindControl("btnRejected");

            if (tripStatus.Text == "Approved" || tripStatus.Text == "Rejected")
            {
                tripStatus.Visible = true;
                tripStatus2.Visible = true;
                approvedB.Visible = false;
                rejectedB.Visible = false;
            }
            else
            {
                tripStatus.Visible = false;
                tripStatus2.Visible = false;
                approvedB.Visible = true;
                rejectedB.Visible = true;
            }
        }
    }
}