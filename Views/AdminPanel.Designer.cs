namespace RestoranRezervasyonSistemi.Views
{
    partial class AdminPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabMasaYonetimi = new System.Windows.Forms.TabPage();
            this.tabKullaniciYonetimi = new System.Windows.Forms.TabPage();
            this.tabRezervasyonYonetimi = new System.Windows.Forms.TabPage();
            this.tabMenuYonetimi = new System.Windows.Forms.TabPage();
            this.dgvMasalar = new System.Windows.Forms.DataGridView();
            this.txtMasaAdi = new System.Windows.Forms.TextBox();
            this.txtKapasite = new System.Windows.Forms.TextBox();
            this.txtKonum = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.lblMasaAdi = new System.Windows.Forms.Label();
            this.lblKapasite = new System.Windows.Forms.Label();
            this.lblKonum = new System.Windows.Forms.Label();
            this.btnMenuYonetimi = new System.Windows.Forms.Button();
            this.lblMenuManagement = new System.Windows.Forms.Label();
            
            // Kullanıcı Yönetimi Kontrolleri
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.btnBanUser = new System.Windows.Forms.Button();
            this.btnUnbanUser = new System.Windows.Forms.Button();
            this.btnRefreshUsers = new System.Windows.Forms.Button();
            this.lblUserManagement = new System.Windows.Forms.Label();
            
            // Rezervasyon Yönetimi Kontrolleri
            this.dgvRezervasyonlar = new System.Windows.Forms.DataGridView();
            this.btnCancelReservation = new System.Windows.Forms.Button();
            this.btnRefreshReservations = new System.Windows.Forms.Button();
            this.btnRezervasyonDetaylari = new System.Windows.Forms.Button();
            this.lblReservationManagement = new System.Windows.Forms.Label();
            this.lblRezervasyonDetaylari = new System.Windows.Forms.Label();
            this.lstRezervasyonMenuleri = new System.Windows.Forms.ListBox();
            
            this.lblBaslik = new System.Windows.Forms.Label();
            this.mainTabControl.SuspendLayout();
            this.tabMasaYonetimi.SuspendLayout();
            this.tabKullaniciYonetimi.SuspendLayout();
            this.tabRezervasyonYonetimi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRezervasyonlar)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.tabMasaYonetimi);
            this.mainTabControl.Controls.Add(this.tabKullaniciYonetimi);
            this.mainTabControl.Controls.Add(this.tabRezervasyonYonetimi);
            this.mainTabControl.Controls.Add(this.tabMenuYonetimi);
            this.mainTabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.mainTabControl.Location = new System.Drawing.Point(12, 60);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1000, 550);
            this.mainTabControl.TabIndex = 0;
            // 
            // tabMasaYonetimi
            // 
            this.tabMasaYonetimi.Controls.Add(this.dgvMasalar);
            this.tabMasaYonetimi.Controls.Add(this.txtMasaAdi);
            this.tabMasaYonetimi.Controls.Add(this.txtKapasite);
            this.tabMasaYonetimi.Controls.Add(this.txtKonum);
            this.tabMasaYonetimi.Controls.Add(this.btnEkle);
            this.tabMasaYonetimi.Controls.Add(this.btnGuncelle);
            this.tabMasaYonetimi.Controls.Add(this.btnSil);
            this.tabMasaYonetimi.Controls.Add(this.lblMasaAdi);
            this.tabMasaYonetimi.Controls.Add(this.lblKapasite);
            this.tabMasaYonetimi.Controls.Add(this.lblKonum);
            this.tabMasaYonetimi.Location = new System.Drawing.Point(4, 22);
            this.tabMasaYonetimi.Name = "tabMasaYonetimi";
            this.tabMasaYonetimi.Padding = new System.Windows.Forms.Padding(3);
            this.tabMasaYonetimi.Size = new System.Drawing.Size(992, 524);
            this.tabMasaYonetimi.TabIndex = 0;
            this.tabMasaYonetimi.Text = "🪑 Masa Yönetimi";
            this.tabMasaYonetimi.UseVisualStyleBackColor = true;
            // 
            // tabKullaniciYonetimi
            // 
            this.tabKullaniciYonetimi.Controls.Add(this.dgvUsers);
            this.tabKullaniciYonetimi.Controls.Add(this.btnBanUser);
            this.tabKullaniciYonetimi.Controls.Add(this.btnUnbanUser);
            this.tabKullaniciYonetimi.Controls.Add(this.btnRefreshUsers);
            this.tabKullaniciYonetimi.Controls.Add(this.lblUserManagement);
            this.tabKullaniciYonetimi.Location = new System.Drawing.Point(4, 22);
            this.tabKullaniciYonetimi.Name = "tabKullaniciYonetimi";
            this.tabKullaniciYonetimi.Padding = new System.Windows.Forms.Padding(3);
            this.tabKullaniciYonetimi.Size = new System.Drawing.Size(992, 524);
            this.tabKullaniciYonetimi.TabIndex = 1;
            this.tabKullaniciYonetimi.Text = "👥 Kullanıcı Yönetimi";
            this.tabKullaniciYonetimi.UseVisualStyleBackColor = true;
            // 
            // tabRezervasyonYonetimi
            // 
            this.tabRezervasyonYonetimi.Controls.Add(this.dgvRezervasyonlar);
            this.tabRezervasyonYonetimi.Controls.Add(this.btnCancelReservation);
            this.tabRezervasyonYonetimi.Controls.Add(this.btnRefreshReservations);
            this.tabRezervasyonYonetimi.Controls.Add(this.btnRezervasyonDetaylari);
            this.tabRezervasyonYonetimi.Controls.Add(this.lblReservationManagement);
            this.tabRezervasyonYonetimi.Controls.Add(this.lblRezervasyonDetaylari);
            this.tabRezervasyonYonetimi.Controls.Add(this.lstRezervasyonMenuleri);
            this.tabRezervasyonYonetimi.Location = new System.Drawing.Point(4, 22);
            this.tabRezervasyonYonetimi.Name = "tabRezervasyonYonetimi";
            this.tabRezervasyonYonetimi.Padding = new System.Windows.Forms.Padding(3);
            this.tabRezervasyonYonetimi.Size = new System.Drawing.Size(992, 524);
            this.tabRezervasyonYonetimi.TabIndex = 2;
            this.tabRezervasyonYonetimi.Text = "📅 Rezervasyon Yönetimi";
            this.tabRezervasyonYonetimi.UseVisualStyleBackColor = true;
            // 
            // tabMenuYonetimi
            // 
            this.tabMenuYonetimi.Controls.Add(this.btnMenuYonetimi);
            this.tabMenuYonetimi.Controls.Add(this.lblMenuManagement);
            this.tabMenuYonetimi.Location = new System.Drawing.Point(4, 22);
            this.tabMenuYonetimi.Name = "tabMenuYonetimi";
            this.tabMenuYonetimi.Padding = new System.Windows.Forms.Padding(3);
            this.tabMenuYonetimi.Size = new System.Drawing.Size(992, 524);
            this.tabMenuYonetimi.TabIndex = 3;
            this.tabMenuYonetimi.Text = "🍽 Menü Yönetimi";
            this.tabMenuYonetimi.UseVisualStyleBackColor = true;
            // 
            // dgvMasalar
            // 
            this.dgvMasalar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dgvMasalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMasalar.Location = new System.Drawing.Point(20, 80);
            this.dgvMasalar.Name = "dgvMasalar";
            this.dgvMasalar.RowHeadersWidth = 51;
            this.dgvMasalar.RowTemplate.Height = 24;
            this.dgvMasalar.Size = new System.Drawing.Size(950, 400);
            this.dgvMasalar.TabIndex = 0;
            this.dgvMasalar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMasalar_CellClick);
            // 
            // txtMasaAdi
            // 
            this.txtMasaAdi.Location = new System.Drawing.Point(20, 40);
            this.txtMasaAdi.Name = "txtMasaAdi";
            this.txtMasaAdi.Size = new System.Drawing.Size(200, 22);
            this.txtMasaAdi.TabIndex = 1;
            // 
            // txtKapasite
            // 
            this.txtKapasite.Location = new System.Drawing.Point(240, 40);
            this.txtKapasite.Name = "txtKapasite";
            this.txtKapasite.Size = new System.Drawing.Size(100, 22);
            this.txtKapasite.TabIndex = 2;
            // 
            // txtKonum
            // 
            this.txtKonum.Location = new System.Drawing.Point(360, 40);
            this.txtKonum.Name = "txtKonum";
            this.txtKonum.Size = new System.Drawing.Size(200, 22);
            this.txtKonum.TabIndex = 3;
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.Green;
            this.btnEkle.Location = new System.Drawing.Point(20, 10);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(100, 30);
            this.btnEkle.TabIndex = 4;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = false;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnGuncelle.Location = new System.Drawing.Point(130, 10);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(100, 30);
            this.btnGuncelle.TabIndex = 5;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = false;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.Color.Red;
            this.btnSil.Location = new System.Drawing.Point(240, 10);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(100, 30);
            this.btnSil.TabIndex = 6;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // lblMasaAdi
            // 
            this.lblMasaAdi.AutoSize = true;
            this.lblMasaAdi.Location = new System.Drawing.Point(20, 20);
            this.lblMasaAdi.Name = "lblMasaAdi";
            this.lblMasaAdi.Size = new System.Drawing.Size(64, 16);
            this.lblMasaAdi.TabIndex = 7;
            this.lblMasaAdi.Text = "Masa Adı";
            // 
            // lblKapasite
            // 
            this.lblKapasite.AutoSize = true;
            this.lblKapasite.Location = new System.Drawing.Point(240, 20);
            this.lblKapasite.Name = "lblKapasite";
            this.lblKapasite.Size = new System.Drawing.Size(60, 16);
            this.lblKapasite.TabIndex = 8;
            this.lblKapasite.Text = "Kapasite";
            // 
            // lblKonum
            // 
            this.lblKonum.AutoSize = true;
            this.lblKonum.Location = new System.Drawing.Point(360, 20);
            this.lblKonum.Name = "lblKonum";
            this.lblKonum.Size = new System.Drawing.Size(92, 16);
            this.lblKonum.TabIndex = 9;
            this.lblKonum.Text = "Masa Konumu";
            
            // 
            // Kullanıcı Yönetimi Kontrolleri
            // 
            // lblUserManagement
            this.lblUserManagement.AutoSize = true;
            this.lblUserManagement.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserManagement.Location = new System.Drawing.Point(20, 10);
            this.lblUserManagement.Name = "lblUserManagement";
            this.lblUserManagement.Size = new System.Drawing.Size(200, 23);
            this.lblUserManagement.TabIndex = 10;
            this.lblUserManagement.Text = "👥 Kullanıcı İşlemleri";
            
            // 
            // lblMenuManagement
            // 
            this.lblMenuManagement.AutoSize = true;
            this.lblMenuManagement.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenuManagement.Location = new System.Drawing.Point(20, 50);
            this.lblMenuManagement.Name = "lblMenuManagement";
            this.lblMenuManagement.Size = new System.Drawing.Size(200, 23);
            this.lblMenuManagement.TabIndex = 19;
            this.lblMenuManagement.Text = "🍽 Menü İşlemleri";
            // 
            // btnMenuYonetimi
            // 
            this.btnMenuYonetimi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnMenuYonetimi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMenuYonetimi.ForeColor = System.Drawing.Color.White;
            this.btnMenuYonetimi.Location = new System.Drawing.Point(20, 10);
            this.btnMenuYonetimi.Name = "btnMenuYonetimi";
            this.btnMenuYonetimi.Size = new System.Drawing.Size(150, 30);
            this.btnMenuYonetimi.TabIndex = 20;
            this.btnMenuYonetimi.Text = "Menü Yönetimini Aç";
            this.btnMenuYonetimi.UseVisualStyleBackColor = false;
            this.btnMenuYonetimi.Click += new System.EventHandler(this.btnMenuYonetimi_Click);
            // 
            // dgvUsers
            this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(20, 80);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Size = new System.Drawing.Size(950, 350);
            this.dgvUsers.TabIndex = 11;
            this.dgvUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellClick);
            
            // btnBanUser
            this.btnBanUser.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnBanUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBanUser.ForeColor = System.Drawing.Color.White;
            this.btnBanUser.Location = new System.Drawing.Point(20, 440);
            this.btnBanUser.Name = "btnBanUser";
            this.btnBanUser.Size = new System.Drawing.Size(180, 45);
            this.btnBanUser.TabIndex = 12;
            this.btnBanUser.Text = "Kullanıcıyı Banla";
            this.btnBanUser.UseVisualStyleBackColor = false;
            this.btnBanUser.Click += new System.EventHandler(this.btnBanUser_Click);
            
            // btnUnbanUser
            this.btnUnbanUser.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnUnbanUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnUnbanUser.ForeColor = System.Drawing.Color.White;
            this.btnUnbanUser.Location = new System.Drawing.Point(210, 440);
            this.btnUnbanUser.Name = "btnUnbanUser";
            this.btnUnbanUser.Size = new System.Drawing.Size(200, 45);
            this.btnUnbanUser.TabIndex = 13;
            this.btnUnbanUser.Text = "Banı Kaldır";
            this.btnUnbanUser.UseVisualStyleBackColor = false;
            this.btnUnbanUser.Click += new System.EventHandler(this.btnUnbanUser_Click);
            
            // btnRefreshUsers
            this.btnRefreshUsers.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.btnRefreshUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefreshUsers.ForeColor = System.Drawing.Color.White;
            this.btnRefreshUsers.Location = new System.Drawing.Point(400, 440);
            this.btnRefreshUsers.Name = "btnRefreshUsers";
            this.btnRefreshUsers.Size = new System.Drawing.Size(150, 45);
            this.btnRefreshUsers.TabIndex = 14;
            this.btnRefreshUsers.Text = "Listeyi Yenile";
            this.btnRefreshUsers.UseVisualStyleBackColor = false;
            this.btnRefreshUsers.Click += new System.EventHandler(this.btnRefreshUsers_Click);
            
            // 
            // Rezervasyon Yönetimi Kontrolleri
            // 
            // lblReservationManagement
            this.lblReservationManagement.AutoSize = true;
            this.lblReservationManagement.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblReservationManagement.Location = new System.Drawing.Point(20, 10);
            this.lblReservationManagement.Name = "lblReservationManagement";
            this.lblReservationManagement.Size = new System.Drawing.Size(250, 23);
            this.lblReservationManagement.TabIndex = 15;
            this.lblReservationManagement.Text = "📅 Rezervasyon İşlemleri";
            
            // dgvRezervasyonlar
            this.dgvRezervasyonlar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRezervasyonlar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dgvRezervasyonlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRezervasyonlar.Location = new System.Drawing.Point(20, 80);
            this.dgvRezervasyonlar.Name = "dgvRezervasyonlar";
            this.dgvRezervasyonlar.RowHeadersWidth = 51;
            this.dgvRezervasyonlar.RowTemplate.Height = 24;
            this.dgvRezervasyonlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRezervasyonlar.MultiSelect = false;
            this.dgvRezervasyonlar.Size = new System.Drawing.Size(950, 350);
            this.dgvRezervasyonlar.TabIndex = 16;
            this.dgvRezervasyonlar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRezervasyonlar_CellClick);
            
            // btnCancelReservation
            this.btnCancelReservation.BackColor = System.Drawing.Color.FromArgb(255, 140, 0);
            this.btnCancelReservation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancelReservation.ForeColor = System.Drawing.Color.White;
            this.btnCancelReservation.Location = new System.Drawing.Point(20, 440);
            this.btnCancelReservation.Name = "btnCancelReservation";
            this.btnCancelReservation.Size = new System.Drawing.Size(240, 45);
            this.btnCancelReservation.TabIndex = 17;
            this.btnCancelReservation.Text = "Rezervasyonu İptal Et";
            this.btnCancelReservation.UseVisualStyleBackColor = false;
            this.btnCancelReservation.Click += new System.EventHandler(this.btnCancelReservation_Click);
            
            // btnRefreshReservations
            this.btnRefreshReservations.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.btnRefreshReservations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefreshReservations.ForeColor = System.Drawing.Color.White;
            this.btnRefreshReservations.Location = new System.Drawing.Point(270, 440);
            this.btnRefreshReservations.Name = "btnRefreshReservations";
            this.btnRefreshReservations.Size = new System.Drawing.Size(150, 45);
            this.btnRefreshReservations.TabIndex = 18;
            this.btnRefreshReservations.Text = "Listeyi Yenile";
            this.btnRefreshReservations.UseVisualStyleBackColor = false;
            this.btnRefreshReservations.Click += new System.EventHandler(this.btnRefreshReservations_Click);
            
            // btnRezervasyonDetaylari
            // 
            this.btnRezervasyonDetaylari.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnRezervasyonDetaylari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRezervasyonDetaylari.ForeColor = System.Drawing.Color.White;
            this.btnRezervasyonDetaylari.Location = new System.Drawing.Point(430, 440);
            this.btnRezervasyonDetaylari.Name = "btnRezervasyonDetaylari";
            this.btnRezervasyonDetaylari.Size = new System.Drawing.Size(280, 45);
            this.btnRezervasyonDetaylari.TabIndex = 22;
            this.btnRezervasyonDetaylari.Text = "📋 Rezervasyon Detayları";
            this.btnRezervasyonDetaylari.UseVisualStyleBackColor = false;
            this.btnRezervasyonDetaylari.Click += new System.EventHandler(this.btnRezervasyonDetaylari_Click);
            
            // lblRezervasyonDetaylari
            // 
            this.lblRezervasyonDetaylari.AutoSize = true;
            this.lblRezervasyonDetaylari.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRezervasyonDetaylari.ForeColor = System.Drawing.Color.White;
            this.lblRezervasyonDetaylari.Location = new System.Drawing.Point(20, 440);
            this.lblRezervasyonDetaylari.Name = "lblRezervasyonDetaylari";
            this.lblRezervasyonDetaylari.Size = new System.Drawing.Size(200, 23);
            this.lblRezervasyonDetaylari.TabIndex = 20;
            this.lblRezervasyonDetaylari.Text = "Seçili Rezervasyon Menüleri:";
            // 
            // lstRezervasyonMenuleri
            // 
            this.lstRezervasyonMenuleri.FormattingEnabled = true;
            this.lstRezervasyonMenuleri.Location = new System.Drawing.Point(20, 470);
            this.lstRezervasyonMenuleri.Name = "lstRezervasyonMenuleri";
            this.lstRezervasyonMenuleri.Size = new System.Drawing.Size(950, 50);
            this.lstRezervasyonMenuleri.TabIndex = 21;
            // 
            // lblBaslik
            // 
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(12, 15);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(400, 35);
            this.lblBaslik.TabIndex = 19;
            this.lblBaslik.Text = "🏢 RESTORAN YÖNETİM PANELİ";
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.ClientSize = new System.Drawing.Size(1024, 650);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.mainTabControl);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "AdminPanel";
            this.Text = "Restoran Yönetim Paneli";
            this.Load += new System.EventHandler(this.AdminPanel_Load);
            this.mainTabControl.ResumeLayout(false);
            this.tabMasaYonetimi.ResumeLayout(false);
            this.tabMasaYonetimi.PerformLayout();
            this.tabKullaniciYonetimi.ResumeLayout(false);
            this.tabKullaniciYonetimi.PerformLayout();
            this.tabRezervasyonYonetimi.ResumeLayout(false);
            this.tabRezervasyonYonetimi.PerformLayout();
            this.tabMenuYonetimi.ResumeLayout(false);
            this.tabMenuYonetimi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRezervasyonlar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabMasaYonetimi;
        private System.Windows.Forms.TabPage tabKullaniciYonetimi;
        private System.Windows.Forms.TabPage tabRezervasyonYonetimi;
        private System.Windows.Forms.DataGridView dgvMasalar;
        private System.Windows.Forms.TextBox txtMasaAdi;
        private System.Windows.Forms.TextBox txtKapasite;
        private System.Windows.Forms.TextBox txtKonum;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Label lblMasaAdi;
        private System.Windows.Forms.Label lblKapasite;
        private System.Windows.Forms.Label lblKonum;
        
        // Kullanıcı Yönetimi Kontrolleri
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Button btnBanUser;
        private System.Windows.Forms.Button btnUnbanUser;
        private System.Windows.Forms.Button btnRefreshUsers;
        private System.Windows.Forms.Label lblUserManagement;
        
        // Rezervasyon Yönetimi Kontrolleri
        private System.Windows.Forms.DataGridView dgvRezervasyonlar;
        private System.Windows.Forms.Button btnCancelReservation;
        private System.Windows.Forms.Button btnRefreshReservations;
        private System.Windows.Forms.Button btnRezervasyonDetaylari;
        private System.Windows.Forms.Label lblReservationManagement;
        private System.Windows.Forms.Label lblRezervasyonDetaylari;
        private System.Windows.Forms.ListBox lstRezervasyonMenuleri;
        
        private System.Windows.Forms.Label lblBaslik;
        
        // Menü Yönetimi Kontrolleri
        private System.Windows.Forms.TabPage tabMenuYonetimi;
        private System.Windows.Forms.Button btnMenuYonetimi;
        private System.Windows.Forms.Label lblMenuManagement;
    }
}
