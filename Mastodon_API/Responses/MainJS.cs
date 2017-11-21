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
        /// The only varibles here id the clientID that relates currently to only 1 website.
        /// </summary>
        /// <param name="clientWebsiteData"></param>
        public string GetMainJS(string clientID)
        {
            //This minified string comes from MainJS.js
            string minJS = "(function(){let sliderDiv=document.createElement('div');sliderDiv.setAttribute('id','sliderContainer');document.body.appendChild(sliderDiv);loadSlickHTML();loadSlickCSS();loadSlickJS();setTimeout(function(){loadSlickImage()},500);function loadSlickHTML(){let htmlURL='http://localhost:51186/api/slider/html/?';getSlickResource(htmlURL,handleHTMLCallback)}function handleHTMLCallback(data){document.getElementById('sliderContainer').innerHTML=data}function loadSlickCSS(){let cssURL='http://localhost:51186/api/slider/css/?';getSlickResource(cssURL,handleCSSCallback)}function handleCSSCallback(data){let style=document.createElement('style');style.type='text/css';style.innerHTML=data;document.getElementsByTagName('head')[0].appendChild(style)}function loadSlickJS(){let jsURL='http://localhost:51186/api/slider/js/?';getSlickResource(jsURL,handleJSCallback)}function handleJSCallback(data){let script=document.createElement('script');script.innerHTML=data;document.getElementsByTagName('head')[0].appendChild(script)}function loadSlickImage(){let imageURL='http://localhost:51186/api/slider/image/?';getImageResource(imageURL,handleImageCallback)}function handleImageCallback(data){showSlickImageOnScreen(data)}function getImageResource(imageURL,callback){let oReq=new XMLHttpRequest();oReq.open('GET',imageURL,true);oReq.responseType='blob';oReq.onload=function(oEvent){callback(oReq.response)};oReq.send()}function showSlickImageOnScreen(blobData){let urlCreator=window.URL||window.webkitURL;let imageUrl=urlCreator.createObjectURL(blobData);document.querySelector('#slickImage').src=imageUrl}})();function submitSlider(){let name=document.getElementById('sliderName').value;let email=document.getElementById('sliderEmail').value;let comment=document.getElementById('sliderComment').value;if(!name||!email||!comment){let responseMsg=document.getElementById('sliderResponseMessage');responseMsg.innerHTML='Please fill out all form fields.';responseMsg.style.color='red';return}let submitURL='http://localhost:51186/api/slider/submit/?/'+name+'/'+email+'/'+comment;getSlickResource(submitURL,handleSubmitCallback)}function getSlickResource(resourceURL,callback){let xhr=new XMLHttpRequest();xhr.onreadystatechange=function(){if(xhr.readyState===4){callback(xhr.response);}else{}};xhr.open('GET',resourceURL,true);xhr.send()}function handleSubmitCallback(data){let responseMsg=document.getElementById('sliderResponseMessage');if(data==='SUCCESS'){responseMsg.innerHTML='You message has been sent!';responseMsg.style.color='green'}else{responseMsg.innerHTML='Please fill out all form fields.';responseMsg.style.color='red'}}";
            return minJS.Replace("?", clientID);            
        }
    }
}
