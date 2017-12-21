using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OsOEasy.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Website { get; set; }
        public string SubscriptionPlan { get; set; }
        public DateTime AccountCreationDate { get; set; }
    }
}
