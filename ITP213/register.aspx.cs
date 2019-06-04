using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            PanelPart1.Visible = false;
            btnNext.Visible = false;
            PanelPart2.Visible = true;
            btnBack.Visible = true;
            btnRegister.Visible = true;
            Label1.Text = "Verifying your phone number";
            lblLogin.Visible = false;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            PanelPart1.Visible = true;
            btnNext.Visible = true;
            PanelPart2.Visible = false;
            btnBack.Visible = false;
            btnRegister.Visible = false;
            Label1.Text = "Register";
            lblLogin.Visible = true;
        }
    }
}