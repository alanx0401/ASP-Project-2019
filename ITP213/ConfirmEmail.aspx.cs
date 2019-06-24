using ITP213.DAL;
using ITP213.Email;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ConfirmEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // first load
            {
                string emailToken = Request.QueryString["x"];

                if (emailToken != null)
                {
                    // SELECT
                    Register obj = RegisterDAO.checkTokenInEmailVerificationTable(emailToken); // if emailToken exist
                    if (obj != null) // token exists
                    {
                        // check if the verification email has expired
                        var currentDateTime = DateTime.Now;
                        var emailDateTimeSend = obj.dateTimeSend;
                        var diff = currentDateTime.Subtract(emailDateTimeSend);

                        string UUID = obj.UUID;

                        if (diff.Hours < 24) // token is still valid, change account verification
                        {
                            int result = RegisterDAO.deleteVerifyEmailOTPTable(UUID);
                            if (result == 1)
                            {
                                int result2 = RegisterDAO.updateEmailVerifiedInAccountTable(UUID);
                                if (result2 == 1)
                                {
                                    lblResult.Text = "Email is successfully verified.";
                                }
                                else
                                {
                                    lblResult.Text = "Sorry! An error has occurred!";
                                }
                            }
                        }
                        else // token expired; resend
                        {
                            int result = DAL.RegisterDAO.deleteVerifyEmailOTPTable(UUID); // delete current expired token
                            if (result == 1)
                            {
                                string randomToken = Guid.NewGuid().ToString(); // email Token

                                // insert
                                int result2 = RegisterDAO.insertIntoVerifyEmail(UUID, randomToken);
                                if (result2 == 1)
                                {
                                    // get account Table
                                    DAL.Login LoginObj = RegisterDAO.getLoginByUUID(UUID);

                                    Execute(LoginObj.name, LoginObj.email, randomToken);
                                    lblResult.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                                }
                                else
                                {
                                    lblResult.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                                }
                                
                                
                            }
                        }
                    }
                    else // token does not exist
                    {
                        lblResult.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                    }
                }
                else // token does not exist
                {
                    lblResult.Text = "We're sorry, the email address verification link you've submitted is invalid, expired, or has already been used.";
                }
            }
        
        }

        public static async Task<SendEmailResponse> Execute(string displayName, string email, string randomToken)
        {
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_API_KEY"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("nyptravel2019@gmail.com", "NYP Travel");
            var subject = "NYP Travel - Email Confirmation";

            var to = new EmailAddress(email, displayName);
            var plainTextContent = "Confirm your account";


            Uri uri = HttpContext.Current.Request.Url;
            string host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
            var htmlContent = "Hi, " + displayName + ". Please confirm your account by clicking <strong><a href=\"" + host + "/ConfirmEmail.aspx/?x=" + randomToken + "\" + >here</a></strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);



            return new SendEmailResponse();
        }
    }
}