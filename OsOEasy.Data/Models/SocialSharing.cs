using System;
using System.ComponentModel.DataAnnotations;

namespace OsOEasy.Data.Models
{
    [Serializable]
    public class SocialSharing
    {
        [Key]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public bool ActivePromotion { get; set; }
        public string Title { get; set; }
        public string SideOfScreen { get; set; }

        public bool UseFacebook { get; set; }
        public string FacebookURL { get; set; }
        public string FacebookImageName { get; set; }
        public string FacebookImageType { get; set; }

        public bool UseTwitter { get; set; }
        public string TwitterURL { get; set; }
        public string TwitterImageName { get; set; }
        public string TwitterImageType { get; set; }

        public bool UseInstagram { get; set; }
        public string InstagramURL { get; set; }
        public string InstagramImageName { get; set; }
        public string InstagramImageType { get; set; }

        public bool UseLinkedin { get; set; }
        public string LinkedinURL { get; set; }
        public string LinkedinImageName { get; set; }
        public string LinkedinImageType { get; set; }

        public bool UsePinterest { get; set; }
        public string PinterestURL { get; set; }
        public string PinterestImageName { get; set; }
        public string PinterestImageType { get; set; }
    }
}