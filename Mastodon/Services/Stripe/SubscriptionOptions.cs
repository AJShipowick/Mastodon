namespace OsOEasy.Services.Stripe
{

    /// <summary>
    //  The PlanId variables are set via the Stipe service and must match the exact stripe plan id
    /// </summary>
    public static class SubscriptionOptions
    {
        public const string FreeAccount = "Free Account";
        public const int MaxFreeAccountClaims = 100;

        public const string Bronze = "Bronze";
        public const string BronzePlanID = "plan_D80DmkSonO4avA";
        public const int MaxBronzeAccountClaims = 1500;

        public const string Silver = "Silver";
        public const string SilverPlanID = "plan_D7upPcd0GVyzgi";
        public const int MaxSilverAccountClaims = 5000;

        public const string Gold = "Gold";
        public const string GoldPlanID = "plan_D7uqUOIyi04VU7";
        //Unlimited claims per month        
    }
}