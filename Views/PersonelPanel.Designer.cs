namespace RestoranRezervasyonSistemi.Views
{
    partial class PersonelPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvMasalar = new System.Windows.Forms.DataGridView();
            this.dgvMenuItems = new System.Windows.Forms.DataGridView();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnMasaKapat = new System.Windows.Forms.Button();
            this.btnUrunEkle = new System.Windows.Forms.Button();
            this.btnUrunGuncelle = new System.Windows.Forms.Button();
            this.btnUrunSil = new System.Windows.Forms.Button();
            this.btnAdisyonAl = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.btnYenile = new System.Windows.Forms.Button();
            this.lblMasalar = new System.Windows.Forms.Label();
            this.lblMenu = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMasalar = new System.Windows.Forms.TabPage();
            this.tabMenu = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabMasalar.SuspendLayout();
            this.tabMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblUser.Location = new System.Drawing.Point(20, 20);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(200, 24);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "👤 Kullanıcı Adı";
            // 
            // btnCikis
            // 
            this.btnCikis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCikis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCikis.ForeColor = System.Drawing.Color.White;
            this.btnCikis.Location = new System.Drawing.Point(680, 20);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(100, 40);
            this.btnCikis.TabIndex = 1;
            this.btnCikis.Text = "🚪 Çıkış";
            this.btnCikis.UseVisualStyleBackColor = false;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMasalar);
            this.tabControl1.Controls.Add(this.tabMenu);
            this.tabControl1.Location = new System.Drawing.Point(20, 70);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 440);
            this.tabControl1.TabIndex = 2;
            // 
            // tabMasalar
            // 
            this.tabMasalar.Controls.Add(this.btnYenile);
            this.tabMasalar.Controls.Add(this.btnMasaKapat);
            this.tabMasalar.Controls.Add(this.lblMasalar);
            this.tabMasalar.Controls.Add(this.dgvMasalar);
            this.tabMasalar.Controls.Add(this.btnAdisyonAl);
            this.tabMasalar.Location = new System.Drawing.Point(4, 25);
            this.tabMasalar.Name = "tabMasalar";
            this.tabMasalar.Padding = new System.Windows.Forms.Padding(3);
            this.tabMasalar.Size = new System.Drawing.Size(752, 411);
            this.tabMasalar.TabIndex = 0;
            this.tabMasalar.Text = "🪑 Masalar";
            this.tabMasalar.UseVisualStyleBackColor = true;
            // 
            // lblMasalar
            // 
            this.lblMasalar.AutoSize = true;
            this.lblMasalar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMasalar.Location = new System.Drawing.Point(20, 20);
            this.lblMasalar.Name = "lblMasalar";
            this.lblMasalar.Size = new System.Drawing.Size(75, 28);
            this.lblMasalar.TabIndex = 0;
            this.lblMasalar.Text = "Masalar";
            // 
            // dgvMasalar
            // 
            this.dgvMasalar.AllowUserToAddRows = false;
            this.dgvMasalar.AllowUserToDeleteRows = false;
            this.dgvMasalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMasalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMasalar.Location = new System.Drawing.Point(20, 60);
            this.dgvMasalar.Name = "dgvMasalar";
            this.dgvMasalar.ReadOnly = true;
            this.dgvMasalar.RowHeadersVisible = false;
            this.dgvMasalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMasalar.Size = new System.Drawing.Size(710, 250);
            this.dgvMasalar.TabIndex = 1;
            // 
            // btnMasaKapat
            // 
            this.btnMasaKapat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnMasaKapat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMasaKapat.ForeColor = System.Drawing.Color.White;
            this.btnMasaKapat.Location = new System.Drawing.Point(20, 330);
            this.btnMasaKapat.Name = "btnMasaKapat";
            this.btnMasaKapat.Size = new System.Drawing.Size(150, 45);
            this.btnMasaKapat.TabIndex = 2;
            this.btnMasaKapat.Text = "🔒 Masa Kapat";
            this.btnMasaKapat.UseVisualStyleBackColor = false;
            this.btnMasaKapat.Click += new System.EventHandler(this.btnMasaKapat_Click);
            // 
            // btnAdisyonAl
            // 
            this.btnAdisyonAl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAdisyonAl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAdisyonAl.ForeColor = System.Drawing.Color.White;
            this.btnAdisyonAl.Location = new System.Drawing.Point(190, 330);
            this.btnAdisyonAl.Name = "btnAdisyonAl";
            this.btnAdisyonAl.Size = new System.Drawing.Size(150, 45);
            this.btnAdisyonAl.TabIndex = 3;
            this.btnAdisyonAl.Text = "📝 Adisyon Al";
            this.btnAdisyonAl.UseVisualStyleBackColor = false;
            this.btnAdisyonAl.Click += new System.EventHandler(this.btnAdisyonAl_Click);
            // 
            // btnYenile
            // 
            this.btnYenile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnYenile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYenile.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Location = new System.Drawing.Point(580, 330);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(150, 45);
            this.btnYenile.TabIndex = 4;
            this.btnYenile.Text = "🔄 Yenile";
            this.btnYenile.UseVisualStyleBackColor = false;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            // 
            // tabMenu
            // 
            this.tabMenu.Controls.Add(this.btnUrunSil);
            this.tabMenu.Controls.Add(this.btnUrunGuncelle);
            this.tabMenu.Controls.Add(this.btnUrunEkle);
            this.tabMenu.Controls.Add(this.lblMenu);
            this.tabMenu.Controls.Add(this.dgvMenuItems);
            this.tabMenu.Location = new System.Drawing.Point(4, 25);
            this.tabMenu.Name = "tabMenu";
            this.tabMenu.Padding = new System.Windows.Forms.Padding(3);
            this.tabMenu.Size = new System.Drawing.Size(752, 411);
            this.tabMenu.TabIndex = 1;
            this.tabMenu.Text = "🍽️ Menü Yönetimi";
            this.tabMenu.UseVisualStyleBackColor = true;
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMenu.Location = new System.Drawing.Point(20, 20);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(140, 28);
            this.lblMenu.TabIndex = 0;
            this.lblMenu.Text = "Menü Ürünleri";
            // 
            // dgvMenuItems
            // 
            this.dgvMenuItems.AllowUserToAddRows = false;
            this.dgvMenuItems.AllowUserToDeleteRows = false;
            this.dgvMenuItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMenuItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenuItems.Location = new System.Drawing.Point(20, 60);
            this.dgvMenuItems.Name = "dgvMenuItems";
            this.dgvMenuItems.ReadOnly = true;
            this.dgvMenuItems.RowHeadersVisible = false;
            this.dgvMenuItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenuItems.Size = new System.Drawing.Size(710, 250);
            this.dgvMenuItems.TabIndex = 1;
            // 
            // btnUrunEkle
            // 
            this.btnUrunEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnUrunEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUrunEkle.ForeColor = System.Drawing.Color.White;
            this.btnUrunEkle.Location = new System.Drawing.Point(20, 330);
            this.btnUrunEkle.Name = "btnUrunEkle";
            this.btnUrunEkle.Size = new System.Drawing.Size(150, 45);
            this.btnUrunEkle.TabIndex = 2;
            this.btnUrunEkle.Text = "➕ Ürün Ekle";
            this.btnUrunEkle.UseVisualStyleBackColor = false;
            this.btnUrunEkle.Click += new System.EventHandler(this.btnUrunEkle_Click);
            // 
            // btnUrunGuncelle
            // 
            this.btnUrunGuncelle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUrunGuncelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUrunGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnUrunGuncelle.Location = new System.Drawing.Point(190, 330);
            this.btnUrunGuncelle.Name = "btnUrunGuncelle";
            this.btnUrunGuncelle.Size = new System.Drawing.Size(150, 45);
            this.btnUrunGuncelle.TabIndex = 3;
            this.btnUrunGuncelle.Text = "✏️ Ürün Güncelle";
            this.btnUrunGuncelle.UseVisualStyleBackColor = false;
            this.btnUrunGuncelle.Click += new System.EventHandler(this.btnUrunGuncelle_Click);
            // 
            // btnUrunSil
            // 
            this.btnUrunSil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnUrunSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUrunSil.ForeColor = System.Drawing.Color.White;
            this.btnUrunSil.Location = new System.Drawing.Point(360, 330);
            this.btnUrunSil.Name = "btnUrunSil";
            this.btnUrunSil.Size = new System.Drawing.Size(150, 45);
            this.btnUrunSil.TabIndex = 4;
            this.btnUrunSil.Text = "🗑️ Ürün Sil";
            this.btnUrunSil.UseVisualStyleBackColor = false;
            this.btnUrunSil.Click += new System.EventHandler(this.btnUrunSil_Click);
            // 
            // PersonelPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 540);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.lblUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PersonelPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Personel Paneli";
            this.Load += new System.EventHandler(this.PersonelPanel_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabMasalar.ResumeLayout(false);
            this.tabMasalar.PerformLayout();
            this.tabMenu.ResumeLayout(false);
            this.tabMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMasalar;
        private System.Windows.Forms.DataGridView dgvMenuItems;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnMasaKapat;
        private System.Windows.Forms.Button btnUrunEkle;
        private System.Windows.Forms.Button btnUrunGuncelle;
        private System.Windows.Forms.Button btnUrunSil;
        private System.Windows.Forms.Button btnAdisyonAl;
        private System.Windows.Forms.Button btnCikis;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Label lblMasalar;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMasalar;
        private System.Windows.Forms.TabPage tabMenu;
    }
}
