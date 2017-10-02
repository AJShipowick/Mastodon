using Mastodon.Slider.Models;

namespace Mastodon_API.Responses
{

    public interface ISliderHTML
    {
        string getSliderHTML(ClientsWebsite clientWebsiteData);
    }

    public class SliderHTML : ISliderHTML
    {
        public string getSliderHTML(ClientsWebsite clientWebsiteData)
        {
            //user clientWebsiteData.stuff....
            //build form SliderHTML.html
            return "<div id='slickSlider'> <img id='slickImage' onclick='slickSliderClicked()'/> <div id='slickContactForm' class='slickFont Fixed'> <h2>Contact Form</h2> <p>This is my form.Please fill it out.It's awesome!</p><div class='slickInputBox'> <input type='text' placeholder='Your Name'> </div><div class='slickInputBox'> <input type='text' placeholder='Your Email'> </div><textarea class='userInputTextArea'>Message</textarea> <button class='button button1'>Send Message</button> </div></div>";
        }
    }
}
