using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class CreateAnnouncement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (!IsPostBack) // first time coming to the page
                {
                    if (Session["accountType"] != null)
                    {

                        if (Session["accountType"].ToString() == "lecturer")
                        {

                            ddlTripName.DataSource = AnnouncementDAO.displayONGOINGAllocatedTrips(Session["staffID"].ToString());
                            ddlTripName.Items.Insert(0, new ListItem("--Select Trip--", "0"));
                            ddlTripName.AppendDataBoundItems = true;
                            ddlTripName.DataTextField = "tripNameAndTripType";
                            ddlTripName.DataValueField = "tripID";
                            ddlTripName.DataBind();

                            string id = Request.QueryString["AnnouncementID"];

                            if (id != null)
                            {
                                lblTitle.Text = "Update Announcement";
                                lblTitle2.Text = "Update Announcement";
                                btnCreate.Text = "Update";

                                Announcement obj = DAL.AnnouncementDAO.getAnnouncementByAnnouncementID(Convert.ToInt32(id));

                                tbTitle.Text = obj.announcementTitle;
                                tbMessage.Text = obj.announcementMessage;
                                tbTimeDue.Text = obj.timeDue;
                                if (obj.studentView == "True")
                                {
                                    cbStudents.Checked = true;
                                }
                                if (obj.lecturerView == "True")
                                {
                                    cbLecturers.Checked = true;
                                }

                                ddlTripName.Items.FindByValue(obj.tripID.ToString()).Selected = true;
                                //ddlTripName.Items.FindByValue(obj.tripID.ToString()).Selected = true;
                                //DropDownList1.Items.FindByValue("SOMECREDITPROBLEMS").Selected = true;
                                //tbTitle.Text = a.announcementTitle;
                                //tbMessage.Text = a.announcementMessage;
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
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (ValidateAnnouncement())
            {
                if (btnCreate.Text == "Update")
                {
                    string id = Request.QueryString["AnnouncementID"];
                    string msg = tbMessage.Text;
                    string title = tbTitle.Text;

                    string studentView = "False";
                    string lecturerView = "False";
                    if (cbStudents.Checked == true)
                    {
                        studentView = "True";
                    }
                    if (cbLecturers.Checked == true)
                    {
                        lecturerView = "True";
                    }
                    //tbTitle.Text, tbMessage.Text, Convert.ToInt32(ddlTripName.SelectedValue.ToString()), Session["staffID"].ToString(), studentView, lecturerView, tbTimeDue.Text
                    AnnouncementDAO.updateById(Convert.ToInt32(ddlTripName.SelectedValue.ToString()), Convert.ToInt32(id), title, msg, Convert.ToDateTime(tbTimeDue.Text), studentView, lecturerView);
                    Response.Redirect("ViewAnnouncement.aspx");
                }

                else if (btnCreate.Text == "Create")
                {
                    string studentView = "False";
                    string lecturerView = "False";
                    if (cbStudents.Checked == true)
                    {
                        studentView = "True";
                    }
                    if (cbLecturers.Checked == true)
                    {
                        lecturerView = "True";
                    }
                    AnnouncementDAO.insertAnnouncement(tbTitle.Text, tbMessage.Text, Convert.ToInt32(ddlTripName.SelectedValue.ToString()), studentView, lecturerView, tbTimeDue.Text, Session["staffID"].ToString());
                    panelSuccess.Visible = true;
                    panelAlert.Visible = false;
                    lblSuccess.ForeColor = System.Drawing.Color.Green;
                    lblSuccess.Text = "Announcement created successfully!!";
                    Response.Redirect("ViewAnnouncement.aspx");
                }
            }
            
        }

        private bool ValidateAnnouncement()
        {
            bool result = false;
            lblMsg.Text = String.Empty;

            panelAlert.Visible = true;
            panelSuccess.Visible = false;

            if (ddlTripName.SelectedItem.Value == "0")
            {
                lblMsg.Text += "Please select a trip" + "<br>";
                ddlTripName.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8D7DA");
                ddlTripName.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E6707B");
            }
            else
            {
                ddlTripName.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ddlTripName.BorderColor = System.Drawing.ColorTranslator.FromHtml("#CED4DA");
            }

            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                lblMsg.Text += "Please write your title" + "<br>";
                tbTitle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8D7DA");
                tbTitle.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E6707B");
            }
            else
            {
                tbTitle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                tbTitle.BorderColor = System.Drawing.ColorTranslator.FromHtml("#CED4DA");
            }

            if (string.IsNullOrEmpty(tbMessage.Text))
            {
                lblMsg.Text += "Please write your message" + "<br>";
                tbMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8D7DA");
                tbMessage.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E6707B");
            }
            else
            {
                tbMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                tbMessage.BorderColor = System.Drawing.ColorTranslator.FromHtml("#CED4DA");
            }

            DateTime temp;
            if (!DateTime.TryParse(tbTimeDue.Text, out temp))
            {
                lblMsg.Text += "Invalid DateTime format!" + "<br>";
                tbTimeDue.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8D7DA");
                tbTimeDue.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E6707B");
            }
            else
            {
                tbTimeDue.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                tbTimeDue.BorderColor = System.Drawing.ColorTranslator.FromHtml("#CED4DA");
            }

            DateTime temp2;
            if (tbTimeDue.Text != null && DateTime.TryParse(tbTimeDue.Text, out temp2))
            {
                var timeDue = DateTime.Parse(tbTimeDue.Text);
                var currentTime = DateTime.Now;

                if (timeDue < currentTime)
                {
                    lblMsg.Text += "Due time has already passed!";
                    tbTimeDue.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8D7DA");
                    tbTimeDue.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E6707B");
                }
                else
                {
                    tbTimeDue.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    tbTimeDue.BorderColor = System.Drawing.ColorTranslator.FromHtml("#CED4DA");
                }
            }


            if (cbLecturers.Checked == false && cbStudents.Checked == false)
            {
                lblMsg.Text += "Please check who can see the announcement" + "<br>";
                lblCbValidation.Visible = true;
            }
            else
            {
                lblCbValidation.Visible = false;
            }


            
            result = String.IsNullOrEmpty(lblMsg.Text);
            return result;
        }
    }
}