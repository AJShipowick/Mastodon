using OsOEasy.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace OsOEasy.Promo.Models.DBModels
{ 
    public class AccountActivity
    {
        [Key]
        public string Id { get; set; }
        public virtual string ApplicationUser { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool MonthlyPlanPayment { get; set; }
        public bool SpecialPurchase { get; set; }
        public string SpecialPurchaseItem { get; set; }
        public string PaymentNotes { get; set; }
    }
}