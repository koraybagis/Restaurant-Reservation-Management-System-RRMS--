using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace RestoranRezervasyonSistemi
{
    public partial class ForgotPasswordForm : Form
    {
        string connectionString = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";

        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Kullanıcı var mı kontrol et
                    string query = "SELECT COUNT(*) FROM users WHERE email=@mail";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mail", txtResetMail.Text);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        string code = new Random().Next(100000, 999999).ToString();

                        // Veritabanındaki kodu güncelle (Doğrulama için)
                        string update = "UPDATE users SET verification_code=@code WHERE email=@mail";
                        SqlCommand upCmd = new SqlCommand(update, conn);
                        upCmd.Parameters.AddWithValue("@code", code);
                        upCmd.Parameters.AddWithValue("@mail", txtResetMail.Text);
                        upCmd.ExecuteNonQuery();

                        // Mail Gönder (Senin bilgilerinle)
                        MailMessage mail = new MailMessage();
                        SmtpClient sc = new SmtpClient();
                        
                        sc.Credentials = new NetworkCredential("merttemizcanbir@gmail.com", "wwidqtllkzghrnio");
                        sc.Port = 587;
                        sc.Host = "smtp.gmail.com";
                        sc.EnableSsl = true;

                        mail.From = new MailAddress("merttemizcanbir@gmail.com");
                        mail.To.Add(txtResetMail.Text);
                        mail.Subject = "Şifre Sıfırlama Kodu";
                        mail.Body = "Şifre sıfırlama talebiniz için kodunuz: " + code;

                        sc.Send(mail);

                        MessageBox.Show("Sıfırlama kodu mailinize gönderildi!");

                        
                        VerificationForm vf = new VerificationForm();
                        vf.GelenMail = txtResetMail.Text; 
                        vf.GelenKod = code;              
                        vf.IsPasswordReset = true;       
                        vf.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bu mail adresi sistemde kayıtlı değil!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}