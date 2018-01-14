namespace OsOEasy_API.Responses
{
    public interface IMainJS
    {
        string GetMainJS(string clientID);
    }

    public class MainJS : IMainJS
    {

        /// <summary>
        /// Gets the main JS to load user defined HTML/CSS/JS in other calls.
        /// The only varibles here id the clientID that relates currently to only 1 website and 1 promotion.
        /// </summary>
        /// <param name="clientWebsiteData"></param>
        public string GetMainJS(string promotionId)
        {
            //This minified string is built from MainJS.js
            string minJS = "(function(){let sliderDiv=document.createElement('div');sliderDiv.setAttribute('id','osoContainer');document.body.appendChild(sliderDiv);loadSlickHTML();loadSlickCSS();loadSlickJS();function loadSlickHTML(){let htmlURL='http://localhost:51186/api/promo/html/?';getSlickResource(htmlURL,handleHTMLCallback)}function handleHTMLCallback(data){document.getElementById('osoContainer').innerHTML=data}function loadSlickCSS(){let cssURL='http://localhost:51186/api/promo/css/?';getSlickResource(cssURL,handleCSSCallback)}function handleCSSCallback(data){let style=document.createElement('style');style.type='text/css';style.innerHTML=data;document.getElementsByTagName('head')[0].appendChild(style)}function loadSlickJS(){let jsURL='http://localhost:51186/api/promo/js/?';getSlickResource(jsURL,handleJSCallback)}function handleJSCallback(data){let script=document.createElement('script');script.innerHTML=data;document.getElementsByTagName('head')[0].appendChild(script)}})();function submitSlider(){let name=document.getElementById('osoUserName').value;let email=document.getElementById('osoUserEmail').value;if(!name||!email){let responseMsg=document.getElementById('osoPromoResponseMessage');responseMsg.innerHTML='Please fill out all form fields.';responseMsg.style.color='red';return}let submitURL='http://localhost:51186/api/promo/submit/?/'+name+'/'+email;getSlickResource(submitURL,handleSubmitCallback)}function getSlickResource(resourceURL,callback){let xhr=new XMLHttpRequest();xhr.onreadystatechange=function(){if(xhr.readyState===4){callback(xhr.response);}};xhr.open('GET',resourceURL,true);xhr.send()}function handleSubmitCallback(data){console.log(data);}";
            return minJS.Replace("?", promotionId);            
        }
    }
}
