using MyFunctions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class StudentParticulars : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "lecturer")
                {
                    
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

        protected void btnGetStudent_Click(object sender, EventArgs e)
        {
            GeneralRecord studObjGen = new GeneralRecord();
            GeneralRecordDAO studDaoGen = new GeneralRecordDAO();
            studObjGen = studDaoGen.getStudentByAdminNo(tbAdminNo.Text);

            AcademicResult studObjAcad = new AcademicResult();
            AcademicResultDAO studDaoAcad = new AcademicResultDAO();
            studObjAcad = studDaoAcad.getStudentByAdminNo(tbAdminNo.Text);

            CCARecord studObjCCA = new CCARecord();
            CCARecordDAO studDaoCCA = new CCARecordDAO();
            studObjCCA = studDaoCCA.getStudentByAdminNo(tbAdminNo.Text);

            if (studObjGen == null)
            {
                PanelErrorResult.Visible = true;
                PanelInfo.Visible = false;
                Lbl_err.Text = "Student record not found!";
            }
            else
            {
                PanelInfo.Visible = true;
                PanelErrorResult.Visible = false;
                PanelGeneralRecord.Visible = true;
                Lbl_StudName.Text = studObjGen.studentName;
                Lbl_AdminNo.Text = studObjGen.adminNo;
                Lbl_Sch.Text = studObjGen.school;
                Lbl_Course.Text = studObjGen.course;
                Lbl_Email.Text = studObjGen.email;
                Lbl_DOB.Text = studObjGen.dateOfBirth.Substring(0, 9);
                Lbl_DietNeeds.Text = studObjGen.dietaryNeeds;
                Lbl_Lang.Text = studObjGen.spokenLanguage;

                PanelAcademicResult.Visible = true;
                Lbl_StudNameAcad.Text = studObjGen.studentName;
                Lbl_AdminNoAcad.Text = studObjAcad.adminNo;
                Lbl_CumGPA.Text = studObjAcad.cumulativeGPA;
                Lbl_Sem1Year1GPA.Text = studObjAcad.sem1Year1GPA;
                Lbl_Sem2Year1GPA.Text = studObjAcad.sem2Year1GPA;
                Lbl_Sem1Year2GPA.Text = studObjAcad.sem1Year2GPA;
                Lbl_Sem2Year2GPA.Text = studObjAcad.sem2Year2GPA;
                Lbl_Sem1Year3GPA.Text = studObjAcad.sem1Year3GPA;
                Lbl_Sem2Year3GPA.Text = studObjAcad.sem2Year3GPA;

                PanelCCARecord.Visible = true;
                Lbl_StudNameCCA.Text = studObjGen.studentName;
                Lbl_AdminNoCCA.Text = studObjCCA.adminNo;
                Lbl_Classification.Text = studObjCCA.classification;
                Lbl_AchievementTitle.Text = studObjCCA.achievementTitle;
                Lbl_Date.Text = studObjCCA.date.Substring(0,9);
                Lbl_Points.Text = studObjCCA.ccaPoints;
            }
        }
    }
}