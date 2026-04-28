using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using RestoranRezervasyonSistemi.Controllers;
using RestoranRezervasyonSistemi.Models;
using RestoranRezervasyonSistemi.Services;

namespace RestoranRezervasyonSistemi.Views
{
    public partial class MainForm : Form
    {
        public string AktifKullaniciMail { get; set; }
        public User CurrentUser { get; set; }

        private readonly TableController _tableController;
        private readonly ToolTip _toolTip;

        public MainForm()
        {
            _tableController = new TableController();
            _toolTip = CreateToolTip();
            InitializeComponent();
        }

        private ToolTip CreateToolTip()
        {
            return new ToolTip
            {
                AutoPopDelay = 8000,
                InitialDelay = 400,
                ReshowDelay = 200,
                ShowAlways = true
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTables();
                SetupUserManagementButton();
                SetupPersonelGirisButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ana form yüklenirken hata: {ex.Message}\n\nHata Detayı: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupUserManagementButton()
        {
            var isAdmin = string.Equals(UserRoleService.Normalize(CurrentUser?.Role), Models.UserRole.Admin, StringComparison.OrdinalIgnoreCase);
            if (!isAdmin)
            {
                btnUserManagement.Visible = false;
                return;
            }

            btnUserManagement.Visible = true;
            btnUserManagement.Click -= BtnUserManagement_Click;
            btnUserManagement.Click += BtnUserManagement_Click;
        }

        private void BtnUserManagement_Click(object sender, EventArgs e)
        {
            using (var userManagement = new UserManagementForm())
            {
                userManagement.ShowDialog();
            }
        }

        private void SetupPersonelGirisButton()
        {
            btnPersonelGiris.Click -= BtnPersonelGiris_Click;
            btnPersonelGiris.Click += BtnPersonelGiris_Click;
        }

        private void BtnPersonelGiris_Click(object sender, EventArgs e)
        {
            using (var personelLoginForm = new PersonelLoginForm())
            {
                if (personelLoginForm.ShowDialog() == DialogResult.OK)
                {
                    var loggedInUser = personelLoginForm.LoggedInUser;
                    
                    if (loggedInUser == null)
                    {
                        MessageBox.Show("Giriş başarısız. Kullanıcı bilgisi alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var normalizedRole = UserRoleService.Normalize(loggedInUser.Role);
                    if (string.Equals(normalizedRole, Models.UserRole.Waiter, StringComparison.OrdinalIgnoreCase))
                    {
                        using (var personelPanel = new PersonelPanel(loggedInUser))
                        {
                            personelPanel.ShowDialog();
                        }
                    }
                    else if (string.Equals(normalizedRole, Models.UserRole.Cleaner, StringComparison.OrdinalIgnoreCase))
                    {
                        using (var temizlikPanel = new TemizlikPanel(loggedInUser))
                        {
                            temizlikPanel.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bu kullanıcı için yetki tanımlanmamış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        public void LoadTables()
        {
            try
            {
                flpMasalar.Controls.Clear();

                var tables = _tableController.GetAllTables();
                
                if (tables == null || tables.Count == 0)
                {
                    MessageBox.Show("Veritabanında masa bulunamadı. Geçici olarak dummy masalar gösteriliyor.\n\nLütfen Admin Panelinden masalar ekleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tables = CreateFallbackTables();
                }

                var tableButtonInfos = new List<TableButtonInfo>();

                foreach (var table in tables)
                {
                    var buttonColor = GetTableButtonColor(table);
                    var tooltipText = GetTableTooltipText(table);
                    
                    tableButtonInfos.Add(new TableButtonInfo(table, buttonColor, tooltipText));
                }

                foreach (var tableInfo in tableButtonInfos)
                {
                    var button = CreateTableButton(tableInfo);
                    flpMasalar.Controls.Add(button);
                }

                flpMasalar.Refresh();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Veritabanı hatası (masa yüklenirken): {ex.Message}\n\nSQL Hata Kodu: {ex.Number}\n\nDummy masalar gösteriliyor.", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadFallbackTables();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"Null reference hatası (masa yüklenirken): {ex.Message}\n\nDummy masalar gösteriliyor.", "Null Reference Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadFallbackTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Masalar yüklenirken hata oluştu: {ex.Message}\n\nDummy masalar gösteriliyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadFallbackTables();
            }
        }

        private void LoadFallbackTables()
        {
            flpMasalar.Controls.Clear();
            foreach (var table in CreateFallbackTables())
            {
                var buttonColor = GetTableButtonColor(table);
                var tooltipText = GetTableTooltipText(table);
                var tableInfo = new TableButtonInfo(table, buttonColor, tooltipText);
                var button = CreateTableButton(tableInfo);
                flpMasalar.Controls.Add(button);
            }
        }

        private static List<Table> CreateFallbackTables()
        {
            return new List<Table>
            {
                new Table { Id = 1, TableName = "Masa 1", Capacity = 4, Location = "Bahçe", Status = TableStatus.Available },
                new Table { Id = 2, TableName = "Masa 2", Capacity = 4, Location = "Bahçe", Status = TableStatus.Available },
                new Table { Id = 3, TableName = "Masa 3", Capacity = 6, Location = "İç Mekan", Status = TableStatus.Available },
                new Table { Id = 4, TableName = "Masa 4", Capacity = 6, Location = "İç Mekan", Status = TableStatus.Available }
            };
        }

        private Button CreateTableButton(TableButtonInfo tableInfo)
        {
            var table = tableInfo.Table;
            
            var button = new Button
            {
                Text = FormatButtonText(table),
                Name = $"btnMasa{table.Id}",
                Size = new Size(TableConstants.ButtonSize, TableConstants.ButtonSize),
                BackColor = tableInfo.ButtonColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", TableConstants.ButtonFontSize, FontStyle.Bold)
            };

            _toolTip.SetToolTip(button, tableInfo.TooltipText);
            button.Click += (s, ev) => HandleTableClick(table);

            return button;
        }

        private string FormatButtonText(Table table)
        {
            return $"{table.TableName}\n{table.Location}\n({table.Capacity} Kişilik)";
        }

        private Color GetTableButtonColor(Table table)
        {
            var normalizedStatus = TableStatusService.Normalize(table?.Status);
            return TableStatusService.GetColor(normalizedStatus);
        }

        private string GetTableTooltipText(Table table)
        {
            return $"{table.TableName}\nKonum: {table.Location}\nKapasite: {table.Capacity}\nDurum: {table.Status}";
        }

        private void HandleTableClick(Table table)
        {
            try
            {
                // Null kontrolleri
                if (table == null)
                {
                    MessageBox.Show("Masa bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(this.AktifKullaniciMail))
                {
                    MessageBox.Show("Kullanıcı mail adresi bulunamadı. Lütfen tekrar giriş yapın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.CurrentUser == null)
                {
                    MessageBox.Show("Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var rezervasyonDetay = new RezervasyonDetay())
                {
                    rezervasyonDetay.SecilenMasaAd = table.TableName ?? "Bilinmeyen Masa";
                    rezervasyonDetay.SecilenMasaId = table.Id;
                    rezervasyonDetay.GirisYapanAdminMail = this.AktifKullaniciMail;
                    rezervasyonDetay.AktifKullanici = this.CurrentUser;
                    rezervasyonDetay.SecilenMasaKapasite = table.Capacity;

                    rezervasyonDetay.ShowDialog();
                }

                LoadTables();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"Null reference hatası: {ex.Message}\n\nLütfen tüm alanların dolu olduğundan emin olun.", "Null Reference Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Masa tıklanırken hata oluştu: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void pnlTableArea_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}