using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mastodon_API.Data;
using Mastodon.Slider.Models;

namespace Mastodon_API.Controllers
{
    [Route("api/[controller]")]
    public class SliderController : Controller
    {

        APIDbContext _apiDbContext;

        public SliderController(APIDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpGet]
        [Route("{clientID}")]
        public string Get(string clientID) //IEnumerable<string>
        {

            //Build html/css/js based on user clientID
            //What about 1 user with multiple sites????


            List<ClientsWebsite> clientWebsites = null;
            using (_apiDbContext)
            {
                clientWebsites = _apiDbContext.ClientsWebsites
                    .Where(c => c.ClientID == "6056e4a8-32a8-4042-b750-50c609edf140").ToList();
            }

            return clientWebsites.FirstOrDefault().WebsiteName;

        }

        [HttpGet]
        [Route("html/{clientID}")]
        public string GetHTML(string clientID) //IEnumerable<string>
        {
            //
            return "html response";
        }

        [HttpGet]
        [Route("js/{clientID}")]
        public string GetJS(string clientID) //IEnumerable<string>
        {
            //
            return "js response";
        }

        [HttpGet]
        [Route("css/{clientID}")]
        public string GetCSS(string clientID) //IEnumerable<string>
        {
            //
            return "css response";
        }
        [HttpGet]
        [Route("image/{clientID}")]
        public string GetImage(string clientID) //IEnumerable<string>
        {
            //
            return "image response";
        }
    }
}
