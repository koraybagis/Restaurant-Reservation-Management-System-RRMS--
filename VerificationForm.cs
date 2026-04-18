using System.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace RestoranRezervasyonSistemi
{
    public partial class VerificationForm : Form
    {
        // Dışarıdan gelen veriler
        public string GelenMail { get; set; }
        public string GelenKullaniciAdi { get; set; }
        public string GelenSifre { get; set; }
        public string GelenKod { get; set; }

        // Bu değişken Kayıt mı (False) yoksa Şifre Sıfırlama mı (True) olduğunu belirler
        public bool IsPasswordReset { get; set; } = false;

        // Senin çalışan bağlantı adresini buraya ekledim
        string connectionString = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";

        public VerificationForm()
        {
            InitializeComponent();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                // ÖNCE: Maildeki kod ile ekrandaki kutu uyuşuyor mu? (Veritabanına gitmeden önce)
                if (txtCode.Text.Trim() == GelenKod.Trim())
                {
                    // DURUM 1: ŞİFRE SIFIRLAMA
                    if (IsPasswordReset)
                    {
                        MessageBox.Show("Kod Doğrulandı! Lütfen yeni şifrenizi belirleyin.");
                        ResetPasswordForm rpf = new ResetPasswordForm();
                        rpf.UserMail = GelenMail;
                        rpf.Show();
                        this.Hide();
                    }
                    // DURUM 2: YENİ KAYIT (REGISTER)
                    else
                    {
                        // Kod doğru, şimdi kullanıcıyı veritabanına ASIL kaydetme vakti!
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string saveQuery = "INSERT INTO users (username, password, email) VALUES (@user, @pass, @mail)";
                            SqlCommand saveCmd = new SqlCommand(saveQuery, conn);
                            saveCmd.Parameters.AddWithValue("@user", GelenKullaniciAdi);
                            saveCmd.Parameters.AddWithValue("@pass", GelenSifre);
                            saveCmd.Parameters.AddWithValue("@mail", GelenMail);
                            saveCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Kayıt Başarılı! Hoş geldiniz, " + GelenKullaniciAdi + "!");
                        MainForm anaMenu = new MainForm();
                        anaMenu.AktifKullaniciMail = GelenMail;
                        anaMenu.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Hatalı kod girdiniz! Lütfen mailinizi tekrar kontrol edin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }
    }
}