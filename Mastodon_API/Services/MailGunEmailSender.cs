using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace OsOEasy.API.Services
{
    /// <summary>
    /// MailGun Sending Emails via API
    /// https://documentation.mailgun.com/en/latest/quickstart-sending.html#how-to-start-sending-email
    /// https://documentation.mailgun.com/en/latest/best_practices.html#email-best-practices
    /// </summary>
    public interface IMailGunEmailSender
    {
        Task<IRestResponse> SendMailGunEmailAsync(EmailType emailType, String toAddress, String userName, String promoCode);
    }

    public enum EmailType
    {
        ClaimPromotion,
        PromotionLimitNotice
    }

    public class MailGunEmailSender : IMailGunEmailSender
    {

        private static readonly string From_CEO = "Adam@OsOEasyPromo.com";
        private static readonly string From_Support = "Support@OsOEasyPromo.com";

        private static readonly string Email_Signature = "\r\n\r\n" +
                                                    "Made possible by www.OsOEasyPromo.com";
        private static readonly string Email_Signature_CEO = "\r\n\r\n" +
                                            "Happy Promotions!\r\n\r\n" +
                                            "Adam Shipowick, CEO and Founder";

        #region Claim Promotion    
        private static string Subject_ClaimPromotion = "{0}, Claim your promotion";
        private static string Message_ClaimPromotion = "Hi {0},\r\n\r\n" +
                                                    "Claim your promotion with the promo code: \r\n {1}";
        #endregion

        #region Promotion Limit Notice      
        private static string Subject_LimitNotice = "{0}, Action required, your OsO Easy Promo plan limit has been reached";
        private static string Message_LimitNotice = "Hi {0}, it looks like you have exceeded your current plan limits for accepting promotion entries.  " +
                                                        "No more entries will be accepted unless you upgrade your plan. \r\n" +
                                                        "Please visit your www.OsOEasyPromo.com Dashboard and upgrade your current plan to continue accepting promotion entries.";
        #endregion


        public async Task<IRestResponse> SendMailGunEmailAsync(EmailType emailType, String toAddress, String userName, String promoCode)
        {

            IRestResponse response = null;

            switch (emailType)
            {
                case EmailType.ClaimPromotion:
                    response = await SendMailAsync(emailType, From_Support, toAddress,
                        String.Format(Subject_ClaimPromotion, userName), String.Format(Message_ClaimPromotion, userName, promoCode) + Email_Signature);
                    break;
                case EmailType.PromotionLimitNotice:
                    response = await SendMailAsync(emailType, From_CEO, toAddress,
                        String.Format(Subject_LimitNotice, userName), String.Format(Message_LimitNotice, userName) + Email_Signature_CEO);
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
                Authenticator = new HttpBasicAuthenticator("api", Environment.GetEnvironmentVariable("MAILGUN"))
            };
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "coupon.osoeasypromo.com", ParameterType.UrlSegment);
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
