using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace RestoranRezervasyonSistemi
{
    public partial class RegisterForm : Form
    {
        // Senin çalışan bağlantı adresin
        string connectionString = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 6 haneli rastgele kod üret
                Random rnd = new Random();
                string code = rnd.Next(100000, 999999).ToString();

                // 2. Mail Ayarları (Senin verdiğin bilgilerle)
                MailMessage mail = new MailMessage();
                SmtpClient sc = new SmtpClient();
                sc.Credentials = new NetworkCredential("merttemizcanbir@gmail.com", "wwidqtllkzghrnio");
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;

                mail.From = new MailAddress("merttemizcanbir@gmail.com", "Restoran Otomasyon");
                mail.To.Add(txtMail.Text); // Formdaki email kutusu
                mail.Subject = "Kayıt Onay Kodu";
                mail.Body = $"Merhaba {txtUser.Text}, Kayıt onay kodunuz: {code}";

                sc.Send(mail);

                MessageBox.Show("Onay kodu mail adresinize gönderildi!");

                // 3. Onay Formuna (VerificationForm) yönlendir
                VerificationForm vf = new VerificationForm();
                vf.GelenMail = txtMail.Text;
                vf.GelenKullaniciAdi = txtUser.Text;
                vf.GelenSifre = txtPass.Text;
                vf.GelenKod = code; // Üretilen kodu onay formuna yolluyoruz
                vf.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderim hatası: " + ex.Message);
            }
        }
    }
}