using System.ComponentModel.DataAnnotations;

namespace OsOEasy.Data.Models
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
