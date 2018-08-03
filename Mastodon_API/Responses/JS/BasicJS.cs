using System.IO;

namespace OsOEasy.API.Responses.JS
{

    public interface IBasicJS
    {
        string GetSliderJS();
    }

    public class BasicJS : IBasicJS
    {
        public string GetSliderJS()
        {
            string minJS = File.ReadAllText("Responses/JS/BasicJS.min.js");
            return minJS;
        }
    }
}
