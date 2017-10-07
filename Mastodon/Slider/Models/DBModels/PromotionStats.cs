using System;
using System.ComponentModel.DataAnnotations;

namespace Mastodon.Slider.Models.DBModels
{
    [Serializable]
    public class PromotionStats
    {
        [Key]
        public int Id { get; set; }
        public int TimesViewed { get; set; }
        public int TimesClaimed { get; set; }
    }
}