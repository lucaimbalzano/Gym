using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gym.Web.Models.Admin
{
    public class CreateCustomerModel
    {
        public CreateCustomerModel()
        {
            Address = new AddressModel();
        }
        [DisplayName("Nome")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Cognome")]
        [Required]
        public string LastName { get; set; }

        [DisplayName("E-Mail"), Required, EmailAddress]
        public string Email { get; set; }

        [DisplayName("Tessera")]
        public string Code { get; set; }

        [DisplayName("Sesso")]
        public string Gender { get; set; }

        [DisplayName("Telefono")]
        public string Phone { get; set; }

        [DisplayName("Note")]
        public string Notes { get; set; }

        [DisplayName("Scadenza Cert.Medico")]
        public DateTime? MedCertDate { get; set; }

        public AddressModel Address { get; set; }


        [DisplayName("Data nascita"), Required]
        public DateTime BirthDate { get; set; }
    }
}