using Mastodon.Slider.Models;
using System.Text;

namespace Mastodon_API.Responses
{

    public interface IMainJS
    {
        string GetMainJS(ClientsWebsite clientWebsiteData);
    }

    public class MainJS : IMainJS
    {
        public string GetMainJS(ClientsWebsite clientWebsiteData)
        {

            //use clientWebsiteData.stuff....

            var sb = new StringBuilder(1000);        
            sb.AppendLine("!function(){function e(e){document.getElementById('slickSlider').innerHTML=e}function t(e){var t=document.createElement('style');t.type='text/css',t.innerHTML=e,document.getElementsByTagName('head')[0].appendChild(t)}function n(e){var t=document.createElement('script');t.innerHTML=e,document.getElementsByTagName('head')[0].appendChild(t)}function d(){c('http://localhost:51186/api/slider/image/6666ddfdd',o)}function o(e){a(e)}function i(e,t){var n=new XMLHttpRequest;n.onreadystatechange=function(){4===n.readyState&&t(n.response)},n.open('GET',e,!0),n.send()}function c(e,t){var n=new XMLHttpRequest;n.open('GET',e,!0),n.responseType='blob',n.onload=function(e){t(n.response)},n.send()}function a(e){var t=(window.URL||window.webkitURL).createObjectURL(e);document.querySelector('#slickImage').src=t}var s=document.createElement('div');s.setAttribute('id','slickSlider'),document.body.appendChild(s),i('http://localhost:51186/api/slider/html/6666ddfdd',e),i('http://localhost:51186/api/slider/css/6666ddfdd',t),i('http://localhost:51186/api/slider/js/6666ddfdd',n),setTimeout(function(){d()},500)}();");
            return sb.ToString(); 
        }
    }
}
