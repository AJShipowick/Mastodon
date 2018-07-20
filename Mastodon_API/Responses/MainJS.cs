namespace OsOEasy.API.Responses
{
    public interface IMainJS
    {
        string GetMainJS(string clientId, string promotionId);
    }

    public class MainJS : IMainJS
    {

        /// <summary>
        /// Gets the main JS to load user defined HTML/CSS/JS in other calls.
        /// The only varibles here id the clientID that relates currently to only 1 website and 1 promotion.
        /// </summary>
        /// <param name="clientWebsiteData"></param>
        public string GetMainJS(string clientId, string promotionId)
        {
            //This minified string is built from MainJS.js
            string minJS = "(function(){function b(h){document.getElementById('osoContainer').innerHTML=h}function d(h){let i=document.createElement('style');i.type='text/css',i.innerHTML=h,document.getElementsByTagName('head')[0].appendChild(i)}function f(h){let i=document.createElement('script');i.innerHTML=h,document.getElementsByTagName('head')[0].appendChild(i)}let g=document.createElement('div');g.setAttribute('id','osoContainer'),document.body.appendChild(g),document.getElementById('osoContainer').style.right='-300px',function(){getSlickResource('https://api.osoeasypromo.com/api/promo/html/?',b)}(),function(){getSlickResource('https://api.osoeasypromo.com/api/promo/css/?',d)}(),function(){getSlickResource('https://api.osoeasypromo.com/api/promo/js/?',f)}()})();function submitOSOEasyPromotion(){document.getElementById('osoPromoResponseMessage').style.display='none';let a=document.getElementById('osoUserName').value,b=document.getElementById('osoUserEmail').value;if(!a||!b){let d=document.getElementById('osoPromoResponseMessage');return d.innerHTML='Please fill out all form fields.',d.style.color='red',void(document.getElementById('osoPromoResponseMessage').style.display='block')}getSlickResource('https://api.osoeasypromo.com/api/promo/submit/?/'+a+'/'+b+'/CLIENTID',handleSubmitCallback)}function getSlickResource(a,b){let c=new XMLHttpRequest;c.onreadystatechange=function(){4===c.readyState&&b(c.response)},c.open('GET',a,!0),c.send()}function handleSubmitCallback(a){document.getElementById('osoFormInput').style.display='none','SUCCESS'!==a&&(document.getElementById('thankYou').innerHTML='Error sending coupon code, please verify email is correct and try again later.',console.log('Error submitting OsoEasyPromo for user: '+a)),document.getElementById('thankYou').style.display='block'}";
            minJS = minJS.Replace("CLIENTID", clientId);
            return minJS.Replace("?", promotionId);            
        }
    }
}
