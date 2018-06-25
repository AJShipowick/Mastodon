using OsOEasy.Data.Models;

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
            string minCSS = ".osoFont{font-family:Helvetica}#osoContainer{position:fixed;width:300px;right:-300px;top:30%}#osoImage{position:fixed;z-index:99 !important;right:0;top:50%;cursor:pointer;width:80px}#osoContactForm{position:relative;width:100%;width:-moz-available;width:-webkit-fill-available;border:?couponBorder?;padding:10px 20px;box-shadow:5px 0 50px gray;background-color:?backgroundColor?;float:right}.osoInputBox input[type='text']{padding:10px;border:solid 1px #dcdcdc;transition:box-shadow 0.3s, border 0.3s;width:95%;margin-bottom:5px}.osoInputBox input[type='text'].focus{border:solid 1px #707070;box-shadow:0 0 5px 1px #969696}.userInputTextArea{padding:10px;height:50px;width:95%;resize:none;border:solid 1px #dcdcdc;margin-bottom:5px}.userInputTextArea.focus{border:solid 1px #707070;box-shadow:0 0 5px 1px #969696}.button{background-color:?buttonColor?;border:none;color:white;padding:5%;font-size:100%;cursor:pointer}.coupon{border:3px dashed #ccc}";
            //todo implement custom user settings for CSS here...color, fonts....stuff.
            minCSS = minCSS.Replace("?backgroundColor?", promotion.BackgroundColor);
            minCSS = minCSS.Replace("?buttonColor?", promotion.ButtonColor);            

            if (promotion.ShowCouponBorder)
            {
                minCSS = promotion.ShowCouponBorder ? minCSS.Replace("?couponBorder?", "4px dashed #ccc") 
                    : minCSS.Replace("?couponBorder?", "1px solid #d8d8d8");
            }

            //build form SliderCSS.css
            return minCSS;
        }
    }
}
