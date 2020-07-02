using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Models.Account
{
    public class RegisterModel
    {
   
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6), MaxLength(10), RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$", ErrorMessage = "La password deve essere tra 6 e 10 caratteri e contenere almeno una cifra, un carattere maiuscolo e uno minuscolo")]
        public string Password { get; set; }

        [Required, Compare("Password",ErrorMessage = "Le due password non corrispondono")]
        public string ConfirmPassword { get; set; }

        public bool AcceptTerms { get; set; }
    }
}