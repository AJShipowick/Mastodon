using Microsoft.AspNetCore.Hosting;
using OsOEasy.API.Shared;
using OsOEasy.Data.Models;
using System.IO;

namespace OsOEasy.API.Responses.HTML
{

    public interface IBasicHTML
    {
        string getSliderHTML(Promotion promotion);
    }

    public class BasicHTML : IBasicHTML
    {

        private IHostingEnvironment _env;

        public BasicHTML(IHostingEnvironment env)
        {
            _env = env;
        }

        public string getSliderHTML(Promotion promotion)
        {
            string minHTML = File.ReadAllText("Responses/HTML/BasicHTML.min.html");

            minHTML = minHTML.Replace("?image?", string.Format("'https://api.osoeasypromo.com/images/Promo/{0}/{1}",
                    promotion.ImageType, promotion.ImageName + "'"));

            if (_env.IsDevelopment())
            {
                minHTML = minHTML.Replace(Common.LIVE_API_URL, Properties.Resources.Local_API_URL);
            }

            minHTML = minHTML.Replace("?title?", promotion.Title);
            minHTML = minHTML.Replace("?discount?", promotion.Discount);
            minHTML = minHTML.Replace("?endDate?", promotion.EndDate);
            minHTML = minHTML.Replace("?details1?", promotion.Details1);
            minHTML = minHTML.Replace("?details2?", promotion.Details2);
            minHTML = minHTML.Replace("?finePrint?", promotion.FinePrint);
            minHTML = minHTML.Replace("?thankYou?", promotion.ThankYouMessage);
            return minHTML;
        }
    }
}
