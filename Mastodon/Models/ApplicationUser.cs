using Mastodon.Promo.Models.DBModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Mastodon.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Promotion> Promotions { get; set; }
        public virtual ICollection<AccountActivity> AccountActivity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string SubscriptionPlan { get; set; }
        public string UserPromoScript { get; set; }
        public string ClientNotes { get; set; }
    }

    public class SubscriptionOptions
    {
        public const string FreeAccount = "Free Account";
        public const string Bronze = "Bronze";
        public const string Silver = "Silver";
        public const string Gold = "Gold";
    }

}