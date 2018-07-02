using OsOEasy.Data;
using OsOEasy.Data.Models;
using OsOEasy.Models.PromoModels;
using OsOEasy.Services.Stripe;
using OsOEasy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OsOEasy.Promo.Models
{
    public interface IBuilder
    {
        Dashboard CreateDashboardModel(ApplicationDbContext dbContext, ApplicationUser user, List<Promotion> promotions);
    }

    public class Builder : IBuilder
    {
        public readonly ICommon _Common;

        public Builder(ICommon common)
        {
            _Common = common;
        }

        public Dashboard CreateDashboardModel(ApplicationDbContext dbContext, ApplicationUser user, List<Promotion> allUserPromotions)
        {
            Dashboard model = new Dashboard();

            if (allUserPromotions != null && allUserPromotions.Count() > 0)
            {
                Promotion activePromo = (from x in allUserPromotions
                                         where x.ActivePromotion == true
                                         select x).SingleOrDefault();
                if (activePromo != null)
                {
                    SetActivePromoDetails(dbContext, model, activePromo);
                }

                List<Promotion> inactivePromos = (from x in allUserPromotions
                                                  where x.ActivePromotion != true
                                                  orderby x.CreationDate descending
                                                  select x).ToList();
                if (inactivePromos != null)
                {
                    SetInactivePromoDetails(model, inactivePromos);
                }
            }

            model.ActivePromoScript = user.UserPromoScript;
            model.CurrentSubscription = user.SubscriptionPlan;

            model.DashboardMessage = GetDashboardMessage(user);
            model.AccountWarning = user.AccountSuspended || user.MonthlyPromotionLimitReached;

            return model;
        }

        private String GetDashboardMessage(ApplicationUser user)
        {
            if (CommonAccount.FreeTrialActive(user))
            {
                return GetFreeTrialMessage(CommonAccount.DaysSinceAccountSignup(user.AccountCreationDate));
            }

            String dashboardMessage = String.Empty;
            if (user.AccountSuspended)
            {
                dashboardMessage = "Account suppended, make a payment on your account here";
            }
            else if (user.MonthlyPromotionLimitReached)
            {
                dashboardMessage = "Account promotion limit reached, upgrade account here";
            }
            else
            {
                dashboardMessage = "You rock, view account here";
            }

            return dashboardMessage;
        }

        //With free plan there are not traffic limitations
        private string GetFreeTrialMessage(int daysSinceSignup)
        {
            if (daysSinceSignup == CommonAccount._DaysForFreeTrial)
            {
                return "This is the last day of your free trial!  Pick a plan here";
            }
            else
            {
                return String.Format("You have {0} days left in your free trial.  View account here", (CommonAccount._DaysForFreeTrial - daysSinceSignup).ToString());
            }
        }

        private void SetActivePromoDetails(ApplicationDbContext dbContext, Dashboard model, Promotion activePromo)
        {
            model.ActivePromoName = activePromo.Title;
            model.ActivePromoDiscount = activePromo.Discount;
            model.ActivePromoEndDate = activePromo.EndDate;
            model.ActivePromoId = activePromo.Id;
            //model.IsActivePromo = activePromo.ActivePromotion;

            PromotionStats stats = (from x in dbContext.PromotionStats where x.Promotion.Id == activePromo.Id select x).FirstOrDefault();

            model.ActivePromoClaimedEntries = stats != null ? stats.TimesClaimed : 0;
            model.ActivePromoViews = stats != null ? stats.TimesViewed : 0;
        }

        private void SetInactivePromoDetails(Dashboard model, List<Promotion> inactivePromos)
        {
            model.InactivePromos = new List<InactivePromos>();
            foreach (Promotion item in inactivePromos)
            {
                //todo limit number of old promos to show?????
                //page them???
                var inactiveItem = new InactivePromos { PromoId = item.Id, PromoName = item.Title, PromoDiscount = item.Discount };
                model.InactivePromos.Add(inactiveItem);
            }
        }
    }
}
