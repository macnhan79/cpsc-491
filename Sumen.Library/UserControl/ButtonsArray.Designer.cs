namespace PhoHa7.Library.UserControl
{
    partial class ButtonsArray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonsArray));
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnPrintBarcode = new DevExpress.XtraEditors.SimpleButton();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnWord = new DevExpress.XtraEditors.SimpleButton();
            this.btnInan = new DevExpress.XtraEditors.SimpleButton();
            this.btnXem = new DevExpress.XtraEditors.SimpleButton();
            this.btnLap = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnBoqua = new DevExpress.XtraEditors.SimpleButton();
            this.btnChon = new DevExpress.XtraEditors.SimpleButton();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.grpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtons
            // 
            this.grpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpButtons.BackColor = System.Drawing.Color.Transparent;
            this.grpButtons.Controls.Add(this.btnPrintBarcode);
            this.grpButtons.Controls.Add(this.btnReset);
            this.grpButtons.Controls.Add(this.btnThoat);
            this.grpButtons.Controls.Add(this.btnExcel);
            this.grpButtons.Controls.Add(this.btnWord);
            this.grpButtons.Controls.Add(this.btnInan);
            this.grpButtons.Controls.Add(this.btnXem);
            this.grpButtons.Controls.Add(this.btnLap);
            this.grpButtons.Controls.Add(this.btnXoa);
            this.grpButtons.Controls.Add(this.btnSua);
            this.grpButtons.Controls.Add(this.btnThem);
            this.grpButtons.Controls.Add(this.btnLuu);
            this.grpButtons.Controls.Add(this.btnBoqua);
            this.grpButtons.Controls.Add(this.btnChon);
            this.grpButtons.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.grpButtons.Location = new System.Drawing.Point(6, 1);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(916, 47);
            this.grpButtons.TabIndex = 1;
            this.grpButtons.TabStop = false;
            // 
            // btnPrintBarcode
            // 
            this.btnPrintBarcode.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintBarcode.Image")));
            this.btnPrintBarcode.Location = new System.Drawing.Point(533, 16);
            this.btnPrintBarcode.Name = "btnPrintBarcode";
            this.btnPrintBarcode.Size = new System.Drawing.Size(75, 22);
            this.btnPrintBarcode.TabIndex = 13;
            this.btnPrintBarcode.Text = "Mã vạch";
            this.btnPrintBarcode.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnPrintBarcode.Click += new System.EventHandler(this.btnPrintBarcode_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(758, 16);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 22);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Đặt lại";
            this.btnReset.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.CausesValidation = false;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.ImageIndex = 5;
            this.btnThoat.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnThoat.Location = new System.Drawing.Point(833, 16);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 22);
            this.btnThoat.TabIndex = 11;
            this.btnThoat.Text = "Th&oát";
            this.btnThoat.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageIndex = 2;
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(683, 16);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 22);
            this.btnExcel.TabIndex = 10;
            this.btnExcel.Text = "&Excel";
            this.btnExcel.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnWord
            // 
            this.btnWord.Image = ((System.Drawing.Image)(resources.GetObject("btnWord.Image")));
            this.btnWord.ImageIndex = 6;
            this.btnWord.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnWord.Location = new System.Drawing.Point(608, 16);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(75, 22);
            this.btnWord.TabIndex = 9;
            this.btnWord.Text = "&Word";
            this.btnWord.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnWord.Click += new System.EventHandler(this.btnWord_Click);
            // 
            // btnInan
            // 
            this.btnInan.Image = ((System.Drawing.Image)(resources.GetObject("btnInan.Image")));
            this.btnInan.ImageIndex = 7;
            this.btnInan.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnInan.Location = new System.Drawing.Point(458, 16);
            this.btnInan.Name = "btnInan";
            this.btnInan.Size = new System.Drawing.Size(75, 22);
            this.btnInan.TabIndex = 8;
            this.btnInan.Text = "&In ấn";
            this.btnInan.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnInan.Click += new System.EventHandler(this.btnInan_Click);
            // 
            // btnXem
            // 
            this.btnXem.Image = ((System.Drawing.Image)(resources.GetObject("btnXem.Image")));
            this.btnXem.ImageIndex = 9;
            this.btnXem.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnXem.Location = new System.Drawing.Point(383, 16);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(75, 22);
            this.btnXem.TabIndex = 7;
            this.btnXem.Text = "&Xem";
            this.btnXem.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnLap
            // 
            this.btnLap.Image = ((System.Drawing.Image)(resources.GetObject("btnLap.Image")));
            this.btnLap.ImageIndex = 8;
            this.btnLap.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnLap.Location = new System.Drawing.Point(308, 16);
            this.btnLap.Name = "btnLap";
            this.btnLap.Size = new System.Drawing.Size(75, 22);
            this.btnLap.TabIndex = 6;
            this.btnLap.Text = "&Lập";
            this.btnLap.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnLap.Click += new System.EventHandler(this.btnLap_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.ImageIndex = 3;
            this.btnXoa.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnXoa.Location = new System.Drawing.Point(158, 16);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 22);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "&Xóa";
            this.btnXoa.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.Image")));
            this.btnSua.ImageIndex = 4;
            this.btnSua.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSua.Location = new System.Drawing.Point(83, 16);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 22);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "&Sửa";
            this.btnSua.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.Image")));
            this.btnThem.ImageIndex = 0;
            this.btnThem.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnThem.Location = new System.Drawing.Point(8, 16);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 22);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "&Thêm";
            this.btnThem.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.ImageIndex = 10;
            this.btnLuu.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnLuu.Location = new System.Drawing.Point(8, 16);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 22);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "&Lưu";
            this.btnLuu.Visible = false;
            this.btnLuu.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnBoqua
            // 
            this.btnBoqua.Image = ((System.Drawing.Image)(resources.GetObject("btnBoqua.Image")));
            this.btnBoqua.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBoqua.Location = new System.Drawing.Point(83, 16);
            this.btnBoqua.Name = "btnBoqua";
            this.btnBoqua.Size = new System.Drawing.Size(75, 22);
            this.btnBoqua.TabIndex = 4;
            this.btnBoqua.Text = "&Bỏ qua";
            this.btnBoqua.Visible = false;
            this.btnBoqua.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnBoqua.Click += new System.EventHandler(this.btnBoqua_Click);
            // 
            // btnChon
            // 
            this.btnChon.Image = ((System.Drawing.Image)(resources.GetObject("btnChon.Image")));
            this.btnChon.ImageIndex = 1;
            this.btnChon.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnChon.Location = new System.Drawing.Point(233, 16);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(75, 22);
            this.btnChon.TabIndex = 5;
            this.btnChon.Text = "&Chọn";
            this.btnChon.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "add1.png");
            this.imageList1.Images.SetKeyName(1, "agt_action_success.png");
            this.imageList1.Images.SetKeyName(2, "application_vnd_ms_excel.png 16 16.png");
            this.imageList1.Images.SetKeyName(3, "deletered.png");
            this.imageList1.Images.SetKeyName(4, "edit.png");
            this.imageList1.Images.SetKeyName(5, "exit 16 16.png");
            this.imageList1.Images.SetKeyName(6, "page_white_word.png");
            this.imageList1.Images.SetKeyName(7, "printer_blue 16 16.png");
            this.imageList1.Images.SetKeyName(8, "timespan.png");
            this.imageList1.Images.SetKeyName(9, "view.png");
            this.imageList1.Images.SetKeyName(10, "3floppy_unmount 16 16.png");
            // 
            // ButtonsArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.grpButtons);
            this.Name = "ButtonsArray";
            this.Size = new System.Drawing.Size(932, 56);
            this.Resize += new System.EventHandler(this.ButtonsArray_Resize);
            this.grpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnThoat;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnWord;
        public DevExpress.XtraEditors.SimpleButton btnInan;
        public DevExpress.XtraEditors.SimpleButton btnXem;
        public DevExpress.XtraEditors.SimpleButton btnLap;
        public DevExpress.XtraEditors.SimpleButton btnXoa;
        public DevExpress.XtraEditors.SimpleButton btnSua;
        public System.Windows.Forms.GroupBox grpButtons;
        public DevExpress.XtraEditors.SimpleButton btnThem;
        public DevExpress.XtraEditors.SimpleButton btnLuu;
        public DevExpress.XtraEditors.SimpleButton btnBoqua;
        public DevExpress.XtraEditors.SimpleButton btnChon;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private DevExpress.XtraEditors.SimpleButton btnPrintBarcode;
    }
}
