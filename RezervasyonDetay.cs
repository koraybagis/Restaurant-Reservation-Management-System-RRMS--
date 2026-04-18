using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace RestoranRezervasyonSistemi
{
    public partial class RezervasyonDetay : Form
    {
        public string SecilenMasaAd { get; set; }
        public int SecilenMasaId { get; set; }
        public string GirisYapanAdminMail { get; set; }
        public int SecilenMasaKapasite { get; set; }
        string dogrulamaKodu;

        public RezervasyonDetay()
        {
            InitializeComponent();
        }

        private void RezervasyonDetay_Load(object sender, EventArgs e)
        {
            lblBilgi.Text = SecilenMasaAd + " Rezervasyon İşlemi";
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

            if (string.IsNullOrEmpty(GirisYapanAdminMail)) GirisYapanAdminMail = "merttemizcanbir@gmail.com";

            Random rnd = new Random();
            dogrulamaKodu = rnd.Next(100000, 999999).ToString();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
                sc.Credentials = new NetworkCredential("merttemizcanbir@gmail.com", "wwidqtllkzghrnio");
                sc.EnableSsl = true;
                mail.From = new MailAddress("merttemizcanbir@gmail.com");
                mail.To.Add(GirisYapanAdminMail);
                mail.Subject = "Rezervasyon İşlem Onayı";
                mail.Body = $"Onay kodunuz: {dogrulamaKodu}";

                sc.Send(mail);
                MessageBox.Show($"Güvenlik kodu {GirisYapanAdminMail} adresine gönderildi.", "Onay Gerekli");

                IslemOnay onayEkrani = new IslemOnay();
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
            string connectionString = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO reservations (table_id, customer_name, customer_phone, reservation_date, guest_count, reservation_time, customer_email) " +
                                 "VALUES (@tid, @name, @phone, @date, @count, @time, @mail)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@tid", SecilenMasaId);
                    cmd.Parameters.AddWithValue("@name", txtMusteriAd.Text);
                    cmd.Parameters.AddWithValue("@phone", txtMusteriTel.Text);
                    cmd.Parameters.AddWithValue("@date", dtpTarih.Value.Date);
                    cmd.Parameters.AddWithValue("@count", (int)nmrKisiSayisi.Value);
                    cmd.Parameters.AddWithValue("@time", dtpSaat.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@mail", GirisYapanAdminMail);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Başarıyla rezerve edildi!");
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private bool CakismaVarMi(int masaId, DateTime tarih, TimeSpan saat)
        {
            string connectionString = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM reservations WHERE table_id = @tid AND reservation_date = @date AND ABS(DATEDIFF(minute, reservation_time, @time)) < 120";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tid", masaId);
                cmd.Parameters.AddWithValue("@date", tarih.Date);
                cmd.Parameters.AddWithValue("@time", saat);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        
        
        private void btnIptalEt_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Rezervasyonunuzu iptal etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo);
            if (onay == DialogResult.Yes)
            {
                RezervasyonListesi listeMotoru = new RezervasyonListesi();
                // Bilgileri parametre olarak gönderiyoruz 
                bool sonuc = listeMotoru.RezervasyonIptalEt(this.SecilenMasaId, this.GirisYapanAdminMail);

                if (sonuc)
                {
                    MessageBox.Show("İptal edildi.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sadece kendi rezervasyonunuzu iptal edebilirsiniz!", "Yetki Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
            RezervasyonListesi liste = new RezervasyonListesi();
            liste.ShowDialog();
        }
    }
}