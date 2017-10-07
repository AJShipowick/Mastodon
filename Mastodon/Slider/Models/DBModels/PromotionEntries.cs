using System.ComponentModel.DataAnnotations;

namespace Mastodon.Slider.Models.DBModels
{
    public class PromotionEntries
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
