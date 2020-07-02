using Gym.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Lib.Services.Customers
{
    public class ReservationService : BaseService
    {
        public ReservationService(gymEntities db) : base(db)
        {
        }

        public Reservation GetReservationById(int reservationId)
        {
            return db.Reservation.FirstOrDefault(x => x.Id == reservationId);
        }
        public IList<Reservation> GetAllReservations(
            string type = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            return GetCustomerReservations(null, type, startDate, endDate);
        }



        public IList<Reservation> GetCustomerReservations(
            int? customerId = null,
            string type = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var query = db.Reservation
                .Include("ReservationType1")
                .AsQueryable();

            if (customerId != null)
                query = query.Where(x => x.CustomerId == customerId.Value);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(x => x.ReservationType == type);

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            return query.ToList();
        }



        public string InsertActivity(
            int customerId,
            DateTime startDate, 
            float duration, 
            string reservationType
        )
        {
            var customer = db.Customer.FirstOrDefault(x => x.UserId == customerId);
            if (customer == null)
                return "Utente non trovato";

            DateTime endDate = startDate.AddHours(duration);

            var existingReservation = GetCustomerReservations(customerId, null, startDate, endDate);
            if (existingReservation.Any())
                return "Una sola prenotazione per fascia oraria!!!";
           
            customer.Reservation.Add(new Reservation
            {
                EndDate = endDate,
                StartDate = startDate,
                ReservationType = reservationType
            });
            db.SaveChanges();

            return null;
            
        }

        public string DeleteReservation(int customerId, int reservationId)
        {
            var reservation = GetReservationById(reservationId);
            if (reservation == null)
                return "Prenotazione inesistente";
            if (reservation.CustomerId != customerId)
                return "Accesso non consentito su questa prenotazione";
            if (DateTime.Now > reservation.StartDate)
                return "Prenotazione scaduta. Puoi eliminare solo le prenotazioni in avanti.";

            db.Reservation.Remove(reservation);
            db.SaveChanges();
            return null;
        }
    }
}
