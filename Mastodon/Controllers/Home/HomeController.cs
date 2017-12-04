using Microsoft.AspNetCore.Mvc;

namespace OsOEasy.Controllers.Home
{

    //Main controller for landing page
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Features()
        {
            return View();
        }

        public IActionResult Pricing()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult BusinessAnalyser()
        {
            return View();
        }
    }
}
