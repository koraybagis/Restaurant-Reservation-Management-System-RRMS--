using System;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;

namespace RestoranRezervasyonSistemi.Services
{
    public sealed class TableOrderService
    {
        private readonly TableController _tableController;
        private readonly ReservationController _reservationController;
        private readonly MenuController _menuController;

        public TableOrderService(TableController tableController, ReservationController reservationController, MenuController menuController)
        {
            _tableController = tableController;
            _reservationController = reservationController;
            _menuController = menuController;
        }

        public void AddItemToTable(Table table, RestoranRezervasyonSistemi.Models.MenuItem menuItem)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (menuItem == null) throw new ArgumentNullException(nameof(menuItem));

            var status = TableStatusService.Normalize(table.Status);
            if (status == TableStatus.Dirty)
                throw new InvalidOperationException("Kirli masaya ürün eklenemez.");

            if (status == TableStatus.Available)
                _tableController.UpdateTableStatus(table.Id, TableStatus.Occupied);

            var reservationId = _reservationController.EnsureOpenReservationForTable(table.Id);
            _menuController.AddItemToTableOrder(reservationId, menuItem.Id, 1);
        }
    }
}
