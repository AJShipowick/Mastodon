using OsOEasy.Data;
using OsOEasy.Data.Models;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OsOEasy.API.Services
{
    public interface IPromoService
    {
        void UpdatePromotionStats(Promotion clientPromotion, ApplicationDbContext apiDbContext);
        void HandleCLaimedPromotion(Promotion clientPromotion, ApplicationDbContext apiDbContext, string name, string email, ApplicationUser appUser);
        Task<IRestResponse> SendPromoEmail(String toAddress, String userName, String promoCode);
    }

    public class PromoService : IPromoService
    {

        public IMailGunEmailSender _EmailSender;

        public PromoService(IMailGunEmailSender emailSender)
        {
            _EmailSender = emailSender;
        }

        public void UpdatePromotionStats(Promotion clientPromotion, ApplicationDbContext apiDbContext)
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
        public void HandleCLaimedPromotion(Promotion clientPromotion, ApplicationDbContext apiDbContext, string name, string email, ApplicationUser appUser)
        {
            PromotionStats stats = apiDbContext.PromotionStats.Where(c => c.Promotion == clientPromotion).FirstOrDefault();

            if (stats == null)
            {
                stats = new PromotionStats { Promotion = clientPromotion };
                apiDbContext.PromotionStats.Add(stats);
            }
            stats.TimesClaimed++;
            UpdateClaimsPerMonthStat(appUser);

            var entry = new PromotionEntries { Promotion = clientPromotion, Name = name, EmailAddress = email };
            apiDbContext.PromotionEntries.Add(entry);

            apiDbContext.SaveChanges();
        }

        private void UpdateClaimsPerMonthStat(ApplicationUser appUser)
        {
            DateTime lastClaimDate = appUser.DateOfLastPromoClaim;
            if (lastClaimDate.Month.CompareTo(DateTime.Today.Month) < 0)
            {
                // 1st claim of a new month!
                // Last claim date was earlier than current month (eg: previous month or earlier...)
                appUser.DateOfLastPromoClaim = DateTime.Today;
                appUser.PromoClaimsForCurrentMonth = 1;
            }
            else
            {
                appUser.PromoClaimsForCurrentMonth++;
            }
        }

        public Task<IRestResponse> SendPromoEmail(String toAddress, String userName, String promoCode)
        {
            return _EmailSender.SendMailGunEmailAsync(EmailType.ClaimPromotion, toAddress, userName, promoCode);
        }
    }
}
