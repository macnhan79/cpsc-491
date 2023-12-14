namespace PhoHa7.Library.Froms.MsgBox
{
    partial class FrmError
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnWrite = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnDetail = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ptbIcon = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMsg = new System.Windows.Forms.LinkLabel();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.rtxtDetail = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbIcon)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnEmail);
            this.panel2.Controls.Add(this.btnWrite);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnDetail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(310, 35);
            this.panel2.TabIndex = 5;
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(104, 7);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(49, 23);
            this.btnEmail.TabIndex = 5;
            this.btnEmail.Text = "&Email";

            this.btnEmail.Visible = false;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(54, 7);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(49, 23);
            this.btnWrite.TabIndex = 4;
            this.btnWrite.Text = "Gh&i";

            this.btnWrite.Visible = false;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 2);
            this.label1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(257, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(49, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đó&ng";

            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(4, 7);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(49, 23);
            this.btnDetail.TabIndex = 2;
            this.btnDetail.Text = "&Chi tiết";

            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ptbIcon);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(46, 80);
            this.panel3.TabIndex = 7;
            // 
            // ptbIcon
            // 
            this.ptbIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ptbIcon.Image = global::PhoHa7.Library.Properties.Resources.Loi;
            this.ptbIcon.Location = new System.Drawing.Point(11, 25);
            this.ptbIcon.Name = "ptbIcon";
            this.ptbIcon.Size = new System.Drawing.Size(34, 33);
            this.ptbIcon.TabIndex = 0;
            this.ptbIcon.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(46, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 80);
            this.panel1.TabIndex = 6;
            // 
            // txtMsg
            // 
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.txtMsg.Location = new System.Drawing.Point(0, 0);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(264, 80);
            this.txtMsg.TabIndex = 1;
            this.txtMsg.Text = "linkLabel1";
            this.txtMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMsg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.txtMsg_LinkClicked);
            // 
            // pnlDetail
            // 
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDetail.Controls.Add(this.rtxtDetail);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetail.Location = new System.Drawing.Point(0, 115);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(310, 126);
            this.pnlDetail.TabIndex = 8;
            this.pnlDetail.Visible = false;
            // 
            // rtxtDetail
            // 
            this.rtxtDetail.BackColor = System.Drawing.SystemColors.Control;
            this.rtxtDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtDetail.Location = new System.Drawing.Point(0, 0);
            this.rtxtDetail.Name = "rtxtDetail";
            this.rtxtDetail.Size = new System.Drawing.Size(306, 122);
            this.rtxtDetail.TabIndex = 10;
            this.rtxtDetail.Text = "";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(310, 115);
            this.panel5.TabIndex = 9;
            // 
            // FrmError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 241);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmError";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lỗi";
            this.Load += new System.EventHandler(this.FrmError_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbIcon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnDetail;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.PictureBox ptbIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnWrite;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RichTextBox rtxtDetail;
        private DevExpress.XtraEditors.SimpleButton btnEmail;
        private System.Windows.Forms.LinkLabel txtMsg;
    }
}