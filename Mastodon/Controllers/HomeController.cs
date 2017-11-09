using Mastodon.Shared;
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

        public IActionResult BusinessAnalyser()
        {
            return View();
        }

        public IActionResult ExportBusinesses()
        {
            var myExport = new CsvExport(",", false);

            //foreach (PromotionEntries entry in promoEntries)
            //{
            //    myExport.AddRow();
            //    myExport["Name"] = entry.Name;
            //    myExport["Email"] = entry.EmailAddress;
            //}

            return File(myExport.ExportToBytes(), "text/csv", "PromoStats.csv");
        }

    }
}
