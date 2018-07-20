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
            sb.Append("<script>/*Begin Oso Easy Promo Script*/");

            //Insert minified JS script below from OsOSiteLoadingScript.js
            sb.Append("(function(){let a=new XMLHttpRequest;a.onreadystatechange=function(){if(4===a.readyState)if(200===a.status){if(document.body.className='ok',!a.responseText||a.responseText.includes('ERROR')||a.responseText.includes('WARNING'))return void console.log(a.responseText);let b=document.createElement('script');b.innerHTML=a.responseText,document.getElementsByTagName('head')[0].appendChild(b)}else console.log('Oso Easy Promo Error'+a.responseText)},a.open('GET','https://api.osoeasypromo.com/api/promo/USERID',!0),a.send(null)})();");

            sb.Append("/*End Oso Easy Promo Script*/</script>");

            sb = sb.Replace("USERID", customerID);
            return sb.ToString();

        }

    }
}
