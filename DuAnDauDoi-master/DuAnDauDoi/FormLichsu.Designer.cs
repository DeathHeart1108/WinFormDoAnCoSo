namespace DuAnDauDoi
{
    partial class FormLichsu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHD = new System.Windows.Forms.Button();
            this.btnChotCa = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChotCaNgay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(18, 80);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1128, 411);
            this.dataGridView1.TabIndex = 0;
            // 
            // txtFind
            // 
            this.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtFind.Location = new System.Drawing.Point(503, 24);
            this.txtFind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(643, 39);
            this.txtFind.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label1.Image = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
            this.label1.Location = new System.Drawing.Point(295, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "🔍 Tìm kiếm";
            // 
            // btnHD
            // 
            this.btnHD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnHD.BackgroundImage = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
            this.btnHD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHD.FlatAppearance.BorderSize = 0;
            this.btnHD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHD.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnHD.ForeColor = System.Drawing.Color.Black;
            this.btnHD.Location = new System.Drawing.Point(18, 11);
            this.btnHD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHD.Name = "btnHD";
            this.btnHD.Size = new System.Drawing.Size(258, 56);
            this.btnHD.TabIndex = 3;
            this.btnHD.Text = "📄 Hóa Đơn";
            this.btnHD.UseVisualStyleBackColor = false;
            // 
            // btnChotCa
            // 
            this.btnChotCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnChotCa.BackgroundImage = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
            this.btnChotCa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChotCa.FlatAppearance.BorderSize = 0;
            this.btnChotCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChotCa.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnChotCa.ForeColor = System.Drawing.Color.Black;
            this.btnChotCa.Location = new System.Drawing.Point(426, 497);
            this.btnChotCa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChotCa.Name = "btnChotCa";
            this.btnChotCa.Size = new System.Drawing.Size(369, 48);
            this.btnChotCa.TabIndex = 4;
            this.btnChotCa.Text = "💰 Chốt Ca Hôm Nay";
            this.btnChotCa.UseVisualStyleBackColor = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(222, 504);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(178, 39);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label2.Location = new System.Drawing.Point(18, 508);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "📅 Chọn ngày:";
            // 
            // btnChotCaNgay
            // 
            this.btnChotCaNgay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnChotCaNgay.BackgroundImage = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
            this.btnChotCaNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChotCaNgay.FlatAppearance.BorderSize = 0;
            this.btnChotCaNgay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChotCaNgay.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnChotCaNgay.ForeColor = System.Drawing.Color.Black;
            this.btnChotCaNgay.Location = new System.Drawing.Point(810, 497);
            this.btnChotCaNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChotCaNgay.Name = "btnChotCaNgay";
            this.btnChotCaNgay.Size = new System.Drawing.Size(336, 48);
            this.btnChotCaNgay.TabIndex = 7;
            this.btnChotCaNgay.Text = "📊 Chốt Ca Theo Ngày";
            this.btnChotCaNgay.UseVisualStyleBackColor = false;
            // 
            // FormLichsu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(198)))), ((int)(((byte)(207)))));
            this.BackgroundImage = global::DuAnDauDoi.Properties.Resources.download;
            this.ClientSize = new System.Drawing.Size(1158, 552);
            this.Controls.Add(this.btnChotCaNgay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnChotCa);
            this.Controls.Add(this.btnHD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormLichsu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📋 Lịch Sử Giao Dịch";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

                        private System.Windows.Forms.DataGridView dataGridView1;
                        private System.Windows.Forms.TextBox txtFind;
                        private System.Windows.Forms.Label label1;
                        private System.Windows.Forms.Button btnHD;
                        private System.Windows.Forms.Button btnChotCa;
                        private System.Windows.Forms.DateTimePicker dateTimePicker1;
                        private System.Windows.Forms.Label label2;
                        private System.Windows.Forms.Button btnChotCaNgay;
                    }
                }