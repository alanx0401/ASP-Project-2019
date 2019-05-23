using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class CreateTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "student")
                {

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["accountType"].ToString() == "student")
            {
                if (Request.QueryString["tripID"] != null)
                {
                    int tripID = Convert.ToInt32(Request.QueryString["tripID"]);
                    TestDAO.insertTestAnswers(tripID,Session["name"].ToString(),Session["adminNo"].ToString(), radioButtonListQ1.SelectedItem.ToString(), radioButtonListQ2.SelectedItem.ToString(), txtBoxQ3.Text, txtBoxQ4.Text, txtBoxQ5.Text);
                    Response.Redirect("/ViewIndividualTrip.aspx?tripID=" + tripID);

                }
                
            }
        }
    }
}