using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mastodon.Slider.Models
{
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

    public class ClientsWebsite
    {
        [Key]
        public string ClientID { get; set; }
        public string CustomSiteScript { get; set; }
        public string WebsiteName { get; set; }
        public string CompanyName { get; set; }
        public string SliderImagePath { get; set; }
        public string MessageHeader { get; set; }
        public string MesasgeBody { get; set; }
        public string AdditionalMessage { get; set; }
        [NotMapped]
        public bool WebsiteUpdated { get; set; }
    }
}