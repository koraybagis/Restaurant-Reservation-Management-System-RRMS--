using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Controllers;

namespace RestoranRezervasyonSistemi.Services
{
    public static class TableConstants
    {
        public const int WarningMinutesBeforeReservation = 30;
        public const int ReservationDurationMinutes = 150;
        public const int ButtonSize = 120;
        public const int ButtonFontSize = 10;
    }

    public class TableService
    {
        private readonly TableController _tableController;
        private readonly ReservationController _reservationController;

        public TableService(TableController tableController, ReservationController reservationController)
        {
            _tableController = tableController ?? throw new ArgumentNullException(nameof(tableController));
            _reservationController = reservationController ?? throw new ArgumentNullException(nameof(reservationController));
        }

        public List<TableButtonInfo> GetTableButtonInfos()
        {
            var tables = _tableController.GetAllTables();
            var currentTime = DateTime.Now.TimeOfDay;
            var today = DateTime.Today;

            return tables.Select(table => new TableButtonInfo(
                table,
                GetTableColor(table, currentTime),
                GetTooltipText(table, today, currentTime)
            )).ToList();
        }

        private Color GetTableColor(Table table, TimeSpan currentTime)
        {
            var normalizedStatus = TableStatusService.Normalize(table.Status);
            var baseColor = TableStatusService.GetColor(normalizedStatus);

            if (normalizedStatus == TableStatus.Dirty || normalizedStatus == TableStatus.Occupied)
                return baseColor;

            if (table.ReservationTime == null)
                return baseColor;

            var reservationTime = (TimeSpan)table.ReservationTime;
            var warningTime = reservationTime.Add(TimeSpan.FromMinutes(-TableConstants.WarningMinutesBeforeReservation));
            var endTime = reservationTime.Add(TimeSpan.FromMinutes(TableConstants.ReservationDurationMinutes));

            return currentTime >= warningTime && currentTime <= endTime 
                ? Color.Firebrick 
                : baseColor;
        }

        private string GetTooltipText(Table table, DateTime today, TimeSpan currentTime)
        {
            var next = _reservationController.GetNextReservationTime(table.Id, today, currentTime);
            var earliest = _reservationController.GetEarliestAvailableTime(table.Id, today, currentTime);

            var nextText = next.HasValue ? next.Value.ToString(@"hh\:mm") : "Yok";
            var earliestText = earliest.ToString(@"hh\:mm");

            return $"{table.TableName}\nKonum: {table.Location}\nKapasite: {table.Capacity}\n" +
                   $"Sonraki rezervasyon: {nextText}\n" +
                   $"En erken uygun saat: {earliestText}\n" +
                   $"Oturma süresi: {(TableConstants.ReservationDurationMinutes / 60)} saat";
        }
    }
}
