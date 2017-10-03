namespace Mastodon_API.Responses
{
    public interface IMainJS
    {
        string GetMainJS(string clientID);
    }

    public class MainJS : IMainJS
    {

        /// <summary>
        /// Gets the main JS to load user defined HTML/CSS/JS in other calls.
        /// The only varibles here id the clientID that relates currently to only 1 website.
        /// </summary>
        /// <param name="clientWebsiteData"></param>
        public string GetMainJS(string clientID)
        {
            //This minified string comes from MainJS.js
            string minJS = "(function(){var sliderDiv=document.createElement('div');sliderDiv.setAttribute('id','sliderContainer');document.body.appendChild(sliderDiv);loadSlickHTML();loadSlickCSS();loadSlickJS();setTimeout(function(){loadSlickImage()},250);function loadSlickHTML(){var htmlURL='http://localhost:51186/api/slider/html/?';getSlickResource(htmlURL,handleHTMLCallback)}function handleHTMLCallback(data){document.getElementById('sliderContainer').innerHTML=data}function loadSlickCSS(){var cssURL='http://localhost:51186/api/slider/css/?';getSlickResource(cssURL,handleCSSCallback)}function handleCSSCallback(data){var style=document.createElement('style');style.type='text/css';style.innerHTML=data;document.getElementsByTagName('head')[0].appendChild(style)}function loadSlickJS(){var jsURL='http://localhost:51186/api/slider/js/?';getSlickResource(jsURL,handleJSCallback)}function handleJSCallback(data){var script=document.createElement('script');script.innerHTML=data;document.getElementsByTagName('head')[0].appendChild(script)}function loadSlickImage(){var imageURL='http://localhost:51186/api/slider/image/?';getImageResource(imageURL,handleImageCallback)}function handleImageCallback(data){showSlickImageOnScreen(data)}function getSlickResource(resourceURL,callback){var xhr=new XMLHttpRequest();xhr.onreadystatechange=function(){if(xhr.readyState===4){callback(xhr.response);}else{}};xhr.open('GET',resourceURL,true);xhr.send()}function getImageResource(imageURL,callback){var oReq=new XMLHttpRequest();oReq.open('GET',imageURL,true);oReq.responseType='blob';oReq.onload=function(oEvent){callback(oReq.response)};oReq.send()}function showSlickImageOnScreen(blobData){var urlCreator=window.URL||window.webkitURL;var imageUrl=urlCreator.createObjectURL(blobData);document.querySelector('#slickImage').src=imageUrl}})();";
            return minJS.Replace("?", clientID);            
        }
    }
}
