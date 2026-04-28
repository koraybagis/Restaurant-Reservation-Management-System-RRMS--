using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Controllers;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class MenuYonetimForm : Form
    {
        private readonly MenuController _menuController = new MenuController();
        private List<RestoranRezervasyonSistemi.Models.MenuItem> _menuItems;

        public MenuYonetimForm()
        {
            InitializeComponent();
            LoadMenuItems();
        }

        public MenuYonetimForm(RestoranRezervasyonSistemi.Models.MenuItem menuItem)
        {
            InitializeComponent();
            LoadMenuItems();
            // Seçili menü öğesini formda göster
            if (menuItem != null)
            {
                var index = _menuItems.FindIndex(item => item.Id == menuItem.Id);
                if (index >= 0)
                {
                    lstMenu.SelectedIndex = index;
                }
            }
        }

        private void LoadMenuItems()
        {
            try
            {
                var menuItems = _menuController.GetAllMenuItemsIncludingUnavailable();
                _menuItems = menuItems;
                lstMenu.Items.Clear();

                foreach (var item in _menuItems)
                {
                    string availability = item.IsAvailable ? "" : "(Mevcut Değil)";
                    lstMenu.Items.Add($"{item.Name} - {item.Price:F2} TL ({item.Category}) {availability}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Menü yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text) || nudFiyat.Value <= 0)
            {
                MessageBox.Show("Lütfen yemek adı ve fiyat giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var menuItem = new MenuItemModel
                {
                    Name = txtAd.Text,
                    Description = txtAciklama.Text,
                    Price = nudFiyat.Value,
                    ImagePath = txtResimYolu.Text,
                    Category = cmbKategori.Text,
                    IsAvailable = chkMevcut.Checked,
                    CreatedDate = DateTime.Now
                };

                _menuController.AddMenuItem(menuItem.ToMenuItem());
                MessageBox.Show("Yemek başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadMenuItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (lstMenu.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen güncellenecek yemeği seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAd.Text) || nudFiyat.Value <= 0)
            {
                MessageBox.Show("Lütfen yemek adı ve fiyat giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedItem = _menuItems[lstMenu.SelectedIndex];
                selectedItem.Name = txtAd.Text;
                selectedItem.Description = txtAciklama.Text;
                selectedItem.Price = nudFiyat.Value;
                selectedItem.ImagePath = txtResimYolu.Text;
                selectedItem.Category = cmbKategori.Text;
                selectedItem.IsAvailable = chkMevcut.Checked;

                _menuController.UpdateMenuItem(selectedItem);
                MessageBox.Show("Yemek başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadMenuItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lstMenu.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen silinecek yemeği seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bu yemeği silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var selectedItem = _menuItems[lstMenu.SelectedIndex];
                    _menuController.DeleteMenuItem(selectedItem.Id);
                    MessageBox.Show("Yemek başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadMenuItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lstMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMenu.SelectedIndex >= 0 && lstMenu.SelectedIndex < _menuItems.Count)
            {
                var selectedItem = _menuItems[lstMenu.SelectedIndex];
                txtAd.Text = selectedItem.Name;
                txtAciklama.Text = selectedItem.Description;
                nudFiyat.Value = selectedItem.Price;
                txtResimYolu.Text = selectedItem.ImagePath;
                cmbKategori.Text = selectedItem.Category;
                chkMevcut.Checked = selectedItem.IsAvailable;
                
                // Seçilen yemeğin detaylarını göster
                LoadMenuItemDetails(new MenuItemModel(selectedItem));
            }
        }

        private void LoadMenuItemDetails(MenuItemModel menuItem)
        {
            try
            {
                // Yemek detaylarını beyaz renkle göster
                string details = $"Ad: {menuItem.Name}\n" +
                              $"Kategori: {menuItem.Category}\n" +
                              $"Fiyat: {menuItem.Price:F2} TL\n" +
                              $"Durum: {(menuItem.IsAvailable ? "Mevcut" : "Mevcut Değil")}\n" +
                              $"Eklenme: {menuItem.CreatedDate:dd.MM.yyyy}";
                
                lblYemekDetay.ForeColor = System.Drawing.Color.White;
                lblYemekDetay.Text = details;
                
                // Yemek resmini göster
                LoadMenuItemImage(menuItem.ImagePath);
            }
            catch (Exception ex)
            {
                lblYemekDetay.ForeColor = System.Drawing.Color.Red;
                lblYemekDetay.Text = "Detaylar yüklenemedi: " + ex.Message;
                picYemekResmi.Image = null;
            }
        }

        private void LoadMenuItemImage(string imagePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imagePath))
                {
                    picYemekResmi.Image = null;
                    return;
                }

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = Path.Combine(basePath, imagePath);
                
                if (File.Exists(fullPath))
                {
                    picYemekResmi.Image = Image.FromFile(fullPath);
                }
                else
                {
                    picYemekResmi.Image = null;
                }
            }
            catch (Exception)
            {
                picYemekResmi.Image = null;
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtAd.Clear();
            txtAciklama.Clear();
            nudFiyat.Value = 0;
            txtResimYolu.Clear();
            cmbKategori.Text = "";
            chkMevcut.Checked = true;
            lstMenu.SelectedIndex = -1;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
