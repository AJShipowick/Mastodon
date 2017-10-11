using Mastodon.Data;
using Mastodon.Models;
using Mastodon.Promo.Models.DBModels;
using Mastodon.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Mastodon.Promo.Controllers
{
    [Area("Promotion")]
    public class CreatePromoController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ICommon _common;

        public CreatePromoController(ApplicationDbContext dbContext, ICommon common)
        {
            _dbContext = dbContext;
            _common = common;
        }

        public async Task<IActionResult> CreateNewPromo()
        {
            var user = await _common.GetCurrentUserAsync(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            return View();
        }

        [HttpGet]
        public string GetNewPromoModel()
        {
            var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(7).ToString("MM/dd/yyyy");         
            var newPromo = new Promotion { EndDate = endDate };

            return JsonConvert.SerializeObject(newPromo);
        }

        [HttpPost]
        public async Task<string> SaveNewPromo([FromBody]Promotion vm)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext)
                {
                    ApplicationUser appUserId = await _common.GetCurrentUserAsync(HttpContext);
                    vm.ApplicationUserId = appUserId;
                    _dbContext.Promotion.Add(vm);

                    await _dbContext.SaveChangesAsync();
                }
            }

            return "Success";
        }

    }
}