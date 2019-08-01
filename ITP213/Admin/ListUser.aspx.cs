using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITP213.DAL;

namespace ITP213
{
    public partial class ListUser : System.Web.UI.Page
    {
        string uuid;
        protected void Page_Load(object sender, EventArgs e)
        {
            SecurityDAO secManager = new SecurityDAO();
            //SessionManagerDAO sessionManager = new SessionManagerDAO();
               
            if (!secManager.check_account_admin(uuid))
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void gv_UserTable_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gv_UserTable_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (true)
            {
                SqlDataSource1.UpdateCommand = "";
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}