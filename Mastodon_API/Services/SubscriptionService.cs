using OsOEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsOEasy_API.Services
{

    public interface ISubscriptionService
    {
        bool SubscriptionActiveAndWithinTrafficLimit(ApplicationUser user);
    }

    public class SubscriptionService: ISubscriptionService
    {
        public bool SubscriptionActiveAndWithinTrafficLimit(ApplicationUser user)
        {
            string subPlan = user.SubscriptionPlan;
            switch (user.SubscriptionPlan)
            {
                case SubscriptionOptions.FreeAccount:

                    break;
                case SubscriptionOptions.Bronze:

                    break;
                case SubscriptionOptions.Silver:

                    break;
                case SubscriptionOptions.Gold:

                    break;
            }


            return true;
        }

    }
}
