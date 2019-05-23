using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class CreateReflection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "student")
                {
                    if (!IsPostBack)
                    {
                        ddlTripName.DataSource = TripAllocationDAO.displayTripsBasedOnAdminNo(Session["adminNo"].ToString(), "Internship");
                        ddlTripName.Items.Insert(0, new ListItem("--Select Trip", "0"));
                        ddlTripName.AppendDataBoundItems = true;
                        ddlTripName.DataTextField = "tripName";
                        ddlTripName.DataValueField = "tripID";
                        ddlTripName.DataBind();
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

        protected void btnSubmitReflecction_Click(object sender, EventArgs e)
        {
            InternshipDAO.insert(tbReflection.Text, Session["adminNo"].ToString(), "SG");

            Response.Redirect("Default.aspx");
        }
    }
}