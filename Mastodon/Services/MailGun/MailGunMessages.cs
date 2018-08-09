namespace OsOEasy.Services.MailGun
{
    public static class MailGunMessages
    {

        public const string From_CEO = "Adam@OsoEasyPromo.com";
        public const string From_Support = "Support@OsoEasyPromo.com";
        public const string Email_Signature = "\r\n\r\n" +
                                                    "Happy Promotions!\r\n\r\n" +
                                                    "Adam Shipowick, CEO and Founder";

        #region New Users        
        public const string Subject_NewUser = "{0}, Welcome to OsoEasyPromo";
        public const string Message_NewUser = "Hi {0},\r\n\r\n" +
                                                    "I'm so glad you decided to try www.OsoEasyPromo.com for free!\r\n\r\n" +
                                                    "Make sure to setup your website script so you can start running unlimited custom promotions on your website right away.\r\n\r\n" +
                                                    "I would love to help you answer any questions you may have about our unique service.\r\n" +
                                                    "You can reply directly to this email if you would like.";
        #endregion

        #region Reset Password        
        public const string Subject_ResetPassword = "Password Reset for Oso Easy Promo";
        public const string Message_ResetPassword = "Hi {0}, we heard you need to reset your password. \r\n\r\n";
        public const string Action_ResetPassword = "Please reset your www.OsoEasyPromo.com password by clicking below:\r\n\r\n {0}";
        #endregion

        #region Subscribers
        public const string Subject_New_Subscriber = "{0}, Thanks for subscribing to OsoEasyPromo";
        public const string Message_New_Subscriber = "Hi {0}, \r\n\r\n" + 
                                                            "We're so glad you decided to take the step to become a paying subscriber of OsoEasyPromo! \r\n\r\n" + 
                                                            "Let us know any questions you may have and best of luck with your new promotions.";

        public const string Subject_Upgrade_Subscription = "{0}, Thanks for upgrading your OsoEasyPromo subscription";
        public const string Message_Upgrade_Subscription = "Hi {0}, great choice upgrading your subscription!  We really hope you enjoy our service. \r\n\r\n" +
                                                                "Let us know if you have any questions or concerns.";

        public const string Subject_Downgrade_Subscription = "OsoEasyPromo downgrade subscription confirmation";
        public const string Message_Downgrade_Subscription = "Hi {0}, sorry to see you downgrade your subscription.\r\n" + 
                                                                    "If we can help in any way, please let us know.";

        public const string Subject_Cancel_Subscription = "OsoEasyPromo cancel subscription confirmation";
        public const string Message_Cancel_Subscription = "Hi {0}, sorry to see you cancel your subscription.\r\n" +
                                                                    "If we can help in any way, please let us know.";

        #endregion

    }
}
