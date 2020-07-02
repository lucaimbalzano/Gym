using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Gym.Web.Models.Customer
{
    public class ReservationModel
    {
        [DisplayName("Inizio")]
        public DateTime StartDate { get; set; }


        [DisplayName("Durata")]
        public float Duration { get; set; }


        [DisplayName("Tipologia")]
        public string ReservationType { get; set; }


        [DisplayName("PersonalTrainer")]
        public string PersonalTrainer { get; set; }

        [DisplayName("Corso")]
        public string Course { get; set; }
    }
}