using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class CreateRemarks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "lecturer")
                {
                    if (Request.QueryString["injuryReportID"] != null)
                    {
                        DAL.ReportInjury obj = ReportInjuryDAO.getRemarkByinjuryReportID(Convert.ToInt32(Request.QueryString["injuryReportID"]));
                        //tbRemarks.Text = obj.remark.ToString();

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

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            ReportInjuryDAO.updateRemarksByInjuryReportID(Convert.ToInt32(Request.QueryString["injuryReportID"].ToString()), tbRemarks.Text);
            //Label1.Text = tbRemarks.Text;

            Response.Redirect("/ViewInjuryReport.aspx");
        }
    }
}