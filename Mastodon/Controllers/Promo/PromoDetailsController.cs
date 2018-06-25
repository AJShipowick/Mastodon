using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OsOEasy.Data;
using OsOEasy.Data.Models;
using OsOEasy.Models.PromoModels;
using OsOEasy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OsOEasy.Controllers.Promo
{
    [Area("Details")]
    public class PromoDetailsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public PromoDetailsController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult PromoDetails(string promoId, string promoName)
        {
            HttpContext.Session.SetString("promoStatsId", String.IsNullOrEmpty(promoId) ? "" : promoId);
            HttpContext.Session.SetString("promoStatsName", String.IsNullOrEmpty(promoId) ? "" : promoName);
            return View();
        }

        public string GetPromoDetails()
        {
            var promoStatsId = HttpContext.Session.GetString("promoStatsId");
            var promoStatsName = HttpContext.Session.GetString("promoStatsName");

            PromoDetails promoDetails = new PromoDetails();

            if (!String.IsNullOrEmpty(promoStatsId))
            {
                using (_dbContext)
                {
                    PromotionStats promoStats = (from x in _dbContext.PromotionStats where x.Promotion.Id == promoStatsId select x).FirstOrDefault();
                    List<PromotionEntries> promoEntries = (from x in _dbContext.PromotionEntries where x.Promotion.Id == promoStatsId select x).ToList();

                    promoDetails.PromoName = (!String.IsNullOrEmpty(promoStatsName) ? promoStatsName : "");
                    promoDetails.PromoId = promoStatsId;
                    promoDetails.TimesClaimed = (promoStats != null ? promoStats.TimesClaimed : 0);
                    promoDetails.TimesViewed = (promoStats != null ? promoStats.TimesViewed : 0);
                    promoDetails.PromoEntries = promoEntries;
                }
            }

            return (JsonConvert.SerializeObject(promoDetails));
        }

        public IActionResult ExportPromoUsers(string promoId)
        {
            var myExport = new CsvExport(",", false);
            List<PromotionEntries> promoEntries = new List<PromotionEntries>();

            using (_dbContext)
            {
                promoEntries = (from x in _dbContext.PromotionEntries where x.Promotion.Id == promoId select x).ToList();
            }

            foreach (PromotionEntries entry in promoEntries)
            {
                myExport.AddRow();
                myExport["Name"] = entry.Name;
                myExport["Email"] = entry.EmailAddress;
            }

            return File(myExport.ExportToBytes(), "text/csv", "PromoStats.csv");
        }
    }
}