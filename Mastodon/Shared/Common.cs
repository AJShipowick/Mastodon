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
        bool FreeTrialActive(ApplicationUser user);
        int DaysSinceAccountSignup(DateTime accountCreationDate);
    }

    public class Common : ICommon
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public static int _DaysForFreeTrial = 10;

        public Common(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public bool FreeTrialActive(ApplicationUser user)
        {
            int daysSinceSignup = DaysSinceAccountSignup(user.AccountCreationDate);
            bool freeTrailActive = (user.SubscriptionPlan == SubscriptionOptions.FreeAccount && daysSinceSignup <= _DaysForFreeTrial);

            return freeTrailActive;
        }

        public int DaysSinceAccountSignup(DateTime accountCreationDate)
        {
            return DateTime.Today.Subtract(accountCreationDate).Days;
        }

        public Task<ApplicationUser> GetCurrentUserAsync(HttpContext context)
        {
            return _userManager.GetUserAsync(context.User);
        }

    }
}
