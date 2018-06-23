using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OsOEasy.Models;
using OsOEasy.Models.DBModels;
using OsOEasy_API.Data;
using OsOEasy_API.Responses;
using OsOEasy_API.Responses.CSS;
using OsOEasy_API.Responses.HTML;
using OsOEasy_API.Responses.JS;
using OsOEasy_API.Services;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OsOEasy_API.Controllers
{
    [Route("api/[controller]")]
    public class PromoController : Controller
    {

        APIDbContext _APIDbContext;
        private readonly UserManager<ApplicationUser> _UserManager;
        IPromoService _PromoService;
        IMainJS _MainJS;
        IBasicHTML _PromoHTML;
        IBasicCSS _PromoCSS;
        IBasicJS _PromoJS;
        ISubscriptionService _SubscriptionService;

        public PromoController(APIDbContext apiDbContext, UserManager<ApplicationUser> userManager, IPromoService promoService, IMainJS mainJS, IBasicHTML sliderHTML,
            IBasicCSS sliderCSS, IBasicJS sliderJS, ISubscriptionService subscriptionService)
        {
            _APIDbContext = apiDbContext;
            _UserManager = userManager;
            _PromoService = promoService;
            _MainJS = mainJS;
            _PromoHTML = sliderHTML;
            _PromoCSS = sliderCSS;
            _PromoJS = sliderJS;
            _SubscriptionService = subscriptionService;
        }

        [HttpGet]
        [Route("{clientID}")]
        public async Task<string> GetAsync(string clientID)
        {
            Promotion clientPromotion = null;

            try
            {
                using (_APIDbContext)
                {
                    ApplicationUser user = await _UserManager.FindByIdAsync(clientID);
                    if (!_SubscriptionService.SubscriptionActiveAndWithinTrafficLimit(user))
                    {
                        return "Issue found with OsOEasyPromo account, please check account status at OsOEasyPromo.com!";
                    }

                    clientPromotion = _APIDbContext.Promotion
                        .Where(c => c.ApplicationUser.Id == clientID && c.ActivePromotion == true).FirstOrDefault();

                    if (clientPromotion != null)
                    {
                        //Task the update to stats out?
                        //https://stackoverflow.com/questions/1018610/simplest-way-to-do-a-fire-and-forget-method-in-c
                        _PromoService.UpdatePromotionStats(clientPromotion, _APIDbContext);
                        return _MainJS.GetMainJS(clientPromotion.Id);
                    }
                    else
                    {
                        return "Error, no active promotion found";
                    }
                }

            }
            catch (Exception ex)
            {
                return "Error, unknown exception occured.";
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
        [Route("submit/{promoId}/{name}/{email}")]
        public async Task<string> ClaimPromotionAsync(string promoId, string name, string email)
        {
            Promotion clientPromotion = null;
            IRestResponse response = null;

            try
            {
                using (_APIDbContext)
                {
                    clientPromotion = _APIDbContext.Promotion
                        .Where(c => c.Id == promoId).FirstOrDefault();
                    response = await _PromoService.SendPromoEmail(email, name, clientPromotion.Code);                    

                    if (response.IsSuccessful)
                    {
                        _PromoService.HandleCLaimedPromotion(clientPromotion, _APIDbContext, name, email, clientPromotion.ApplicationUser);
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