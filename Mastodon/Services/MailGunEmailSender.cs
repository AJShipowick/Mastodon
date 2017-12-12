using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace OsOEasy.Services
{
    /// <summary>
    /// MailGun Sending Emails via API
    /// https://documentation.mailgun.com/en/latest/quickstart-sending.html#how-to-start-sending-email
    /// https://documentation.mailgun.com/en/latest/best_practices.html#email-best-practices
    /// </summary>
    public interface IMailGunEmailSender
    {
        Task<IRestResponse> SendMailGunEmailAsync(EmailType emailType, String toAddress, String userName, String callbackURL);
    }

    public enum EmailType
    {
        NewUserSignup,
        ResetPassword
    }

    public class MailGunEmailSender : IMailGunEmailSender
    {

        private static string From_Support = "Support@OsOEasyPromo.com";
        private static string Email_Signature = "\r\n\r\n" +
                                                    "Happy Promotions!\r\n\r\n" +
                                                    "The OsOEasy Team";

        #region New Users        
        private static string Subject_NewUser = "{0}, Welcome to OsoEasyPromo";
        private static string Message_NewUser = "Hi {0},\r\n\r\n" +
                                                    "We're so glad you decided to try OsOEasyPromo.com for free!\r\n\r\n" +
                                                    "Make sure to setup your website script so you can start running unlimited custom promotions on your website right away.\r\n\r\n" +
                                                    "We would love to help you answer any quesitons you may have about our unique service.\r\n" +
                                                    "You can reply directly to this email if you would like.";
        #endregion

        #region Reset Password        
        private static string Subject_ResetPassword = "Password Reset for OsO Easy Promo";
        private static string Message_ResetPassword = "Hi {0}, we heard you need to reset your password. \r\n\r\n";
        private static string Action_ResetPassword = "Please reset your OsOEasyPromo.com password by clicking below:\r\n\r\n {0}";
        #endregion


        public async Task<IRestResponse> SendMailGunEmailAsync(EmailType emailType, String toAddress, String userName, String callbackURL)
        {

            IRestResponse response = null;

            switch (emailType)
            {
                case EmailType.NewUserSignup:
                    response = await SendMailAsync(emailType, From_Support, toAddress,
                        String.Format(Subject_NewUser, userName), String.Format(Message_NewUser, userName) + Email_Signature);
                    //send follow-up emails using 
                    break;
                case EmailType.ResetPassword:
                    response = await SendMailAsync(emailType, From_Support, toAddress, Subject_ResetPassword,
                        String.Format(Message_ResetPassword, userName) + String.Format(Action_ResetPassword, callbackURL) + Email_Signature);
                    break;
                default:
                    break;
            }

            //todo handle responses....log them??....
            if (response != null && response.IsSuccessful)
            {
                //var success = response.ResponseStatus
            }
            else
            {
                var error = response.ErrorMessage;
            }

            return response;

        }

        private Task<IRestResponse> SendMailAsync(EmailType emailType, String from, String to, String subject, String message)
        {
            RestClient client = new RestClient
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api", "key-7dc1f62ef27b4b6d12965cc80ae78b17")
            };
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "support.osoeasypromo.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", from);
            request.AddParameter("to", to);
            request.AddParameter("subject", subject);
            request.AddParameter("text", message);
            request.AddParameter("o:tag", emailType.ToString());


            //Can only send a maxinum of 3 days in the future...
            //https://tools.ietf.org/html/rfc2822.html#section-3.3
            // request.AddParameter ("o:deliverytime", "Fri, 14 Oct 2011 23:10:10 -0000");

            request.Method = Method.POST;
            return client.ExecutePostTaskAsync(request);
        }

    }
}