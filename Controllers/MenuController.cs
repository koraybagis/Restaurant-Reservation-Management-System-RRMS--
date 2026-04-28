using RestoranRezervasyonSistemi.Data;
using RestoranRezervasyonSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RestoranRezervasyonSistemi.Controllers
{
    public sealed class MenuController
    {
        private readonly MenuRepository _menuRepository = new MenuRepository();

        public List<RestoranRezervasyonSistemi.Models.MenuItem> GetAllMenuItems() => _menuRepository.GetAllMenuItems();

        public List<RestoranRezervasyonSistemi.Models.MenuItem> GetAllMenuItemsIncludingUnavailable() => _menuRepository.GetAllMenuItemsIncludingUnavailable();

        public List<RestoranRezervasyonSistemi.Models.MenuItem> GetMenuItemsByCategory(string category) => _menuRepository.GetMenuItemsByCategory(category);

        public RestoranRezervasyonSistemi.Models.MenuItem GetMenuItemById(int id) => _menuRepository.GetMenuItemById(id);

        public List<string> GetCategories() => _menuRepository.GetCategories();

        public void AddMenuItem(RestoranRezervasyonSistemi.Models.MenuItem item) => _menuRepository.AddMenuItem(item);

        public void UpdateMenuItem(RestoranRezervasyonSistemi.Models.MenuItem item) => _menuRepository.UpdateMenuItem(item);

        public bool DeleteMenuItem(int id) => _menuRepository.DeleteMenuItem(id);

        public void AddReservationMenuItem(ReservationMenuItem rmi) => _menuRepository.AddReservationMenuItem(rmi);

        public List<ReservationMenuItem> GetReservationMenuItems(int reservationId) => _menuRepository.GetReservationMenuItems(reservationId);

        public decimal GetReservationTotalPrice(int reservationId) => _menuRepository.GetReservationTotalPrice(reservationId);

        public bool DeleteReservationMenuItems(int reservationId) => _menuRepository.DeleteReservationMenuItems(reservationId);

        public void AddReservationMenuItems(int reservationId, List<(int MenuItemId, int Quantity)> selectedItems)
        {
            foreach (var (menuItemId, quantity) in selectedItems)
            {
                var menuItem = GetMenuItemById(menuItemId);
                if (menuItem != null)
                {
                    var rmi = new ReservationMenuItem
                    {
                        ReservationId = reservationId,
                        MenuItemId = menuItemId,
                        Quantity = quantity,
                        UnitPrice = menuItem.Price,
                        TotalPrice = menuItem.Price * quantity
                    };
                    AddReservationMenuItem(rmi);
                }
            }
        }

        public Dictionary<string, List<RestoranRezervasyonSistemi.Models.MenuItem>> GetMenuItemsByCategories()
        {
            var result = new Dictionary<string, List<RestoranRezervasyonSistemi.Models.MenuItem>>();
            var categories = GetCategories();
            
            foreach (var category in categories)
            {
                result[category] = GetMenuItemsByCategory(category);
            }
            
            return result;
        }

        public void AddItemToTableOrder(int reservationId, int menuItemId, int quantity = 1)
        {
            var menuItem = GetMenuItemById(menuItemId);
            if (menuItem == null)
                throw new InvalidOperationException("Seçilen ürün bulunamadı.");

            _menuRepository.AddOrUpdateReservationMenuItem(reservationId, menuItemId, quantity, menuItem.Price);
        }

        public bool RemoveItemFromTableOrder(int reservationId, int menuItemId)
        {
            return _menuRepository.RemoveOneReservationMenuItem(reservationId, menuItemId);
        }

        public DataTable GetTableAdisyon(int tableId, DateTime date)
        {
            return _menuRepository.GetTableAdisyon(tableId, date);
        }
    }
}
