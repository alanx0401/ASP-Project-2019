using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ViewComplain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "lecturer")
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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void deleteCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "delete_Click")
            {
                string ID = e.CommandArgument.ToString();
                lblTitle.Text = ID;
                ComplainDAO.updateByComplainID(Convert.ToInt32(ID));
                Response.Redirect("ViewComplain.aspx");
            }
        }

        protected void deleteCommand1(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "delete_Click")
            {
                string ID = e.CommandArgument.ToString();
                lblTitle.Text = ID;
                ComplainDAO.updateByComplainID(Convert.ToInt32(ID));
                Response.Redirect("ViewComplain.aspx");
            }
        }
    }
}