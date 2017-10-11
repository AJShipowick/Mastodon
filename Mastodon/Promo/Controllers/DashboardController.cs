using Mastodon.Data;
using Mastodon.Models;
using Mastodon.Promo.Models;
using Mastodon.Promo.Models.DBModels;
using Mastodon.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mastodon.Promo.Controllers
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
            List<PromotionStats> promotionStats = null;

            //using (_dbContext)
            //{
            //    if (_dbContext.Promotion.Count() > 0)
            //    {
            //        allUserPromotions = _dbContext.Promotion
            //            .Where(c => c.Id == user.Id).ToList();
            //    }

            //    if (_dbContext.PromotionStats.Count() > 0)
            //    {
            //        promotionStats = _dbContext.PromotionStats
            //            .Where(c => c.Id == user.Id).ToList();
            //    }
            //}

            Dashboard dashboardModel = _dashboardBuilder.CreateDashboardModel(user, allUserPromotions, promotionStats);

            return JsonConvert.SerializeObject(dashboardModel);
        }
    }
}