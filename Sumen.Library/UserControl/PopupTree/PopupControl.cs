namespace PhoHa7.Library.UserControl.PopupTree
{
    public class PopupControl : DevExpress.XtraEditors.PopupContainerEdit
    {
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControlKhoa;
        private DevExpress.XtraTreeList.TreeList treeListKhoa;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NAME_TREE;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn CODE;
        private DevExpress.XtraTreeList.Columns.TreeListColumn PARENTID_TREE;
        private DevExpress.XtraTreeList.Columns.TreeListColumn PARENTNAME;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Title;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Parent_id;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit fProperties;

        private void InitializeComponent()
        {
            this.fProperties = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.popupContainerControlKhoa = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeListKhoa = new DevExpress.XtraTreeList.TreeList();
            this.Title = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.CODE = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Parent_id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.PARENTNAME = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlKhoa)).BeginInit();
            this.popupContainerControlKhoa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListKhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // fProperties
            // 
            this.fProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fProperties.Name = "fProperties";
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
            this.Title,
            this.ID,
            this.CODE,
            this.Parent_id,
            this.PARENTNAME});
            this.treeListKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListKhoa.Location = new System.Drawing.Point(0, 0);
            this.treeListKhoa.Name = "treeListKhoa";
            this.treeListKhoa.OptionsBehavior.Editable = false;
            this.treeListKhoa.ParentFieldName = "Cat_ParentID";
            this.treeListKhoa.Size = new System.Drawing.Size(273, 236);
            this.treeListKhoa.TabIndex = 0;
            // 
            // Title
            // 
            this.Title.Caption = "Cat_Title";
            this.Title.FieldName = "Cat_Title";
            this.Title.Name = "Cat_Title";
            this.Title.Visible = true;
            this.Title.VisibleIndex = 0;
            // 
            // ID
            // 
            this.ID.Caption = "Cat_ID";
            this.ID.FieldName = "Cat_ID";
            this.ID.Name = "Cat_ID";
            // 
            // CODE
            // 
            this.CODE.Caption = "CODE";
            this.CODE.FieldName = "CODE";
            this.CODE.Name = "CODE";
            // 
            // Parent_id
            // 
            this.Parent_id.Caption = "Cat_Parent_id";
            this.Parent_id.FieldName = "Cat_Parent_id";
            this.Parent_id.Name = "Cat_Parent_id";
            // 
            // PARENTNAME
            // 
            this.PARENTNAME.Caption = "PARENTNAME";
            this.PARENTNAME.FieldName = "PARENTNAME";
            this.PARENTNAME.Name = "PARENTNAME";
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlKhoa)).EndInit();
            this.popupContainerControlKhoa.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListKhoa)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
