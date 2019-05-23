using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] != null)
            {
                /*lblTitle.Text = $"accountID:{Session["accountID"].ToString()}, accountType:{Session["accountType"].ToString()},name:{Session["name"].ToString()}, email:{Session["email"].ToString()}, mobile:{Session["mobile"].ToString()}, dateOfBirth:{Session["dateOfBirth"].ToString()},";
                if (Session["accountType"].ToString() == "parent")
                {
                    lblTitle.Text = Session["adminNo"].ToString();
                }
                else if (Session["accountType"].ToString() == "lecturer")
                {
                    lblTitle.Text = $"staffID:{Session["staffID"].ToString()}, lecturerSchool: {Session["school"].ToString()}, staffRole: {Session["staffRole"].ToString()}";
                }
                else if (Session["accountType"].ToString() == "student")
                {
                    lblTitle.Text = $"adminNo:{Session["adminNo"]}";
                }*/

            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}