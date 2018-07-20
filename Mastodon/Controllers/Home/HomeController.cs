using Microsoft.AspNetCore.Mvc;
using OsOEasy.Services.MailGun;

namespace OsOEasy.Controllers.Home
{

    //Main controller for landing page
    public class HomeController : Controller
    {
        private readonly IMailGunEmailSender _emailSender;

        public HomeController(IMailGunEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

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

        public IActionResult Contact()
        {
            return View();
        }

        public void SendContactRequest(string userEmail, string userComment)
        {
            _emailSender.SendContactRequestAsync(EmailType.UserContactRequest, userEmail, userComment);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
