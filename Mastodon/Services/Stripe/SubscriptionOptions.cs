namespace OsOEasy.Services.Stripe
{

    /// <summary>
    //  The PlanId variables are set via the Stipe service and must match the exact stripe plan id
    /// </summary>
    public static class SubscriptionOptions
    {
        public const string FreeAccount = "Free Account";
        public const int MaxFreeAccountClaims = 25;

        public const string Bronze = "Bronze";
        public const string BronzePlanID = "plan_D80DmkSonO4avA";
        public const int MaxBronzeAccountClaims = 500;

        public const string Silver = "Silver";
        public const string SilverPlanID = "plan_D7upPcd0GVyzgi";
        public const int MaxSilverAccountClaims = 2000;

        //This special Enterprise plan should be billed seperatly through Stripe
        public const string Gold = "Gold";
        //Unlimited claims per month        
    }
}