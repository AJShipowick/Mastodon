using Mastodon.Promo.Models.DBModels;

namespace Mastodon_API.Responses.HTML
{

    public interface IBasicHTML
    {
        string getSliderHTML(Promotion clientWebsiteData);
    }

    public class BasicHTML : IBasicHTML
    {
        public string getSliderHTML(Promotion clientWebsiteData)
        {
            //user clientWebsiteData.stuff....
            //build form SliderHTML.html
            string minHTML = "<img id='slickImage' onclick='slickSliderClicked()'/><div id='slickContactForm' class='slickFont'> <h2>?FormName?</h2> <p>?CallToActionMessage?</p><div class='slickInputBox'> <input required id='sliderName' type='text' placeholder='Name'> </div><div class='slickInputBox'> <input required id='sliderEmail' type='text' placeholder='Email'> </div><div> <textarea required id='sliderComment' class='userInputTextArea' placeholder='Message'></textarea> </div><p id='sliderResponseMessage'></p><div> <button class='button' onclick='submitSlider()'>Send Message</button> </div></div>";
            //minHTML = minHTML.Replace("?FormName?", clientWebsiteData.FormName);
            //minHTML = minHTML.Replace("?CallToActionMessage?", clientWebsiteData.CallToActionMessage);
            return minHTML;
        }
    }
}
