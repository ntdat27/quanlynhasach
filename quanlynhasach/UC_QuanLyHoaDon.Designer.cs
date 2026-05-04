namespace quanlynhasach
{
    partial class UC_QuanLyHoaDon
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpTuNgay = new ReaLTaiizor.Controls.PoisonDateTime();
            this.dtpDenNgay = new ReaLTaiizor.Controls.PoisonDateTime();
            this.btnLocHoaDon = new ReaLTaiizor.Controls.MaterialButton();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            this.materialCard4 = new ReaLTaiizor.Controls.MaterialCard();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimSdt = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.materialCard4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium;
            this.dtpTuNgay.Location = new System.Drawing.Point(17, 54);
            this.dtpTuNgay.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 29);
            this.dtpTuNgay.TabIndex = 2;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium;
            this.dtpDenNgay.Location = new System.Drawing.Point(223, 54);
            this.dtpDenNgay.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 29);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // btnLocHoaDon
            // 
            this.btnLocHoaDon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLocHoaDon.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLocHoaDon.Depth = 0;
            this.btnLocHoaDon.HighEmphasis = true;
            this.btnLocHoaDon.Icon = null;
            this.btnLocHoaDon.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.btnLocHoaDon.Location = new System.Drawing.Point(834, 53);
            this.btnLocHoaDon.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLocHoaDon.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.btnLocHoaDon.Name = "btnLocHoaDon";
            this.btnLocHoaDon.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLocHoaDon.Size = new System.Drawing.Size(64, 36);
            this.btnLocHoaDon.TabIndex = 9;
            this.btnLocHoaDon.Text = "Lọc";
            this.btnLocHoaDon.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLocHoaDon.UseAccentColor = false;
            this.btnLocHoaDon.UseVisualStyleBackColor = true;
            this.btnLocHoaDon.Click += new System.EventHandler(this.btnLocHoaDon_Click);
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvHoaDon.Location = new System.Drawing.Point(0, 278);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.Size = new System.Drawing.Size(1000, 422);
            this.dgvHoaDon.TabIndex = 10;
            this.dgvHoaDon.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoaDon_CellDoubleClick);
            // 
            // bigLabel1
            // 
            this.bigLabel1.AutoSize = true;
            this.bigLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel1.Font = new System.Drawing.Font("Segoe UI", 25F);
            this.bigLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bigLabel1.Location = new System.Drawing.Point(3, 0);
            this.bigLabel1.Name = "bigLabel1";
            this.bigLabel1.Size = new System.Drawing.Size(282, 46);
            this.bigLabel1.TabIndex = 15;
            this.bigLabel1.Text = "Quản Lý Hóa Đơn";
            // 
            // materialCard4
            // 
            this.materialCard4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard4.Controls.Add(this.txtTimSdt);
            this.materialCard4.Controls.Add(this.label2);
            this.materialCard4.Controls.Add(this.label1);
            this.materialCard4.Controls.Add(this.dtpDenNgay);
            this.materialCard4.Controls.Add(this.dtpTuNgay);
            this.materialCard4.Controls.Add(this.btnLocHoaDon);
            this.materialCard4.Depth = 0;
            this.materialCard4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.materialCard4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard4.Location = new System.Drawing.Point(14, 89);
            this.materialCard4.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialCard4.Name = "materialCard4";
            this.materialCard4.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard4.Size = new System.Drawing.Size(925, 100);
            this.materialCard4.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "Từ Ngày";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(219, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Đến Ngày";
            // 
            // txtTimSdt
            // 
            this.txtTimSdt.AnimateReadOnly = false;
            this.txtTimSdt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtTimSdt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtTimSdt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtTimSdt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtTimSdt.Depth = 0;
            this.txtTimSdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTimSdt.HideSelection = true;
            this.txtTimSdt.LeadingIcon = null;
            this.txtTimSdt.Location = new System.Drawing.Point(429, 35);
            this.txtTimSdt.MaxLength = 32767;
            this.txtTimSdt.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.txtTimSdt.Name = "txtTimSdt";
            this.txtTimSdt.PasswordChar = '\0';
            this.txtTimSdt.PrefixSuffixText = null;
            this.txtTimSdt.ReadOnly = false;
            this.txtTimSdt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTimSdt.SelectedText = "";
            this.txtTimSdt.SelectionLength = 0;
            this.txtTimSdt.SelectionStart = 0;
            this.txtTimSdt.ShortcutsEnabled = true;
            this.txtTimSdt.Size = new System.Drawing.Size(250, 48);
            this.txtTimSdt.TabIndex = 12;
            this.txtTimSdt.TabStop = false;
            this.txtTimSdt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTimSdt.TrailingIcon = null;
            this.txtTimSdt.UseSystemPasswordChar = false;
            // 
            // UC_QuanLyHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialCard4);
            this.Controls.Add(this.bigLabel1);
            this.Controls.Add(this.dgvHoaDon);
            this.Name = "UC_QuanLyHoaDon";
            this.Size = new System.Drawing.Size(1000, 700);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.materialCard4.ResumeLayout(false);
            this.materialCard4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.PoisonDateTime dtpTuNgay;
        private ReaLTaiizor.Controls.PoisonDateTime dtpDenNgay;
        private ReaLTaiizor.Controls.MaterialButton btnLocHoaDon;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.MaterialCard materialCard4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit txtTimSdt;
    }
}
