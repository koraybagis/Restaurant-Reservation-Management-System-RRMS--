using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Data;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Services;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class RezervasyonDetay : Form
    {
        public string SecilenMasaAd { get; set; }
        public int SecilenMasaId { get; set; }
        public string GirisYapanAdminMail { get; set; }
        public User AktifKullanici { get; set; }
        public int SecilenMasaKapasite { get; set; }
        string dogrulamaKodu;

        // 6. hafta kodları eklendi
        private readonly ReservationController _reservationController = new ReservationController();
        private readonly SmtpEmailService _emailService = new SmtpEmailService();
        private int? _currentUserReservationId;

        // 9. hafta kodları eklendi - Menü ve yemek seçimi
        private readonly MenuController _menuController = new MenuController();
        private readonly ReservationWorkflowService _reservationWorkflowService;
        private Dictionary<int, RestoranRezervasyonSistemi.Models.MenuItem> _menuItemsCache;
        private Dictionary<int, int> _yemekAdetleri = new Dictionary<int, int>(); // MenuItemId, Adet

        public RezervasyonDetay()
        {
            InitializeComponent();
            _reservationWorkflowService = new ReservationWorkflowService(_reservationController, _menuController);
        }

        public RezervasyonDetay(int reservationId)
        {
            InitializeComponent();
            _reservationWorkflowService = new ReservationWorkflowService(_reservationController, _menuController);
            _currentUserReservationId = reservationId;
        }

        private void RezervasyonDetay_Load(object sender, EventArgs e)
        {
            lblBilgi.Text = SecilenMasaAd + " Rezervasyon İşlemi";

            // Login sonrası müşteri ad/soyad ve telefon otomatik gelsin.
            // DB'de full_name yoksa Username ile devam eder.
            if (AktifKullanici != null)
            {
                var displayName = string.IsNullOrWhiteSpace(AktifKullanici.FullName)
                    ? AktifKullanici.Username
                    : AktifKullanici.FullName;

                if (!string.IsNullOrWhiteSpace(displayName))
                    txtMusteriAd.Text = displayName;

                if (!string.IsNullOrWhiteSpace(AktifKullanici.Phone))
                    txtMusteriTel.Text = AktifKullanici.Phone;
            }

            // İptal butonu yalnızca iptal edilecek rezervasyon varsa aktif olsun.
            btnIptalEt.Enabled = false;
            dtpTarih.ValueChanged += (s, ev) => UpdateCancelButtonState();
            dtpSaat.ValueChanged += (s, ev) => UpdateCancelButtonState();
            UpdateCancelButtonState();

            // 9. hafta: Menüyü yükle
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {
            try
            {
                var menuItems = _menuController.GetAllMenuItems();
                
                if (menuItems == null || menuItems.Count == 0)
                {
                    MessageBox.Show("Menü yüklenemedi veya menü bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                _menuItemsCache = menuItems.ToDictionary(m => m.Id);

                lstYemekSecimi.Items.Clear();
                lstYemekSecimi.Columns.Clear();
                lstYemekSecimi.Columns.Add(colYemekAdi);
                lstYemekSecimi.Columns.Add(colFiyat);
                lstYemekSecimi.Columns.Add(colAdet);

                foreach (var item in menuItems)
                {
                    var listViewItem = new ListViewItem(new string[] { item.Name, $"{item.Price:F2} TL", "1" });
                    listViewItem.Tag = item.Id;
                    lstYemekSecimi.Items.Add(listViewItem);
                    
                    // Varsayılan adet 1 olarak ayarla
                    _yemekAdetleri[item.Id] = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Menü yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LstYemekSecimi_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                UpdateTotalPrice();
            });
        }

        private void LstYemekSecimi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçili yemeğin resmini göster
            ShowFoodImage();
            UpdateTotalPrice();
        }

        private void ShowFoodImage()
        {
            if (lstYemekSecimi.SelectedItems.Count > 0 && _menuItemsCache != null)
            {
                var selectedItem = lstYemekSecimi.SelectedItems[0];
                var menuItemId = (int)selectedItem.Tag;
                
                if (_menuItemsCache.ContainsKey(menuItemId))
                {
                    var menuItem = _menuItemsCache[menuItemId];
                    LoadFoodImage(menuItem.ImagePath);
                }
            }
        }

        private void LoadFoodImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    picYemekResmi.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Varsayılan resim veya resim yoksa boş bırak
                    picYemekResmi.Image = null;
                }
            }
            catch
            {
                // Resim yüklenemezse hata vermeden devam et
                picYemekResmi.Image = null;
            }
        }

        private void lstYemekSecimi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstYemekSecimi.SelectedItems.Count > 0)
            {
                var selectedItem = lstYemekSecimi.SelectedItems[0];
                var menuItemId = (int)selectedItem.Tag;
                var currentAdet = _yemekAdetleri.ContainsKey(menuItemId) ? _yemekAdetleri[menuItemId] : 1;
                
                using (var adetForm = new AdetGirisiForm(currentAdet))
                {
                    if (adetForm.ShowDialog() == DialogResult.OK)
                    {
                        _yemekAdetleri[menuItemId] = adetForm.SecilenAdet;
                        selectedItem.SubItems[2].Text = adetForm.SecilenAdet.ToString();
                        UpdateTotalPrice();
                    }
                }
            }
        }

        private void UpdateTotalPrice()
        {
            decimal total = 0;

            for (int i = 0; i < lstYemekSecimi.Items.Count; i++)
            {
                if (lstYemekSecimi.Items[i].Checked && _menuItemsCache != null)
                {
                    var menuItemId = (int)lstYemekSecimi.Items[i].Tag;
                    var menuItem = _menuItemsCache[menuItemId];
                    var adet = _yemekAdetleri.ContainsKey(menuItemId) ? _yemekAdetleri[menuItemId] : 1;
                    total += menuItem.Price * adet;
                }
            }

            lblToplamFiyat.Text = $"Toplam Fiyat: {total:F2} TL";
        }

        private void UpdateCancelButtonState()
        {
            try
            {
                _currentUserReservationId = null;

                var customerName = txtMusteriAd.Text?.Trim();
                var customerEmail = GirisYapanAdminMail;

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(customerEmail))
                {
                    btnIptalEt.Enabled = false;
                    return;
                }

                var selectedTime = dtpSaat.Value.TimeOfDay;
                var r = _reservationWorkflowService.FindUserReservation(SecilenMasaId, dtpTarih.Value.Date, customerEmail, customerName, selectedTime);
                if (r == null)
                {
                    btnIptalEt.Enabled = false;
                    return;
                }

                _currentUserReservationId = r.Value.ReservationId;
                btnIptalEt.Enabled = true;

                // Kullanıcının rezervasyon saatini otomatik seçili yap (iptal kolay olsun).
                var desired = DateTime.Today.Add(r.Value.ReservationTime);
                if (dtpSaat.Value.TimeOfDay != r.Value.ReservationTime)
                    dtpSaat.Value = desired;
            }
            catch
            {
                btnIptalEt.Enabled = false;
                _currentUserReservationId = null;
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (nmrKisiSayisi.Value > SecilenMasaKapasite)
            {
                MessageBox.Show($"Hata: Seçtiğiniz masa en fazla {SecilenMasaKapasite} kişiliktir.", "Kapasite Aşımı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CakismaVarMi(SecilenMasaId, dtpTarih.Value, dtpSaat.Value.TimeOfDay))
            {
                MessageBox.Show("Bu masa seçilen saatlerde dolu.", "Çakışma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(GirisYapanAdminMail))
            {
                MessageBox.Show("Doğrulama e-postası bulunamadı. Lütfen yeniden giriş yapın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dogrulamaKodu = _reservationWorkflowService.CreateVerificationCode();

            try
            {
                _emailService.Send(GirisYapanAdminMail, "Rezervasyon İşlem Onayı", $"Onay kodunuz: {dogrulamaKodu}");
                MessageBox.Show($"Güvenlik kodu {GirisYapanAdminMail} adresine gönderildi.", "Onay Gerekli");

                var onayEkrani = new IslemOnay();
                onayEkrani.GonderilenKod = dogrulamaKodu;

                if (onayEkrani.ShowDialog() == DialogResult.OK)
                {
                    AsilKaydiYap();
                }
            }
            catch (Exception ex) { MessageBox.Show("Mail hatası: " + ex.Message); }
        }

        private void AsilKaydiYap()
        {
            try
            {
                int reservationId = _reservationWorkflowService.CreateReservation(
                    tableId: SecilenMasaId,
                    customerName: txtMusteriAd.Text,
                    customerPhone: txtMusteriTel.Text,
                    date: dtpTarih.Value.Date,
                    time: dtpSaat.Value.TimeOfDay,
                    guestCount: (int)nmrKisiSayisi.Value,
                    customerEmail: GirisYapanAdminMail
                );

                // 9. hafta: Seçilen yemekleri kaydet
                SaveSelectedMenuItems(reservationId);

                MessageBox.Show("Başarıyla rezerve edildi!");
                Close();
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void SaveSelectedMenuItems(int reservationId)
        {
            try
            {
                if (_menuItemsCache == null) return;

                var selectedItems = new List<(int MenuItemId, int Quantity)>();

                for (int i = 0; i < lstYemekSecimi.Items.Count; i++)
                {
                    if (lstYemekSecimi.Items[i].Checked)
                    {
                        var menuItemId = (int)lstYemekSecimi.Items[i].Tag;
                        var adet = _yemekAdetleri.ContainsKey(menuItemId) ? _yemekAdetleri[menuItemId] : 1;
                        selectedItems.Add((menuItemId, adet));
                    }
                }

                _reservationWorkflowService.SaveSelectedMenuItems(reservationId, selectedItems);
            }
            catch (Exception ex)
            {
                // Yemek seçimi hatası rezervasyonu engellemesin
                MessageBox.Show("Yemek seçimi kaydedilirken hata oluştu: " + ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool CakismaVarMi(int masaId, DateTime tarih, TimeSpan saat)
        {
            return _reservationController.HasConflict(masaId, tarih.Date, saat);
        }

        
        
        private void btnIptalEt_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Rezervasyonunuzu iptal etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo);
            if (onay == DialogResult.Yes)
            {
                var customerName = txtMusteriAd.Text?.Trim();
                var customerEmail = GirisYapanAdminMail;

                bool sonuc = false;

                if (_currentUserReservationId.HasValue)
                {
                    sonuc = _reservationController.CancelByIdForUser(_currentUserReservationId.Value, customerEmail, customerName);
                }
                else
                {
                    // Fallback: try by table/date/time
                    sonuc = _reservationController.CancelForUser(
                        tableId: SecilenMasaId,
                        date: dtpTarih.Value.Date,
                        time: dtpSaat.Value.TimeOfDay,
                        customerEmail: customerEmail,
                        customerName: customerName
                    );
                }

                if (sonuc)
                {
                    MessageBox.Show("İptal edildi.");
                    this.Close();
                }
                else
                {
                    // Eğer aynı masa/tarih/saat için rezervasyon varsa ama senin ad+mail eşleşmiyorsa -> yetki yok
                    // Hiç rezervasyon yoksa -> daha doğru mesaj
                    var anyReservationExists = _reservationController.ReservationExists(
                        tableId: SecilenMasaId,
                        date: dtpTarih.Value.Date,
                        time: dtpSaat.Value.TimeOfDay
                    );

                    if (anyReservationExists)
                    {
                        MessageBox.Show("Başkalarının rezervasyonunu iptal edemezsiniz!", "Yetki Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Bu tarih/saat için iptal edilecek rezervasyon bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}