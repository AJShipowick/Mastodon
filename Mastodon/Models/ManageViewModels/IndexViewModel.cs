using System;
using System.ComponentModel.DataAnnotations;

namespace OsOEasy.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Website { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string AccountMessage { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public PaymentViewModel PaymentViewModel { get; set; }
    }
}