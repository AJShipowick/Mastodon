using OsOEasy.Data.Models;
using System;

namespace OsOEasy.API.Services
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

            int promoClaimsThisMonth = FindClaimsForCurrentMonth(user);
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

        private int FindClaimsForCurrentMonth(ApplicationUser user)
        {
            DateTime lastClaimDate = user.DateOfLastPromoClaim;
            if (lastClaimDate.Month.CompareTo(DateTime.Today.Month) < 0)  
            {
                // Last claim date was earlier than current month (eg: previous month or earlier...)
                return 0;
            }

            return user.PromoClaimsForCurrentMonth;
        }
    }
}
