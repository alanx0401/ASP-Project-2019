using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyFunctions.DAL;

namespace ITP213
{
    public partial class InformationSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "lecturer")
                {
                    AcademicResultDAO acadDAO = new AcademicResultDAO();
                    List<AcademicResult> tdList = new List<AcademicResult>();
                    tdList = acadDAO.getTDbyAdminNo();
                    GridView1.DataSource = tdList;
                    GridView1.DataBind();

                    GeneralRecordDAO genDAO = new GeneralRecordDAO();
                    List<GeneralRecord> genList = new List<GeneralRecord>();
                    genList = genDAO.getTDbyAdminNo();
                    GridView2.DataSource = genList;
                    GridView2.DataBind();
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

        protected void DropDownListSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListSummary.SelectedValue == "Acad")
            {
                PanelAcademicSummary.Visible = true;
                PanelDietSummary.Visible = false;
            }
            else if (DropDownListSummary.SelectedValue == "Diet")
            {
                PanelAcademicSummary.Visible = false;
                PanelDietSummary.Visible = true;
            }
            else
            {
                PanelAcademicSummary.Visible = false;
                PanelDietSummary.Visible = false;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            AcademicResultDAO acadDAO = new AcademicResultDAO();
            List<AcademicResult> tdList = new List<AcademicResult>();
            tdList = acadDAO.getTDbyAdminNo();
            GridView1.DataSource = tdList;
            GridView1.DataBind();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            GeneralRecordDAO genDAO = new GeneralRecordDAO();
            List<GeneralRecord> genList = new List<GeneralRecord>();
            genList = genDAO.getTDbyAdminNo();
            GridView2.DataSource = genList;
            GridView2.DataBind();
        }
    }
}