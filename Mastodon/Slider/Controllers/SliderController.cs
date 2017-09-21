using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mastodon.Slider.Controllers
{
    [Area("Slider")]
    public class SliderController : Controller
    {
        public IActionResult Index()
        {

            //Check if user is logged in or not.....

            return View();
        }
    }
}