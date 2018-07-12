using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace OsOEasy.Services.MailGun
{
    /// <summary>
    /// MailGun Sending Emails via API
    /// https://documentation.mailgun.com/en/latest/quickstart-sending.html#how-to-start-sending-email
    /// https://documentation.mailgun.com/en/latest/best_practices.html#email-best-practices
    /// </summary>
    public interface IMailGunEmailSender
    {
        Task<IRestResponse> SendEmailAsync(EmailType emailType, String toAddress, String userName);
        Task<IRestResponse> SendResetPasswordEmailAsync(EmailType emailType, String toAddress, String userName, String callbackURL);
    }

    public enum EmailType
    {
        NewUserSignup,
        ResetPassword,
        NewSubscriber,
        UpgradeSubscription,
        DowngradeSubscription_PaidToPaid,
        DowngradeSubscription_PaidToFree,
        CancelSubscription,
        Unknown
    }

    public class MailGunEmailSender : IMailGunEmailSender
    {

        public async Task<IRestResponse> SendEmailAsync(EmailType emailType, String toAddress, String userName)
        {
            IRestResponse response = null;

            switch (emailType)
            {
                case EmailType.NewUserSignup:
                    response = await SendMailAsync(emailType, MailGunMessages.From_CEO, toAddress,
                        String.Format(MailGunMessages.Subject_NewUser, userName),
                        String.Format(MailGunMessages.Message_NewUser, userName) + MailGunMessages.Email_Signature);
                    break;
                case EmailType.NewSubscriber:
                    response = await SendMailAsync(emailType, MailGunMessages.From_Support, toAddress,
                        String.Format(MailGunMessages.Subject_New_Subscriber, userName),
                        String.Format(MailGunMessages.Message_New_Subscriber, userName) + MailGunMessages.Email_Signature);
                    break;
                case EmailType.UpgradeSubscription:
                    response = await SendMailAsync(emailType, MailGunMessages.From_Support, toAddress,
                        String.Format(MailGunMessages.Subject_Upgrade_Subscription, userName),
                        String.Format(MailGunMessages.Message_Upgrade_Subscription, userName) + MailGunMessages.Email_Signature);
                    break;
                case EmailType.DowngradeSubscription_PaidToPaid:  //eg: gold to silver or silver to bronze
                    //todo, send email to sales/support to try to get them to put user back on a higher paid plan.
                    response = await SendMailAsync(emailType, MailGunMessages.From_Support, toAddress,
                        String.Format(MailGunMessages.Subject_Downgrade_Subscription, userName),
                        String.Format(MailGunMessages.Message_Downgrade_Subscription, userName) + MailGunMessages.Email_Signature);
                    break;
                case EmailType.DowngradeSubscription_PaidToFree:  //eg: Any paid plan to the free plan
                    //todo, send email to sales/support to try to get them to put user back on a paid plan.
                    response = await SendMailAsync(emailType, MailGunMessages.From_Support, toAddress,
                        String.Format(MailGunMessages.Subject_Downgrade_Subscription, userName),
                        String.Format(MailGunMessages.Message_Downgrade_Subscription, userName) + MailGunMessages.Email_Signature);
                    break;
                case EmailType.CancelSubscription:
                    //todo, send email to sales/support to try to get them re-activate a users account.
                    response = await SendMailAsync(emailType, MailGunMessages.From_Support, toAddress,
                        String.Format(MailGunMessages.Subject_Cancel_Subscription, userName),
                        String.Format(MailGunMessages.Message_Cancel_Subscription, userName) + MailGunMessages.Email_Signature);
                    break;
                case EmailType.Unknown:
                    //todo log error
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

        public async Task<IRestResponse> SendResetPasswordEmailAsync(EmailType emailType, String toAddress, String userName, String callbackURL)
        {
            IRestResponse response = null;

            response = await SendMailAsync(emailType, MailGunMessages.From_Support, toAddress, MailGunMessages.Subject_ResetPassword,
                        String.Format(MailGunMessages.Message_ResetPassword, userName) + String.Format(MailGunMessages.Action_ResetPassword, callbackURL) +
                        MailGunMessages.Email_Signature);

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
            request.AddParameter("domain", "support.osoeasypromo.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", from);
            request.AddParameter("to", to);

            //todo, remove this after getting more users....
            //This is so I can follow up with the first users and know when users sign up for plans.
            request.AddParameter("bcc", "Adam@OsoEasyPromo.com");

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