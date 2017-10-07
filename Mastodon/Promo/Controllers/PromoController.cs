using Mastodon.Data;
using Mastodon.Models;
using Mastodon.Promo.Models;
using Mastodon.Promo.Models.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mastodon.Promo.Controllers
{
    [Area("Promo")]
    public class PromoController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IBuilder _dashboardBuilder;

        public PromoController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IBuilder dashboardBuilder)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _dashboardBuilder = dashboardBuilder;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            return View();
        }

        [HttpGet]
        public async Task<string> GetUserSettings()
        {
            var user = await GetCurrentUserAsync();

            List<Promotion> allUserPromotions = null;
            List<PromotionStats> promotionStats = null;

            using (_dbContext)
            {
                if (_dbContext.Promotion.Count() > 0)
                {
                    allUserPromotions = _dbContext.Promotion
                        .Where(c => c.Id == user.Id).ToList();
                }

                if (_dbContext.PromotionStats.Count() > 0)
                {
                    promotionStats = _dbContext.PromotionStats
                        .Where(c => c.Id == user.Id).ToList();
                }
            }

            Dashboard dashboardModel = _dashboardBuilder.CreateDashboardModel(user, allUserPromotions, promotionStats);

            return JsonConvert.SerializeObject(dashboardModel);
        }

        [HttpPost]
        public async Task<string> SaveCustomSettings([FromBody]Promotion vm)
        {
            //if (ModelState.IsValid)
            //{
            //    using (_dbContext)
            //    {
            //        _dbContext.ClientsWebsites.Add(vm);

            //        var matchingItems = from x in _dbContext.ClientsWebsites
            //                            where x.ClientID == vm.ClientID
            //                            select x;
            //        if (matchingItems.Count() > 0)
            //        {
            //            _dbContext.Update(vm);
            //        }
            //        await _dbContext.SaveChangesAsync();
            //    }
            //}

            return "Success";
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}