namespace DuAnDauDoi
{
    partial class FormChuyenBan
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
            this.lbBAN = new System.Windows.Forms.Label();
            this.lbBAN2 = new System.Windows.Forms.Label();
            this.btnXacnhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbBAN
            // 
            this.lbBAN.AutoSize = true;
            this.lbBAN.BackColor = System.Drawing.Color.Transparent;
            this.lbBAN.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lbBAN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lbBAN.Image = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
            this.lbBAN.Location = new System.Drawing.Point(26, 23);
            this.lbBAN.Name = "lbBAN";
            this.lbBAN.Size = new System.Drawing.Size(197, 60);
            this.lbBAN.TabIndex = 1;
            this.lbBAN.Text = "┬─┬ Bàn:";
            // 
            // lbBAN2
            // 
            this.lbBAN2.AutoSize = true;
            this.lbBAN2.BackColor = System.Drawing.Color.Transparent;
            this.lbBAN2.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lbBAN2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lbBAN2.Image = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
            this.lbBAN2.Location = new System.Drawing.Point(326, 23);
            this.lbBAN2.Name = "lbBAN2";
            this.lbBAN2.Size = new System.Drawing.Size(197, 60);
            this.lbBAN2.TabIndex = 2;
            this.lbBAN2.Text = "┬─┬ Bàn:";
            // 
            // btnXacnhan
            // 
            this.btnXacnhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnXacnhan.BackgroundImage = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
            this.btnXacnhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacnhan.FlatAppearance.BorderSize = 0;
            this.btnXacnhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacnhan.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnXacnhan.ForeColor = System.Drawing.Color.Black;
            this.btnXacnhan.Location = new System.Drawing.Point(23, 169);
            this.btnXacnhan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXacnhan.Name = "btnXacnhan";
            this.btnXacnhan.Size = new System.Drawing.Size(200, 70);
            this.btnXacnhan.TabIndex = 6;
            this.btnXacnhan.Text = "✓ Xác Nhận";
            this.btnXacnhan.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnHuy.BackgroundImage = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.Black;
            this.btnHuy.Location = new System.Drawing.Point(374, 169);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(149, 70);
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "✕ Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            // 
            // FormChuyenBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DuAnDauDoi.Properties.Resources.download;
            this.ClientSize = new System.Drawing.Size(571, 272);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacnhan);
            this.Controls.Add(this.lbBAN2);
            this.Controls.Add(this.lbBAN);
            this.Name = "FormChuyenBan";
            this.Text = "FormChuyenBan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbBAN;
        private System.Windows.Forms.Label lbBAN2;
        private System.Windows.Forms.Button btnXacnhan;
        private System.Windows.Forms.Button btnHuy;
    }
}