using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gym.Web.Models.Admin
{
    public class CustomersListModel
    {
        public CustomersListModel()
        {
            Customers = new List<CustomerModel>();
        }
        public IList<CustomerModel> Customers { get; set; }
    }

    public class CustomerModel { 
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public Gender Gender { get; set; }

        public string Code { get; set; }

        public string Phone { get; set; }

        public DateTime Registration { get; set; }

        public string Notes { get; set; }

        public string Email { get; set; }

        public DateTime? MedCert { get; set; }

        public DateTime? Subscription { get; set; }

        public bool IsUnder20 { get; set; }


    }

    public enum Gender
    {
        MALE,
        FEMALE
    }


}
