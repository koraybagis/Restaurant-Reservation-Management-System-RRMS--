using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class AdminPanel : Form
    {
        TableController tableController = new TableController();

        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            dgvMasalar.DataSource = null;
            dgvMasalar.DataSource = tableController.GetAllTables();

            if (dgvMasalar.Columns["Id"] != null) dgvMasalar.Columns["Id"].HeaderText = "No";
            if (dgvMasalar.Columns["TableName"] != null) dgvMasalar.Columns["TableName"].HeaderText = "Masa Adı";
            if (dgvMasalar.Columns["Capacity"] != null) dgvMasalar.Columns["Capacity"].HeaderText = "Kapasite";
            if (dgvMasalar.Columns["Location"] != null) dgvMasalar.Columns["Location"].HeaderText = "Konum";
        }

        // EKLE BUTONU
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                Table yeniMasa = new Table
                {
                    TableName = txtMasaAdi.Text,
                    Capacity = int.Parse(txtKapasite.Text),
                    Location = txtKonum.Text
                };

                if (tableController.AddTable(yeniMasa))
                {
                    MessageBox.Show("Masa başarıyla eklendi!");
                    Listele();
                    Temizle();
                }
            }
            catch (Exception) { MessageBox.Show("Hata: Bilgileri kontrol ediniz."); }
        }

        // SİL BUTONU
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvMasalar.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvMasalar.CurrentRow.Cells["Id"].Value);
                DialogResult onay = MessageBox.Show("Seçili masayı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);

                if (onay == DialogResult.Yes)
                {
                    if (tableController.DeleteTable(id))
                    {
                        MessageBox.Show("Masa başarıyla silindi.");
                        Listele();
                    }
                }
            }
        }

        // GÜNCELLE BUTONU
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvMasalar.CurrentRow != null)
            {
                try
                {
                    Table guncellenecek = new Table
                    {
                        Id = Convert.ToInt32(dgvMasalar.CurrentRow.Cells["Id"].Value),
                        TableName = txtMasaAdi.Text,
                        Capacity = int.Parse(txtKapasite.Text),
                        Location = txtKonum.Text
                    };

                    if (tableController.UpdateTable(guncellenecek))
                    {
                        MessageBox.Show("Masa bilgileri güncellendi.");
                        Listele();
                        Temizle();
                    }
                }
                catch (Exception) { MessageBox.Show("Hata: Sayısal değerleri kontrol ediniz."); }
            }
        }

        // Tabloya tıklandığında kutuları doldurma (Kolaylık için)
        private void dgvMasalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMasalar.CurrentRow != null)
            {
                txtMasaAdi.Text = dgvMasalar.CurrentRow.Cells["TableName"].Value.ToString();
                txtKapasite.Text = dgvMasalar.CurrentRow.Cells["Capacity"].Value.ToString();
                txtKonum.Text = dgvMasalar.CurrentRow.Cells["Location"].Value.ToString();
            }
        }

        private void Temizle()
        {
            txtMasaAdi.Clear();
            txtKapasite.Clear();
            txtKonum.Clear();
        }
    }
}