namespace PhoMac.Main.POS.Views.UC
{
    partial class UCTable
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTime = new DevExpress.XtraEditors.LabelControl();
            this.lblTableName = new DevExpress.XtraEditors.LabelControl();
            this.lblServerName = new DevExpress.XtraEditors.LabelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblTime, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTableName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblServerName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(668, 378);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTime
            // 
            this.lblTime.AllowDrop = true;
            this.lblTime.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTime.Appearance.Options.UseFont = true;
            this.lblTime.Appearance.Options.UseTextOptions = true;
            this.lblTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTime.Location = new System.Drawing.Point(3, 3);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(662, 19);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time";
            this.lblTime.Click += new System.EventHandler(this.lblTableName_Click);
            this.lblTime.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblTableName_DragDrop);
            this.lblTime.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblTableName_DragEnter);
            this.lblTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
            // 
            // lblTableName
            // 
            this.lblTableName.AllowHtmlString = true;
            this.lblTableName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTableName.Appearance.Options.UseFont = true;
            this.lblTableName.Appearance.Options.UseTextOptions = true;
            this.lblTableName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTableName.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTableName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTableName.Location = new System.Drawing.Point(3, 28);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(662, 297);
            this.lblTableName.TabIndex = 1;
            this.lblTableName.Text = "Table name<br> abc";
            this.lblTableName.SizeChanged += new System.EventHandler(this.lblTableName_SizeChanged);
            this.lblTableName.Click += new System.EventHandler(this.lblTableName_Click);
            this.lblTableName.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblTableName_DragDrop);
            this.lblTableName.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblTableName_DragEnter);
            
            // 
            // lblServerName
            // 
            this.lblServerName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblServerName.Appearance.Options.UseFont = true;
            this.lblServerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServerName.Location = new System.Drawing.Point(3, 331);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(662, 19);
            this.lblServerName.TabIndex = 2;
            this.lblServerName.Text = "Server Name";
            this.lblServerName.Click += new System.EventHandler(this.lblTableName_Click);
            this.lblServerName.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblTableName_DragDrop);
            this.lblServerName.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblTableName_DragEnter);
            this.lblServerName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
            // 
            // lblStatus
            // 
            this.lblStatus.AllowDrop = true;
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblStatus.Appearance.Options.UseFont = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(3, 356);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(662, 19);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status";
            this.lblStatus.Click += new System.EventHandler(this.lblTableName_Click);
            this.lblStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblTableName_DragDrop);
            this.lblStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblTableName_DragEnter);
            this.lblStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTableName_MouseDown);
            // 
            // UCTable
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCTable";
            this.Size = new System.Drawing.Size(668, 378);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl lblTime;
        private DevExpress.XtraEditors.LabelControl lblTableName;
        private DevExpress.XtraEditors.LabelControl lblServerName;
        private DevExpress.XtraEditors.LabelControl lblStatus;
    }
}
