using System;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Services;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class PersonelLoginForm : Form
    {
        private readonly LoginController _loginController;
        public User LoggedInUser { get; private set; }

        public PersonelLoginForm()
        {
            InitializeComponent();
            _loginController = new LoginController();
        }

        private void btnGarsonGiris_Click(object sender, EventArgs e)
        {
            PerformLogin(UserRole.Waiter);
        }

        private void btnTemizlikGiris_Click(object sender, EventArgs e)
        {
            PerformLogin(UserRole.Cleaner);
        }

        private void PerformLogin(string role)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Kullanıcı adı ve şifre boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var user = _loginController.CheckLogin(username, password);
                
                var expectedRole = UserRoleService.Normalize(role);
                var actualRole = user == null ? string.Empty : UserRoleService.Normalize(user.Role);

                if (user != null && string.Equals(actualRole, expectedRole, StringComparison.OrdinalIgnoreCase))
                {
                    LoggedInUser = user;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Geçersiz {role} bilgileri!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş yapılırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PersonelLoginForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
