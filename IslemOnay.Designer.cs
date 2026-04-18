namespace RestoranRezervasyonSistemi
{
    partial class IslemOnay
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
            this.lblMail = new System.Windows.Forms.Label();
            this.txtOnayKodu = new System.Windows.Forms.TextBox();
            this.btnDogrula = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(35, 23);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(207, 16);
            this.lblMail.TabIndex = 0;
            this.lblMail.Text = "Lütlen Mailinize gelen kodu giriniz:";
            // 
            // txtOnayKodu
            // 
            this.txtOnayKodu.Location = new System.Drawing.Point(38, 60);
            this.txtOnayKodu.Name = "txtOnayKodu";
            this.txtOnayKodu.Size = new System.Drawing.Size(204, 22);
            this.txtOnayKodu.TabIndex = 1;
            // 
            // btnDogrula
            // 
            this.btnDogrula.BackColor = System.Drawing.Color.Green;
            this.btnDogrula.Location = new System.Drawing.Point(38, 103);
            this.btnDogrula.Name = "btnDogrula";
            this.btnDogrula.Size = new System.Drawing.Size(204, 35);
            this.btnDogrula.TabIndex = 2;
            this.btnDogrula.Text = "Doğrula";
            this.btnDogrula.UseVisualStyleBackColor = false;
            this.btnDogrula.Click += new System.EventHandler(this.btnDogrula_Click);
            // 
            // IslemOnay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(274, 178);
            this.Controls.Add(this.btnDogrula);
            this.Controls.Add(this.txtOnayKodu);
            this.Controls.Add(this.lblMail);
            this.Name = "IslemOnay";
            this.Text = "IslemOnay";
            this.Load += new System.EventHandler(this.IslemOnay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.TextBox txtOnayKodu;
        private System.Windows.Forms.Button btnDogrula;
    }
}