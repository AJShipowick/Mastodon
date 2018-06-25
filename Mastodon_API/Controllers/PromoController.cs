using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsOEasy.Models;
using OsOEasy.Models.DBModels;
using OsOEasy.API.Data;
using OsOEasy.API.Responses;
using OsOEasy.API.Responses.CSS;
using OsOEasy.API.Responses.HTML;
using OsOEasy.API.Responses.JS;
using OsOEasy.API.Services;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OsOEasy.API.Controllers
{
    [Route("api/[controller]")]
    public class PromoController : Controller
    {

        APIDbContext _APIDbContext;
        IPromoService _PromoService;
        IMainJS _MainJS;
        IBasicHTML _PromoHTML;
        IBasicCSS _PromoCSS;
        IBasicJS _PromoJS;
        ISubscriptionService _SubscriptionService;

        public PromoController(APIDbContext apiDbContext,  IPromoService promoService, IMainJS mainJS, IBasicHTML sliderHTML,
            IBasicCSS sliderCSS, IBasicJS sliderJS, ISubscriptionService subscriptionService)
        {
            _APIDbContext = apiDbContext;
            _PromoService = promoService;
            _MainJS = mainJS;
            _PromoHTML = sliderHTML;
            _PromoCSS = sliderCSS;
            _PromoJS = sliderJS;
            _SubscriptionService = subscriptionService;
        }

        [HttpGet]
        [Route("{clientID}")]
        public string GetAsync(string clientID)
        {
            Promotion clientPromotion = null;

            try
            {
                using (_APIDbContext)
                {

                    if (_APIDbContext.Promotion.Count() < 1) { return "WARNING, no active promotions found."; }

                    clientPromotion = _APIDbContext.Promotion
                        .Where(c => c.ApplicationUser.Id == clientID && c.ActivePromotion == true).FirstOrDefault();

                    ApplicationUser appUser = _APIDbContext.Users.Where(u => u.Id == clientID).First();
                    if (!_SubscriptionService.SubscriptionActiveAndWithinTrafficLimit(appUser))
                    {
                        //Update account with error about status

                        return "ERROR, issue found with OsOEasyPromo account, please check account status at OsOEasyPromo.com!";
                    }

                    if (clientPromotion != null)
                    {
                        //Task the update to stats out?
                        //https://stackoverflow.com/questions/1018610/simplest-way-to-do-a-fire-and-forget-method-in-c
                        _PromoService.UpdatePromotionStats(clientPromotion, _APIDbContext);
                        return _MainJS.GetMainJS(clientID ,clientPromotion.Id);
                    }
                    else
                    {
                        return "WARNING, no active promotion found";
                    }
                }

            }
            catch (Exception ex)
            {
                return "ERROR, unknown exception occured getting promotion.";
            }
        }

        [HttpGet]
        [Route("html/{promoId}")]
        public string GetHTML(string promoId)
        {
            Promotion clientPromotion = null;

            try
            {
                using (_APIDbContext)
                {
                    clientPromotion = _APIDbContext.Promotion
                        .Where(c => c.Id == promoId).FirstOrDefault();
                }

                return _PromoHTML.getSliderHTML(clientPromotion);
            }
            catch (Exception ex)
            {
                //todo log exception
                return "Error getting slider HTML.";
            }
        }

        [HttpGet]
        [Route("css/{promoId}")]
        public string GetCSS(string promoId)
        {
            Promotion clientPromotion = null;

            try
            {
                using (_APIDbContext)
                {
                    clientPromotion = _APIDbContext.Promotion
                        .Where(c => c.Id == promoId).FirstOrDefault();
                }

                return _PromoCSS.GetSliderCSS(clientPromotion);
            }
            catch (Exception ex)
            {
                //todo log exception
                return "Error getting slider CSS.";
            }
        }

        [HttpGet]
        [Route("js/{promoId}")]
        public string GetJS(string promoId)
        {
            try
            {
                return _PromoJS.GetSliderJS();
            }
            catch (Exception ex)
            {
                //todo log exception
                return "Error getting slider JS.";
            }
        }

        [HttpGet]
        [Route("submit/{promoId}/{name}/{email}/{clientId}")]
        public async Task<string> ClaimPromotionAsync(string promoId, string name, string email, string clientId)
        {
            Promotion clientPromotion = null;
            IRestResponse response = null;

            try
            {
                using (_APIDbContext)
                {
                    clientPromotion = _APIDbContext.Promotion
                        .Where(c => c.Id == promoId).First();
                    response = await _PromoService.SendPromoEmail(email, name, clientPromotion.Code);                    

                    if (response.IsSuccessful)
                    {
                        ApplicationUser appUser = _APIDbContext.Users.Where(u => u.Id == clientId).First();
                        _PromoService.HandleCLaimedPromotion(clientPromotion, _APIDbContext, name, email, appUser);
                        return "SUCCESS";
                    }
                    else
                    {
                        return "ERROR";
                    }
                }

            }
            catch (Exception ex)
            {
                //todo log exception
                return "ERROR";
            }
        }
    }
}