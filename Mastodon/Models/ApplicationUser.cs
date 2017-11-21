using Microsoft.AspNetCore.Identity;

namespace OsOEasy.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string SubscriptionPlan { get; set; }
        public string UserPromoScript { get; set; }
        public string ClientNotes { get; set; }
        public bool NewUser { get; set; }
        public bool IsAdmin { get; set; }  //todo use this in future to manager/view users accounts without having to directly query DB to find out about their stuff.....
    }

    public class SubscriptionOptions
    {
        public const string FreeAccount = "Free Account";
        public const string Bronze = "Bronze";
        public const string Silver = "Silver";
        public const string Gold = "Gold";
    }

}