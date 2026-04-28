using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Services;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class TemizlikPanel : Form
    {
        private readonly TableController _tableController;
        private readonly ReservationController _reservationController;
        private readonly User _currentUser;
        private List<Table> _tables;

        public TemizlikPanel(User user)
        {
            InitializeComponent();
            _tableController = new TableController();
            _reservationController = new ReservationController();
            _currentUser = user;
        }

        private void TemizlikPanel_Load(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!string.Equals(UserRoleService.Normalize(_currentUser.Role), UserRole.Cleaner, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bu panele sadece temizlik personeli erişebilir.", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            var displayName = string.IsNullOrWhiteSpace(_currentUser.FullName) ? _currentUser.Username : _currentUser.FullName;
            var role = string.IsNullOrWhiteSpace(_currentUser.Role) ? "Bilinmeyen" : _currentUser.Role;
            lblUser.Text = $"👤 {displayName} ({role})";
            LoadTables();
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

        private void btnMasaTemizle_Click(object sender, EventArgs e)
        {
            if (dgvMasalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir masa seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedTable = (Table)dgvMasalar.SelectedRows[0].DataBoundItem;
            
            if (GetEffectiveTableStatus(selectedTable) != TableStatus.Dirty)
            {
                MessageBox.Show("Sadece kirli masalar temizlenebilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (DialogService.ShowConfirmation($"{selectedTable.TableName} masasını temizlemek istediğinizden emin misiniz?\nMasa temiz (yeşil) olarak işaretlenecektir.", "Onay"))
                {
                    var updated = _tableController.UpdateTableStatus(selectedTable.Id, TableStatus.Available);
                    if (!updated)
                    {
                        MessageBox.Show("Masa durumu güncellenemedi. Lütfen listeyi yenileyip tekrar deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadTables();
                        return;
                    }

                    MessageBox.Show("Masa başarıyla temizlendi ve kullanılabilir olarak işaretlendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTables();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Masa temizlenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
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
