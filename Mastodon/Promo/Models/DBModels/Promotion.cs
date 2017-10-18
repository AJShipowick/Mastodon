using Mastodon.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mastodon.Promo.Models.DBModels
{
    [Serializable]
    public class Promotion
    {
        [Key]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public bool ActivePromotion { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string EndDate { get; set; }
        public string Discount { get; set; }
        public bool ShowCouponBorder { get; set; }
        public string ImageName { get; set; }
        public string BackgroundColor { get; set; }
        public string ButtonColor { get; set; }
        public string Details1 { get; set; }
        public string Details2 { get; set; }
        public string FinePrint { get; set; }
    }
}