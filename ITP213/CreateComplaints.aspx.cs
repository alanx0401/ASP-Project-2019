using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class CreateComplaints : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "student" || Session["accountType"].ToString() == "parent")
                {
                    ddlTripName.DataSource = WithdrawalRequestDAO.displayAllocatedTrips(Session["adminNo"].ToString());
                    ddlTripName.AppendDataBoundItems = true;
                    ddlTripName.DataTextField = "tripNameAndTripType";
                    ddlTripName.DataValueField = "tripID";
                    ddlTripName.DataBind();

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

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            string s = "Images/" + FileUpload1.FileName;
            ComplainDAO.insertComplain(txtBoxSubject.Text, txtBoxComments.Text, Session["name"].ToString(), Session["adminNo"].ToString(), ddlTripName.SelectedItem.ToString(),s,txtBoxComplainType.Text);
            FileUpload1.SaveAs(Server.MapPath("~/Images/") + FileUpload1.FileName);
            txtBoxSubject.Text = "";
            txtBoxComments.Text = "";
            txtBoxComplainType.Text = "";
            Label2.Text = "Complaint submitted";
            Label2.Visible = true;
            ddlTripName.Items.Clear();

            Response.Redirect("/CreateComplaints.aspx");

        }
    }
 }