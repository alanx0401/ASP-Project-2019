using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class CreateBlog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["accountType"].ToString() == "student" || Session["accountType"].ToString() ==  "parent")
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
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
            
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            if (Session["accountType"].ToString() == "student")
            {
                
                BlogDAO.insertBlog(txtBoxTitle.Text, txtBoxContent.Text, Session["name"].ToString(), Session["adminNo"].ToString(), ddlTripName.SelectedItem.ToString());
                lblMsg.Text = "Blog Published".ToString();
                lblMsg.Visible = true;
                txtBoxContent.Text = "";
                txtBoxTitle.Text = "";
                ddlTripName.Items.Clear();
            }
            

        }
    }
}