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

namespace Mastodon.Slider.Controllers
{
    [Area("Slider")]
    public class SliderController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public SliderController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

            List<ClientsWebsite> clientWebsites = null;
            using (var db = new ApplicationDbContext())
            {
                clientWebsites = db.ClientsWebsites
                    .Where(c => c.ClientID == user.Id).ToList();
            }

            if (clientWebsites.Count() > 0)
            {
                clientWebsites[0].CustomSiteScript = "scriptURLTEST.com";
                return JsonConvert.SerializeObject(clientWebsites[0]);
            }
            else
            {
                return JsonConvert.SerializeObject(new ClientsWebsite { ClientID = user.Id });
            }
        }

        [HttpPost]
        public async Task<string> SaveCustomSettings([FromBody]ClientsWebsite vm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.ClientsWebsites.Add(vm);

                    var matchingItems = from x in db.ClientsWebsites
                                        where x.ClientID == vm.ClientID
                                        select x;
                    if (matchingItems.Count() > 0)
                    {
                        db.Update(vm);
                    }
                    await db.SaveChangesAsync();
                }
            }

            return "Success";
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}