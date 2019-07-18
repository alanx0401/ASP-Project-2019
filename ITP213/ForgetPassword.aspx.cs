using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Net;
using System.Net.Mail;
namespace ITP213
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetpw_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionString["ConnStr"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select email,password from account where email=@email";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@email", tbEmail.Text);
            sqlconn.Open();
            SqlDataReader sdr = sqlcomm.ExecuteReader();

            if (sdr.Read())
            {
                string useremail = sdr["email"].ToString();
                string userpassword = sdr["password"].ToString();

                MailMessage mm = new MailMessage("example@gmail.com", tbEmail.Text);
                
            }
        }
    }
}