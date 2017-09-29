using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mastodon_API.Data;
using Mastodon.Slider.Models;
using Mastodon_API.Responses;
using System.Threading.Tasks;

namespace Mastodon_API.Controllers
{
    [Route("api/[controller]")]
    public class SliderController : Controller
    {

        APIDbContext _apiDbContext;
        IMainJS _mainJS;
        ISliderHTML _sliderHTML;
        ISliderCSS _sliderCSS;
        ISliderJS _sliderJS;

        public SliderController(APIDbContext apiDbContext, IMainJS mainJS, ISliderHTML sliderHTML, ISliderCSS sliderCSS, ISliderJS sliderJS)
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
            using (_apiDbContext)
            {
                clientWebsites = _apiDbContext.ClientsWebsites
                    .Where(c => c.ClientID == clientID).FirstOrDefault();
            }

            return _mainJS.GetMainJS(clientWebsites);
        }

        [HttpGet]
        [Route("html/{clientID}")]
        public string GetHTML(string clientID)
        {
            ClientsWebsite clientWebsites = null;
            using (_apiDbContext)
            {
                clientWebsites = _apiDbContext.ClientsWebsites
                    .Where(c => c.ClientID == clientID).FirstOrDefault();
            }

            return _sliderHTML.getSliderHTML(clientWebsites);
        }

        [HttpGet]
        [Route("css/{clientID}")]
        public string GetCSS(string clientID)
        {

            ClientsWebsite clientWebsites = null;
            using (_apiDbContext)
            {
                clientWebsites = _apiDbContext.ClientsWebsites
                    .Where(c => c.ClientID == clientID).FirstOrDefault();
            }

            return _sliderCSS.GetSliderCSS(clientWebsites);
        }

        [HttpGet]
        [Route("image/{clientID}")]
        public IActionResult GetImage(string clientID)
        {

            //https://stackoverflow.com/questions/40794275/return-jpeg-image-from-asp-net-core-webapi

            var image = System.IO.File.OpenRead("wwwroot/images/ContactUs2.png");
            return File(image, "image/png");

        }

        [HttpGet]
        [Route("js/{clientID}")]
        public string GetJS(string clientID)
        {
            ClientsWebsite clientWebsites = null;
            using (_apiDbContext)
            {
                clientWebsites = _apiDbContext.ClientsWebsites
                    .Where(c => c.ClientID == clientID).FirstOrDefault();
            }

            return _sliderJS.GetSliderJS(clientWebsites);

        }
    }
}
