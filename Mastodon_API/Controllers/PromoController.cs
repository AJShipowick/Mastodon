using OsOEasy.Promo.Models;
using OsOEasy_API.Data;
using OsOEasy_API.Responses;
using OsOEasy_API.Responses.CSS;
using OsOEasy_API.Responses.HTML;
using OsOEasy_API.Responses.JS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using OsOEasy.Models.DBModels;
using OsOEasy_API.Services;
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
                    }
                    else
                    {
                        return "No active promotion found";
                    }
                }

            }
            catch (Exception ex)
            {
                //todo log exception
                return "Error getting main slider.";
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
        [Route("image/{promoId}")]
        public IActionResult GetImage(string promoId)
        {
            Promotion clientPromotion = null;

            try
            {
                using (_APIDbContext)
                {
                    clientPromotion = _APIDbContext.Promotion
                        .Where(c => c.Id == promoId).FirstOrDefault();
                }

                var image = System.IO.File.OpenRead(string.Format("wwwroot/images/promo/{0}/{1}",
                    clientPromotion.ImageType, clientPromotion.ImageName));
                return File(image, "image/svg");
            }
            catch (Exception ex)
            {
                //todo log exception
                return null;
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
        public string SubmitForm(string promoId, string name, string email)
        {
            //ClientsWebsite clientWebsites = null;

            ////todo validate form fields here...and in JS as well....

            //try
            //{
            //    using (_apiDbContext)
            //    {
            //        clientWebsites = _apiDbContext.ClientsWebsites
            //            .Where(c => c.ClientID == clientID).FirstOrDefault();
            //    }

            //    //send email async to owner of the slider and notify them of the contact....

            //    return "SUCCESS";
            //}
            //catch (Exception ex)
            //{
            //    //todo log exception
            //    return "ERROR";
            //}

            return "";

        }

    }
}