using OsOEasy.Models;
using System;

namespace OsOEasy_API.Services
{

    public interface ISubscriptionService
    {
        bool SubscriptionActiveAndWithinTrafficLimit(ApplicationUser user);
    }

    public class SubscriptionService : ISubscriptionService
    {
        public bool SubscriptionActiveAndWithinTrafficLimit(ApplicationUser user)
        {

            if (user.AccountSuspended) { return false; }

            int promoClaimsThisMonth = user.ClaimsPerMonth[DateTime.Now];
            switch (user.SubscriptionPlan)
            {
                case SubscriptionOptions.FreeAccount:
                    //0-100 Claims a month
                    return promoClaimsThisMonth <= SubscriptionOptions.MaxFreeAccountClaims;
                case SubscriptionOptions.Bronze:
                    //Up to 1,500 Claims a month
                    return promoClaimsThisMonth <= SubscriptionOptions.MaxBronzeAccountClaims;
                case SubscriptionOptions.Silver:
                    //Up to 5,000 Claims a month
                    return promoClaimsThisMonth <= SubscriptionOptions.MaxSilverAccountClaims;
                case SubscriptionOptions.Gold:
                    //Unlimited Claims a month
                    return true;
            }
            return true;
        }
    }
}
