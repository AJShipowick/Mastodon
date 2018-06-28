using OsOEasy.Data.Models;
using Stripe;
using System;
using System.Collections.Generic;

namespace OsOEasy.Services
{
    public interface IStripeService
    {
        StripeSubscription SubscribeToPlan(ApplicationUser dbUser, string stripeToken, string planToSubscribeTo);
    }

    public class StripeService : IStripeService
    {

        private static readonly string BronzePlanID = "plan_D80DmkSonO4avA";
        private static readonly string SilverPlanID = "plan_D7upPcd0GVyzgi";
        private static readonly string GoldPlanID = "plan_D7uqUOIyi04VU7";

        public StripeService()
        {
            StripeConfiguration.SetApiKey(Environment.GetEnvironmentVariable("STRIPE", EnvironmentVariableTarget.Machine));
        }

        public StripeSubscription SubscribeToPlan(ApplicationUser dbUser, string stripeToken, string planToSubscribeTo)
        {
            StripeCustomer customer = CreateStripeCustomer(dbUser, stripeToken);
            var currentSubscriptionId = String.Empty;
            if (customer.Subscriptions != null && customer.Subscriptions.TotalCount > 0)
            {
                currentSubscriptionId = customer.Subscriptions.Data[0].Id;
            }

            var service = new StripeSubscriptionService();
            StripeSubscription subscription = null;
            if (String.IsNullOrEmpty(currentSubscriptionId))
            {
                subscription = AddNewUserSubscription(customer.Id, planToSubscribeTo, service);
            }
            else
            {

                subscription = UpdateUserSubscription(customer.Id, currentSubscriptionId, planToSubscribeTo, service);
            }

            return subscription;
        }

        private StripeSubscription AddNewUserSubscription(string id, string planToSubscribeTo, StripeSubscriptionService service)
        {
            var items = new List<StripeSubscriptionItemOption>
                { new StripeSubscriptionItemOption {PlanId = GetPlanIdFromPlanName(planToSubscribeTo)} };

            var options = new StripeSubscriptionCreateOptions
            {
                Items = items,
            };
            StripeSubscription subscription = service.Create(id, options);

            return subscription;
        }

        private StripeSubscription UpdateUserSubscription(string id, string currentSubscriptionId, string planToSubscribeTo, StripeSubscriptionService service)
        {
            StripeSubscription subscription = service.Get(currentSubscriptionId);

            var items = new List<StripeSubscriptionItemUpdateOption> {
                new StripeSubscriptionItemUpdateOption {
                    Id = subscription.Items.Data[0].Id,
                    PlanId = GetPlanIdFromPlanName(planToSubscribeTo),
                },
            };
            var options = new StripeSubscriptionUpdateOptions
            {
                CancelAtPeriodEnd = false,
                Items = items,
            };
            subscription = service.Update(currentSubscriptionId, options);

            return subscription;
        }

        private StripeCustomer CreateStripeCustomer(ApplicationUser dbUser, string stripeToken)
        {
            StripeCustomer existingCustomer = GetStripeCustomer(dbUser.StripeCustomerId);
            if (existingCustomer != null) { return existingCustomer; }

            var options = new StripeCustomerCreateOptions
            {
                Email = dbUser.Email,
                SourceToken = stripeToken,
            };
            var service = new StripeCustomerService();
            StripeCustomer customer = service.Create(options);
            return customer;
        }

        public StripeCustomer GetStripeCustomer(string userId)
        {
            StripeCustomer customer = null;

            try
            {
                if (String.IsNullOrEmpty(userId)) { return null; }

                var customerService = new StripeCustomerService();
                customer = customerService.Get(userId);
            }
            catch (Exception ex)
            {
                // todo log ex
            }
            return customer;
        }

        private string GetPlanIdFromPlanName(string planName)
        {
            switch (planName.ToUpper())
            {
                case "BRONZE":
                    return BronzePlanID;
                case "SILVER":
                    return SilverPlanID;
                case "GOLD":
                    return GoldPlanID;
                default:
                    return "UnknownID";
            }
        }
    }
}