using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gym.Web.Models.Admin
{
    public class RenewCustomerSubscriptionModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        [DisplayName("Abbonamento")]
        [Required]
        public string Subscription { get; set; }

        [DisplayName("Data inizio"), Required]
        public DateTime Start { get; set; }

        [DisplayName("Data fine")]
        public DateTime End { get; set; }

        [DisplayName("Prezzo")]
        public decimal Price { get; set; }

        public IList<SelectListItem> AvailableSubscriptions { get; set; }
    }
}