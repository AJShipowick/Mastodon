using Microsoft.AspNetCore.Mvc;

namespace Mastodon.Controllers
{

    //Main controller for landing page

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "About us";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact us";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
