using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestoranRezervasyonSistemi
{
    public partial class RezervasyonListesi : Form
    {
        public RezervasyonListesi()
        {
            InitializeComponent();
        }

        private void RezervasyonListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        public void Listele()
        {
            string cs = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    string sql = @"SELECT r.id, t.table_name as [Masa], r.customer_name as [Müşteri], 
                                   r.reservation_date as [Tarih], r.reservation_time as [Saat] 
                                   FROM reservations r 
                                   JOIN tables t ON r.table_id = t.id 
                                   ORDER BY r.reservation_date, r.reservation_time";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRezervasyonlar.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        // Detay formundan çağrılan Yetki Kontrollü Silme Motoru
        public bool RezervasyonIptalEt(int masaId, string silecekKullaniciMail)
        {
            string cs = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    // Sadece masa ID ve mail uyuşuyorsa siler
                    string sql = "DELETE FROM reservations WHERE table_id = @masaId AND customer_email = @mail AND reservation_date = CAST(GETDATE() AS DATE)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@masaId", masaId);
                    cmd.Parameters.AddWithValue("@mail", silecekKullaniciMail);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIptalEt_Click(object sender, EventArgs e)
        {
            // 1. Önce tabloda bir satır seçili mi kontrol et
            if (dgvRezervasyonlar.SelectedRows.Count > 0)
            {
                // 2. Seçili satırdaki ID'yi al 
                int seciliRezervasyonId = Convert.ToInt32(dgvRezervasyonlar.SelectedRows[0].Cells["id"].Value);

                DialogResult onay = MessageBox.Show("Bu rezervasyonu iptal etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo);

                if (onay == DialogResult.Yes)
                {
                    string cs = @"Data Source=127.0.0.1,1433;Initial Catalog=rrms_db;Integrated Security=True;TrustServerCertificate=True";
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        conn.Open();
                        // Listeden sildiğimiz için direkt rezervasyonun kendi ID'si üzerinden siliyoruz
                        string sql = "DELETE FROM reservations WHERE id = @id";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", seciliRezervasyonId);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Rezervasyon başarıyla silindi.");
                            Listele(); // Listeyi yenile
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen iptal etmek istediğiniz rezervasyonu listeden seçin.");
            }
        }
    }
}