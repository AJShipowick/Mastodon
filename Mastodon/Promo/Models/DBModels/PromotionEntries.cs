using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OsOEasy.Promo.Models.DBModels
{
    public class PromotionEntries
    {
        [Key]
        public string Id { get; set; }
        public virtual Promotion Promotion { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
