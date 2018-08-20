using Microsoft.AspNetCore.Hosting;
using OsOEasy.Shared;
using System;
using System.IO;
using System.Text;

namespace OsOEasy.Services
{

    public interface IOsOSiteLoadingScript
    {
        string BuildSiteLoadingScript(string customerID);
    }

    /// <summary>
    /// Builds the loading script for the user
    /// This js is built from OsOSiteLoadingScript.min.js
    /// </summary>
    public class OsOSiteLoadingScript : IOsOSiteLoadingScript
    {

        private IHostingEnvironment _env;

        public OsOSiteLoadingScript(IHostingEnvironment env)
        {
            _env = env;
        }

        public string BuildSiteLoadingScript(string customerID)
        {
            string loadingScript = File.ReadAllText("OsOSiteLoadingScript.min.js");

            StringBuilder sb = new StringBuilder();
            sb.Append("<script>/*Begin Oso Easy Promo Script*/");
            sb.Append(loadingScript);
            sb.Append("/*End Oso Easy Promo Script*/</script>");

            sb = sb.Replace("USERID", customerID);

            if (_env.IsDevelopment())
            {
                sb = sb.Replace(Common.LIVE_API_URL, Properties.Resource.Local_API_URL);
            }

            return sb.ToString();

        }
    }
}
