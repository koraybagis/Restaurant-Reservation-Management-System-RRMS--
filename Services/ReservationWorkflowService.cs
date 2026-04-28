using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using RestoranRezervasyonSistemi.Controllers;

namespace RestoranRezervasyonSistemi.Services
{
    public sealed class ReservationWorkflowService
    {
        private readonly ReservationController _reservationController;
        private readonly MenuController _menuController;

        public ReservationWorkflowService(ReservationController reservationController, MenuController menuController)
        {
            _reservationController = reservationController;
            _menuController = menuController;
        }

        public (int ReservationId, TimeSpan ReservationTime)? FindUserReservation(int tableId, DateTime date, string customerEmail, string customerName, TimeSpan fromTime)
        {
            return _reservationController.GetNextReservationForUser(tableId, date, customerEmail, customerName, fromTime);
        }

        public string CreateVerificationCode()
        {
            var bytes = new byte[4];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            var value = BitConverter.ToUInt32(bytes, 0) % 900000;
            return (value + 100000).ToString();
        }

        public int CreateReservation(int tableId, string customerName, string customerPhone, DateTime date, TimeSpan time, int guestCount, string customerEmail)
        {
            return _reservationController.CreateReservation(tableId, customerName, customerPhone, date, time, guestCount, customerEmail);
        }

        public void SaveSelectedMenuItems(int reservationId, List<(int MenuItemId, int Quantity)> selectedItems)
        {
            if (selectedItems == null || selectedItems.Count == 0)
                return;

            _menuController.AddReservationMenuItems(reservationId, selectedItems);
        }
    }
}
