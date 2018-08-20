using OsOEasy.Data.Models;
using System.IO;

namespace OsOEasy.API.Responses.CSS
{

    public interface IBasicCSS
    {
        string GetSliderCSS(Promotion clientWebsiteData);
    }

    public class BasicCSS : IBasicCSS
    {
        public string GetSliderCSS(Promotion promotion)
        {
            string minCSS = File.ReadAllText("Responses/Coupon/CSS/BasicCSS.min.css");

            var imageWidth = (promotion.ShowLargeImage ? "96px" : "64px");

            minCSS = minCSS.Replace("oso_background_color", promotion.BackgroundColor);
            minCSS = minCSS.Replace("oso_button_color", promotion.ButtonColor);
            minCSS = minCSS.Replace("oso_side_of_screen", promotion.SideOfScreen);
            minCSS = minCSS.Replace("oso_image_width", imageWidth);

            if (promotion.ShowCouponBorder)
            {
                minCSS = promotion.ShowCouponBorder ? minCSS.Replace("oso_coupon_border", "4px dashed #ccc") 
                    : minCSS.Replace("oso_coupon_border", "1px solid #d8d8d8");
            }

            return minCSS;
        }
    }
}
