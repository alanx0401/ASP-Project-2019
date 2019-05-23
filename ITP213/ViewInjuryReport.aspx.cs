using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ViewInjuryReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (!Page.IsPostBack)
                {
                    if (Session["accountType"].ToString() == "lecturer")
                    {
                        if (ReportInjuryDAO.getAllReports() == null)
                        {
                            PanelViewInjury.Visible = true;
                        }
                        else
                        {
                            PanelViewInjury.Visible = false;
                            RepeaterViewInjury.DataSource = ReportInjuryDAO.getAllReports();
                            RepeaterViewInjury.DataBind();
                        }
                    }
                    else
                    {
                        Response.Redirect("/UnauthorizedErrorPage.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
            
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                lblTesting.Text = name;
                Response.Redirect("/ReportInjury.aspx?injuryReportID=" + name);
            }
        }

        protected void btnDelete_command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "trips_Click")
            {
                string name = e.CommandArgument.ToString();
                lblTesting.Text = name;
                DAL.ReportInjuryDAO.deleteById(Convert.ToInt32(name));
                Response.Redirect("/ViewInjuryReport.aspx");
            }
        }

        protected void RepeaterViewInjury_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string lblStudentName;
            string lblDateTimeInjury;
            string lblLocation;
            string lblTrip;
            string lblNatureOfInjury;
            string lblCauseOfInjury;
            string lblLocationOnBody;
            string lblAgency;
            string lblFirstAidGiven;
            string lblFirstAiderName;
            string lblTreatment;
            RepeaterItem item = e.Item;
            Label tempL = (Label)item.FindControl("Label20");
            string createdBy = tempL.Text;
            string currentID = Session["staffID"].ToString();

            Button editB = (Button)item.FindControl("btnStudyTripsEdit");
            Button deleteB = (Button)item.FindControl("btnStudyTripsDelete");
            // btnAddRemarks
            Button remarksB = (Button)item.FindControl("btnAddRemarks");
            /*Label lblSendDate1 = (Label)item.FindControl("lblSendDate");
            Label lblRemark1 = (Label)item.FindControl("lblRemark");

            Panel PanelInjuryReportDate1 = (Panel)item.FindControl("PanelInjuryReportDate");
            Panel PanelRemark1 = (Panel)item.FindControl("PanelRemark");

            if (String.IsNullOrEmpty(lblSendDate1.Text) || String.IsNullOrWhiteSpace(lblSendDate1.Text))
            {
                PanelInjuryReportDate1.Visible = false;
            }
            else
            {
                PanelInjuryReportDate1.Visible = true;
            }

            if (String.IsNullOrEmpty(lblSendDate1.Text) || String.IsNullOrWhiteSpace(lblSendDate1.Text))
            {
                PanelRemark1.Visible = false;
            }
            else
            {
                PanelRemark1.Visible = true;
            }*/

            int i = 0;

            if (createdBy == currentID)
            {
                editB.Visible = true;
                deleteB.Visible = true;
                remarksB.Visible = true;
            }
            else
            {
                editB.Visible = false;
                deleteB.Visible = false;
                remarksB.Visible = false;
            }
        }

        protected void btnSendReportViaSMS_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendReport_command(object sender, CommandEventArgs e)
        {

            if (e.CommandName == "informParent")
            {
                string injuryReportID = e.CommandArgument.ToString();

                DAL.ReportInjury obj = ReportInjuryDAO.getInjuryReportByID(Convert.ToInt32(injuryReportID));
                string studentName = obj.studentName.Trim();
                string dateTimeInjury = obj.dateTimeOfInjury.Trim();
                string location = obj.location.Trim();
                string trip = obj.tripName; //kiv
                string natureOfInjury = obj.natureOfInjury.Trim();
                string causeOfInjury = obj.causeOfInjury.Trim();
                string locationOnBody = obj.locationOnBody.Trim();
                string agency = obj.agency.Trim();
                string firstAidGiven = obj.firstAidGiven.Trim();
                string firstAiderName = obj.firstAiderName.Trim();
                string treatment = obj.treatment.Trim();

                lblTesting.Text = $"Dear Parents,<br>" +
                        $"{studentName} has been injured on {dateTimeInjury}." +
                        $"Below are the details:<br>Location: {location}"
                        + $"<br>Nature of injury: {natureOfInjury}"
                        + $"<br>Cause of injury: {causeOfInjury}"
                        + $"<br>Location: {locationOnBody}" + $"<br>Agency: {agency}"
                        + $"<br>First Aid Given: {firstAidGiven}"
                        + $"<br>First Aider name: {firstAiderName}"
                        + $"<br>Treatment: {treatment}";
                string msg = $"Dear Parents," +
                        $"{studentName} has been injured on {dateTimeInjury}." +
                        $"Below are the details:Location: {location}, "
                        + $"Nature of injury: {natureOfInjury}, "
                        + $"Cause of injury: {causeOfInjury}, "
                        + $"Location: {locationOnBody}, " + $"Agency: {agency}, "
                        + $"First Aid Given: {firstAidGiven}, "
                        + $"First Aider name: {firstAiderName}, "
                        + $"Treatment: {treatment}";
                //=================================================================================
                // ***************************** DO NOT DELETE 
                DAL.ReportInjury mobileObj = ReportInjuryDAO.getParentMobileByReportID(Convert.ToInt32(injuryReportID));
                string mobile = mobileObj.parentNum;

                int result = ReportInjuryDAO.updateSendDateById(Convert.ToInt32(injuryReportID));
                SMSSvrRef.SMSSoapClient S = new SMSSvrRef.SMSSoapClient();
                try
                {
                    string display = S.sendMessage("EADPJ47", "061785", mobile, msg);
                    lblTesting.Text = display;
                }
                catch (Exception error)
                {
                    lblTesting.Text = error.Message;
                }
                //=================================================================================
                Response.Redirect("/ViewInjuryReport.aspx");
            }

        }

        protected void btnAddRemarks_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AddRemarks")
            {
                string injuryReportID = e.CommandArgument.ToString();

                Response.Redirect("/CreateRemarks.aspx?injuryReportID=" + injuryReportID);

                //int result = ReportInjuryDAO.updateRemarksByInjuryReportID(Convert.ToInt32(injuryReportID), tbRe)
            }
        }
    }
}