using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Services;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class PersonelPanel : Form
    {
        private readonly TableController _tableController;
        private readonly MenuController _menuController;
        private readonly ReservationController _reservationController;
        private readonly TableOrderService _tableOrderService;
        private readonly User _currentUser;
        private List<Table> _tables;
        private List<RestoranRezervasyonSistemi.Models.MenuItem> _menuItems;

        public PersonelPanel(User user)
        {
            InitializeComponent();
            _tableController = new TableController();
            _menuController = new MenuController();
            _reservationController = new ReservationController();
            _tableOrderService = new TableOrderService(_tableController, _reservationController, _menuController);
            _currentUser = user;
        }

        private void PersonelPanel_Load(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            var displayName = string.IsNullOrWhiteSpace(_currentUser.FullName) ? _currentUser.Username : _currentUser.FullName;
            var role = string.IsNullOrWhiteSpace(_currentUser.Role) ? "Bilinmeyen" : _currentUser.Role;
            lblUser.Text = $"👤 {displayName} ({role})";
            LoadTables();
            LoadMenuItems();
        }

        private void LoadTables()
        {
            try
            {
                _tables = _tableController.GetAllTables();
                dgvMasalar.DataSource = null;
                dgvMasalar.DataSource = _tables;
                
                // Renkleri ayarla
                foreach (DataGridViewRow row in dgvMasalar.Rows)
                {
                    var table = (Table)row.DataBoundItem;
                    var normalizedStatus = GetEffectiveTableStatus(table);
                    row.DefaultCellStyle.BackColor = TableStatusService.GetColor(normalizedStatus);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Masalar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMenuItems()
        {
            try
            {
                _menuItems = _menuController.GetAllMenuItems();
                dgvMenuItems.DataSource = null;
                dgvMenuItems.DataSource = _menuItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Menü yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMasaKapat_Click(object sender, EventArgs e)
        {
            if (dgvMasalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir masa seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedTable = (Table)dgvMasalar.SelectedRows[0].DataBoundItem;
            
            if (GetEffectiveTableStatus(selectedTable) != TableStatus.Occupied)
            {
                MessageBox.Show("Sadece dolu masalar kapatılabilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (DialogService.ShowConfirmation($"{selectedTable.TableName} masasını kapatmak istediğinizden emin misiniz?\nMasa kirli olarak işaretlenecektir.", "Onay"))
                {
                    var activeReservationId = _reservationController.GetActiveReservationIdForTable(
                        selectedTable.Id,
                        DateTime.Today,
                        DateTime.Now.TimeOfDay
                    );

                    if (activeReservationId.HasValue)
                    {
                        var cancelled = _reservationController.CancelByIdWithMenuItems(activeReservationId.Value);
                        if (!cancelled)
                        {
                            MessageBox.Show("Aktif rezervasyon kapatılamadı. İşlem iptal edildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LoadTables();
                            return;
                        }
                    }

                    var updated = _tableController.UpdateTableStatus(selectedTable.Id, TableStatus.Dirty);
                    if (!updated)
                    {
                        MessageBox.Show("Masa durumu güncellenemedi. Lütfen tekrar deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadTables();
                        return;
                    }

                    MessageBox.Show("Masa başarıyla kapatıldı ve kirli olarak işaretlendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTables();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Masa kapatılırken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedTableAndMenuItem(out var selectedTable, out var selectedItem))
                return;

            try
            {
                _tableOrderService.AddItemToTable(selectedTable, selectedItem);

                MessageBox.Show($"{selectedItem.Name} ürünü {selectedTable.TableName} masasına eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTables();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Masa siparişine ürün eklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUrunGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvMenuItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = (RestoranRezervasyonSistemi.Models.MenuItem)dgvMenuItems.SelectedRows[0].DataBoundItem;
            MessageBox.Show($"{selectedItem.Name} seçili. Adisyon için Ürün Ekle/Ürün Sil butonlarını kullanın.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedTableAndMenuItem(out var selectedTable, out var selectedItem))
                return;

            if (GetEffectiveTableStatus(selectedTable) != TableStatus.Occupied)
            {
                MessageBox.Show("Sadece dolu masaların adisyonundan ürün silinebilir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int? reservationId = _reservationController.GetOpenReservationForTable(selectedTable.Id);
                if (!reservationId.HasValue)
                {
                    MessageBox.Show("Bu masa için aktif adisyon bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var removed = _menuController.RemoveItemFromTableOrder(reservationId.Value, selectedItem.Id);
                if (!removed)
                {
                    MessageBox.Show("Bu ürün masa adisyonunda bulunmuyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show($"{selectedItem.Name} ürünü {selectedTable.TableName} masasının adisyonundan düşüldü.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Masa siparişinden ürün silinirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdisyonAl_Click(object sender, EventArgs e)
        {
            if (dgvMasalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir masa seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedTable = (Table)dgvMasalar.SelectedRows[0].DataBoundItem;
            
            if (GetEffectiveTableStatus(selectedTable) != TableStatus.Occupied)
            {
                MessageBox.Show("Sadece dolu masalara adisyon alınabilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable adisyon = _menuController.GetTableAdisyon(selectedTable.Id, DateTime.Now.Date);
                if (adisyon.Rows.Count == 0)
                {
                    MessageBox.Show($"{selectedTable.TableName} için adisyonda ürün bulunmuyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var sb = new StringBuilder();
                sb.AppendLine($"{selectedTable.TableName} - Adisyon");
                sb.AppendLine(new string('-', 36));

                decimal total = 0;
                foreach (DataRow row in adisyon.Rows)
                {
                    var urun = row["UrunAdi"]?.ToString();
                    var adet = Convert.ToInt32(row["Adet"]);
                    var tutar = Convert.ToDecimal(row["Tutar"]);
                    total += tutar;
                    sb.AppendLine($"{urun} x{adet} = {tutar:F2} TL");
                }

                sb.AppendLine(new string('-', 36));
                sb.AppendLine($"TOPLAM: {total:F2} TL");
                MessageBox.Show(sb.ToString(), "Adisyon", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adisyon alınırken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadTables();
            LoadMenuItems();
        }

        private bool TryGetSelectedTableAndMenuItem(out Table selectedTable, out RestoranRezervasyonSistemi.Models.MenuItem selectedItem)
        {
            selectedTable = null;
            selectedItem = null;

            if (dgvMasalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen önce bir masa seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dgvMenuItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen menüden bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            selectedTable = (Table)dgvMasalar.SelectedRows[0].DataBoundItem;
            selectedItem = (RestoranRezervasyonSistemi.Models.MenuItem)dgvMenuItems.SelectedRows[0].DataBoundItem;
            return true;
        }

        private string GetEffectiveTableStatus(Table table)
        {
            var normalizedStatus = TableStatusService.Normalize(table?.Status);
            if (normalizedStatus == TableStatus.Dirty || normalizedStatus == TableStatus.Occupied)
                return normalizedStatus;

            if (table == null)
                return TableStatus.Available;

            try
            {
                var hasActiveReservation = _reservationController.HasActiveReservationForTable(table.Id, DateTime.Today, DateTime.Now.TimeOfDay);
                return hasActiveReservation ? TableStatus.Occupied : normalizedStatus;
            }
            catch
            {
                return normalizedStatus;
            }
        }

    }
}
