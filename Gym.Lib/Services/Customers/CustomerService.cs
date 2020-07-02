using Gym.Lib.Domain;
using Gym.Lib.Extensions;
using Gym.Lib.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Lib.Services.Customers
{
    public class CustomerService : BaseService
    {
        public CustomerService(gymEntities db) : base(db)
        {
        }

        public Customer InsertCustomer(
            string name,
            string lastName,
            string email,
            string code,
            DateTime bithDate,
            string gender,
            string phone,
            string notes,
            DateTime? medCert,
            string street,
            string streetNum,
            string city,
            string zip,
            string country,
            out string pwd)
        {
            pwd = Guid.NewGuid().ToString().Replace("-", "").Substring(0,10);
            var customer = new Customer
            {
                Code = code,
                Name = name,
                Surname = lastName,
                Gender = gender,
                MedCertDate = medCert,
                BirthDate = bithDate,
                Phone = phone,
                Notes = notes,
                RegistrationDate = DateTime.Now,
                Address = new Address
                {
                    City = city,
                    Country = country,
                    SNum = streetNum,
                    Street = street,
                    Zip = zip
                },
                User = new User
                {
                    Email = email,
                    Role = "User",
                    PasswordEncrypted = AuthService.CalculateHash(pwd)
                }
            };

            db.Customer.Add(customer);
            db.SaveChanges();
            return customer;



            //TODO INVIARE VIA MAIL LA PASSWORD PWD
        }


        public IList<Customer> GetCustomersList()
        {
            var query = db.Customer
                .Include("Address")
                .Include("User")
                .Include("CustomerSubscription");

            return query.ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return db.Customer.FirstOrDefault(c => c.UserId == customerId);
        }

        public string RenewCustomerSubscription(int customerId, DateTime start, string subscription)
        {
            var customer = GetCustomerById(customerId);
            if (customer == null)
                return "Utente non trovato";

            if (start < DateTime.Now.Date)
                return "Abbonamento nel passato!";

            var ss = db.Subscription.FirstOrDefault(x => x.Name == subscription);
            if (ss == null)
                return "Abbonamento non trovato";

            decimal price = ss.Price;
            if (customer.IsUnder20())
                price *= 0.80m;

            customer.CustomerSubscription.Add(new CustomerSubscription
            {
                StartDate = start,
                EndDate = start.AddMonths(ss.Duration),
                Price = price,
                Subscription = subscription
            });
            db.SaveChanges();
            return null;
        }
    }
}
