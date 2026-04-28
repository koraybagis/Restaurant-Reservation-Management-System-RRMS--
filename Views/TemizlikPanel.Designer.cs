namespace RestoranRezervasyonSistemi.Views
{
    partial class TemizlikPanel
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
            this.lblUser = new System.Windows.Forms.Label();
            this.btnMasaTemizle = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.btnYenile = new System.Windows.Forms.Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).BeginInit();
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
            // lblBaslik
            // 
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblBaslik.Location = new System.Drawing.Point(300, 80);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(200, 31);
            this.lblBaslik.TabIndex = 2;
            this.lblBaslik.Text = "🧹 Temizlik Paneli";
            // 
            // dgvMasalar
            // 
            this.dgvMasalar.AllowUserToAddRows = false;
            this.dgvMasalar.AllowUserToDeleteRows = false;
            this.dgvMasalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMasalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMasalar.Location = new System.Drawing.Point(50, 140);
            this.dgvMasalar.Name = "dgvMasalar";
            this.dgvMasalar.ReadOnly = true;
            this.dgvMasalar.RowHeadersVisible = false;
            this.dgvMasalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMasalar.Size = new System.Drawing.Size(700, 300);
            this.dgvMasalar.TabIndex = 3;
            // 
            // btnMasaTemizle
            // 
            this.btnMasaTemizle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnMasaTemizle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMasaTemizle.ForeColor = System.Drawing.Color.White;
            this.btnMasaTemizle.Location = new System.Drawing.Point(250, 470);
            this.btnMasaTemizle.Name = "btnMasaTemizle";
            this.btnMasaTemizle.Size = new System.Drawing.Size(180, 50);
            this.btnMasaTemizle.TabIndex = 4;
            this.btnMasaTemizle.Text = "✨ Masa Temizle";
            this.btnMasaTemizle.UseVisualStyleBackColor = false;
            this.btnMasaTemizle.Click += new System.EventHandler(this.btnMasaTemizle_Click);
            // 
            // btnYenile
            // 
            this.btnYenile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnYenile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYenile.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Location = new System.Drawing.Point(450, 470);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(150, 50);
            this.btnYenile.TabIndex = 5;
            this.btnYenile.Text = "🔄 Yenile";
            this.btnYenile.UseVisualStyleBackColor = false;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            // 
            // TemizlikPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.btnMasaTemizle);
            this.Controls.Add(this.dgvMasalar);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.lblUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TemizlikPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Temizlik Paneli";
            this.Load += new System.EventHandler(this.TemizlikPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMasalar;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnMasaTemizle;
        private System.Windows.Forms.Button btnCikis;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Label lblBaslik;
    }
}
