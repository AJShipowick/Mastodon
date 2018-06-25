using OsOEasy.Data.Models;
using System;
using System.Collections.Generic;

namespace OsOEasy.Models.PromoModels
{
    [Serializable]
    public class PromoDetails
    {
        public string PromoName { get; set; }
        public string PromoId { get; set; }
        public int TimesViewed { get; set; }
        public int TimesClaimed { get; set; }
        public List<PromotionEntries> PromoEntries { get; set; }
    }
}
