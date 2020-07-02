using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gym.Web.Models.Admin
{
    public class AddressModel
    {
        [DisplayName("Via"), Required]
        public string Street { get; set; }
        
        [DisplayName("N."), Required]
        public string StreetNumber { get; set; }

        [DisplayName("Città"), Required]
        public string City { get; set; }

        [DisplayName("CAP"), Required]
        public string Zip { get; set; }

        [DisplayName("Provincia/Paese"), Required]
        public string Country { get; set; }
    }
}