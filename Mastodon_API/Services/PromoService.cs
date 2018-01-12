using System;
using System.Linq;
using OsOEasy.Models.DBModels;
using OsOEasy_API.Data;

namespace OsOEasy_API.Services
{
    public interface IPromoService
    {
        void UpdatePromotionStats(Promotion clientPromotion, APIDbContext apiDbContext);
    }

    public class PromoService : IPromoService
    {

        public void UpdatePromotionStats(Promotion clientPromotion, APIDbContext apiDbContext)
        {
            PromotionStats stats = apiDbContext.PromotionStats.Where(c => c.Promotion == clientPromotion).FirstOrDefault();

            if (stats == null)
            {
                stats = new PromotionStats { Promotion = clientPromotion };
                apiDbContext.PromotionStats.Add(stats);
            }

            stats.TimesViewed++;
            apiDbContext.SaveChanges();
        }
    }
}
