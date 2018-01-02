using OsOEasy.Data;
using OsOEasy.Models;
using OsOEasy.Models.DBModels;
using OsOEasy.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace OsOEasy.Controllers.Promo
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

            Promotion promoModel = GetPromoModel(promoId);

            return View(promoModel);
        }

        public Promotion GetPromoModel(string promoId)
        {
            var promo = new Promotion();

            if (String.IsNullOrEmpty(promoId))
            {
                //New promotion
                var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(1);
                promo.EndDate = endDate;
            }
            else
            {
                //Edit existing promtion
                promo = (from x in _dbContext.Promotion where x.Id == promoId select x).FirstOrDefault();
                HttpContext.Session.SetInt32("activePromo", Convert.ToInt32(promo.ActivePromotion));
            }

            return promo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNewPromo(Promotion promoItem)
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
                            //Update existing promtion
                            promoItem.ActivePromotion = Convert.ToBoolean(HttpContext.Session.GetInt32("activePromo"));
                            _dbContext.Entry(_dbContext.Promotion.Find(promoItem.Id)).CurrentValues.SetValues(promoItem);
                        }
                        else
                        {
                            //Creat new promotion
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

        [HttpGet]
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