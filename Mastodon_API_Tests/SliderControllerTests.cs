using Mastodon_API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mastodon_API_Tests
{
    [TestClass]
    public class MainJS
    {
        [TestMethod]
        public void MainJS_NoQuestionMarks()
        {
            var api = new Mastodon_API.Responses.MainJS();
            var userID = "123456-789456-123456-789456";

            var actual = api.GetMainJS(userID);

            //verify stuff.....
        }
    }
}
