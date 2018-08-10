using System;
using System.ComponentModel.DataAnnotations;

namespace OsOEasy.Data.Models
{
    [Serializable]
    public class Promotion
    {
        [Key]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public bool ActivePromotion { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string EndDate { get; set; }
        public string DisplayEndDate { get; set; }
        public string Discount { get; set; }
        public bool ShowCouponBorder { get; set; }
        public bool ShowLargeImage { get; set; }
        public string SideOfScreen { get; set; }
        public string ThankYouMessage { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public string BackgroundColor { get; set; }
        public string ButtonColor { get; set; }
        public string Details1 { get; set; }
        public string Details2 { get; set; }
        public string FinePrint { get; set; }
    }
}