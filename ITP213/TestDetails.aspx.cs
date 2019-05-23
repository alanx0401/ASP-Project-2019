using ITP213.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class TestDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["accountType"] != null)
            {
                if (Session["accountType"].ToString() == "lecturer")
                {
                    string id = Request.QueryString["testID"].ToString();
                    Test b = TestDAO.getTestAnswersById(Convert.ToInt32(id));
                    lblQuestionOne.Text = b.qOneAnswer;
                    lblQuestionTwo.Text = b.qTwoAnswer;
                    lblQuestionThree.Text = b.qThreeAnswer;
                    lblQuestionFour.Text = b.qFourAnswer;
                    lblQuestionFive.Text = b.qFiveAnswer;
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
}