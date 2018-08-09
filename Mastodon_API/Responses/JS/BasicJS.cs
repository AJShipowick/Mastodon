using OsOEasy.Data.Models;
using System.IO;

namespace OsOEasy.API.Responses.JS
{

    public interface IBasicJS
    {
        string GetSliderJS(Promotion promotion);
    }

    public class BasicJS : IBasicJS
    {
        public string GetSliderJS(Promotion promotion)
        {
            string minJS = File.ReadAllText("Responses/JS/BasicJS.min.js");
            minJS = minJS.Replace("oso_side_of_screen", promotion.SideOfScreen);

            return minJS;
        }
    }
}
