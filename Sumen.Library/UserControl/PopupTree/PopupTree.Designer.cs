namespace PhoHa7.Library.UserControl.PopupTree
{
    partial class PopupTree
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
            this.popupContainerEditKhoa = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControlKhoa = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeListKhoa = new DevExpress.XtraTreeList.TreeList();
            this.NAME_TREE = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Cat_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Cat_CODE = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.PARENTID_TREE = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.PARENTNAME = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditKhoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlKhoa)).BeginInit();
            this.popupContainerControlKhoa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListKhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerEditKhoa
            // 
            this.popupContainerEditKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupContainerEditKhoa.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEditKhoa.Name = "popupContainerEditKhoa";
            this.popupContainerEditKhoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEditKhoa.Properties.PopupControl = this.popupContainerControlKhoa;
            this.popupContainerEditKhoa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.popupContainerEditKhoa.Size = new System.Drawing.Size(277, 20);
            this.popupContainerEditKhoa.TabIndex = 0;
            this.popupContainerEditKhoa.QueryResultValue += new DevExpress.XtraEditors.Controls.QueryResultValueEventHandler(this.popupContainerEditKhoa_QueryResultValue);
            this.popupContainerEditKhoa.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.popupContainerEditKhoa_QueryPopUp);
            this.popupContainerEditKhoa.EditValueChanged += new System.EventHandler(this.popupContainerEditKhoa_EditValueChanged);
            this.popupContainerEditKhoa.TextChanged += new System.EventHandler(this.popupContainerEditKhoa_TextChanged);
            this.popupContainerEditKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.popupContainerEditKhoa_KeyDown);
            this.popupContainerEditKhoa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.popupContainerEditKhoa_KeyUp);
            this.popupContainerEditKhoa.Leave += new System.EventHandler(this.popupContainerEditKhoa_Leave);
            // 
            // popupContainerControlKhoa
            // 
            this.popupContainerControlKhoa.Controls.Add(this.treeListKhoa);
            this.popupContainerControlKhoa.Location = new System.Drawing.Point(3, 29);
            this.popupContainerControlKhoa.Name = "popupContainerControlKhoa";
            this.popupContainerControlKhoa.Size = new System.Drawing.Size(273, 236);
            this.popupContainerControlKhoa.TabIndex = 1;
            // 
            // treeListKhoa
            // 
            this.treeListKhoa.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NAME_TREE,
            this.Cat_ID,
            this.Cat_CODE,
            this.PARENTID_TREE,
            this.PARENTNAME});
            this.treeListKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListKhoa.Location = new System.Drawing.Point(0, 0);
            this.treeListKhoa.Name = "treeListKhoa";
            this.treeListKhoa.OptionsBehavior.Editable = false;
            this.treeListKhoa.ParentFieldName = "ExpandCategoryID";
            this.treeListKhoa.Size = new System.Drawing.Size(273, 236);
            this.treeListKhoa.TabIndex = 0;
            this.treeListKhoa.DoubleClick += new System.EventHandler(this.treeListKhoa_DoubleClick);
            this.treeListKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeListKhoa_KeyDown);
            // 
            // NAME_TREE
            // 
            this.NAME_TREE.Caption = "Tên";
            this.NAME_TREE.FieldName = "ProductName";
            this.NAME_TREE.Name = "NAME_TREE";
            this.NAME_TREE.Visible = true;
            this.NAME_TREE.VisibleIndex = 0;
            // 
            // Cat_ID
            // 
            this.Cat_ID.Caption = "ID";
            this.Cat_ID.FieldName = "ProductID";
            this.Cat_ID.Name = "Cat_ID";
            // 
            // Cat_CODE
            // 
            this.Cat_CODE.Caption = "CODE";
            this.Cat_CODE.FieldName = "BarCode";
            this.Cat_CODE.Name = "Cat_CODE";
            // 
            // PARENTID_TREE
            // 
            this.PARENTID_TREE.Caption = "Parent_id";
            this.PARENTID_TREE.FieldName = "ExpandCategoryID";
            this.PARENTID_TREE.Name = "PARENTID_TREE";
            // 
            // PARENTNAME
            // 
            this.PARENTNAME.Caption = "PARENTNAME";
            this.PARENTNAME.FieldName = "ProductName";
            this.PARENTNAME.Name = "PARENTNAME";
            // 
            // PopupTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControlKhoa);
            this.Controls.Add(this.popupContainerEditKhoa);
            this.Name = "PopupTree";
            this.Size = new System.Drawing.Size(277, 20);
            this.Load += new System.EventHandler(this.Khoa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditKhoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlKhoa)).EndInit();
            this.popupContainerControlKhoa.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListKhoa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEditKhoa;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControlKhoa;
        private DevExpress.XtraTreeList.TreeList treeListKhoa;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NAME_TREE;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn CODE;
        private DevExpress.XtraTreeList.Columns.TreeListColumn PARENTID_TREE;
        private DevExpress.XtraTreeList.Columns.TreeListColumn PARENTNAME;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Cat_ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Cat_CODE;

    }
}