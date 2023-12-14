namespace PhoHa7.Library.UserControl.DanhMuc
{
    partial class UCDanhMuc
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
            this.dgDanhMuc = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnArray = new Library.UserControl.ButtonsArray();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelColDesign = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutButtonArray = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutButtonArray)).BeginInit();
            this.SuspendLayout();
            // 
            // dgDanhMuc
            // 
            this.dgDanhMuc.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.dgDanhMuc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.dgDanhMuc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.dgDanhMuc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.dgDanhMuc.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.dgDanhMuc.EmbeddedNavigator.TextStringFormat = "Dòng {0} / {1}";
            this.dgDanhMuc.Location = new System.Drawing.Point(12, 12);
            this.dgDanhMuc.MainView = this.gridView;
            this.dgDanhMuc.Name = "dgDanhMuc";
            this.dgDanhMuc.Size = new System.Drawing.Size(577, 250);
            this.dgDanhMuc.TabIndex = 0;
            this.dgDanhMuc.UseEmbeddedNavigator = true;
            this.dgDanhMuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView.ChildGridLevelName = "ChildrenView";
            this.gridView.GridControl = this.dgDanhMuc;
            this.gridView.Name = "gridView";
            this.gridView.NewItemRowText = "Nhấp vào đây để thêm dòng mới";
            this.gridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView.OptionsCustomization.AllowColumnMoving = false;
            this.gridView.OptionsCustomization.AllowGroup = false;
            this.gridView.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.ShownEditor += new System.EventHandler(this.gridView_ShownEditor);
            this.gridView.ColumnChanged += new System.EventHandler(this.gridView_ColumnChanged);
            this.gridView.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gridView_InvalidRowException);
            this.gridView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridView_ValidateRow);
            this.gridView.RowCountChanged += new System.EventHandler(this.gridView_RowCountChanged);
            // 
            // btnArray
            // 
            this.btnArray.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnArray.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnArray.btnBackColor = System.Drawing.Color.Transparent;
            this.btnArray.btnBackgroundImage = null;
            this.btnArray.btnFlatStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnArray.btnForeColor = System.Drawing.Color.Black;
            this.btnArray.btnGroupBoxShow = false;
            this.btnArray.btnSize = new System.Drawing.Size(77, 26);
            this.btnArray.btnSpacing = 3;
            this.btnArray.btnStyle = Library.UserControl.ButtonsArray.btnStyleEnum.Array;
            this.btnArray.btnStyleGroup = Library.UserControl.ButtonsArray.btnStyleGroupEnum.None;
            this.btnArray.ButtonChonVisible = false;
            this.btnArray.ButtonExcelVisible = false;
            this.btnArray.ButtonInanVisible = false;
            this.btnArray.ButtonLapVisible = false;
            this.btnArray.ButtonPrintBarcodeVisible = false;
            this.btnArray.ButtonResetVisible = false;
            this.btnArray.ButtonSuaText = Library.UserControl.ButtonsArray.btnUpdateEnum.Sửa;
            this.btnArray.ButtonSuaVisible = true;
            this.btnArray.ButtonThemVisible = true;
            this.btnArray.ButtonThoatVisible = true;
            this.btnArray.ButtonWordVisible = false;
            this.btnArray.ButtonXemVisible = false;
            this.btnArray.ButtonXoaVisible = true;
            this.btnArray.Location = new System.Drawing.Point(118, 2);
            this.btnArray.Name = "btnArray";
            this.btnArray.Size = new System.Drawing.Size(341, 50);
            this.btnArray.TabIndex = 2;
            this.btnArray.btnEventAdd_click += new System.EventHandler(this.btnArray_btnEventAdd_click);
            this.btnArray.btnEventUpdate_click += new System.EventHandler(this.btnArray_btnEventUpdate_click);
            this.btnArray.btnEventDelete_click += new System.EventHandler(this.btnArray_btnEventDelete_click);
            this.btnArray.btnEventSave_click += new System.EventHandler(this.btnArray_btnEventSave_click);
            this.btnArray.btnEventRevert_click += new System.EventHandler(this.btnArray_btnEventRevert_click);
            this.btnArray.btnEventClose_click += new System.EventHandler(this.btnArray_btnEventClose_click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dgDanhMuc);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(397, 128, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(601, 333);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnArray);
            this.panelControl1.Controls.Add(this.labelColDesign);
            this.panelControl1.Location = new System.Drawing.Point(12, 266);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(577, 55);
            this.panelControl1.TabIndex = 4;
            // 
            // labelColDesign
            // 
            this.labelColDesign.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelColDesign.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelColDesign.Location = new System.Drawing.Point(2, 40);
            this.labelColDesign.Name = "labelColDesign";
            this.labelColDesign.Size = new System.Drawing.Size(573, 13);
            this.labelColDesign.TabIndex = 3;
            this.labelColDesign.Text = "Col Design";
            this.labelColDesign.Visible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutButtonArray});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(601, 333);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dgDanhMuc;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(581, 254);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutButtonArray
            // 
            this.layoutButtonArray.Control = this.panelControl1;
            this.layoutButtonArray.CustomizationFormText = "layoutControlItem2";
            this.layoutButtonArray.Location = new System.Drawing.Point(0, 254);
            this.layoutButtonArray.MaxSize = new System.Drawing.Size(0, 59);
            this.layoutButtonArray.MinSize = new System.Drawing.Size(111, 59);
            this.layoutButtonArray.Name = "layoutButtonArray";
            this.layoutButtonArray.Size = new System.Drawing.Size(581, 59);
            this.layoutButtonArray.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutButtonArray.Text = "layoutControlItem2";
            this.layoutButtonArray.TextSize = new System.Drawing.Size(0, 0);
            this.layoutButtonArray.TextToControlDistance = 0;
            this.layoutButtonArray.TextVisible = false;
            // 
            // UCDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCDanhMuc";
            this.Size = new System.Drawing.Size(601, 333);
            this.Load += new System.EventHandler(this.UCDanhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutButtonArray)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutButtonArray;
        private DevExpress.XtraEditors.LabelControl labelColDesign;
        public DevExpress.XtraGrid.GridControl dgDanhMuc;
        public ButtonsArray btnArray;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView;
    }
}
