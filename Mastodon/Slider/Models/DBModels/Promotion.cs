using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mastodon.Slider.Models.DBModels
{
    [Serializable]
    public class Promotion
    {
        [Key]
        public string Id { get; set; }
        public virtual PromotionStats PromotionStats { get; set; }
        public virtual ICollection<PromotionEntries> PromotionEntries { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ActivePromotion { get; set; }
        public int PromotionDetails { get; set; }
        public string ImageName { get; set; }
    }
}