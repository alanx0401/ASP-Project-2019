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
            //currently on old build thus have to use old 
            secManager.apply_session_fixation_patch(Session, Request, Response);

            try
            {
                uuid = Session["accountID"].ToString();
            } catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
                Response.Redirect("Login.aspx");
            }
            
            //string uuid = Session["UUID"].ToString(); // new string when updated

            
            if (!secManager.check_account_admin(uuid))
            {
                Response.Redirect("Error403.aspx");
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