using System;
using System.Collections.Generic;

namespace OsOEasy.Models.PromoModels
{
    [Serializable]
    public class Dashboard
    {
        //Account
        public string CurrentSubscription { get; set; }
        public string DashboardMessage { get; set; }

        //Entries
        public string ActivePromoId { get; set; }
        public string ActivePromo { get; set; }
        public string ActivePromoDiscount { get; set; }
        public string ActivePromoScript { get; set; }
        public int ActivePromoClaimedEntries { get; set; }
        public int ActivePromoViews { get; set; }
        public string ActivePromoEndDate { get; set; }
        public List<InactivePromos> InactivePromos { get; set; }
    }

    public class InactivePromos
    {
        public string PromoId;
        public string PromoName;
        public string PromoDiscount;
    }
}