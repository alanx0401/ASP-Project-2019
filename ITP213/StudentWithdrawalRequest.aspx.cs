using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class StudentWithdrawalRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["tripID"] != null)
                    {
                        ddlTripName.SelectedValue = Request.QueryString["tripID"].ToString();

                    }
                    if (Session["accountType"].ToString() == "student" || Session["accountType"].ToString() == "parent")
                    {
                        lblAdminNo.Text += Session["adminNo"].ToString();

                        ddlTripName.DataSource = WithdrawalRequestDAO.displayAllocatedPendingTrips(Session["adminNo"].ToString());
                        ddlTripName.Items.Insert(0, new ListItem("--Select Trip--", "0"));
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

        protected void tbReasons_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateAnnouncement())
            {
                panelError.Visible = false;
                tbReasons.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                tbReasons.BorderColor = System.Drawing.ColorTranslator.FromHtml("#CED4DA");
                WithdrawalRequestDAO.insert(tbReasons.Text, lblAdminNo.Text, Convert.ToInt32(ddlTripName.SelectedValue));
                Response.Redirect("/Default.aspx");
            }
        }

        private bool ValidateAnnouncement()
        {
            bool result = false;
            lblError.Text = String.Empty;

            panelError.Visible = true;

            if (ddlTripName.SelectedItem.Value == "0")
            {
                lblError.Text += "Please select a trip" + "<br>";
                ddlTripName.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8D7DA");
                ddlTripName.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E6707B");
            }
            else
            {
                ddlTripName.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ddlTripName.BorderColor = System.Drawing.ColorTranslator.FromHtml("#CED4DA");
            }
            
            if (String.IsNullOrEmpty(tbReasons.Text))
            {
                lblError.Text += "Please state your reason" + "<br>";
                tbReasons.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8D7DA");
                tbReasons.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E6707B");
            }
            else
            {
                tbReasons.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                tbReasons.BorderColor = System.Drawing.ColorTranslator.FromHtml("#CED4DA");
            }


            result = String.IsNullOrEmpty(lblError.Text);
            return result;
        }
    }
}