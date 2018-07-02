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
