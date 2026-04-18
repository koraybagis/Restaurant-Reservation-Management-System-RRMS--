using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranRezervasyonSistemi
{
    public partial class ResetPasswordForm : Form
    {
        public string UserMail { get; set; }
        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string pass1 = txtNewPass.Text;
            string pass2 = txtNewPassConfirm.Text;

            // 1. Boş mu kontrol et
            if (string.IsNullOrEmpty(pass1) || string.IsNullOrEmpty(pass2))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!");
                return;
            }

            // 2. Şifreler uyuşuyor mu kontrol et
            if (pass1 != pass2)
            {
                MessageBox.Show("Girdiğiniz şifreler birbirliyle eşleşmiyor!");
                return;
            }

            // 3. Veritabanında Güncelle
            try
            {
                // 
                string connectionString = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Open) conn.Close(); // Varsa açık olanı kapat
                    conn.Open();
                    string updateQuery = "UPDATE users SET password = @newpass WHERE email = @mail";
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);

                    cmd.Parameters.AddWithValue("@newpass", pass1); // Yeni şifre
                    cmd.Parameters.AddWithValue("@mail", UserMail); // VerificationForm'dan gelen mail

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Şifreniz başarıyla güncellendi! Giriş yapabilirsiniz.");

                        // Kullanıcıyı tekrar login ekranına gönderiyoruz
                        LoginForm login = new LoginForm();
                        login.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message);
            }
        }
    }
}
