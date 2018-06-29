using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsOEasy.Services.MailGun
{
    public static class MailGunMessages
    {

        public const string From_CEO = "Adam@OsOEasyPromo.com";
        public const string From_Support = "Support@OsOEasyPromo.com";
        public const string Email_Signature = "\r\n\r\n" +
                                                    "Happy Promotions!\r\n\r\n" +
                                                    "Adam Shipowick, CEO and Founder";

        #region New Users        
        public const string Subject_NewUser = "{0}, Welcome to OsoEasyPromo";
        public const string Message_NewUser = "Hi {0},\r\n\r\n" +
                                                    "I'm so glad you decided to try OsOEasyPromo.com for free!\r\n\r\n" +
                                                    "Make sure to setup your website script so you can start running unlimited custom promotions on your website right away.\r\n\r\n" +
                                                    "I would love to help you answer any quesitons you may have about our unique service.\r\n" +
                                                    "You can reply directly to this email if you would like.";
        #endregion

        #region Reset Password        
        public const string Subject_ResetPassword = "Password Reset for OsO Easy Promo";
        public const string Message_ResetPassword = "Hi {0}, we heard you need to reset your password. \r\n\r\n";
        public const string Action_ResetPassword = "Please reset your OsOEasyPromo.com password by clicking below:\r\n\r\n {0}";
        #endregion

        #region Subscribers
        public const string Subject_Subscriber = "{0}, Thanks for subscribing to OsOEasyPromo";

        #endregion

    }
}
