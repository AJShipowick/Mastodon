using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OsOEasy.Data.Models;
using OsOEasy.Services.Stripe;
using System;
using System.Threading.Tasks;

namespace OsOEasy.Shared
{
    public interface ICommon
    {
        Task<ApplicationUser> GetCurrentUserAsync(HttpContext context);
    }

    public class Common : ICommon
    {

        public const string PromoType_Coupon = "coupon";
        public const string PromoType_Social = "social";
        public const string LIVE_API_URL = "https://api.osoeasypromo.com";
        public const string LIVE_SITE_URL = "https://osoeasypromo.com";

        private readonly UserManager<ApplicationUser> _userManager;

        public Common(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<ApplicationUser> GetCurrentUserAsync(HttpContext context)
        {
            return _userManager.GetUserAsync(context.User);
        }
    }

    public static class CommonAccount
    {
        public static int _DaysForFreeTrial = 10;

        public static bool FreeTrialActive(ApplicationUser user)
        {
            int daysSinceSignup = DaysSinceAccountSignup(user.AccountCreationDate);
            bool freeTrailActive = (user.SubscriptionPlan == SubscriptionOptions.FreeAccount && daysSinceSignup <= _DaysForFreeTrial);

            return freeTrailActive;
        }

        public static int DaysSinceAccountSignup(DateTime accountCreationDate)
        {
            return DateTime.Today.Subtract(accountCreationDate).Days;
        }

    }
}
