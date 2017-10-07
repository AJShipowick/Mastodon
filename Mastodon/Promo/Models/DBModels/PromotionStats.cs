using System;
using System.ComponentModel.DataAnnotations;

namespace Mastodon.Promo.Models.DBModels
{
    [Serializable]
    public class PromotionStats
    {
        [Key]
        public string Id { get; set; }
        public int TimesViewed { get; set; }
        public int TimesClaimed { get; set; }
    }
}