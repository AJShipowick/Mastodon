using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mastodon.Models;
using Microsoft.AspNetCore.Identity;
using Mastodon.Slider.Models;
using Mastodon.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Mastodon.Slider.Models.DBModels;
using static Mastodon.Slider.Models.Builder;

namespace Mastodon.Slider.Controllers
{
    [Area("Slider")]
    public class SliderController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IBuilder _dashboardBuilder;

        public SliderController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IBuilder dashboardBuilder)
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

            List<Promotion> promotions = null;
            using (_dbContext)
            {
                promotions = _dbContext.Promotion
                    .Where(c => c.Id == user.Id).ToList();
            }

            Dashboard dashboardModel = _dashboardBuilder.CreateDashboardModel();

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