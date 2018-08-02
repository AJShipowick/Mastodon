using Microsoft.AspNetCore.Hosting;
using OsOEasy.API.Shared;
using System.IO;

namespace OsOEasy.API.Responses
{
    public interface IMainJS
    {
        string GetMainJS(string clientId, string promotionId);
    }

    public class MainJS : IMainJS
    {
        private IHostingEnvironment _env;

        public MainJS(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Gets the main JS to load user defined HTML/CSS/JS in other calls.        
        /// </summary>
        public string GetMainJS(string clientId, string promotionId)
        {

            string minJS = File.ReadAllText("Responses/MainJS.min.js");

            if (_env.IsDevelopment())
            {
                minJS = minJS.Replace(Common.LIVE_API_URL, Properties.Resources.Local_API_URL);
            }

            minJS = minJS.Replace("CLIENTID", clientId);
            return minJS.Replace("?", promotionId);            
        }
    }
}
