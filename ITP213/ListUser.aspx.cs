using System;
using System.Collections;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            SecurityDAO secManager = new SecurityDAO();
            //SessionManagerDAO sessionManager = new SessionManagerDAO();
            

            if (!secManager.check_account_admin(Session["UUID"].ToString()))
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
            //foreach (DictionaryEntry entry in e.NewValues)
            //{
            //    if (entry.Key.Equals("accountStatus"))
            //    {
            //        e.NewValues[entry.Key] = ddl_AccType.
            //    }
            //}
            //SqlDataSource1.UpdateCommand = "UPDATE [account] SET name = @name, accountType = @accountType, accountStatus = @accountStatus, email = @email, mobile = @mobile WHERE UUID = @UUID";
            
            
        }

        protected void gv_UserTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowState == DataControlRowState.Edit)
            //{
            //    DropDownList ddlaccType = (e.Row.FindControl("ddl_AccType") as DropDownList);
            //    //ddlaccType.Items.Insert(0, new ListItem("Please select"));
            //    string accType = (e.Row.FindControl("lbl_accountType") as Label).Text;
            //    ddlaccType.Items.FindByValue(accType).Selected = true;
            //}
            
        }
    }
}