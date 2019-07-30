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
//test
namespace ITP213
{
    public partial class ForgetPasswordWindow1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetpw_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select email,password from account where email=@email";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@email", tbEmail.Text);
            sqlconn.Open();
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                string username = sdr["email"].ToString();
                string password = sdr["password"].ToString();

                MailMessage mm = new MailMessage("Someonesemail@gmail.com", tbEmail.Text);
                mm.Subject = "Your new password";
                mm.Body=string.Format("Hello : <h1>{0}</h1>, your password is <h1>{1}</h1>", username, password);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential();
                nc.UserName = "Someonesemail@gmail.com";
                nc.Password = "somepassword";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Port = 587;
                smtp.Send(mm);
                LabelMsg.Text = "Your password has been sent to" + tbEmail.Text;
                LabelMsg.ForeColor = Color.Green;
            }
            else
            {
                LabelMsg.Text = "Email does not exist";
                LabelMsg.ForeColor = Color.Red;
            }

        }
    }
}