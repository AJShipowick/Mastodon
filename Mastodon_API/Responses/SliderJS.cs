using Mastodon.Slider.Models;

namespace Mastodon_API.Responses
{

    public interface ISliderJS
    {
        string GetSliderJS(ClientsWebsite clientWebsiteData);
    }

    public class SliderJS : ISliderJS
    {
        public string GetSliderJS(ClientsWebsite clientWebsiteData)
        {

            //user clientWebsiteData.stuff....
            //build form JS....

            return "function slickSliderClicked(){this.slickSliderOpen?closeSlickSlider():(document.documentElement.style.overflowX='hidden',document.getElementById('slickContactForm').style.visibility='visible',document.getElementById('slickSlider').style.right='-300px',document.getElementById('slickImage').style.right='300px',showSlickSlider(),document.documentElement.style.overflowX='inherit'),this.slickSliderOpen=!this.slickSliderOpen}function closeSlickSlider(){document.getElementById('slickContactForm').style.visibility='hidden',document.getElementById('slickImage').style.right='0px'}function showSlickSlider(){var e=document.getElementById('slickSlider');parseInt(e.style.right)<0&&(e.style.right=parseInt(e.style.right)+5+'px',setTimeout(showSlickSlider,1))}var slickSliderOpen;";
        }
    }
}
