using Microsoft.AspNetCore.Identity;
using System;

namespace OsOEasy.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Website { get; set; }
        public string SubscriptionPlan { get; set; }
        public bool AccountSuspended { get; set; }
        public bool MonthlyPromotionLimitReached { get; set; }
        public string UserPromoScript { get; set; }
        public DateTime DateOfLastPromoClaim { get; set; }
        public int PromoClaimsForCurrentMonth { get; set; }

        public bool IsNewUser { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public int TimesLoggedIn { get; set; }
        public DateTime LastLoginDate { get; set; }

        public bool IsAdmin { get; set; }  //todo use this in future to manager/view users accounts without having to directly query DB to find out about their stuff.....
    }

    public static class SubscriptionOptions
    {
        public const string FreeAccount = "Free Account";
        public const int MaxFreeAccountClaims = 100;

        public const string Bronze = "Bronze";
        public const int MaxBronzeAccountClaims = 1500;

        public const string Silver = "Silver";
        public const int MaxSilverAccountClaims = 5000;

        public const string Gold = "Gold";
        //Unlimited claims per month        
    }
}