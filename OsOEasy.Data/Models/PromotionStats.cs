using System;
using System.ComponentModel.DataAnnotations;

namespace OsOEasy.Data.Models
{
    [Serializable]
    public class PromotionStats
    {
        [Key]
        public string Id { get; set; }
        public virtual Promotion Promotion { get; set; }
        public int TimesViewed { get; set; }
        public int TimesClaimed { get; set; }
    }
}