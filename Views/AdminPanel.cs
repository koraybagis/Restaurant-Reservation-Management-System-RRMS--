using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Services;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class AdminPanel : Form
    {
        private readonly AdminPanelService _adminService;
        private List<User> _users;
        private List<Reservation> _reservations;

        public AdminPanel()
        {
            InitializeComponent();
            _adminService = new AdminPanelService(
                new TableController(),
                new UserController(),
                new ReservationController());
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            LoadTables();
            LoadUsers();
            LoadReservations();
        }

        private void btnMenuYonetimi_Click(object sender, EventArgs e)
        {
            var menuForm = new MenuYonetimForm();
            menuForm.ShowDialog();
        }

        private void LoadTables()
        {
            try
            {
                var tables = _adminService.LoadTables();
                dgvMasalar.DataSource = null;
                dgvMasalar.DataSource = tables;

                var tableHeaders = new Dictionary<string, string>
                {
                    { "Id", "No" },
                    { "TableName", "Masa Adı" },
                    { "Capacity", "Kapasite" },
                    { "Location", "Konum" }
                };
                _adminService.SetDataGridViewColumnHeaders(dgvMasalar, tableHeaders);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Masalar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsers()
        {
            try
            {
                _users = _adminService.LoadUsers();
                dgvUsers.DataSource = null;
                dgvUsers.DataSource = _users;

                var userHeaders = new Dictionary<string, string>
                {
                    { "Id", "ID" },
                    { "Username", "Kullanıcı Adı" },
                    { "FullName", "Tam Ad" },
                    { "Email", "E-posta" },
                    { "Phone", "Telefon" },
                    { "Role", "Rol" },
                    { "IsBanned", "Durum" }
                };
                _adminService.SetDataGridViewColumnHeaders(dgvUsers, userHeaders);
                _adminService.StyleUserDataGridView(dgvUsers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcılar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReservations()
        {
            try
            {
                _reservations = _adminService.LoadReservations();
                dgvRezervasyonlar.DataSource = null;
                dgvRezervasyonlar.DataSource = _reservations;

                var reservationHeaders = new Dictionary<string, string>
                {
                    { "Id", "ID" },
                    { "CustomerName", "Müşteri Adı" },
                    { "CustomerPhone", "Telefon" },
                    { "CustomerEmail", "E-posta" },
                    { "ReservationDate", "Tarih" },
                    { "ReservationTime", "Saat" },
                    { "TableId", "Masa ID" },
                    { "GuestCount", "Kişi Sayısı" }
                };
                _adminService.SetDataGridViewColumnHeaders(dgvRezervasyonlar, reservationHeaders);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyonlar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MASAYÖNETİM BUTONLARI
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMasaAdi.Text) || !int.TryParse(txtKapasite.Text, out int kapasite))
            {
                MessageBox.Show("Lütfen geçerli masa adı ve kapasite girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (_adminService.AddTable(txtMasaAdi.Text, kapasite, txtKonum.Text))
            {
                LoadTables();
                ClearTableFields();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvMasalar.CurrentRow != null)
            {
                var idValue = dgvMasalar.CurrentRow.Cells["Id"].Value;
                if (idValue == null || idValue == DBNull.Value)
                {
                    MessageBox.Show("Masa ID bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                int id = Convert.ToInt32(idValue);
                if (_adminService.DeleteTable(id))
                {
                    LoadTables();
                    ClearTableFields();
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvMasalar.CurrentRow != null)
            {
                var idValue = dgvMasalar.CurrentRow.Cells["Id"].Value;
                if (idValue == null || idValue == DBNull.Value)
                {
                    MessageBox.Show("Masa ID bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(txtMasaAdi.Text) || !int.TryParse(txtKapasite.Text, out int kapasite))
                {
                    MessageBox.Show("Lütfen geçerli masa adı ve kapasite girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                int id = Convert.ToInt32(idValue);
                if (_adminService.UpdateTable(id, txtMasaAdi.Text, kapasite, txtKonum.Text))
                {
                    LoadTables();
                    ClearTableFields();
                }
            }
        }

        private void dgvMasalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMasalar.CurrentRow != null)
            {
                txtMasaAdi.Text = dgvMasalar.CurrentRow.Cells["TableName"].Value?.ToString() ?? "";
                txtKapasite.Text = dgvMasalar.CurrentRow.Cells["Capacity"].Value?.ToString() ?? "";
                txtKonum.Text = dgvMasalar.CurrentRow.Cells["Location"].Value?.ToString() ?? "";
            }
        }

        private void ClearTableFields()
        {
            txtMasaAdi.Clear();
            txtKapasite.Clear();
            txtKonum.Clear();
        }

        // KULLANICI YÖNETİM BUTONLARI
        private void btnBanUser_Click(object sender, EventArgs e)
        {
            if (_adminService.ValidateUserSelection(dgvUsers))
            {
                var selectedUser = _adminService.GetSelectedUser(dgvUsers, _users);
                if (_adminService.BanUser(selectedUser))
                {
                    LoadUsers();
                }
            }
        }

        private void btnUnbanUser_Click(object sender, EventArgs e)
        {
            if (_adminService.ValidateUserSelection(dgvUsers))
            {
                var selectedUser = _adminService.GetSelectedUser(dgvUsers, _users);
                if (_adminService.UnbanUser(selectedUser))
                {
                    LoadUsers();
                }
            }
        }

        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
            DialogService.ShowInfo("Kullanıcı listesi yenilendi.");
        }

        // REZERVASYON YÖNETİM BUTONLARI
        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            if (_adminService.ValidateReservationSelection(dgvRezervasyonlar))
            {
                var selectedReservation = _adminService.GetSelectedReservation(dgvRezervasyonlar, _reservations);
                if (_adminService.CancelReservation(selectedReservation))
                {
                    LoadReservations();
                }
            }
        }

        private void btnRefreshReservations_Click(object sender, EventArgs e)
        {
            LoadReservations();
            DialogService.ShowInfo("Rezervasyon listesi yenilendi.");
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridView click event - optional functionality
        }

        private void dgvRezervasyonlar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRezervasyonlar.CurrentRow != null)
            {
                var selectedReservation = _adminService.GetSelectedReservation(dgvRezervasyonlar, _reservations);
                if (selectedReservation != null)
                {
                    LoadReservationMenuItems(selectedReservation.Id);
                }
            }
        }

        private void LoadReservationMenuItems(int reservationId)
        {
            try
            {
                var menuController = new MenuController();
                var menuItems = menuController.GetReservationMenuItems(reservationId);
                
                lstRezervasyonMenuleri.Items.Clear();
                
                if (menuItems.Count > 0)
                {
                    foreach (var item in menuItems)
                    {
                        string menuItemName = menuController.GetMenuItemById(item.MenuItemId)?.Name ?? "Bilinmeyen Ürün";
                        lstRezervasyonMenuleri.Items.Add($"{menuItemName} - Adet: {item.Quantity} - Fiyat: {item.TotalPrice:F2} TL");
                    }
                }
                else
                {
                    lstRezervasyonMenuleri.Items.Add("Bu rezervasyona ait menü seçimi bulunmamaktadır.");
                }
            }
            catch (Exception ex)
            {
                lstRezervasyonMenuleri.Items.Clear();
                lstRezervasyonMenuleri.Items.Add($"Menü bilgileri yüklenemedi: {ex.Message}");
            }
        }

        private void btnRezervasyonDetaylari_Click(object sender, EventArgs e)
        {
            try
            {
                if (_reservations == null || _reservations.Count == 0)
                {
                    DialogService.ShowError("Gösterilecek rezervasyon bulunmamaktadır.");
                    return;
                }

                if (dgvRezervasyonlar.SelectedRows.Count == 0)
                {
                    DialogService.ShowError("Lütfen bir rezervasyon seçin.");
                    return;
                }

                var selectedReservation = _adminService.GetSelectedReservation(dgvRezervasyonlar, _reservations);
                if (selectedReservation != null)
                {
                    ShowReservationDetails(selectedReservation);
                }
                else
                {
                    DialogService.ShowError("Seçilen rezervasyon bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Rezervasyon detayları gösterilirken hata oluştu: {ex.Message}");
            }
        }

        private void ShowReservationDetails(Reservation reservation)
        {
            try
            {
                // Rezervasyon detaylarını birleştir
                string details = $"📋 REZERVASYON DETAYLARI\n\n";
                details += $"👤 Müşteri: {reservation.CustomerName}\n";
                details += $"📞 Telefon: {reservation.CustomerPhone}\n";
                details += $"📧 E-posta: {reservation.CustomerEmail}\n";
                details += $"📅 Tarih: {reservation.ReservationDate:dd.MM.yyyy}\n";
                details += $"⏰ Saat: {reservation.ReservationTime:hh\\:mm}\n";
                details += $"👥 Kişi Sayısı: {reservation.GuestCount}\n";
                details += $"🪑 Masa ID: {reservation.TableId}\n";

                // Menü ürünlerini yükle
                var menuController = new MenuController();
                var menuItems = menuController.GetReservationMenuItems(reservation.Id);
                
                if (menuItems.Count > 0)
                {
                    details += $"\n🍽️ SEÇİLEN MENÜLER:\n";
                    decimal totalPrice = 0;
                    
                    foreach (var item in menuItems)
                    {
                        string menuItemName = menuController.GetMenuItemById(item.MenuItemId)?.Name ?? "Bilinmeyen Ürün";
                        details += $"  • {menuItemName} - Adet: {item.Quantity} - Fiyat: {item.TotalPrice:F2} TL\n";
                        totalPrice += item.TotalPrice;
                    }
                    
                    details += $"\n💰 Toplam Menü Fiyatı: {totalPrice:F2} TL";
                }
                else
                {
                    details += $"\n🍽️ Menü seçimi bulunmamaktadır.";
                }

                // Detayları göster
                DialogService.ShowInfo(details, "Rezervasyon Detayları");
                
                // Ayrıca menü listesini de güncelle
                LoadReservationMenuItems(reservation.Id);
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"Rezervasyon detayları yüklenemedi: {ex.Message}");
            }
        }
    }
}
