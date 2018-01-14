using OsOEasy.Models.DBModels;

namespace OsOEasy_API.Responses.HTML
{

    public interface IBasicHTML
    {
        string getSliderHTML(Promotion promotion);
    }

    public class BasicHTML : IBasicHTML
    {
        public string getSliderHTML(Promotion promotion)
        {
            //todo use stringBuilder???
            //built from SliderHTML.html
            string minHTML = "<img id='osoImage' src='?image?' style='right:0px' onclick='osoSliderClicked()'/><div id='osoContactForm' class='osoFont'> <h2>?title?</h2> <h3>?discount?</h3> <p>Ends: ?endDate?</p><dl> <dt>?details1?</dt> <dt>?details2?</dt> </dl> <p><small>?finePrint?</small></p><div id='osoFormInput'> <div class='osoInputBox'> <input required id='osoUserName' type='text' placeholder='Full name'> </div><div class='osoInputBox'> <input required id='osoUserEmail' type='text' placeholder='Email'> </div><p id='osoPromoResponseMessage'></p><div style='text-align:center'> <button id='osoButton' class='button' onclick='submitOSOEasyPromotion()'>Claim Promotion</button> </div></div><div id='thankYou' hidden> <h2>?thankYou?</h2> </div><div style='text-align:center'> <a href='https://www.OsOEasyPromo.com' style='font-size:60%; color:black'>Built with OsOEasyPromo</a> </div></div>";
            minHTML = minHTML.Replace("?image?", string.Format("http://localhost:51186/images/Promo/{0}/{1}",
                    promotion.ImageType, promotion.ImageName));
            minHTML = minHTML.Replace("?title?", promotion.Title);
            minHTML = minHTML.Replace("?discount?", promotion.Discount);
            minHTML = minHTML.Replace("?endDate?", promotion.EndDate);
            minHTML = minHTML.Replace("?details1?", promotion.Details1);
            minHTML = minHTML.Replace("?details2?", promotion.Details2);
            minHTML = minHTML.Replace("?finePrint?", promotion.FinePrint);
            minHTML = minHTML.Replace("?thankYou?", promotion.ThankYouMessage);
            return minHTML;
        }
    }
}
