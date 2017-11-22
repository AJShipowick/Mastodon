using OsOEasy.Models.DBModels;

namespace OsOEasy_API.Responses.CSS
{

    public interface IBasicCSS
    {
        string GetSliderCSS(Promotion clientWebsiteData);
    }

    public class BasicCSS : IBasicCSS
    {
        public string GetSliderCSS(Promotion clientWebsiteData)
        {

            //todo implement custom user settings for CSS here...color, fonts....stuff.

            //build form SliderCSS.css
            return ".slickFont{font-family:Helvetica}#sliderContainer{position:fixed;z-index:10;right:0;top:10%}#slickImage{position:relative;box-shadow:0 0 8px gray;margin-top:15%;cursor:pointer;float:right}#slickContactForm{position:relative;width:250px;border:1px solid #d8d8d8;padding:10px 20px;box-shadow:5px 0 50px gray;visibility:hidden;background-color:white;float:right}.slickInputBox input[type='text']{padding:10px;border:solid 1px #dcdcdc;transition:box-shadow 0.3s, border 0.3s;width:95%;margin-bottom:5px}.slickInputBox input[type='text'].focus{border:solid 1px #707070;box-shadow:0 0 5px 1px #969696}.userInputTextArea{padding:10px;height:50px;width:95%;resize:none;border:solid 1px #dcdcdc;margin-bottom:5px}.userInputTextArea.focus{border:solid 1px #707070;box-shadow:0 0 5px 1px #969696}.button{background-color:#4CAF50;border:none;color:white;padding:5%;font-size:100%;cursor:pointer}";
        }
    }
}
