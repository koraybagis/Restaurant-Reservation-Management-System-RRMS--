using RestoranRezervasyonSistemi.Data;
using RestoranRezervasyonSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace RestoranRezervasyonSistemi.Controllers
{
    public sealed class ReservationController
    {
        private readonly ReservationRepository _reservations = new ReservationRepository();

        public DataTable GetReservationList() => _reservations.GetReservationList();

        public bool CancelById(int reservationId) => _reservations.DeleteReservationById(reservationId);
        public bool CancelByIdWithMenuItems(int reservationId) => _reservations.CancelReservationWithMenuItems(reservationId);

        public bool CancelByIdForUser(int reservationId, string customerEmail, string customerName) =>
            _reservations.DeleteReservationByIdForUser(reservationId, customerEmail, customerName);

        public bool CancelForUser(int tableId, DateTime date, TimeSpan time, string customerEmail, string customerName) =>
            _reservations.DeleteReservationForUser(tableId, date, time, customerEmail, customerName);

        public bool ReservationExists(int tableId, DateTime date, TimeSpan time) =>
            _reservations.ReservationExists(tableId, date, time);

        public bool HasConflict(int tableId, DateTime date, TimeSpan time)
        {
            // Business rule: each reservation occupies the table for 2.5 hours (150 minutes).
            return _reservations.HasConflict(tableId, date, time, conflictWindowMinutes: 150);
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservations.GetAllReservations();
        }

        public int CreateReservation(int tableId, string customerName, string customerPhone, DateTime date, TimeSpan time, int guestCount, string customerEmail)
        {
            var r = new Reservation
            {
                TableId = tableId,
                CustomerName = customerName,
                CustomerPhone = customerPhone,
                ReservationDate = date.Date,
                ReservationTime = time,
                GuestCount = guestCount,
                CustomerEmail = customerEmail
            };

            return _reservations.InsertReservation(r);
        }

        public (int ReservationId, TimeSpan ReservationTime)? GetNextReservationForUser(int tableId, DateTime date, string customerEmail, string customerName, TimeSpan? fromTime = null) =>
            _reservations.GetNextReservationForUser(tableId, date, customerEmail, customerName, fromTime);

        public TimeSpan? GetNextReservationTime(int tableId, DateTime date, TimeSpan fromTime) =>
            _reservations.GetNextReservationTime(tableId, date, fromTime);

        public TimeSpan GetEarliestAvailableTime(int tableId, DateTime date, TimeSpan fromTime) =>
            _reservations.GetEarliestAvailableTime(tableId, date, fromTime, durationMinutes: 150);

        public int EnsureOpenReservationForTable(int tableId) =>
            _reservations.EnsureOpenReservationForTable(tableId);

        public int? GetOpenReservationForTable(int tableId) =>
            _reservations.GetOpenReservationForTable(tableId);
    }
}

