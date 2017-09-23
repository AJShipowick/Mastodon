using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mastodon.Models;
using Microsoft.AspNetCore.Identity;
using Mastodon.Slider.Models;
using Mastodon.Data;
using Microsoft.AspNetCore.Http;

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

            List<ClientsWebsite> clientWebsites = null;
            using (var db = new ApplicationDbContext())
            {
                clientWebsites = db.ClientsWebsites
                    .Where(c => c.ClientID == user.Id).ToList();
            }

            if (clientWebsites.Count() > 0)
            {
                if (HttpContext.Session.GetString("WebsiteUpdated") == "True")
                {
                    clientWebsites[0].WebsiteUpdated = true;
                    HttpContext.Session.SetString("WebsiteUpdated", "False");
                }
                return View(clientWebsites[0]);  //Show 1st client site from list
            }
            else
            {
                return View(new ClientsWebsite { ClientID = user.Id });
            }

        }

        [HttpPost]
        public async Task<ActionResult> Save(ClientsWebsite vm)
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
                    HttpContext.Session.SetString("WebsiteUpdated", "True");
                }
            }

            return RedirectToAction("Index");

        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}