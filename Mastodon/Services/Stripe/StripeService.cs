using OsOEasy.Data.Models;
using OsOEasy.Services.MailGun;
using Stripe;
using System;
using System.Collections.Generic;

namespace OsOEasy.Services.Stripe
{
    public interface IStripeService
    {
        StripeSubscription SubscribeToPlan(ApplicationUser dbUser, string stripeToken, string planToSubscribeTo);
        void CancleCustomerSubscription(ApplicationUser dbUser);
    }

    public class StripeService : IStripeService
    {

        private readonly IMailGunEmailSender _emailSender;

        public StripeService(IMailGunEmailSender emailSender)
        {
            _emailSender = emailSender;
            StripeConfiguration.SetApiKey(Environment.GetEnvironmentVariable("STRIPE"));
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
                subscription = AddNewUserSubscription(customer.Id, planToSubscribeTo, service, dbUser);
            }
            else if (!planToSubscribeTo.Contains("Free"))
            {
                subscription = UpdateUserSubscription(customer.Id, currentSubscriptionId, planToSubscribeTo, service, dbUser);
            }
            else
            {
                CancleCustomerSubscription(dbUser);
            }

            return subscription;
        }

        private StripeSubscription AddNewUserSubscription(string id, string planToSubscribeTo, StripeSubscriptionService service, ApplicationUser dbUser)
        {
            var items = new List<StripeSubscriptionItemOption>
                { new StripeSubscriptionItemOption {PlanId = GetPlanIdFromPlanName(planToSubscribeTo)} };

            var options = new StripeSubscriptionCreateOptions
            {
                Items = items,
            };
            StripeSubscription subscription = service.Create(id, options);

            _emailSender.SendEmailAsync(EmailType.NewSubscriber, dbUser.Email, dbUser.FirstName);

            return subscription;
        }

        private StripeSubscription UpdateUserSubscription(string id, string currentSubscriptionId, string planToSubscribeTo, StripeSubscriptionService service, ApplicationUser dbUser)
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

            _emailSender.SendEmailAsync(GetUpgradeOrDowngradeStatus(currentSubscriptionId, subscription.Id), dbUser.Email, dbUser.FirstName);

            return subscription;
        }

        private EmailType GetUpgradeOrDowngradeStatus(string oldSubscriptionId, string newSubscriptionId)
        {
            string oldSubName = GetPlanNameFromId(oldSubscriptionId);
            string newSubname = GetPlanNameFromId(newSubscriptionId);

            if (newSubname == SubscriptionOptions.Gold)
            {
                return EmailType.UpgradeSubscription;
            }
            else if (oldSubName == SubscriptionOptions.Gold && newSubname != SubscriptionOptions.FreeAccount)
            {
                return EmailType.DowngradeSubscription_PaidToPaid;
            }
            else if (newSubname == SubscriptionOptions.FreeAccount)
            {
                return EmailType.DowngradeSubscription_PaidToFree;
            }

            if (newSubname == SubscriptionOptions.Silver)
            {
                if (oldSubName == SubscriptionOptions.Gold)
                {
                    return EmailType.DowngradeSubscription_PaidToPaid;
                }
                else
                {
                    return EmailType.UpgradeSubscription;
                }
            }

            if (newSubname == SubscriptionOptions.Bronze)
            {
                if (oldSubName == SubscriptionOptions.Gold || oldSubName == SubscriptionOptions.Silver)
                {
                    return EmailType.DowngradeSubscription_PaidToPaid;
                }
                else
                {
                    return EmailType.UpgradeSubscription;
                }
            }

            return EmailType.Unknown;

        }

        public void CancleCustomerSubscription(ApplicationUser dbUser)
        {
            StripeCustomer customer = GetStripeCustomer(dbUser.StripeCustomerId);

            if (customer != null && customer.Subscriptions != null && customer.Subscriptions.TotalCount > 0)
            {
                var service = new StripeSubscriptionService();
                StripeSubscription subscription = service.Cancel(customer.Subscriptions.Data[0].Id);
            }
            else
            {
                //todo log error, no user or subscription here....
            }

            _emailSender.SendEmailAsync(EmailType.CancelSubscription, dbUser.Email, dbUser.FirstName);

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
            switch (planName)
            {
                case SubscriptionOptions.Bronze:
                    return SubscriptionOptions.BronzePlanID;
                case SubscriptionOptions.Silver:
                    return SubscriptionOptions.SilverPlanID;
                case SubscriptionOptions.Gold:
                    return SubscriptionOptions.GoldPlanID;
                default:
                    return "UnknownID";
            }
        }

        private string GetPlanNameFromId(string planId)
        {
            switch (planId)
            {
                case SubscriptionOptions.BronzePlanID:
                    return SubscriptionOptions.Bronze;
                case SubscriptionOptions.SilverPlanID:
                    return SubscriptionOptions.Silver;
                case SubscriptionOptions.GoldPlanID:
                    return SubscriptionOptions.Gold;
                default:
                    return "UnknownID";
            }
        }
    }
}