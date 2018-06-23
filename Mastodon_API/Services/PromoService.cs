﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OsOEasy.Models;
using OsOEasy.Models.DBModels;
using OsOEasy_API.Data;
using RestSharp;

namespace OsOEasy_API.Services
{
    public interface IPromoService
    {
        void UpdatePromotionStats(Promotion clientPromotion, APIDbContext apiDbContext);
        void HandleCLaimedPromotion(Promotion clientPromotion, APIDbContext apiDbContext, string name, string email, ApplicationUser appUser);
        Task<IRestResponse> SendPromoEmail(String toAddress, String userName, String promoCode);
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
        public void HandleCLaimedPromotion(Promotion clientPromotion, APIDbContext apiDbContext, string name, string email, ApplicationUser appUser)
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

            if (appUser.ClaimsPerMonth == null)
            {
                appUser.ClaimsPerMonth = new Dictionary<DateTime, int>();
            }

            if (appUser.ClaimsPerMonth.TryGetValue(DateTime.Now, out int val))
            {
                appUser.ClaimsPerMonth[DateTime.Now] = val + 1;
            }
            else
            {
                appUser.ClaimsPerMonth.Add(DateTime.Now, 1);
            }

        }

        public Task<IRestResponse> SendPromoEmail(String toAddress, String userName, String promoCode)
        {
            return _EmailSender.SendMailGunEmailAsync(EmailType.ClaimPromotion, toAddress, userName, promoCode);
        }
    }
}
