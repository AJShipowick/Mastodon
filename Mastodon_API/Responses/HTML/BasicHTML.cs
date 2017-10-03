using Mastodon.Slider.Models;

namespace Mastodon_API.Responses.HTML
{

    public interface IBasicHTML
    {
        string getSliderHTML(ClientsWebsite clientWebsiteData);
    }

    public class BasicHTML : IBasicHTML
    {
        public string getSliderHTML(ClientsWebsite clientWebsiteData)
        {
            //user clientWebsiteData.stuff....
            //build form SliderHTML.html
            return "<img id='slickImage' onclick='slickSliderClicked()'/><div id='slickContactForm' class='slickFont'> <h2>Contact Form</h2> <p>This is my form.Please fill it out.It's awesome!</p><div class='slickInputBox'> <input required id='sliderName' type='text' placeholder='Name'> </div><div class='slickInputBox'> <input required id='sliderEmail' type='text' placeholder='Email'> </div><div> <textarea required id='sliderComment' class='userInputTextArea' placeholder='Message'></textarea> </div><p id='sliderResponseMessage'></p><div> <button class='button' onclick='submitSlider()'>Send Message</button> </div></div>";
        }
    }
}
