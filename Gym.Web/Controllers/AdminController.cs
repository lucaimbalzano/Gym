using Gym.Lib.Extensions;
using Gym.Lib.Services.Customers;
using Gym.Web.Models.Admin;
using Gym.Web.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gym.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : AbstractBaseController
    {
        private readonly CustomerService customerService;
        private readonly SubscriptionService subscriptionService;
        private readonly ReservationService reservationService;
        public AdminController(CustomerService customerService, SubscriptionService subscriptionService,
            ReservationService reservationService)
        {
            this.customerService = customerService;
            this.subscriptionService = subscriptionService;
            this.reservationService = reservationService;
        }
        public ActionResult Index()
        {
            return View();
        }   

        public ActionResult CreateCustomer()
        {
            var model = new CreateCustomerModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCustomer(CreateCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                string pwd;
                var customer = customerService.InsertCustomer(
                    model.Name,
                    model.LastName,
                    model.Email,
                    model.Code,
                    model.BirthDate,
                    model.Gender,
                    model.Phone,
                    model.Notes,
                    model.MedCertDate,
                    model.Address.Street,
                    model.Address.StreetNumber,
                    model.Address.City,
                    model.Address.Zip,
                    model.Address.Country,
                    out pwd);
                if(customer != null)
                {
                    return RedirectToAction("Confirmation","Admin", new { extra = pwd });
                }
            }
            return View(model);
        }

        public ActionResult Confirmation(string extra)
        {
            return View("Confirmation",(object)extra);
        }


        public ActionResult CustomersList()
        {
            var model = new CustomersListModel();
            var customers = customerService.GetCustomersList();

            model.Customers = customers.Select(c => new CustomerModel
            {
                Id = c.UserId,
                Name = $"{c.Name} {c.Surname}",
                Code = c.Code,
                Gender = c.Gender == "M" ? Gender.MALE : Gender.FEMALE,
                Address = $"{c.Address.Street}, {c.Address.SNum}, {c.Address.City} ({c.Address.Country}) - {c.Address.Zip}",
                Email = c.User.Email,
                MedCert = c.MedCertDate,
                Notes = c.Notes,
                Phone = c.Phone,
                Registration = c.RegistrationDate,
                Subscription = c.CustomerSubscription.FirstOrDefault(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now)?.EndDate,
                IsUnder20 = c.IsUnder20()
            }).ToList();
            return View(model);
        }

        public ActionResult RenewSubscription(int id)
        {
            
            var customer = customerService.GetCustomerById(id);
            if (customer == null)
                return RedirectToAction("CustomersList");

            var subscriptions = subscriptionService.GetSubscriptionsList();
            var selectedSubscription = subscriptions.FirstOrDefault();

            var model = new RenewCustomerSubscriptionModel
            {
                CustomerName = $"{customer.Name} {customer.Surname}",
                CustomerId = id,
                Start = DateTime.Now.Date,
                AvailableSubscriptions = subscriptions.Select(s => new SelectListItem
                {
                    Value = s.Name,
                    Text = s.Note
                }).ToList(),
                Price = selectedSubscription.Price
            };
            if (customer.IsUnder20())
                model.Price = model.Price * 0.80m;

            model.End = model.Start.AddMonths(selectedSubscription.Duration);
            
            return View(model);
        }


        [HttpPost]
        public ActionResult RenewSubscription(RenewCustomerSubscriptionModel model)
        {
            if (ModelState.IsValid)
            {
                string msg = customerService.RenewCustomerSubscription(model.CustomerId, model.Start, model.Subscription);
                if (string.IsNullOrEmpty(msg))
                    return RedirectToAction("CustomersList");
                else
                    ModelState.AddModelError("", msg);
            }

            //In caso di errore, devo rigenerare tutti i campi del modello che sono necessari alla vista, ma che non erano inclusi nella form (SUBMIT)
            var subscriptions = subscriptionService.GetSubscriptionsList();
            model.AvailableSubscriptions = subscriptions.Select(s => new SelectListItem
            {
                Value = s.Name,
                Text = s.Note
            }).ToList();
            
            return View(model);
        }

        public ActionResult GetSubscriptionInfo(int customerId, DateTime startDate, string subscription)
        {
            var ss = subscriptionService.GetSubscriptionByName(subscription);
            var customer = customerService.GetCustomerById(customerId);

            if(ss != null && customer != null)
            {
                decimal price = ss.Price;
                DateTime endDate = startDate.AddMonths(ss.Duration);

                if (customer.IsUnder20())
                    price *= 0.80m;

                return Json(new
                {
                    EndDate = endDate.ToString("d"),
                    Price = price.ToString("C2"),
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                Success = false,
                Message = "Abbonamento o utente non trovato"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WeightRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WeightRoomReservations(DateTime start, DateTime end)
        {
            var reservations = reservationService.GetAllReservations("SP", start, end);
            var model = reservations.Select(x => new EventModel
            {
                start = x.StartDate.ToString("yyyy-MM-ddTHH:mm:sszz"),
                end = x.EndDate?.ToString("yyyy-MM-ddTHH:mm:sszz"),
                id = x.Id.ToString(),
                title = x.Customer.Name + " " + x.Customer.Surname
            });
            return Json(model);

        }
    }
}