namespace PhoHa7.Library.UserControl
{
    partial class PopupTreeView
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
            this.popupContainerEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnValue = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnText = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl)).BeginInit();
            this.popupContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerEdit
            // 
            this.popupContainerEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.popupContainerEdit.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEdit.Name = "popupContainerEdit";
            this.popupContainerEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit.Properties.PopupControl = this.popupContainerControl;
            this.popupContainerEdit.Properties.ShowPopupCloseButton = false;
            this.popupContainerEdit.Size = new System.Drawing.Size(404, 20);
            this.popupContainerEdit.TabIndex = 3;
            // 
            // popupContainerControl
            // 
            this.popupContainerControl.Controls.Add(this.treeList);
            this.popupContainerControl.Location = new System.Drawing.Point(3, 26);
            this.popupContainerControl.Name = "popupContainerControl";
            this.popupContainerControl.Size = new System.Drawing.Size(398, 161);
            this.popupContainerControl.TabIndex = 4;
            // 
            // treeList
            // 
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnValue,
            this.treeListColumnText});
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.Location = new System.Drawing.Point(0, 0);
            this.treeList.Name = "treeList";
            this.treeList.BeginUnboundLoad();
            this.treeList.AppendNode(new object[] {
            "Value",
            "Text"}, -1);
            this.treeList.AppendNode(new object[] {
            "Child Value",
            "Child Text"}, 0);
            this.treeList.EndUnboundLoad();
            this.treeList.OptionsBehavior.PopulateServiceColumns = true;
            this.treeList.OptionsView.ShowColumns = false;
            this.treeList.Size = new System.Drawing.Size(398, 161);
            this.treeList.TabIndex = 0;
            this.treeList.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList_AfterFocusNode);
            // 
            // treeListColumnValue
            // 
            this.treeListColumnValue.MinWidth = 38;
            this.treeListColumnValue.Name = "treeListColumnValue";
            this.treeListColumnValue.OptionsColumn.AllowEdit = false;
            // 
            // treeListColumnText
            // 
            this.treeListColumnText.MinWidth = 38;
            this.treeListColumnText.Name = "treeListColumnText";
            this.treeListColumnText.OptionsColumn.AllowEdit = false;
            this.treeListColumnText.Visible = true;
            this.treeListColumnText.VisibleIndex = 0;
            // 
            // PopupTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControl);
            this.Controls.Add(this.popupContainerEdit);
            this.Name = "PopupTreeView";
            this.Size = new System.Drawing.Size(404, 190);
            this.SizeChanged += new System.EventHandler(this.PopupTreeView_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl)).EndInit();
            this.popupContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnValue;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnText;
    }
}
