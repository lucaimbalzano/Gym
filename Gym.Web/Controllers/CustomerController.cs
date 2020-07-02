using Gym.Lib.Domain;
using Gym.Lib.Services.Customers;
using Gym.Web.Models.Customer;
using Gym.Web.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gym.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class CustomerController : AbstractBaseController
    {
        private readonly ReservationService reservationService;
        public CustomerController(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }
        
        [NonAction]
        private EventModel PrepareEventModel(Reservation reservation)
        {
            string title = "Sala pesi";
            if (reservation.ReservationType == "PT")
                title = "Personal Trainer";
            else if (reservation.ReservationType == "C")
                title = "Corso";
            return new EventModel
            {
                start = reservation.StartDate.ToString("yyyy-MM-ddTHH:mm:sszz"),
                end = reservation.EndDate?.ToString("yyyy-MM-ddTHH:mm:sszz"),
                id = reservation.Id.ToString(),
                title = title
            };

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerReservations_SP(DateTime start, DateTime end)
        {
            
            var reservations = reservationService.GetCustomerReservations(UserId,"SP", start, end);
            var events = reservations.Select(PrepareEventModel);
            return Json(events);
        }

        [HttpPost]
        public ActionResult CustomerReservations_PT(DateTime start, DateTime end)
        {

            var reservations = reservationService.GetCustomerReservations(UserId, "PT", start, end);
            var events = reservations.Select(PrepareEventModel);
            return Json(events);
        }

        
        [HttpPost]
        public ActionResult CustomerReservations_C(DateTime start, DateTime end)
        {

            var reservations = reservationService.GetCustomerReservations(UserId, "C", start, end);
            var events = reservations.Select(PrepareEventModel);
            return Json(events);
        }


        [HttpPost]
        public PartialViewResult ReservationPopup(DateTime startDate)
        {
            var model = new ReservationModel
            {
                StartDate = startDate,
                Duration = 1
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertActivity(ReservationModel model)
        {
            if (ModelState.IsValid)
            {
                var msg = reservationService.InsertActivity(UserId, model.StartDate, model.Duration, model.ReservationType);
                if (string.IsNullOrEmpty(msg))
                    return RedirectToAction("Index");
            }

            //Si dovrebbero fare vedere gli errori di validazione, ma è complicato.
            //Per farla semplice, facciamo Redirect alla Index
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteActivity(int reservationId)
        {
            string msg = reservationService.DeleteReservation(UserId, reservationId);
            return Json(new
            {
                Success = string.IsNullOrEmpty(msg),
                Message = msg
            });
        }

    }
}