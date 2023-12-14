namespace Library.UserControl
{
    partial class Address
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
            this.popupContainerControlAddress = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeListAddress = new DevExpress.XtraTreeList.TreeList();
            this.DC_TEN = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_TEN_XA = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_MA = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_TINH = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_QUAN = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_ID_HUYEN = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_MA_HUYEN = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_TEN_HUYEN = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_ID_TINH = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_MA_TINH = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.DC_TEN_TINH = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.popupContainerEditAddress = new DevExpress.XtraEditors.PopupContainerEdit();
            this.dgTinhThanh = new DevExpress.XtraGrid.GridControl();
            this.gvTinhThanh = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DC_TEN_ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CHA_DC_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlAddress)).BeginInit();
            this.popupContainerControlAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTinhThanh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTinhThanh)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControlAddress
            // 
            this.popupContainerControlAddress.Controls.Add(this.treeListAddress);
            this.popupContainerControlAddress.Location = new System.Drawing.Point(4, 30);
            this.popupContainerControlAddress.Name = "popupContainerControlAddress";
            this.popupContainerControlAddress.Size = new System.Drawing.Size(254, 191);
            this.popupContainerControlAddress.TabIndex = 0;
            // 
            // treeListAddress
            // 
            this.treeListAddress.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.DC_TEN,
            this.ID,
            this.DC_TEN_XA,
            this.DC_MA,
            this.DC_TINH,
            this.DC_QUAN,
            this.DC_ID_HUYEN,
            this.DC_MA_HUYEN,
            this.DC_TEN_HUYEN,
            this.DC_ID_TINH,
            this.DC_MA_TINH,
            this.DC_TEN_TINH});
            this.treeListAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListAddress.Location = new System.Drawing.Point(0, 0);
            this.treeListAddress.Name = "treeListAddress";
            this.treeListAddress.OptionsBehavior.Editable = false;
            this.treeListAddress.Size = new System.Drawing.Size(254, 191);
            this.treeListAddress.TabIndex = 0;
            this.treeListAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeListAddress_KeyDown);
            this.treeListAddress.DoubleClick += new System.EventHandler(this.treeListAddress_DoubleClick);
            // 
            // DC_TEN
            // 
            this.DC_TEN.Caption = "Tỉnh/Thành phố";
            this.DC_TEN.FieldName = "DC_TEN";
            this.DC_TEN.Name = "DC_TEN";
            this.DC_TEN.Visible = true;
            this.DC_TEN.VisibleIndex = 0;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            // 
            // DC_TEN_XA
            // 
            this.DC_TEN_XA.Caption = "DC_TEN";
            this.DC_TEN_XA.FieldName = "DC_TEN";
            this.DC_TEN_XA.Name = "DC_TEN_XA";
            // 
            // DC_MA
            // 
            this.DC_MA.Caption = "DC_MA";
            this.DC_MA.FieldName = "DC_MA";
            this.DC_MA.Name = "DC_MA";
            // 
            // DC_TINH
            // 
            this.DC_TINH.Caption = "DC_TINH";
            this.DC_TINH.FieldName = "DC_TINH";
            this.DC_TINH.Name = "DC_TINH";
            // 
            // DC_QUAN
            // 
            this.DC_QUAN.Caption = "DC_QUAN";
            this.DC_QUAN.FieldName = "DC_QUAN";
            this.DC_QUAN.Name = "DC_QUAN";
            // 
            // DC_ID_HUYEN
            // 
            this.DC_ID_HUYEN.Caption = "DC_ID_HUYEN";
            this.DC_ID_HUYEN.FieldName = "DC_ID_HUYEN";
            this.DC_ID_HUYEN.Name = "DC_ID_HUYEN";
            // 
            // DC_MA_HUYEN
            // 
            this.DC_MA_HUYEN.Caption = "DC_MA_HUYEN";
            this.DC_MA_HUYEN.FieldName = "DC_MA_HUYEN";
            this.DC_MA_HUYEN.Name = "DC_MA_HUYEN";
            // 
            // DC_TEN_HUYEN
            // 
            this.DC_TEN_HUYEN.Caption = "DC_TEN_HUYEN";
            this.DC_TEN_HUYEN.FieldName = "DC_TEN_HUYEN";
            this.DC_TEN_HUYEN.Name = "DC_TEN_HUYEN";
            // 
            // DC_ID_TINH
            // 
            this.DC_ID_TINH.Caption = "DC_ID_TINH";
            this.DC_ID_TINH.FieldName = "DC_ID_TINH";
            this.DC_ID_TINH.Name = "DC_ID_TINH";
            // 
            // DC_MA_TINH
            // 
            this.DC_MA_TINH.Caption = "DC_MA_TINH";
            this.DC_MA_TINH.FieldName = "DC_MA_TINH";
            this.DC_MA_TINH.Name = "DC_MA_TINH";
            // 
            // DC_TEN_TINH
            // 
            this.DC_TEN_TINH.Caption = "DC_TEN_TINH";
            this.DC_TEN_TINH.FieldName = "DC_TEN_TINH";
            this.DC_TEN_TINH.Name = "DC_TEN_TINH";
            // 
            // popupContainerEditAddress
            // 
            this.popupContainerEditAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupContainerEditAddress.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEditAddress.Name = "popupContainerEditAddress";
            this.popupContainerEditAddress.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEditAddress.Properties.PopupControl = this.popupContainerControlAddress;
            this.popupContainerEditAddress.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.popupContainerEditAddress.Size = new System.Drawing.Size(256, 20);
            this.popupContainerEditAddress.TabIndex = 1;
            this.popupContainerEditAddress.Leave += new System.EventHandler(this.popupContainerEditAddress_Leave);
            this.popupContainerEditAddress.QueryResultValue += new DevExpress.XtraEditors.Controls.QueryResultValueEventHandler(this.popupContainerEditAddress_QueryResultValue);
            this.popupContainerEditAddress.EditValueChanged += new System.EventHandler(this.popupContainerEditAddress_EditValueChanged);
            this.popupContainerEditAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.popupContainerEditAddress_KeyUp);
            this.popupContainerEditAddress.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.popupContainerEditAddress_QueryPopUp);
            this.popupContainerEditAddress.TextChanged += new System.EventHandler(this.popupContainerEditAddress_TextChanged);
            this.popupContainerEditAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.popupContainerEditAddress_KeyDown);
            // 
            // dgTinhThanh
            // 
            this.dgTinhThanh.Location = new System.Drawing.Point(4, 103);
            this.dgTinhThanh.MainView = this.gvTinhThanh;
            this.dgTinhThanh.Name = "dgTinhThanh";
            this.dgTinhThanh.Size = new System.Drawing.Size(250, 165);
            this.dgTinhThanh.TabIndex = 2;
            this.dgTinhThanh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTinhThanh});
            this.dgTinhThanh.Visible = false;
            this.dgTinhThanh.Leave += new System.EventHandler(this.dgTinhThanh_Leave);
            this.dgTinhThanh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgTinhThanh_KeyDown);
            // 
            // gvTinhThanh
            // 
            this.gvTinhThanh.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.DC_TEN_,
            this.CHA_DC_ID});
            this.gvTinhThanh.GridControl = this.dgTinhThanh;
            this.gvTinhThanh.Name = "gvTinhThanh";
            this.gvTinhThanh.OptionsBehavior.Editable = false;
            this.gvTinhThanh.OptionsCustomization.AllowFilter = false;
            this.gvTinhThanh.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvTinhThanh.OptionsView.ShowGroupPanel = false;
            this.gvTinhThanh.DoubleClick += new System.EventHandler(this.gvTinhThanh_DoubleClick);
            // 
            // DC_TEN_
            // 
            this.DC_TEN_.Caption = "Tỉnh/Thành phố";
            this.DC_TEN_.FieldName = "DC_TEN";
            this.DC_TEN_.Name = "DC_TEN_";
            this.DC_TEN_.Visible = true;
            this.DC_TEN_.VisibleIndex = 0;
            // 
            // CHA_DC_ID
            // 
            this.CHA_DC_ID.FieldName = "CHA_DC_ID";
            this.CHA_DC_ID.Name = "CHA_DC_ID";
            // 
            // Address
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgTinhThanh);
            this.Controls.Add(this.popupContainerEditAddress);
            this.Controls.Add(this.popupContainerControlAddress);
            this.Name = "Address";
            this.Size = new System.Drawing.Size(256, 20);
            this.Load += new System.EventHandler(this.AddressInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlAddress)).EndInit();
            this.popupContainerControlAddress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTinhThanh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTinhThanh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popupContainerControlAddress;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEditAddress;
        private DevExpress.XtraTreeList.TreeList treeListAddress;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_TEN;
        private DevExpress.XtraGrid.GridControl dgTinhThanh;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTinhThanh;
        private DevExpress.XtraGrid.Columns.GridColumn DC_TEN_;
        private DevExpress.XtraGrid.Columns.GridColumn CHA_DC_ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_TEN_XA;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_MA;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_TINH;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_QUAN;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_ID_HUYEN;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_MA_HUYEN;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_TEN_HUYEN;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_ID_TINH;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_MA_TINH;
        private DevExpress.XtraTreeList.Columns.TreeListColumn DC_TEN_TINH;
    }
}
