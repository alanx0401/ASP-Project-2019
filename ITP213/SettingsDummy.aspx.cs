using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class SettingsDummy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator(); // settings
            // secret key --> UUID
            var setupInfo = tfa.GenerateSetupCode("NYP Travel", "Your email", "SuperSecretKeyGoesHere1", 300, 300); //the width and height of the Qr Code);


            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl; //  assigning the Qr code information + URL to string
            string manualEntrySetupCode = setupInfo.ManualEntryKey; // show the Manual Entry Key for the users that don't have app or phone
            Image1.ImageUrl = qrCodeImageUrl;// showing the qr code on the page "linking the string to image element"
            Label1.Text = manualEntrySetupCode; // showing the manual Entry setup code for the users that can not use their phone
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string user_enter = TextBox1.Text; // store this password in db when it loads
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            bool isCorrectPIN = tfa.ValidateTwoFactorPIN("SuperSecretKeyGoesHere1", user_enter);

            // string[] GetCurrentPINs(string accountSecretKey)
            string[] allPINS = tfa.GetCurrentPINs("SuperSecretKeyGoesHere1");

            for (int i=0; i<allPINS.Length; i++)
            {
                Label2.Text += i.ToString()+ " :" + allPINS[i] + ", <br>";
            }
            
            if (isCorrectPIN == true)
            {
                Label2.Text = tfa.GetCurrentPIN("SuperSecretKeyGoesHere1", DateTime.Now);
                //Label2.Text = "i am cool";

            }
            else
            {

                //Label2.Text = "i am Fool";
            }
        }
    }
}