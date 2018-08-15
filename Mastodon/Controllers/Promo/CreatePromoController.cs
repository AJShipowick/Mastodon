using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using OsOEasy.Data;
using OsOEasy.Data.Models;
using OsOEasy.Services.Stripe;
using OsOEasy.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OsOEasy.Controllers.Promo
{
    [Area("Promotion")]
    public class CreatePromoController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ICommon _common;
        private readonly IHostingEnvironment _environment;

        public CreatePromoController(ApplicationDbContext dbContext, ICommon common, IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _common = common;
            _environment = environment;
        }

        public async Task<IActionResult> CreateNewPromo(string promoId, string promoType)
        {
            var user = await _common.GetCurrentUserAsync(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            HttpContext.Session.SetString("promoId", (String.IsNullOrEmpty(promoId) ? "" : promoId));
            ViewBag.subscription = user.SubscriptionPlan;
            ViewBag.newPromo = (String.IsNullOrEmpty(promoId));
            ViewBag.promoType = promoType;

            return View();
        }

        [HttpGet]
        public JsonResult GetPromoData()
        {
            Promotion promoModel = GetPromoModel(HttpContext.Session.GetString("promoId"));
            return Json(promoModel);
        }

        public Promotion GetPromoModel(string promoId)
        {
            var promo = new Promotion();

            if (!String.IsNullOrEmpty(promoId))
            {
                //Edit existing promotion
                promo = (from x in _dbContext.Promotion where x.Id == promoId select x).FirstOrDefault();
            }
            else
            {
                //Set new promo defaults
                promo.ImageType = "coupon";
                promo.ImageName = "osoeasypromo_coupon (16).svg";
                promo.BackgroundColor = "#ffffff";
                promo.ButtonColor = "#4CAF50";
                promo.SideOfScreen = "right";
                promo.DisplayEndDate = DateTime.Today.AddDays(14).ToString("MM-dd-yyyy");
            }

            return promo;
        }

        [HttpGet]
        public JsonResult GetPromoImages(string imageType)
        {
            IFileProvider provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            IDirectoryContents contents = provider.GetDirectoryContents("wwwroot/images/Promo/" + imageType);

            List<string> imageItems = new List<string>();
            foreach (IFileInfo item in contents)
            {
                imageItems.Add(item.Name);
            }

            return Json(imageItems);
        }

        [HttpGet]
        public async Task<JsonResult> GetUserImages()
        {
            var user = await _common.GetCurrentUserAsync(HttpContext);

            IFileProvider provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            IDirectoryContents contents = provider.GetDirectoryContents("wwwroot/images/Promo/Users/" + user.Id);

            List<string> imageItems = new List<string>();
            foreach (IFileInfo item in contents)
            {
                imageItems.Add(user.Id + "/" + item.Name);
            }

            return Json(imageItems);
        }

        [HttpGet]
        public JsonResult GetSocialImages(string socialImageType)
        {
            IFileProvider provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            IDirectoryContents contents = provider.GetDirectoryContents("wwwroot/images/Social/" + socialImageType);

            List<string> imageItems = new List<string>();
            foreach (IFileInfo item in contents)
            {
                imageItems.Add(item.Name);
            }

            return Json(imageItems);
        }

        [HttpGet]
        public JsonResult GetSocialData()
        {
            SocialSharing socialModel = GetSocialModel(HttpContext.Session.GetString("promoId"));
            return Json(socialModel);
        }

        public SocialSharing GetSocialModel(string promoId)
        {
            var social = new SocialSharing();

            if (!String.IsNullOrEmpty(promoId))
            {
                //Edit existing promotion
                social = (from x in _dbContext.SocialSharing where x.Id == promoId select x).FirstOrDefault();
            }
            else
            {
                social.SideOfScreen = "left";
            }

            return social;
        }

        //todo [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveNewPromo([FromBody]Promotion promoItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (_dbContext)
                    {
                        promoItem.CreationDate = DateTime.Now;
                        if (!string.IsNullOrEmpty(promoItem.Id))
                        {
                            //Update existing promotion
                            _dbContext.Entry(_dbContext.Promotion.Find(promoItem.Id)).CurrentValues.SetValues(promoItem);
                        }
                        else
                        {
                            //Create new promotion
                            ApplicationUser appUser = await _common.GetCurrentUserAsync(HttpContext);
                            promoItem.ApplicationUser = appUser;
                            _dbContext.Promotion.Add(promoItem);
                        }

                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    //todo log that user is trying to bypass UI JS validation
                }
            }
            catch (Exception ex)
            {
                //todo handle exception
            }

            return RedirectToAction("Dashboard", "Dashboard", new { area = "Dashboard" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveSocial([FromBody]SocialSharing socialModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (_dbContext)
                    {
                        if (!string.IsNullOrEmpty(socialModel.Id))
                        {
                            //Update existing promotion
                            _dbContext.Entry(_dbContext.SocialSharing.Find(socialModel.Id)).CurrentValues.SetValues(socialModel);
                        }
                        else
                        {
                            //Create new promotion
                            ApplicationUser appUser = await _common.GetCurrentUserAsync(HttpContext);
                            socialModel.ApplicationUser = appUser;
                            _dbContext.SocialSharing.Add(socialModel);
                        }

                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    //todo log that user is trying to bypass UI JS validation
                }
            }
            catch (Exception ex)
            {
                //todo handle exception
            }

            return RedirectToAction("Dashboard", "Dashboard", new { area = "Dashboard" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadUserImage(List<IFormFile> files)
        {

            ApplicationUser appUser = await _common.GetCurrentUserAsync(HttpContext);
            if (appUser.SubscriptionPlan == SubscriptionOptions.FreeAccount)
            {
                //Image upload only available for paid users
                return View("CreateNewPromo");
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    if (FileTypeValid(formFile.FileName))
                    {
                        var fileName = BuildUserPicFileName(formFile.FileName, appUser.Id);
                        using (var stream = new FileStream(fileName, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                        //todo handle invalid file types...here and in JS??
                    }
                }
            }

            return View("CreateNewPromo");
        }

        private string BuildUserPicFileName(string fileName, string userId)
        {
            var userFileDirectory = _environment.WebRootPath + "/images/Promo/Users/" + userId + "/";
            Directory.CreateDirectory(userFileDirectory);

            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
            var FileExtension = Path.GetExtension(fileName);
            var fullFileName = myUniqueFileName + FileExtension;

            return userFileDirectory + fullFileName;
        }

        bool FileTypeValid(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".svg":
                    return true;
                case ".gif":
                    return true;
                default:
                    return false;
            }
        }

        //[HttpGet]
        //public void DeletePromo(string promoId)
        //{
        //    using (_dbContext)
        //    {
        //        var promoToDelete = _dbContext.Promotion.Where(c => c.Id == promoId).FirstOrDefault();
        //        _dbContext.Remove(promoToDelete);
        //        _dbContext.SaveChanges();
        //    }
        //}
    }
}