using System;
using System.Collections.Generic;
using OsOEasy.Promo.Models.DBModels;
using System.Linq;
using OsOEasy.Models;
using OsOEasy.Data;

namespace OsOEasy.Promo.Models
{
    public interface IBuilder
    {
        Dashboard CreateDashboardModel(ApplicationDbContext dbContext, ApplicationUser user, List<Promotion> promotions);
    }

    public class Builder : IBuilder
    {

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
                                                  select x).ToList();
                if (inactivePromos != null)
                {
                    SetInactivePromoDetails(model, inactivePromos);
                }
            }

            model.ActivePromoScript = user.UserPromoScript;
            model.CurrentSubscription = user.SubscriptionPlan;

            //todo calculate entries over time for a chart...googlechart?

            return model;
        }

        private void SetActivePromoDetails(ApplicationDbContext dbContext, Dashboard model, Promotion activePromo)
        {
            model.ActivePromo = activePromo.Title;
            model.ActivePromoDiscount = activePromo.Discount;
            model.ActivePromoEndDate = activePromo.EndDate;
            model.ActivePromoId = activePromo.Id;

            PromotionStats stats = (from x in dbContext.PromotionStats where x.Promotion.Id == activePromo.Id select x).FirstOrDefault();

            model.ActivePromoClaimedEntries = stats != null ? stats.TimesClaimed : 0;
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
