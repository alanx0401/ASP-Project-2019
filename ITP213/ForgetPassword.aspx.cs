using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            string email, pw;
            email = tbEmail.Text;
            pw = tbNewpassword.Text;

        }
    }
}