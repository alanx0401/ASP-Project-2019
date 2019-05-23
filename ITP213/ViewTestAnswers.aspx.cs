using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ViewTestAnswers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "lecturer")
                {
                    if (!IsPostBack)
                    {
                        GridView2.DataSource = TestDAO.getAllTests();
                        GridView2.DataBind();
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
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string id = GridView2.Rows[index].Cells[0].Text;
                Response.Redirect("TestDetails.aspx?testID=" + id);
            }
        }
    }
}