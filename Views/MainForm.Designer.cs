namespace RestoranRezervasyonSistemi.Views
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPersonelGiris = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnUserManagement = new System.Windows.Forms.Button();
            this.btnTables = new System.Windows.Forms.Button();
            this.btnRezervations = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.flpMasalar = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.panel1.Controls.Add(this.btnPersonelGiris);
            this.panel1.Controls.Add(this.btnSettings);
            this.panel1.Controls.Add(this.btnUserManagement);
            this.panel1.Controls.Add(this.btnTables);
            this.panel1.Controls.Add(this.btnRezervations);
            this.panel1.Controls.Add(this.btnDashboard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 450);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnPersonelGiris
            // 
            this.btnPersonelGiris.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPersonelGiris.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPersonelGiris.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPersonelGiris.FlatAppearance.BorderSize = 0;
            this.btnPersonelGiris.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPersonelGiris.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPersonelGiris.ForeColor = System.Drawing.Color.White;
            this.btnPersonelGiris.Location = new System.Drawing.Point(0, 270);
            this.btnPersonelGiris.Name = "btnPersonelGiris";
            this.btnPersonelGiris.Size = new System.Drawing.Size(250, 60);
            this.btnPersonelGiris.TabIndex = 5;
            this.btnPersonelGiris.Text = "    👤 Personel Girişi";
            this.btnPersonelGiris.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPersonelGiris.UseVisualStyleBackColor = false;
            // 
            // btnSettings
            // 
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(0, 210);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(250, 60);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "    Ayarlar";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnUserManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUserManagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUserManagement.FlatAppearance.BorderSize = 0;
            this.btnUserManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserManagement.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUserManagement.ForeColor = System.Drawing.Color.White;
            this.btnUserManagement.Location = new System.Drawing.Point(0, 150);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Size = new System.Drawing.Size(250, 60);
            this.btnUserManagement.TabIndex = 3;
            this.btnUserManagement.Text = "    Kullanici Yönetimi";
            this.btnUserManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserManagement.UseVisualStyleBackColor = false;
            // 
            // btnTables
            // 
            this.btnTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnTables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTables.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTables.FlatAppearance.BorderSize = 0;
            this.btnTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTables.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTables.ForeColor = System.Drawing.Color.White;
            this.btnTables.Location = new System.Drawing.Point(0, 120);
            this.btnTables.Name = "btnTables";
            this.btnTables.Size = new System.Drawing.Size(250, 60);
            this.btnTables.TabIndex = 2;
            this.btnTables.Text = "    Masa Planı";
            this.btnTables.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTables.UseVisualStyleBackColor = false;
            // 
            // btnRezervations
            // 
            this.btnRezervations.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRezervations.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRezervations.FlatAppearance.BorderSize = 0;
            this.btnRezervations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRezervations.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRezervations.ForeColor = System.Drawing.Color.White;
            this.btnRezervations.Location = new System.Drawing.Point(0, 60);
            this.btnRezervations.Name = "btnRezervations";
            this.btnRezervations.Size = new System.Drawing.Size(250, 60);
            this.btnRezervations.TabIndex = 1;
            this.btnRezervations.Text = "    Rezervasyonlar";
            this.btnRezervations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRezervations.UseVisualStyleBackColor = true;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(250, 60);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "    Ana Sayfa";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(250, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 60);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(157, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "MASA YERLEŞİM PLANI";
            // 
            // flpMasalar
            // 
            this.flpMasalar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.flpMasalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMasalar.Location = new System.Drawing.Point(250, 60);
            this.flpMasalar.Name = "flpMasalar";
            this.flpMasalar.Size = new System.Drawing.Size(550, 390);
            this.flpMasalar.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flpMasalar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Restoran Rezervasyon Sistemi - Ana Panel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPersonelGiris;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnUserManagement;
        private System.Windows.Forms.Button btnTables;
        private System.Windows.Forms.Button btnRezervations;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpMasalar;
        private System.Windows.Forms.Timer timer1;
    }
}