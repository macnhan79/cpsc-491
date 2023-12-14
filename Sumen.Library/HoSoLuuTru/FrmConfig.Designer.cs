namespace PhoHa7.Library.HoSoLuuTru
{
    partial class FrmConfig
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.tabLocation = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btDefault = new System.Windows.Forms.Button();
            this.btMove = new System.Windows.Forms.Button();
            this.btOpenFolder = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.tabLocation.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLuu);
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 218);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 63);
            this.panel1.TabIndex = 0;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(110, 20);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 9;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(191, 20);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // tabLocation
            // 
            this.tabLocation.Controls.Add(this.panel4);
            this.tabLocation.Location = new System.Drawing.Point(4, 22);
            this.tabLocation.Name = "tabLocation";
            this.tabLocation.Size = new System.Drawing.Size(368, 192);
            this.tabLocation.TabIndex = 2;
            this.tabLocation.Text = "Vị trí lưu";
            this.tabLocation.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btOpenFolder);
            this.panel4.Controls.Add(this.btMove);
            this.panel4.Controls.Add(this.btDefault);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.txtLocation);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(368, 192);
            this.panel4.TabIndex = 0;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(12, 77);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(342, 20);
            this.txtLocation.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(342, 59);
            this.label9.TabIndex = 1;
            this.label9.Text = "Chọn nơi lưu trữ tập tin hình ảnh của Hồ Sơ Lưu Trữ trên ổ cứng của máy tính này," +
    " hoặc máy tính khác cùng mạng";
            // 
            // btDefault
            // 
            this.btDefault.Location = new System.Drawing.Point(12, 103);
            this.btDefault.Name = "btDefault";
            this.btDefault.Size = new System.Drawing.Size(110, 30);
            this.btDefault.TabIndex = 2;
            this.btDefault.Text = "Mặc định";
            this.btDefault.UseVisualStyleBackColor = true;
            this.btDefault.Click += new System.EventHandler(this.btDefault_Click);
            // 
            // btMove
            // 
            this.btMove.Location = new System.Drawing.Point(128, 103);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(110, 30);
            this.btMove.TabIndex = 2;
            this.btMove.Text = "Di chuyển";
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // btOpenFolder
            // 
            this.btOpenFolder.Location = new System.Drawing.Point(244, 103);
            this.btOpenFolder.Name = "btOpenFolder";
            this.btOpenFolder.Size = new System.Drawing.Size(110, 30);
            this.btOpenFolder.TabIndex = 2;
            this.btOpenFolder.Text = "Mở";
            this.btOpenFolder.UseVisualStyleBackColor = true;
            this.btOpenFolder.Click += new System.EventHandler(this.btOpenFolder_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLocation);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(376, 218);
            this.tabControl1.TabIndex = 1;
            // 
            // FrmConfig
            // 
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 281);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tùy Chỉnh";
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.panel1.ResumeLayout(false);
            this.tabLocation.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnThoat;

        private System.Windows.Forms.DataGridViewTextBoxColumn lHSIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lHSMADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lHSTENDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabPage tabLocation;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btOpenFolder;
        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Button btDefault;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TabControl tabControl1;

    }
}