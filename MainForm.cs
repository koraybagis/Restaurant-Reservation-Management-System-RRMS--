using System;
using System.Drawing;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;

namespace RestoranRezervasyonSistemi
{
    public partial class MainForm : Form
    {
        public string AktifKullaniciMail { get; set; }
        TableController tableController = new TableController();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MasalariYukle();
        }

        public void MasalariYukle()
        {
            
            flpMasalar.Controls.Clear();

            var masalar = tableController.GetAllTables();

            foreach (var masa in masalar)
            {
                Button btn = new Button();

                
                string durum = (masa.Status ?? "").ToString().Trim();

                
                TimeSpan suAnkiSaat = DateTime.Now.TimeOfDay; 

                
                if (masa.ReservationTime != null)
                {
                    TimeSpan rezSaati = (TimeSpan)masa.ReservationTime;

                    // Rezervasyona 30 dakika kala kırmızı yap (Bilgisayar saati bazlı)
                    if (suAnkiSaat >= rezSaati.Add(TimeSpan.FromMinutes(-30)) &&
                        suAnkiSaat <= rezSaati.Add(TimeSpan.FromHours(2)))
                    {
                        btn.BackColor = Color.Firebrick; // KIRMIZI
                    }
                    else
                    {
                        btn.BackColor = Color.ForestGreen; // YEŞİL
                    }
                }
                else
                {
                    btn.BackColor = Color.ForestGreen; // YEŞİL
                }

                btn.Text = $"{masa.TableName}\n{masa.Location}\n({masa.Capacity} Kişilik)";
                btn.Name = "btnMasa" + masa.Id;
                btn.Size = new Size(120, 120);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                
                ToolTip tt = new ToolTip();
                tt.AutoPopDelay = 5000;
                tt.InitialDelay = 500;
                tt.ReshowDelay = 200;
                tt.ShowAlways = true;

                if (durum == "Dolu")
                {
                    tt.SetToolTip(btn, $"{masa.TableName} REZERVE\nDurum: Şu an kullanımda.");
                }
                else
                {
                    tt.SetToolTip(btn, $"{masa.TableName} MÜSAİT\nKapasite: {masa.Capacity} Kişi\nKonum: {masa.Location}");
                }

                
                btn.Click += (s, ev) => {
                    RezervasyonDetay detay = new RezervasyonDetay();
                    detay.SecilenMasaAd = masa.TableName;
                    detay.SecilenMasaId = masa.Id;
                    detay.GirisYapanAdminMail = this.AktifKullaniciMail;
                    detay.SecilenMasaKapasite = masa.Capacity;

                    detay.ShowDialog(); // Formu açar ve kapanana kadar bekler

                    // Form kapandıktan sonra masaları tekrar yükle
                    MasalariYukle();
                };

                
                flpMasalar.Controls.Add(btn);
            }

            
            flpMasalar.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MasalariYukle();
        }

        private void pnlTableArea_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}