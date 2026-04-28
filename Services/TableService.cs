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
        public const int ReservationDurationMinutes = 120;
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

            return tables.Select(table =>
            {
                var state = BuildVisualState(table, today, currentTime);
                return new TableButtonInfo(
                    table,
                    state.Color,
                    state.TooltipText,
                    state.IsSelectable,
                    state.ReservationWarningText
                );
            }).ToList();
        }

        private TableVisualState BuildVisualState(Table table, DateTime today, TimeSpan currentTime)
        {
            var normalizedStatus = TableStatusService.Normalize(table.Status);
            var defaultColor = TableStatusService.GetColor(normalizedStatus);
            var searchStart = currentTime.Add(TimeSpan.FromMinutes(-TableConstants.ReservationDurationMinutes));
            if (searchStart < TimeSpan.Zero)
                searchStart = TimeSpan.Zero;

            // Finds either currently active reservation start time or the nearest upcoming one.
            var nextReservation = _reservationController.GetNextReservationTime(table.Id, today, searchStart);
            var tooltipText = GetTooltipText(table, today, currentTime, nextReservation);

            if (normalizedStatus == TableStatus.Dirty)
            {
                const string dirtyMessage = "Masa temizlik bekliyor, rezervasyon yapilamaz.";
                var dirtyTooltip = $"{tooltipText}\n{dirtyMessage}";
                return new TableVisualState(defaultColor, dirtyTooltip, false, dirtyMessage);
            }

            if (normalizedStatus == TableStatus.Occupied)
            {
                return new TableVisualState(defaultColor, tooltipText, true, string.Empty);
            }

            if (!nextReservation.HasValue)
            {
                return new TableVisualState(defaultColor, tooltipText, true, string.Empty);
            }

            var reservationTime = nextReservation.Value;
            var warningTime = reservationTime.Add(TimeSpan.FromMinutes(-TableConstants.WarningMinutesBeforeReservation));
            var reservationEndTime = reservationTime.Add(TimeSpan.FromMinutes(TableConstants.ReservationDurationMinutes));
            var warningText = $"En yakin rezervasyon saati: {reservationTime:hh\\:mm}";

            if ((currentTime >= warningTime && currentTime < reservationTime) ||
                (currentTime >= reservationTime && currentTime < reservationEndTime))
            {
                return new TableVisualState(Color.Firebrick, tooltipText, false, warningText);
            }

            return new TableVisualState(Color.DarkOrange, tooltipText, true, string.Empty);
        }

        private string GetTooltipText(Table table, DateTime today, TimeSpan currentTime, TimeSpan? nextReservation)
        {
            var earliest = _reservationController.GetEarliestAvailableTime(table.Id, today, currentTime);

            var nextText = nextReservation.HasValue ? nextReservation.Value.ToString(@"hh\:mm") : "Yok";
            var earliestText = earliest.ToString(@"hh\:mm");

            return $"{table.TableName}\nKonum: {table.Location}\nKapasite: {table.Capacity}\n" +
                   $"Sonraki rezervasyon: {nextText}\n" +
                   $"En erken uygun saat: {earliestText}\n" +
                   $"Oturma süresi: {(TableConstants.ReservationDurationMinutes / 60)} saat";
        }

        private sealed class TableVisualState
        {
            public Color Color { get; }
            public string TooltipText { get; }
            public bool IsSelectable { get; }
            public string ReservationWarningText { get; }

            public TableVisualState(Color color, string tooltipText, bool isSelectable, string reservationWarningText)
            {
                Color = color;
                TooltipText = tooltipText;
                IsSelectable = isSelectable;
                ReservationWarningText = reservationWarningText;
            }
        }
    }
}
