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
        Dashboard CreateDashboardModel(ApplicationDbContext dbContext, ApplicationUser user, List<Promotion> couponPromotions, List<SocialSharing> socialPromotions);
    }

    public class Builder : IBuilder
    {
        public readonly ICommon _Common;

        public Builder(ICommon common)
        {
            _Common = common;
        }

        public Dashboard CreateDashboardModel(ApplicationDbContext dbContext, ApplicationUser user, List<Promotion> couponPromotions, List<SocialSharing> socialPromotions)
        {
            Dashboard model = new Dashboard();

            if (couponPromotions != null && couponPromotions.Count() > 0)
            {
                Promotion activePromo_Coupon = (from x in couponPromotions
                                                where x.ActivePromotion == true
                                                select x).SingleOrDefault();
                if (activePromo_Coupon != null)
                {
                    SetActiveCouponDetails(dbContext, model, activePromo_Coupon);
                }

                List<Promotion> inactivePromos_Coupons = (from x in couponPromotions
                                                          where x.ActivePromotion != true
                                                          orderby x.CreationDate descending
                                                          select x).ToList();
                if (inactivePromos_Coupons != null)
                {
                    SetInactivePromoDetails(model, inactivePromos_Coupons);
                }
            }

            if (socialPromotions != null && socialPromotions.Count() > 0)
            {
                SocialSharing activePromo_Social = (from x in socialPromotions
                                                    where x.ActivePromotion == true
                                                    select x).SingleOrDefault();

                if (activePromo_Social != null)
                {
                    SetActiveSocialDetails(dbContext, model, activePromo_Social);
                }

                List<SocialSharing> inactivePromo_Social = (from x in socialPromotions
                                                            where x.ActivePromotion != true
                                                            select x).ToList();

                if (inactivePromo_Social != null)
                {
                    SetInactiveSocialDetails(model, inactivePromo_Social);
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
                dashboardMessage = "Account suspended, make a payment on your account here";
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

        private void SetActiveCouponDetails(ApplicationDbContext dbContext, Dashboard model, Promotion activePromo)
        {
            model.ActivePromoName = activePromo.Title;
            model.ActivePromoDiscount = activePromo.Discount;
            model.ActivePromoEndDate = activePromo.DisplayEndDate;
            model.ActivePromoWarningMessage = GetActivePromoWarningMessage(activePromo.EndDate);
            model.ActivePromoId = activePromo.Id;
            model.ActivePromoType = Common.PromoType_Coupon;

            PromotionStats stats = (from x in dbContext.PromotionStats where x.Promotion.Id == activePromo.Id select x).FirstOrDefault();

            model.ActivePromoClaimedEntries = stats != null ? stats.TimesClaimed : 0;
            model.ActivePromoViews = stats != null ? stats.TimesViewed : 0;
        }

        private void SetActiveSocialDetails(ApplicationDbContext dbContext, Dashboard model, SocialSharing activePromo)
        {
            model.ActivePromoName = activePromo.Title;
            model.ActivePromoType = Common.PromoType_Social;
        }

        private string GetActivePromoWarningMessage(string endDate)
        {
            var warningMessage = String.Empty;

            var promoEndDate = DateTime.Parse(endDate);
            var currentDate = DateTime.Today;

            if (currentDate.Date > promoEndDate.Date)
            {
                warningMessage = "End date has past, promo will not display!";
            }

            return warningMessage;
        }

        private void SetInactivePromoDetails(Dashboard model, List<Promotion> inactivePromos)
        {
            model.InactivePromos = new List<InactivePromos>();
            foreach (Promotion item in inactivePromos)
            {
                var inactiveItem = new InactivePromos { PromoId = item.Id, PromoName = item.Title, PromoDiscount = item.Discount, PromoType = Common.PromoType_Coupon };
                model.InactivePromos.Add(inactiveItem);
            }
        }

        private void SetInactiveSocialDetails(Dashboard model, List<SocialSharing> inactivePromos)
        {
            if (model.InactivePromos == null)
            {
                model.InactivePromos = new List<InactivePromos>();
            }

            foreach (SocialSharing item in inactivePromos)
            {
                var inactiveItem = new InactivePromos { PromoId = item.Id, PromoName = item.Title, PromoType = Common.PromoType_Social };
                model.InactivePromos.Add(inactiveItem);
            }
        }
    }
}
