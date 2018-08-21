using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OsOEasy.Data;
using OsOEasy.Data.Models;
using OsOEasy.Models.PromoModels;
using OsOEasy.Promo.Models;
using OsOEasy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsOEasy.Controllers.Promo
{
    [Area("Dashboard")]
    [Authorize]
    public class DashboardController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ICommon _common;
        private readonly IBuilder _dashboardBuilder;

        public DashboardController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, ICommon common, IBuilder dashboardBuilder)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _common = common;
            _dashboardBuilder = dashboardBuilder;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _common.GetCurrentUserAsync(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            return View("Dashboard");
        }

        [HttpGet]
        public async Task<string> GetUserSettings()
        {
            var user = await _common.GetCurrentUserAsync(HttpContext);

            List<Promotion> couponPromotions = null;
            List<SocialSharing> socialPromotions = null;
            Dashboard dashboardModel = null;

            try
            {
                using (_dbContext)
                {
                    if (_dbContext.Promotion.Count() > 0)
                    {
                        couponPromotions = _dbContext.Promotion
                            .Where(c => c.ApplicationUser == user).ToList();
                    }
                    if (_dbContext.SocialSharing.Count() > 0)
                    {
                        socialPromotions = _dbContext.SocialSharing
                            .Where(d => d.ApplicationUser == user).ToList();
                    }

                    dashboardModel = _dashboardBuilder.CreateDashboardModel(_dbContext, user, couponPromotions, socialPromotions);
                }
            }
            catch (Exception ex)
            {
                //todo handle exception
            }

            return JsonConvert.SerializeObject(dashboardModel);
        }

        [HttpGet]
        public void ActivatePromo(string promoId, string promoTypeToActivate, string promoTypeToStop)
        {
            using (_dbContext)
            {
                var activePromoId = FindActivePromoId();
                if (!string.IsNullOrEmpty(activePromoId) || !string.IsNullOrEmpty(activePromoId))
                {
                    switch (promoTypeToStop)
                    {
                        case Common.PromoType_Coupon:
                            var couponPromo = _dbContext.Promotion.Where(c => c.Id == activePromoId).FirstOrDefault();
                            couponPromo.ActivePromotion = false;
                            break;
                        case Common.PromoType_Social:
                            var socialPromo = _dbContext.SocialSharing.Where(c => c.Id == activePromoId).FirstOrDefault();
                            socialPromo.ActivePromotion = false;
                            break;
                    }
                }

                switch (promoTypeToActivate)
                {
                    case Common.PromoType_Coupon:
                        var couponPromo = _dbContext.Promotion.Where(c => c.Id == promoId).FirstOrDefault();
                        couponPromo.ActivePromotion = true;
                        break;
                    case Common.PromoType_Social:
                        var socialPromo = _dbContext.SocialSharing.Where(c => c.Id == promoId).FirstOrDefault();
                        socialPromo.ActivePromotion = true;
                        break;
                }
                _dbContext.SaveChanges();
            }
        }

        private string FindActivePromoId()
        {
            Promotion activePromo_Coupon = (from x in _dbContext.Promotion
                                            where x.ActivePromotion == true
                                            select x).SingleOrDefault();

            if (activePromo_Coupon != null)
            {
                return activePromo_Coupon.Id;
            }

            SocialSharing activePromo_Social = (from x in _dbContext.SocialSharing
                                                where x.ActivePromotion == true
                                                select x).SingleOrDefault();

            if (activePromo_Social != null)
            {
                return activePromo_Social.Id;
            }

            return "";
        }

        [HttpGet]
        public void StopActivePromotion(string promoId, string promoType)
        {
            if (string.IsNullOrEmpty(promoId) || string.IsNullOrEmpty(promoType)) { return; } //No promo to stop

            using (_dbContext)
            {
                switch (promoType)
                {
                    case Common.PromoType_Coupon:
                        var couponPromo = _dbContext.Promotion.Where(c => c.Id == promoId).FirstOrDefault();
                        couponPromo.ActivePromotion = false;
                        break;
                    case Common.PromoType_Social:
                        var socialPromo = _dbContext.SocialSharing.Where(c => c.Id == promoId).FirstOrDefault();
                        socialPromo.ActivePromotion = false;
                        break;
                }
                _dbContext.SaveChanges();
            }
        }
    }
}