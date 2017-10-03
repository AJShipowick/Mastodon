using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mastodon.Slider.Models
{
    [Serializable]
    public class ClientModel
    {
        [Key]
        public string ClientID { get; set; }
        public string PrimaryContact { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public int ClientSubscription { get; set; }
        public string ClientNotes { get; set; }

        public List<ClientsWebsite> ClientSettings { get; set; }
    }

    [Serializable]
    public class ClientsWebsite
    {
        [Key]
        public string ClientID { get; set; }
        public string CustomSiteScript { get; set; }
        public string WebsiteName { get; set; }
        public string ContactEmail { get; set; }
        public string FormName { get; set; }
        public string CallToActionMessage { get; set; }
        public string SliderImageName { get; set; }        
    }
}