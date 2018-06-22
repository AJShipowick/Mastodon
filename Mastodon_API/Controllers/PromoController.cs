using Microsoft.AspNetCore.Mvc;
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
        IPromoService _PromoService;
        IMainJS _MainJS;
        IBasicHTML _PromoHTML;
        IBasicCSS _PromoCSS;
        IBasicJS _PromoJS;

        public PromoController(APIDbContext apiDbContext, IPromoService promoService, IMainJS mainJS, IBasicHTML sliderHTML,
            IBasicCSS sliderCSS, IBasicJS sliderJS)
        {
            _APIDbContext = apiDbContext;
            _PromoService = promoService;
            _MainJS = mainJS;
            _PromoHTML = sliderHTML;
            _PromoCSS = sliderCSS;
            _PromoJS = sliderJS;
        }

        [HttpGet]
        [Route("{clientID}")]
        public string Get(string clientID)
        {
            Promotion clientPromotion = null;

            try
            {
                using (_APIDbContext)
                {
                    clientPromotion = _APIDbContext.Promotion
                        .Where(c => c.ApplicationUser.Id == clientID && c.ActivePromotion == true).FirstOrDefault();

                    if (clientPromotion != null)
                    {
                        //Task the update to stats out?
                        //https://stackoverflow.com/questions/1018610/simplest-way-to-do-a-fire-and-forget-method-in-c
                        _PromoService.UpdatePromotionStats(clientPromotion, _APIDbContext);
                        return _MainJS.GetMainJS(clientPromotion.Id);
                        //return "<script>alert('hi');</script>";
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

                    _PromoService.HandleCLaimedPromotion(clientPromotion, _APIDbContext, name, email);
                    response = await _PromoService.SendPromoEmail(email, name, clientPromotion.Code);
                }

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                //todo log exception
                return "ERROR";
            }
        }
    }
}