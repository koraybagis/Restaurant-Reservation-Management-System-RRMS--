using System;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Services;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class LoginForm : Form
    {
        private readonly LoginController _loginController = new LoginController();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                var identifier = !string.IsNullOrWhiteSpace(txtUser.Text)
                    ? txtUser.Text.Trim()
                    : txtFullName.Text?.Trim();

                var currentUser = _loginController.CheckLogin(identifier, txtPass.Text);

                if (currentUser == null)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı veya hesabınız banlanmış olabilir.");
                    return;
                }

                var normalizedRole = UserRoleService.Normalize(currentUser.Role);
                if (string.Equals(normalizedRole, Models.UserRole.Admin, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Admin Paneline Hoş Geldiniz!");
                    using (var adminPanel = new AdminPanel())
                    {
                        Hide();
                        adminPanel.ShowDialog();
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Giriş Başarılı! Masa Planına Yönlendiriliyorsunuz.");
                    var main = new MainForm
                    {
                        AktifKullaniciMail = currentUser.Email,
                        CurrentUser = currentUser
                    };
                    main.Show();
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnGoRegister_Click(object sender, EventArgs e)
        {
            var rf = new RegisterForm();
            rf.Show();
            this.Hide();
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            var fp = new ForgotPasswordForm();
            fp.Show();
            this.Hide();
        }
    }
}