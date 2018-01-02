using OsOEasy.Data;
using OsOEasy.Models;
using OsOEasy.Models.DBModels;
using OsOEasy.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OsOEasy.Promo.Models;
using OsOEasy.Models.PromoModels;
using Microsoft.AspNetCore.Http;

namespace OsOEasy.Controllers.Promo
{
    [Area("Dashboard")]
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

            //todo, if new user then show something to them????Like how to use the app???
            //ViewData["NewUser"] = user.NewUser;
            //if (user.NewUser != false)
            //{
            //    using (_dbContext)
            //    {
            //        user.NewUser = false;
            //        _dbContext.Update(user);
            //        _dbContext.SaveChanges();
            //    }
            //}

            return View();
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

                    //todo, show promotion charts/graphs stuff....
                    dashboardModel = _dashboardBuilder.CreateDashboardModel(_dbContext, user, allUserPromotions);
                }
            }
            catch (Exception ex)
            {
                //todo handle exception
            }
           
            //HttpContext.Session.SetInt32("activePromo", Convert.ToInt32(dashboardModel.IsActivePromo));
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