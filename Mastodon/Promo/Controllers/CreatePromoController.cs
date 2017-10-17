using Mastodon.Data;
using Mastodon.Models;
using Mastodon.Promo.Models.DBModels;
using Mastodon.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Mastodon.Promo.Controllers
{
    [Area("Promotion")]
    public class CreatePromoController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ICommon _common;

        public CreatePromoController(ApplicationDbContext dbContext, ICommon common)
        {
            _dbContext = dbContext;
            _common = common;
        }

        public async Task<IActionResult> CreateNewPromo(string promoId)
        {
            var user = await _common.GetCurrentUserAsync(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            HttpContext.Session.SetString("promoId", String.IsNullOrEmpty(promoId) ? "" : promoId);

            return View();
        }

        [HttpGet]
        public string GetPromoModel()
        {
            var promo = new Promotion();
            var promoId = HttpContext.Session.GetString("promoId");

            if (String.IsNullOrEmpty(promoId))
            {
                var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(7).ToString("MM/dd/yyyy");
                promo.EndDate = endDate;
            }
            else
            {
                promo = (from x in _dbContext.Promotion where x.Id == promoId select x).FirstOrDefault();
            }

            return JsonConvert.SerializeObject(promo);
        }

        [HttpPost]
        public async Task<string> SaveNewPromo([FromBody]Promotion vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (_dbContext)
                    {
                        if (!string.IsNullOrEmpty(vm.Id))
                        {
                            _dbContext.Entry(_dbContext.Promotion.Find(vm.Id)).CurrentValues.SetValues(vm);
                        }
                        else
                        {
                            ApplicationUser appUser = await _common.GetCurrentUserAsync(HttpContext);
                            vm.ApplicationUser = appUser;
                            _dbContext.Promotion.Add(vm);
                        }

                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //todo handle exception
            }

            return "Success";
        }

        [HttpGet]
        public void ActivatePromo(string promoId)
        {
            using (_dbContext)
            {
                //Set all promos to inactive
                foreach (Promotion promo in _dbContext.Promotion)
                {
                    promo.ActivePromotion = false;
                }

                //Activate selected promo
                var promoToActivate = _dbContext.Promotion.Where(c => c.Id == promoId).FirstOrDefault();
                promoToActivate.ActivePromotion = true;

                _dbContext.SaveChanges();
            }
        }

        public void DeletePromo(string promoId)
        {
            using (_dbContext)
            {
                var promoToDelete = _dbContext.Promotion.Where(c => c.Id == promoId).FirstOrDefault();
                _dbContext.Remove(promoToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}