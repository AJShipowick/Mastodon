using System;
using System.Text;

namespace OsOEasy.Services
{
    /// <summary>
    /// Buils the loading script for the user
    /// This js is built from a minified version of OsOSiteLoadingScript.js
    /// </summary>
    public class OsOSiteLoadingScript
    {

        public String BuildSiteLoadingScript(String customerID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/*Begin Oso Easy Promo Script*/");

            //Insert minified JS script below from OsOSiteLoadingScript.js
            sb.Append("(function(){var a=new XMLHttpRequest;a.onreadystatechange=function(){if(4===a.readyState)if(200===a.status){if(document.body.className='ok',a.responseText.includes('ERROR'))return void console.log(a.responseText);var b=document.createElement('script');b.innerHTML=a.responseText,document.getElementsByTagName('head')[0].appendChild(b)}else console.log('OsO Easy Promo Error'+a.responseText)},a.open('GET','http://localhost:51186/api/promo/USERID',!0),a.send(null)})();");

            sb.Append("/*End Oso Easy Promo Script*/");

            sb = sb.Replace("USERID", customerID);
            return sb.ToString();

        }

    }
}
