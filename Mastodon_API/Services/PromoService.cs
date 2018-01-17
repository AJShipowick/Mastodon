using System;
using System.Linq;
using OsOEasy.Models.DBModels;
using OsOEasy_API.Data;

namespace OsOEasy_API.Services
{
    public interface IPromoService
    {
        void UpdatePromotionStats(Promotion clientPromotion, APIDbContext apiDbContext);
        void HandleCLaimedPromotion(Promotion clientPromotion, APIDbContext apiDbContext, string name, string email);
        void SendPromoEmail(String toAddress, String userName, String promoCode);
    }

    public class PromoService : IPromoService
    {

        public IMailGunEmailSender _EmailSender;

        public PromoService(IMailGunEmailSender emailSender)
        {
            _EmailSender = emailSender;
        }

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
        public void HandleCLaimedPromotion(Promotion clientPromotion, APIDbContext apiDbContext, string name, string email)
        {
            PromotionStats stats = apiDbContext.PromotionStats.Where(c => c.Promotion == clientPromotion).FirstOrDefault();

            if (stats == null)
            {
                stats = new PromotionStats { Promotion = clientPromotion };
                apiDbContext.PromotionStats.Add(stats);
            }
            stats.TimesClaimed++;

            var entry = new PromotionEntries { Promotion = clientPromotion, Name = name, EmailAddress = email };
            apiDbContext.PromotionEntries.Add(entry);

            apiDbContext.SaveChanges();
        }

        public void SendPromoEmail(String toAddress, String userName, String promoCode)
        {
            _EmailSender.SendMailGunEmailAsync(EmailType.ClaimPromotion, toAddress, userName, promoCode);
        }
    }
}
