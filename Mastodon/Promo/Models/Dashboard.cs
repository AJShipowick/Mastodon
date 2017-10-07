using System;
using System.Collections.Generic;

namespace Mastodon.Promo.Models
{
    [Serializable]
    public class Dashboard
    {
        //Entries
        public string ActivePromo { get; set; }
        public string ActivePromoScript { get; set; }
        public int ActivePromoClaimedEntries { get; set; }
        public string ActivePromoStartDate { get; set; }
        public string ActivePromoEndDate { get; set; }
        public Dictionary<DateTime, int> EntriesOverTime { get; set; }
        public List<InactivePromos> InactivePromos { get; set; }
        //Account
        public string CurrentSubscription { get; set; }

        //Paid for images???



    }

    public class InactivePromos
    {
        public string PromoName;
        public string PromoDescription;
    }

}
