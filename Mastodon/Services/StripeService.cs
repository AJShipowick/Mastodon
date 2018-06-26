using Stripe;

namespace OsOEasy.Services
{
    public class StripeService
    {

        public void SubscribeNewUserToStripe(string userId)
        {
            // Set your secret key: remember to change this to your live secret key in production
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.SetApiKey("sk_test_t1MXVZOUKGo8RGGtnkwNZjwL");

            var planOptionList = new System.Collections.Generic.List<StripeSubscriptionItemOption>();
            var subscriptionOption = new StripeSubscriptionItemOption
            {
                PlanId = "free"
            };
            planOptionList.Add(subscriptionOption);

            var subscriptionOptions = new StripeSubscriptionCreateOptions()
            {
                Items = planOptionList
            };

            var subscriptionService = new StripeSubscriptionService();
            StripeSubscription subscription = subscriptionService.Create("cus_D7HoZdYVOkB5sq", subscriptionOptions);
        }

    }
}
