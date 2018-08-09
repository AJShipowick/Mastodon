using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OsOEasy.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Website { get; set; }
        public string SubscriptionPlan { get; set; }
        public string StripeCustomerId { get; set; }
        public bool AccountSuspended { get; set; }  //Used for non-payment on account
        public bool MonthlyPromotionLimitReached { get; set; }  //Used when claimed promotions exceeds account limit
        public string UserPromoScript { get; set; }
        public DateTime DateOfLastPromoClaim { get; set; }
        public int PromoClaimsForCurrentMonth { get; set; }

        public bool IsNewUser { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public int TimesLoggedIn { get; set; }
        public DateTime LastLoginDate { get; set; }

        public bool IsAdmin { get; set; }  //todo use this in future to manager/view users accounts without having to directly query DB to find out about their stuff.....
    }
}