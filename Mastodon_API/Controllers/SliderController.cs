using Mastodon.Slider.Models;
using Mastodon_API.Data;
using Mastodon_API.Responses;
using Mastodon_API.Responses.CSS;
using Mastodon_API.Responses.HTML;
using Mastodon_API.Responses.JS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Mastodon_API.Controllers
{
    [Route("api/[controller]")]
    public class SliderController : Controller
    {

        APIDbContext _apiDbContext;
        IMainJS _mainJS;
        IBasicHTML _sliderHTML;
        IBasicCSS _sliderCSS;
        IBasicJS _sliderJS;

        public SliderController(APIDbContext apiDbContext, IMainJS mainJS, IBasicHTML sliderHTML, IBasicCSS sliderCSS, IBasicJS sliderJS)
        {
            _apiDbContext = apiDbContext;
            _mainJS = mainJS;
            _sliderHTML = sliderHTML;
            _sliderCSS = sliderCSS;
            _sliderJS = sliderJS;
        }

        [HttpGet]
        [Route("{clientID}")]
        public string Get(string clientID)
        {
            ClientsWebsite clientWebsites = null;

            try
            {
                using (_apiDbContext)
                {
                    clientWebsites = _apiDbContext.ClientsWebsites
                        .Where(c => c.ClientID == clientID).FirstOrDefault();
                }

                return _mainJS.GetMainJS(clientWebsites.ClientID);
            }
            catch (Exception ex)
            {
                //todo log exception
                return "Error getting main slider.";
            }
        }

        [HttpGet]
        [Route("html/{clientID}")]
        public string GetHTML(string clientID)
        {
            ClientsWebsite clientWebsites = null;

            try
            {
                using (_apiDbContext)
                {
                    clientWebsites = _apiDbContext.ClientsWebsites
                        .Where(c => c.ClientID == clientID).FirstOrDefault();
                }

                return _sliderHTML.getSliderHTML(clientWebsites);
            }
            catch (Exception ex)
            {
                //todo log exception
                return "Error getting slider HTML.";
            }

        }

        [HttpGet]
        [Route("css/{clientID}")]
        public string GetCSS(string clientID)
        {
            ClientsWebsite clientWebsites = null;

            try
            {
                using (_apiDbContext)
                {
                    clientWebsites = _apiDbContext.ClientsWebsites
                        .Where(c => c.ClientID == clientID).FirstOrDefault();
                }

                return _sliderCSS.GetSliderCSS(clientWebsites);
            }
            catch (Exception ex)
            {
                //todo log exception
                return "Error getting slider CSS.";
            }

        }

        [HttpGet]
        [Route("image/{clientID}")]
        public IActionResult GetImage(string clientID)
        {
            ClientsWebsite clientWebsites = null;

            try
            {
                using (_apiDbContext)
                {
                    clientWebsites = _apiDbContext.ClientsWebsites
                        .Where(c => c.ClientID == clientID).FirstOrDefault();
                }

                var image = System.IO.File.OpenRead(string.Format("wwwroot/images/{0}.png", clientWebsites.SliderImageName));
                return File(image, "image/png");
            }
            catch (Exception ex)
            {
                //todo log exception
                return null;
            }
        }

        [HttpGet]
        [Route("js/{clientID}")]
        public string GetJS(string clientID)
        {
            ClientsWebsite clientWebsites = null;

            try
            {
                using (_apiDbContext)
                {
                    clientWebsites = _apiDbContext.ClientsWebsites
                        .Where(c => c.ClientID == clientID).FirstOrDefault();
                }

                return _sliderJS.GetSliderJS(clientWebsites);
            }
            catch (Exception ex)
            {
                //todo log exception
                return "Error getting slider JS.";
            }
        }

        [HttpGet]
        [Route("submit/{clientID}/{name}/{email}/{comment}")]
        public string SubmitForm(string clientID, string name, string email, string comment)
        {
            ClientsWebsite clientWebsites = null;

            //todo validate form fields here...and in JS as well....

            try
            {
                using (_apiDbContext)
                {
                    clientWebsites = _apiDbContext.ClientsWebsites
                        .Where(c => c.ClientID == clientID).FirstOrDefault();
                }

                //send email async to owner of the slider and notify them of the contact....

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                //todo log exception
                return "ERROR";
            }

        }

    }
}