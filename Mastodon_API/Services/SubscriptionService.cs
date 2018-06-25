using OsOEasy.Data;
using OsOEasy.Data.Models;
using System;

namespace OsOEasy.API.Services
{

    public interface ISubscriptionService
    {
        bool SubscriptionWithinTrafficLimit(ApplicationUser user, ApplicationDbContext DbContext);
    }

    public class SubscriptionService : ISubscriptionService
    {
        public bool SubscriptionWithinTrafficLimit(ApplicationUser user, ApplicationDbContext DbContext)
        {
            bool subscriptionWithinTrafficLimit = true;

            int promoClaimsThisMonth = FindClaimsForCurrentMonth(user);
            switch (user.SubscriptionPlan)
            {
                case SubscriptionOptions.FreeAccount:
                    //0-100 Claims a month
                    subscriptionWithinTrafficLimit = promoClaimsThisMonth <= SubscriptionOptions.MaxFreeAccountClaims;
                    break;
                case SubscriptionOptions.Bronze:
                    //Up to 1,500 Claims a month
                    subscriptionWithinTrafficLimit = promoClaimsThisMonth <= SubscriptionOptions.MaxBronzeAccountClaims;
                    break;
                case SubscriptionOptions.Silver:
                    //Up to 5,000 Claims a month
                    subscriptionWithinTrafficLimit = promoClaimsThisMonth <= SubscriptionOptions.MaxSilverAccountClaims;
                    break;
                case SubscriptionOptions.Gold:
                    //Unlimited Claims a month
                    subscriptionWithinTrafficLimit = true;
                    break;
            }

            if (subscriptionWithinTrafficLimit && user.MonthlyPromotionLimitReached == true)
            {
                // Once a user upgrades a plan OR a new month starts, reset their promo limit flag
                user.MonthlyPromotionLimitReached = false;
                DbContext.SaveChanges();
            }
            else if (subscriptionWithinTrafficLimit == false && user.MonthlyPromotionLimitReached == false)
            {
                // 1st traffic limit violation that has not been set in the DB should come in here...
                // This flag will be cleared after a new month or an upgrade of a plan
                user.MonthlyPromotionLimitReached = true;
                DbContext.SaveChanges();
            }

            return subscriptionWithinTrafficLimit;
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
