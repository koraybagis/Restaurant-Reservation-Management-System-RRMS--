using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using RestoranRezervasyonSistemi.Views;

namespace RestoranRezervasyonSistemi
{
    public partial class LoginForm : Form
    {
        // Önemli: Portu düzelttiğin bağlantı cümlesini buraya aynen koydum.
        string connectionString = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Sorguyu güncelledik: Hem 'role' hem de 'email' bilgisini çekiyoruz
                    string query = "SELECT role, email FROM users WHERE username=@user AND password=@pass";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", txtUser.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPass.Text);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // Giriş başarılı
                        {
                            string userRole = dr["role"].ToString();
                            string userEmail = dr["email"].ToString(); // Kullanıcının mailini aldık

                            if (userRole == "Admin")
                            {
                                MessageBox.Show("Admin Paneline Hoş Geldiniz!");
                                AdminPanel adminPanel = new AdminPanel();
                                // Eğer AdminPanel'de de onay kodu kullanacaksan oraya da aktarabilirsin
                                adminPanel.Show();
                            }
                            else
                            {
                                MessageBox.Show("Giriş Başarılı! Masa Planına Yönlendiriliyorsunuz.");
                                MainForm main = new MainForm();

                                // --- KRİTİK NOKTA: Mail adresini MainForm'a gönderiyoruz ---
                                main.AktifKullaniciMail = userEmail;

                                main.Show();
                            }

                            this.Hide(); // Login formunu gizle
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnGoRegister_Click(object sender, EventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            rf.Show();
            this.Hide();
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm fp = new ForgotPasswordForm();
            fp.Show();
            this.Hide();
        }
    }
}