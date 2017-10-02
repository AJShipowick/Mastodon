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
            //build form SliderJS.js
            return "var slickSliderOpen;function slickSliderClicked(){if(!this.slickSliderOpen){document.documentElement.style.overflowX='hidden';document.getElementById('slickContactForm').style.visibility='visible';document.getElementById('slickSlider').style.right='-300px';showSlickSlider();document.documentElement.style.overflowX='inherit'}else{closeSlickSlider()}this.slickSliderOpen=!this.slickSliderOpen}function closeSlickSlider(){document.getElementById('slickContactForm').style.visibility='hidden';}function showSlickSlider(){var slidingDiv=document.getElementById('slickSlider');var stopPosition=0;if(parseInt(slidingDiv.style.right)<stopPosition){slidingDiv.style.right=parseInt(slidingDiv.style.right)+5+'px';setTimeout(showSlickSlider,1)}}";
        }
    }
}
