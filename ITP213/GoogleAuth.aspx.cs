using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class GoogleAuth : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UUID"] != null)
                {
                    DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());
                    if (obj != null)
                    {
                        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();// settings
                                                                                  // secret key --> UUID
                        string secretKey = Guid.NewGuid() + Session["UUID"].ToString();
                        Session["sk"] = secretKey;

                        var setupInfo = tfa.GenerateSetupCode("NYP Travel", obj.email, secretKey, 200, 200); //the width and height of the Qr Code);

                        string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl; //  assigning the Qr code information + URL to string
                        string manualEntrySetupCode = setupInfo.ManualEntryKey; // show the Manual Entry Key for the users that don't have app or phone
                        Image1.ImageUrl = qrCodeImageUrl;// showing the qr code on the page "linking the string to image element"
                        Label1.Text = manualEntrySetupCode; // showing the manual Entry setup code for the users that can not use their phone
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            verifyGoogleAuth();
        }

        public void verifyGoogleAuth()
        {
            DAL.Settings obj = DAL.SettingsDAO.getAccountTableByUUID(Session["UUID"].ToString());

            if (obj != null)
            {
                string user_enter = TextBox1.Text; // store this password in db when it loads

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

                if (Session["sk"] != null)
                {
                    bool isCorrectPIN = tfa.ValidateTwoFactorPIN(Session["sk"].ToString(), user_enter);

                    /*string[] allPINS = tfa.GetCurrentPINs(Session["sk"].ToString());

                    for (int i = 0; i < allPINS.Length; i++)
                    {
                        Label2.Text += i.ToString() + " :" + allPINS[i] + ", <br>";
                    }*/

                    if (isCorrectPIN == true)
                    {
                        int result = DAL.SettingsDAO.updateGoogleAuthEnabledAndSecretKeyInAccount(Session["UUID"].ToString(), Session["sk"].ToString());
                        Label2.Text = "Done! You're all set!";

                    }
                    else
                    {

                        Label2.Text = "Incorrect password. Please try again";
                    }
                }
                else
                {
                    Label2.Text = "Sorry! An error has ocurred!";
                }
            }
        }
    }
}