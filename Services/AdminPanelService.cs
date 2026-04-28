using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Services;

namespace RestoranRezervasyonSistemi.Services
{
    public class AdminPanelService
    {
        private readonly TableController _tableController;
        private readonly UserController _userController;
        private readonly ReservationController _reservationController;

        public AdminPanelService(TableController tableController, UserController userController, ReservationController reservationController)
        {
            _tableController = tableController;
            _userController = userController;
            _reservationController = reservationController;
        }

        public List<User> LoadUsers()
        {
            try
            {
                return _userController.GetAllUsers();
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Kullanıcılar yüklenirken hata: {ex.Message}");
                return new List<User>();
            }
        }

        public List<Reservation> LoadReservations()
        {
            try
            {
                return _reservationController.GetAllReservations();
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Rezervasyonlar yüklenirken hata: {ex.Message}");
                return new List<Reservation>();
            }
        }

        public List<Table> LoadTables()
        {
            try
            {
                return _tableController.GetAllTables();
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Masalar yüklenirken hata: {ex.Message}");
                return new List<Table>();
            }
        }

        public void StyleUserDataGridView(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                bool isBanned = Convert.ToBoolean(row.Cells["IsBanned"].Value);
                if (isBanned)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(180, 0, 0);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        public void SetDataGridViewColumnHeaders(DataGridView dgv, Dictionary<string, string> columnHeaders)
        {
            foreach (var header in columnHeaders)
            {
                if (dgv.Columns[header.Key] != null)
                {
                    dgv.Columns[header.Key].HeaderText = header.Value;
                }
            }
        }

        public bool ValidateUserSelection(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0 && dgv.CurrentRow == null)
            {
                DialogService.ShowWarning("Lütfen bir kullanıcı seçin.");
                return false;
            }
            return true;
        }

        public bool ValidateReservationSelection(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0 && dgv.CurrentRow == null)
            {
                DialogService.ShowWarning("Lütfen bir rezervasyon seçin.");
                return false;
            }
            return true;
        }

        public User GetSelectedUser(DataGridView dgv, List<User> users)
        {
            var selectedRow = dgv.SelectedRows.Count > 0 ? dgv.SelectedRows[0] : dgv.CurrentRow;
            if (selectedRow == null)
                return null;

            int userId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            return users.FirstOrDefault(u => u.Id == userId);
        }

        public Reservation GetSelectedReservation(DataGridView dgv, List<Reservation> reservations)
        {
            try
            {
                if (dgv.SelectedRows.Count == 0 && dgv.CurrentRow == null)
                {
                    return null;
                }

                var selectedRow = dgv.SelectedRows.Count > 0 ? dgv.SelectedRows[0] : dgv.CurrentRow;
                
                if (selectedRow.Cells["Id"]?.Value == null)
                {
                    return null;
                }

                int reservationId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                return reservations.FirstOrDefault(r => r.Id == reservationId);
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Rezervasyon seçimi okunamadı: {ex.Message}");
                return null;
            }
        }

        public bool BanUser(User user)
        {
            try
            {
                if (user == null)
                {
                    DialogService.ShowWarning("Geçerli bir kullanıcı seçin.");
                    return false;
                }

                if (DialogService.ShowConfirmation($"{user.FullName} kullanıcısını banlamak istediğinizden emin misiniz?", "Ban Onayı"))
                {
                    if (_userController.BanUser(user.Id, user.FullName))
                    {
                        DialogService.ShowInfo("Kullanıcı başarıyla banlandı.");
                        return true;
                    }
                    else
                    {
                        DialogService.ShowError("Banlama işlemi başarısız oldu.");
                    }
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Hata: {ex.Message}");
            }
            return false;
        }

        public bool UnbanUser(User user)
        {
            try
            {
                if (user == null)
                {
                    DialogService.ShowWarning("Geçerli bir kullanıcı seçin.");
                    return false;
                }

                if (DialogService.ShowConfirmation($"{user.FullName} kullanıcısının banını kaldırmak istediğinizden emin misiniz?", "Ban Kaldırma Onayı"))
                {
                    if (_userController.UnbanUser(user.Id, user.FullName))
                    {
                        DialogService.ShowInfo("Kullanıcının banı başarıyla kaldırıldı.");
                        return true;
                    }
                    else
                    {
                        DialogService.ShowError("Ban kaldırma işlemi başarısız oldu.");
                    }
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Hata: {ex.Message}");
            }
            return false;
        }

        public bool CancelReservation(Reservation reservation)
        {
            try
            {
                if (reservation == null)
                {
                    DialogService.ShowWarning("Geçerli bir rezervasyon seçin.");
                    return false;
                }

                if (DialogService.ShowConfirmation($"{reservation.CustomerName} kullanıcısının rezervasyonunu iptal etmek istediğinizden emin misiniz?\n\nRezervasyona ait menü ürünleri de silinecektir.", "Rezervasyon İptal Onayı"))
                {
                    if (_reservationController.CancelByIdWithMenuItems(reservation.Id))
                    {
                        DialogService.ShowInfo("Rezervasyon ve menü ürünleri başarıyla iptal edildi.");
                        return true;
                    }
                    else
                    {
                        DialogService.ShowError("Rezervasyon iptal işlemi başarısız oldu.");
                    }
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Hata: {ex.Message}");
            }
            return false;
        }

        public bool AddTable(string tableName, int capacity, string location)
        {
            try
            {
                var yeniMasa = new Table
                {
                    TableName = tableName,
                    Capacity = capacity,
                    Location = location
                };

                if (_tableController.AddTable(yeniMasa))
                {
                    DialogService.ShowInfo("Masa başarıyla eklendi!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Hata: {ex.Message}");
            }
            return false;
        }

        public bool UpdateTable(int id, string tableName, int capacity, string location)
        {
            try
            {
                var guncellenecek = new Table
                {
                    Id = id,
                    TableName = tableName,
                    Capacity = capacity,
                    Location = location
                };

                if (_tableController.UpdateTable(guncellenecek))
                {
                    DialogService.ShowInfo("Masa bilgileri güncellendi.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Hata: {ex.Message}");
            }
            return false;
        }

        public bool DeleteTable(int id)
        {
            try
            {
                if (DialogService.ShowConfirmation("Seçili masayı silmek istediğinize emin misiniz?", "Silme Onayı"))
                {
                    if (_tableController.DeleteTable(id))
                    {
                        DialogService.ShowInfo("Masa başarıyla silindi.");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Hata: {ex.Message}");
            }
            return false;
        }
    }
}
