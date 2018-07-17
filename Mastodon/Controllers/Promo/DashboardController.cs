using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

            List<Promotion> allUserPromotions = null;
            Dashboard dashboardModel = null;

            try
            {
                using (_dbContext)
                {
                    if (_dbContext.Promotion.Count() > 0)
                    {
                        allUserPromotions = _dbContext.Promotion
                            .Where(c => c.ApplicationUser == user).ToList();
                    }
                    
                    dashboardModel = _dashboardBuilder.CreateDashboardModel(_dbContext, user, allUserPromotions);
                }
            }
            catch (Exception ex)
            {
                //todo handle exception
            }
           
            return JsonConvert.SerializeObject(dashboardModel);
        }

        [HttpGet]
        public void ActivatePromo(string promoId)
        {
            using (_dbContext)
            {
                //Set all promos to inactive
                foreach (Promotion promo in _dbContext.Promotion)
                {
                    promo.ActivePromotion = false;
                }

                if (String.IsNullOrEmpty(promoId) && !String.IsNullOrEmpty(HttpContext.Session.GetString("savedPromoId")))
                {
                    //This case handles a NEW promo that is saved and activated at the same time
                    promoId = HttpContext.Session.GetString("savedPromoId");
                }

                //Activate selected promo
                var promoToActivate = _dbContext.Promotion.Where(c => c.Id == promoId).FirstOrDefault();
                promoToActivate.ActivePromotion = true;

                _dbContext.SaveChanges();
            }
        }

        [HttpGet]
        public void StopActivePromotion(string promoId)
        {
            using (_dbContext)
            {
                var activePromo = (from x in _dbContext.Promotion where x.Id == promoId select x).First();
                activePromo.ActivePromotion = false;

                _dbContext.SaveChanges();
            }
        }
    }
}