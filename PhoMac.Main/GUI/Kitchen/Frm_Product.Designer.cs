using PhoHa7.Library.UserControl;
namespace PhoMac.Main.GUI
{
    partial class Frm_Product
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonsArray1 = new PhoHa7.Library.UserControl.ButtonsArray();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colCat_Title = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCat_Code = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCat_Description = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCat_Actived = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colOrderBy = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCategory = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.treeList1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(515, 187, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(711, 385);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonsArray1);
            this.panelControl1.Location = new System.Drawing.Point(12, 318);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(687, 55);
            this.panelControl1.TabIndex = 5;
            // 
            // buttonsArray1
            // 
            this.buttonsArray1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonsArray1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonsArray1.btnBackColor = System.Drawing.Color.Transparent;
            this.buttonsArray1.btnBackgroundImage = null;
            this.buttonsArray1.btnFlatStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.buttonsArray1.btnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.buttonsArray1.btnGroupBoxShow = false;
            this.buttonsArray1.btnSize = new System.Drawing.Size(77, 28);
            this.buttonsArray1.btnSpacing = 3;
            this.buttonsArray1.btnStyle = PhoHa7.Library.UserControl.ButtonsArray.btnStyleEnum.Array;
            this.buttonsArray1.btnStyleGroup = PhoHa7.Library.UserControl.ButtonsArray.btnStyleGroupEnum.None;
            this.buttonsArray1.ButtonChonVisible = false;
            this.buttonsArray1.ButtonExcelVisible = false;
            this.buttonsArray1.ButtonInanVisible = false;
            this.buttonsArray1.ButtonLapVisible = false;
            this.buttonsArray1.ButtonPrintBarcodeVisible = false;
            this.buttonsArray1.ButtonResetVisible = false;
            this.buttonsArray1.ButtonSuaText = PhoHa7.Library.UserControl.ButtonsArray.btnUpdateEnum.Update;
            this.buttonsArray1.ButtonSuaVisible = true;
            this.buttonsArray1.ButtonThemVisible = true;
            this.buttonsArray1.ButtonThoatVisible = true;
            this.buttonsArray1.ButtonWordVisible = false;
            this.buttonsArray1.ButtonXemVisible = false;
            this.buttonsArray1.ButtonXoaVisible = true;
            this.buttonsArray1.Location = new System.Drawing.Point(173, 1);
            this.buttonsArray1.Name = "buttonsArray1";
            this.buttonsArray1.Size = new System.Drawing.Size(341, 52);
            this.buttonsArray1.TabIndex = 6;
            this.buttonsArray1.btnEventAdd_click += new System.EventHandler(this.buttonsArray1_btnEventAdd_click);
            this.buttonsArray1.btnEventUpdate_click += new System.EventHandler(this.buttonsArray1_btnEventUpdate_click_1);
            this.buttonsArray1.btnEventDelete_click += new System.EventHandler(this.buttonsArray1_btnEventDelete_click);
            this.buttonsArray1.btnEventClose_click += new System.EventHandler(this.buttonsArray1_btnEventClose_click);
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCat_Title,
            this.colCat_Code,
            this.colCat_Description,
            this.colCat_Actived,
            this.colOrderBy,
            this.colType,
            this.colCategory});
            this.treeList1.KeyFieldName = "ProductID";
            this.treeList1.Location = new System.Drawing.Point(12, 12);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.CopyToClipboardWithNodeHierarchy = false;
            this.treeList1.OptionsBehavior.EnableFiltering = true;
            this.treeList1.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            this.treeList1.OptionsFilter.ShowAllValuesInFilterPopup = true;
            this.treeList1.OptionsFind.AllowFindPanel = true;
            this.treeList1.OptionsFind.AlwaysVisible = true;
            this.treeList1.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.Always;
            this.treeList1.OptionsView.ShowAutoFilterRow = true;
            this.treeList1.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.ShowAlways;
            this.treeList1.ParentFieldName = "ExpandCategoryID";
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.treeList1.Size = new System.Drawing.Size(687, 302);
            this.treeList1.TabIndex = 4;
            // 
            // colCat_Title
            // 
            this.colCat_Title.AppearanceHeader.Options.UseTextOptions = true;
            this.colCat_Title.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCat_Title.Caption = "Tiêu đề";
            this.colCat_Title.FieldName = "ProductName";
            this.colCat_Title.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colCat_Title.Name = "colCat_Title";
            this.colCat_Title.OptionsFilter.FilterPopupMode = DevExpress.XtraTreeList.FilterPopupMode.List;
            this.colCat_Title.Visible = true;
            this.colCat_Title.VisibleIndex = 1;
            this.colCat_Title.Width = 134;
            // 
            // colCat_Code
            // 
            this.colCat_Code.AppearanceCell.Options.UseTextOptions = true;
            this.colCat_Code.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCat_Code.AppearanceHeader.Options.UseTextOptions = true;
            this.colCat_Code.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCat_Code.Caption = "Mã";
            this.colCat_Code.FieldName = "BarCode";
            this.colCat_Code.Name = "colCat_Code";
            this.colCat_Code.Visible = true;
            this.colCat_Code.VisibleIndex = 0;
            this.colCat_Code.Width = 134;
            // 
            // colCat_Description
            // 
            this.colCat_Description.AppearanceHeader.Options.UseTextOptions = true;
            this.colCat_Description.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCat_Description.Caption = "Mô tả";
            this.colCat_Description.FieldName = "KitchenName";
            this.colCat_Description.Name = "colCat_Description";
            this.colCat_Description.Visible = true;
            this.colCat_Description.VisibleIndex = 2;
            this.colCat_Description.Width = 134;
            // 
            // colCat_Actived
            // 
            this.colCat_Actived.AppearanceHeader.Options.UseTextOptions = true;
            this.colCat_Actived.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCat_Actived.Caption = "Kích hoạt";
            this.colCat_Actived.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colCat_Actived.FieldName = "Active";
            this.colCat_Actived.Name = "colCat_Actived";
            this.colCat_Actived.Visible = true;
            this.colCat_Actived.VisibleIndex = 3;
            this.colCat_Actived.Width = 134;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueGrayed = false;
            // 
            // colOrderBy
            // 
            this.colOrderBy.Caption = "Số thứ tự";
            this.colOrderBy.FieldName = "OrderBy";
            this.colOrderBy.Name = "colOrderBy";
            this.colOrderBy.Visible = true;
            this.colOrderBy.VisibleIndex = 4;
            this.colOrderBy.Width = 133;
            // 
            // colType
            // 
            this.colType.Caption = "Loại";
            this.colType.FieldName = "ProductTypeName";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 5;
            // 
            // colCategory
            // 
            this.colCategory.Caption = "Danh mục";
            this.colCategory.FieldName = "CategoryID";
            this.colCategory.Name = "colCategory";
            this.colCategory.Visible = true;
            this.colCategory.VisibleIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(711, 385);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeList1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(691, 306);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 306);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 59);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 59);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(691, 59);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // Frm_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_Product";
            this.SetText = "Danh mục";
            this.Size = new System.Drawing.Size(711, 385);
            this.Load += new System.EventHandler(this.Frm_Product_Category_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private ButtonsArray buttonsArray1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCat_Title;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCat_Description;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCat_Actived;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCat_Code;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOrderBy;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCategory;

    }
}
