using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mastodon.Models;
using Microsoft.AspNetCore.Identity;

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
                //return View("../Account/Login", );
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}