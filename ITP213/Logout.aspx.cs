using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
            if (Request.Cookies["authcookie"] != null)
            {
                Response.Cookies["authcookie"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Redirect("login.aspx");
        }
    }
}