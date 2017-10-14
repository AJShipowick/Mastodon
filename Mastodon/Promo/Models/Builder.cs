using System;
using System.Collections.Generic;
using Mastodon.Promo.Models.DBModels;
using System.Linq;
using Mastodon.Models;

namespace Mastodon.Promo.Models
{
    public interface IBuilder
    {
        Dashboard CreateDashboardModel(ApplicationUser user, List<Promotion> promotions, List<PromotionStats> promotionStats);
    }

    public class Builder : IBuilder
    {
        public Dashboard CreateDashboardModel(ApplicationUser user, List<Promotion> allUserPromotions, List<PromotionStats> promotionStats)
        {
            Dashboard model = new Dashboard();

            if (allUserPromotions != null && allUserPromotions.Count() > 0)
            {
                Promotion activePromo = (from x in allUserPromotions
                                         where x.ActivePromotion == true
                                         select x).SingleOrDefault();
                if (activePromo != null)
                {
                    SetActivePromoDetails(model, activePromo, promotionStats);
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

        private void SetActivePromoDetails(Dashboard model, Promotion activePromo, List<PromotionStats> promotionStats)
        {
            model.ActivePromo = activePromo.Title;
            model.ActivePromoEndDate = activePromo.EndDate;
            model.ActivePromoId = activePromo.Id;

            PromotionStats stats = null;
            if (promotionStats != null)
            {
                stats = (from x in promotionStats
                              where x.Promotion == activePromo
                              select x).FirstOrDefault();
            }

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
