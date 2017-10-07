using System;

namespace Mastodon.Slider.Models
{
    public interface IBuilder
    {
        Dashboard CreateDashboardModel();
    }

    public class Builder : IBuilder
    {
        public Dashboard CreateDashboardModel()
        {
            Dashboard model = new Dashboard();

            model.ActivePromo = "My Random Promo!";
            model.ActivePromoScript = "<script>var myScript == 'AWESOME!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!'</script>";
            model.ActivePromoClaimedEntries = 6667;
            model.ActivePromoStartDate = DateTime.Today;
            model.ActivePromoEndDate = DateTime.Today.AddDays(10);
            model.CurrentSubscription = "Advanced :)";

            return model;
        }
    }
}
