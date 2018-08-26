using Microsoft.AspNetCore.Hosting;
using OsOEasy.API.Shared;
using OsOEasy.Data.Models;
using System.IO;
using System.Text;

namespace OsOEasy.API.Responses
{
    public interface IMainJS
    {
        string GetMainCouponJS(string clientId, Promotion promotion);
        string GetMainSocialHTML(SocialSharing socialPromotion);
    }

    public class MainJS : IMainJS
    {
        private IHostingEnvironment _env;

        private const string _Facebook = "facebook";
        private const string _Twitter = "twitter";
        private const string _Instagram = "instagram";
        private const string _Linkedin = "linkedin";
        private const string _Pinterest = "pinterest";

        public MainJS(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Gets the main Coupon JS to load user defined HTML/CSS/JS in other calls.        
        /// </summary>
        public string GetMainCouponJS(string clientId, Promotion promotion)
        {

            string minJS = File.ReadAllText("Responses/MainJS.min.js");

            if (_env.IsDevelopment())
            {
                minJS = minJS.Replace(Common.LIVE_API_URL, Properties.Resources.Local_API_URL);
            }

            minJS = minJS.Replace("CLIENTID", clientId);
            minJS = minJS.Replace("oso_side_of_screen", promotion.SideOfScreen);
            minJS = minJS.Replace("?", promotion.Id);

            return minJS;
        }

        /// <summary>
        /// String builder building HTML based off of MailHTML.html file
        /// </summary>
        public string GetMainSocialHTML(SocialSharing socialPromotion)
        {
            var html = new StringBuilder();

            var sideOfScreen = socialPromotion.SideOfScreen == "right" ? "right:0px;" : "left:0px;";
            html.Append("<div style='position:fixed;z-index:99 !important;" + sideOfScreen + "top: 45%;'" + "id='OsoEasyPromo.com_SocialPromotion'>");

            //Facebook
            if (socialPromotion.UseFacebook)
            {
                html.Append(GetSocialHTMLLine(_Facebook, socialPromotion.FacebookURL, socialPromotion.FacebookImageName));
            }

            //Twitter
            if (socialPromotion.UseTwitter)
            {
                html.Append(GetSocialHTMLLine(_Twitter, socialPromotion.TwitterURL, socialPromotion.TwitterImageName));
            }

            //Instagram
            if (socialPromotion.UseInstagram)
            {
                html.Append(GetSocialHTMLLine(_Instagram, socialPromotion.InstagramURL, socialPromotion.InstagramImageName));
            }

            //Linkedin
            if (socialPromotion.UseLinkedin)
            {
                html.Append(GetSocialHTMLLine(_Linkedin, socialPromotion.LinkedinURL, socialPromotion.LinkedinImageName));
            }

            //Pinterest
            if (socialPromotion.UsePinterest)
            {
                html.Append(GetSocialHTMLLine(_Pinterest, socialPromotion.PinterestURL, socialPromotion.PinterestImageName));
            }

            //Closing div tag
            html.Append("</div>");

            return html.ToString();
        }

        private string GetSocialHTMLLine(string imageType, string socialURL, string imageName)
        {
            var fullSocialURL = string.Format("'https://www.{0}.com/{1}'", imageType, socialURL);
            var imageURL = GetSocialImageURL(imageType, imageName);

            var fullHTMLLine = string.Format("<a href={0} target=_blank><img src={1} style='cursor: pointer; height: 45px; width: 45px; display: block'/></a>", fullSocialURL, imageURL);

            return fullHTMLLine;
        }

        private string GetSocialImageURL(string imageType, string imageName)
        {
            string imageFullPath = string.Format("'https://osoeasypromo.com/images/social/{0}/{1}'", imageType, imageName);

            if (_env.IsDevelopment())
            {
                imageFullPath = imageFullPath.Replace(Common.LIVE_SITE_URL, Properties.Resources.Local_WEB_URL);
            }

            return imageFullPath;
        }
    }
}