namespace PhoMac.Main.POS.Views.UC
{
    partial class UCProduct
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
            this.lblImage = new System.Windows.Forms.PictureBox();
            this.lblBarcode = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.panelNameAndBarcode = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelNameAndBarcode)).BeginInit();
            this.panelNameAndBarcode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.25F));
            this.tableLayoutPanel1.Controls.Add(this.panelNameAndBarcode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblImage, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(996, 573);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblImage
            // 
            this.lblImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImage.Location = new System.Drawing.Point(3, 3);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(330, 423);
            this.lblImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.lblImage.TabIndex = 1;
            this.lblImage.TabStop = false;
            // 
            // lblBarcode
            // 
            this.lblBarcode.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.Appearance.Options.UseFont = true;
            this.lblBarcode.Appearance.Options.UseTextOptions = true;
            this.lblBarcode.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblBarcode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBarcode.Location = new System.Drawing.Point(0, 0);
            this.lblBarcode.Margin = new System.Windows.Forms.Padding(0);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(654, 39);
            this.lblBarcode.TabIndex = 0;
            this.lblBarcode.Text = "Barcode";
            // 
            // lblName
            // 
            this.lblName.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblName.Appearance.Options.UseFont = true;
            this.lblName.Appearance.Options.UseTextOptions = true;
            this.lblName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(0, 39);
            this.lblName.Margin = new System.Windows.Forms.Padding(0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(654, 384);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // panelNameAndBarcode
            // 
            this.panelNameAndBarcode.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelNameAndBarcode.Appearance.Options.UseBackColor = true;
            this.panelNameAndBarcode.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelNameAndBarcode.Controls.Add(this.lblName);
            this.panelNameAndBarcode.Controls.Add(this.lblBarcode);
            this.panelNameAndBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNameAndBarcode.Location = new System.Drawing.Point(339, 3);
            this.panelNameAndBarcode.Name = "panelNameAndBarcode";
            this.panelNameAndBarcode.Size = new System.Drawing.Size(654, 423);
            this.panelNameAndBarcode.TabIndex = 0;
            // 
            // UCProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximumSize = new System.Drawing.Size(998, 575);
            this.Name = "UCProduct";
            this.Size = new System.Drawing.Size(996, 573);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelNameAndBarcode)).EndInit();
            this.panelNameAndBarcode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox lblImage;
        private DevExpress.XtraEditors.PanelControl panelNameAndBarcode;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblBarcode;


    }
}
