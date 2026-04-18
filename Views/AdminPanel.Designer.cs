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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMasalar
            // 
            this.dgvMasalar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dgvMasalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMasalar.Location = new System.Drawing.Point(20, 20);
            this.dgvMasalar.Name = "dgvMasalar";
            this.dgvMasalar.RowHeadersWidth = 51;
            this.dgvMasalar.RowTemplate.Height = 24;
            this.dgvMasalar.Size = new System.Drawing.Size(500, 400);
            this.dgvMasalar.TabIndex = 0;
            this.dgvMasalar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMasalar_CellClick);
            // 
            // txtMasaAdi
            // 
            this.txtMasaAdi.Location = new System.Drawing.Point(540, 40);
            this.txtMasaAdi.Name = "txtMasaAdi";
            this.txtMasaAdi.Size = new System.Drawing.Size(250, 22);
            this.txtMasaAdi.TabIndex = 1;
            // 
            // txtKapasite
            // 
            this.txtKapasite.Location = new System.Drawing.Point(540, 100);
            this.txtKapasite.Name = "txtKapasite";
            this.txtKapasite.Size = new System.Drawing.Size(250, 22);
            this.txtKapasite.TabIndex = 2;
            // 
            // txtKonum
            // 
            this.txtKonum.Location = new System.Drawing.Point(540, 160);
            this.txtKonum.Name = "txtKonum";
            this.txtKonum.Size = new System.Drawing.Size(250, 22);
            this.txtKonum.TabIndex = 3;
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.Green;
            this.btnEkle.Location = new System.Drawing.Point(540, 210);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(250, 40);
            this.btnEkle.TabIndex = 4;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = false;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnGuncelle.Location = new System.Drawing.Point(540, 260);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(250, 40);
            this.btnGuncelle.TabIndex = 5;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = false;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.Color.Red;
            this.btnSil.Location = new System.Drawing.Point(540, 310);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(250, 40);
            this.btnSil.TabIndex = 6;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // lblMasaAdi
            // 
            this.lblMasaAdi.AutoSize = true;
            this.lblMasaAdi.Location = new System.Drawing.Point(540, 20);
            this.lblMasaAdi.Name = "lblMasaAdi";
            this.lblMasaAdi.Size = new System.Drawing.Size(64, 16);
            this.lblMasaAdi.TabIndex = 7;
            this.lblMasaAdi.Text = "Masa Adı";
            // 
            // lblKapasite
            // 
            this.lblKapasite.AutoSize = true;
            this.lblKapasite.Location = new System.Drawing.Point(540, 80);
            this.lblKapasite.Name = "lblKapasite";
            this.lblKapasite.Size = new System.Drawing.Size(60, 16);
            this.lblKapasite.TabIndex = 8;
            this.lblKapasite.Text = "Kapasite";
            // 
            // lblKonum
            // 
            this.lblKonum.AutoSize = true;
            this.lblKonum.Location = new System.Drawing.Point(540, 140);
            this.lblKonum.Name = "lblKonum";
            this.lblKonum.Size = new System.Drawing.Size(92, 16);
            this.lblKonum.TabIndex = 9;
            this.lblKonum.Text = "Masa Konumu";
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(832, 453);
            this.Controls.Add(this.lblKonum);
            this.Controls.Add(this.lblKapasite);
            this.Controls.Add(this.lblMasaAdi);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.txtKonum);
            this.Controls.Add(this.txtKapasite);
            this.Controls.Add(this.txtMasaAdi);
            this.Controls.Add(this.dgvMasalar);
            this.Name = "AdminPanel";
            this.Text = "Restoran Yönetim Paneli - Masa İşlemleri";
            this.Load += new System.EventHandler(this.AdminPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
    }
}