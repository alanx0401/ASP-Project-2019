using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ManageYourAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            if (Session["UUID"] != null)
            {
                DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
                if (obj != null)
                {

                    lblPhoneNumber.Text = obj.mobile;
                    HyperLinkPhoneNum.NavigateUrl = "/OneTimePassword.aspx";
                    lblVerifiedPhoneStatus.Text = obj.phoneVerified;
                    if (obj.phoneVerified == "Yes") 
                    {
                        lblVerifiedPhoneStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblVerifiedPhoneStatus.ForeColor = System.Drawing.Color.Red;
                    }

                    if (obj.googleAuthEnabled == "Yes")
                    {
                        lblGoogleAuth.Text = "Enabled";
                        HyperLinkGoogleAuth.Text = "Change phone";
                        HyperLinkGoogleAuth.NavigateUrl = "/GoogleAuth.aspx";
                    }
                    else
                    {
                        lblGoogleAuth.Text = "Not enabled";
                        HyperLinkGoogleAuth.Text = "Add phone";
                        HyperLinkGoogleAuth.NavigateUrl = "/GoogleAuth.aspx";
                    }
                    if (obj.phoneVerified == "Yes") // only allow user to enable OTP 2FA when the phone has been verified
                    {
                        Panel1.Visible = true;
                        if (obj.otpEnabled == "Yes")
                        {
                            lblOTP.Text = "Enabled";
                            LinkButtonOTP.Text = "Disable"; // change number and also change number from db
                        }
                        else
                        {
                            lblOTP.Text = "Disabled";
                            LinkButtonOTP.Text = "Enable"; // use the database's number and add it.
                        }
                    
                    }
                    else
                    {
                        Panel1.Visible = false;
                        lblResult.Text = "To enable OTP, please verify your phone";
                    }
                }
            }
            else
            {
                Response.Redirect("/Default.aspx");
            }
            //}
        }

        protected void LinkButtonOTP_Click(object sender, EventArgs e)
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
            if (obj != null)
            {
                // check is otp is enabled or not;
                if (obj.otpEnabled == "Yes") // Wants to remove 2FA
                {
                    int result = DAL.SettingsDAO.updateOTPEnabledInAccount(Session["UUID"].ToString(), "No");
                    
                    if (result == 1)
                    {
                        lblResult.Text = "You have successfully disabled OTP";
                    }
                    else
                    {
                        lblResult.Text = "Sorry! An error has ocurred. Please try again later.";
                    }
                    
                }
                else // Wants to enable 2FA
                {
                    int result = DAL.SettingsDAO.updateOTPEnabledInAccount(Session["UUID"].ToString(), "Yes");
                    if (result == 1)
                    {
                        lblResult.Text = "You have successfully enabled OTP";
                    }
                    else
                    {
                        lblResult.Text = "Sorry! An error has ocurred. Please try again later.";
                    }
                }

                Response.Redirect("/ManageYourAccount.aspx"); //To see the updated result
            }
            else
            {
                lblResult.Text = "Sorry! An error has ocurred. Please try again later.";
            }
        }
    }
}