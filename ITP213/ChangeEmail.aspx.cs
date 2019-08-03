using ITP213.Email;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITP213
{
    public partial class ChangeEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbMessage.Attributes.Add("autocomplete", "off");
            tbEmail.Attributes.Add("autocomplete", "off");
        }

        protected void btnPanel1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (ReCaptchContainer.Visible == true)
                    {
                        if (IsReCaptchValid() == true)
                        {
                            StringBuilder sb = new StringBuilder(
                            HttpUtility.HtmlEncode(tbMessage.Text));

                            sb.Replace("&lt;i&gt;", "<i>");
                            sb.Replace("&lt;/i&gt;", "");
                            Execute(tbEmail.Text.Trim(), sb.ToString());
                            Response.Redirect("/login.aspx");
                        }
                        else
                        {
                            lblError.Text = "Captcha value is not valid";
                        }
                    }
                    else
                    {
                        ReCaptchContainer.Visible = true;
                    }
                }
            }
            catch
            {
                lblError.Text = "Sorry! An error has occurred. Please try again.";
            }
            finally { }
            
        }

        public bool IsReCaptchValid() // CAPTCHA
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = Environment.GetEnvironmentVariable("SecretKey");
            //var secretKey = ConfigurationManager.AppSettings["SecretKey"];
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }

        public static async Task<SendEmailResponse> Execute(string email, string bodyMessage)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            //var apiKey = ConfigurationManager.AppSettings["SENDGRID_API_KEY"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(email, "User");
            var subject = "Request to change email as it is created incorrectly.";
            
            var to = new EmailAddress("nyptravel2019@gmail.com", "NYP Travel");
            var plainTextContent = "Confirm your account";

            StringBuilder sb = new StringBuilder(
                            HttpUtility.HtmlEncode(bodyMessage));


            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, sb.ToString());
            var response = await client.SendEmailAsync(msg);



            return new SendEmailResponse();
        }
    }
}