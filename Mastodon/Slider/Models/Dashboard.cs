using System;
using System.Collections.Generic;

namespace Mastodon.Slider.Models
{
    [Serializable]
    public class Dashboard
    {
        //Entries
        public string ActivePromo { get; set; }
        public string ActivePromoScript { get; set; }
        public int ActivePromoClaimedEntries { get; set; }
        public DateTime ActivePromoStartDate { get; set; }
        public DateTime ActivePromoEndDate { get; set; }
        public Dictionary<DateTime, int> EntriesOverTime { get; set; }

        //Account
        public string CurrentSubscription { get; set; }

        //Paid for images???



    }
}
