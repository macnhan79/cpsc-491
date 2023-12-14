namespace PhoMac.Main.GUI
{
    partial class TicketView
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.lblNumberOfOrder = new System.Windows.Forms.Label();
            this.btnCompleteNoPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnCompleteAll = new DevExpress.XtraEditors.SimpleButton();
            this.lblTime = new DevExpress.XtraEditors.LabelControl();
            this.btnCompleted = new DevExpress.XtraEditors.SimpleButton();
            this.lblTable = new DevExpress.XtraEditors.LabelControl();
            this.lblServer = new DevExpress.XtraEditors.LabelControl();
            this.gControlGroup = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colQuality = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colCompleted = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSmall = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSaleItemID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToGo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCancelItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBarCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExtraName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExtraWith = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExtraWithout = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsBlink = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmergency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutLblCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLblCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblCustomerName);
            this.layoutControl1.Controls.Add(this.lblNumberOfOrder);
            this.layoutControl1.Controls.Add(this.btnCompleteNoPrint);
            this.layoutControl1.Controls.Add(this.btnCompleteAll);
            this.layoutControl1.Controls.Add(this.lblTime);
            this.layoutControl1.Controls.Add(this.btnCompleted);
            this.layoutControl1.Controls.Add(this.lblTable);
            this.layoutControl1.Controls.Add(this.lblServer);
            this.layoutControl1.Controls.Add(this.gControlGroup);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(6);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(678, 266, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(640, 996);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.Location = new System.Drawing.Point(12, 112);
            this.lblCustomerName.Margin = new System.Windows.Forms.Padding(6);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(297, 19);
            this.lblCustomerName.StyleController = this.layoutControl1;
            this.lblCustomerName.TabIndex = 14;
            this.lblCustomerName.Text = "Customer";
            // 
            // lblNumberOfOrder
            // 
            this.lblNumberOfOrder.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblNumberOfOrder.Location = new System.Drawing.Point(12, 12);
            this.lblNumberOfOrder.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumberOfOrder.Name = "lblNumberOfOrder";
            this.lblNumberOfOrder.Size = new System.Drawing.Size(616, 47);
            this.lblNumberOfOrder.TabIndex = 12;
            this.lblNumberOfOrder.Text = "label1";
            this.lblNumberOfOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCompleteNoPrint
            // 
            this.btnCompleteNoPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompleteNoPrint.Appearance.Options.UseFont = true;
            this.btnCompleteNoPrint.Image = global::PhoMac.Main.Properties.Resources.correct_mark;
            this.btnCompleteNoPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnCompleteNoPrint.Location = new System.Drawing.Point(104, 923);
            this.btnCompleteNoPrint.Margin = new System.Windows.Forms.Padding(6);
            this.btnCompleteNoPrint.Name = "btnCompleteNoPrint";
            this.btnCompleteNoPrint.Size = new System.Drawing.Size(78, 61);
            this.btnCompleteNoPrint.StyleController = this.layoutControl1;
            this.btnCompleteNoPrint.TabIndex = 11;
            this.btnCompleteNoPrint.Text = "Done";
            this.btnCompleteNoPrint.Click += new System.EventHandler(this.btnCompleteNoPrint_Click);
            // 
            // btnCompleteAll
            // 
            this.btnCompleteAll.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.btnCompleteAll.Appearance.Options.UseFont = true;
            this.btnCompleteAll.Image = global::PhoMac.Main.Properties.Resources.task_complete;
            this.btnCompleteAll.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnCompleteAll.Location = new System.Drawing.Point(12, 923);
            this.btnCompleteAll.Margin = new System.Windows.Forms.Padding(6);
            this.btnCompleteAll.Name = "btnCompleteAll";
            this.btnCompleteAll.Size = new System.Drawing.Size(88, 61);
            this.btnCompleteAll.StyleController = this.layoutControl1;
            this.btnCompleteAll.TabIndex = 10;
            this.btnCompleteAll.Text = "Complete";
            this.btnCompleteAll.Click += new System.EventHandler(this.btnCompleteAll_Click);
            // 
            // lblTime
            // 
            this.lblTime.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTime.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTime.Location = new System.Drawing.Point(410, 135);
            this.lblTime.Margin = new System.Windows.Forms.Padding(6);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(218, 95);
            this.lblTime.StyleController = this.layoutControl1;
            this.lblTime.TabIndex = 9;
            this.lblTime.Text = "Time";
            // 
            // btnCompleted
            // 
            this.btnCompleted.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnCompleted.Appearance.Options.UseFont = true;
            this.btnCompleted.Image = global::PhoMac.Main.Properties.Resources.printer_;
            this.btnCompleted.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnCompleted.Location = new System.Drawing.Point(186, 923);
            this.btnCompleted.Margin = new System.Windows.Forms.Padding(6);
            this.btnCompleted.Name = "btnCompleted";
            this.btnCompleted.Size = new System.Drawing.Size(123, 61);
            this.btnCompleted.StyleController = this.layoutControl1;
            this.btnCompleted.TabIndex = 8;
            this.btnCompleted.Text = "Done & Print";
            this.btnCompleted.Click += new System.EventHandler(this.btnCompleted_Click);
            // 
            // lblTable
            // 
            this.lblTable.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTable.Location = new System.Drawing.Point(12, 135);
            this.lblTable.Margin = new System.Windows.Forms.Padding(6);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(394, 95);
            this.lblTable.StyleController = this.layoutControl1;
            this.lblTable.TabIndex = 7;
            this.lblTable.Text = "Table";
            // 
            // lblServer
            // 
            this.lblServer.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblServer.Location = new System.Drawing.Point(12, 63);
            this.lblServer.Margin = new System.Windows.Forms.Padding(6);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(616, 45);
            this.lblServer.StyleController = this.layoutControl1;
            this.lblServer.TabIndex = 6;
            this.lblServer.Text = "Server";
            // 
            // gControlGroup
            // 
            this.gControlGroup.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.gControlGroup.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.gControlGroup.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gControlGroup.Location = new System.Drawing.Point(12, 234);
            this.gControlGroup.MainView = this.gridView1;
            this.gControlGroup.Margin = new System.Windows.Forms.Padding(6);
            this.gControlGroup.Name = "gControlGroup";
            this.gControlGroup.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemRichTextEdit1,
            this.repositoryItemButtonEdit1});
            this.gControlGroup.Size = new System.Drawing.Size(616, 685);
            this.gControlGroup.TabIndex = 4;
            this.gControlGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gControlGroup.DataSourceChanged += new System.EventHandler(this.gControlGroup_DataSourceChanged);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.gridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Transparent;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colQuality,
            this.colName,
            this.colCompleted,
            this.colIsSmall,
            this.colCategory,
            this.colSaleItemID,
            this.colToGo,
            this.colCancelItem,
            this.colIsChange,
            this.colBarCode,
            this.colExtraName,
            this.colOption,
            this.colMType,
            this.colExtraWith,
            this.colExtraWithout,
            this.colCustomSelect,
            this.colIsBlink,
            this.colOrderBy,
            this.colEmergency,
            this.colProductID});
            this.gridView1.GridControl = this.gControlGroup;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCategory, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colOrderBy, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
            // 
            // colQuality
            // 
            this.colQuality.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colQuality.AppearanceCell.Options.UseForeColor = true;
            this.colQuality.AppearanceCell.Options.UseTextOptions = true;
            this.colQuality.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuality.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQuality.AppearanceHeader.BackColor = System.Drawing.Color.White;
            this.colQuality.AppearanceHeader.Options.UseBackColor = true;
            this.colQuality.Caption = "Qty";
            this.colQuality.FieldName = "Qty";
            this.colQuality.Name = "colQuality";
            this.colQuality.OptionsColumn.AllowEdit = false;
            this.colQuality.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colQuality.OptionsColumn.ReadOnly = true;
            this.colQuality.OptionsFilter.AllowAutoFilter = false;
            this.colQuality.OptionsFilter.AllowFilter = false;
            this.colQuality.Visible = true;
            this.colQuality.VisibleIndex = 0;
            this.colQuality.Width = 39;
            // 
            // colName
            // 
            this.colName.AppearanceCell.Options.UseTextOptions = true;
            this.colName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colName.Caption = "Name";
            this.colName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colName.FieldName = "Description";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colName.OptionsColumn.FixedWidth = true;
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.OptionsFilter.AllowAutoFilter = false;
            this.colName.OptionsFilter.AllowFilter = false;
            this.colName.ToolTip = "abc";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 168;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemMemoEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemMemoEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemMemoEdit1.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit1.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemMemoEdit1.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit1.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemMemoEdit1.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemMemoEdit1.LinesCount = 3;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            this.repositoryItemMemoEdit1.ReadOnly = true;
            // 
            // colCompleted
            // 
            this.colCompleted.FieldName = "Done";
            this.colCompleted.Name = "colCompleted";
            this.colCompleted.OptionsColumn.AllowEdit = false;
            this.colCompleted.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCompleted.OptionsColumn.ReadOnly = true;
            this.colCompleted.OptionsFilter.AllowAutoFilter = false;
            this.colCompleted.OptionsFilter.AllowFilter = false;
            this.colCompleted.Visible = true;
            this.colCompleted.VisibleIndex = 2;
            // 
            // colIsSmall
            // 
            this.colIsSmall.FieldName = "IsSmall";
            this.colIsSmall.Name = "colIsSmall";
            // 
            // colCategory
            // 
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            // 
            // colSaleItemID
            // 
            this.colSaleItemID.FieldName = "SaleItemID";
            this.colSaleItemID.Name = "colSaleItemID";
            // 
            // colToGo
            // 
            this.colToGo.FieldName = "TakeOut";
            this.colToGo.Name = "colToGo";
            // 
            // colCancelItem
            // 
            this.colCancelItem.FieldName = "Cancel";
            this.colCancelItem.Name = "colCancelItem";
            // 
            // colIsChange
            // 
            this.colIsChange.FieldName = "IsChange";
            this.colIsChange.Name = "colIsChange";
            // 
            // colBarCode
            // 
            this.colBarCode.FieldName = "BarCode";
            this.colBarCode.Name = "colBarCode";
            // 
            // colExtraName
            // 
            this.colExtraName.FieldName = "ExtraName";
            this.colExtraName.Name = "colExtraName";
            // 
            // colOption
            // 
            this.colOption.FieldName = "OptionRequire";
            this.colOption.Name = "colOption";
            // 
            // colMType
            // 
            this.colMType.Caption = "MType";
            this.colMType.FieldName = "MType";
            this.colMType.Name = "colMType";
            // 
            // colExtraWith
            // 
            this.colExtraWith.FieldName = "ExtraWith";
            this.colExtraWith.Name = "colExtraWith";
            // 
            // colExtraWithout
            // 
            this.colExtraWithout.FieldName = "ExtraWithout";
            this.colExtraWithout.Name = "colExtraWithout";
            // 
            // colCustomSelect
            // 
            this.colCustomSelect.FieldName = "CustomSelect";
            this.colCustomSelect.Name = "colCustomSelect";
            // 
            // colIsBlink
            // 
            this.colIsBlink.FieldName = "Blink";
            this.colIsBlink.Name = "colIsBlink";
            this.colIsBlink.Tag = true;
            this.colIsBlink.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            // 
            // colOrderBy
            // 
            this.colOrderBy.FieldName = "OrderBy";
            this.colOrderBy.Name = "colOrderBy";
            // 
            // colEmergency
            // 
            this.colEmergency.FieldName = "Emergency";
            this.colEmergency.Name = "colEmergency";
            // 
            // colProductID
            // 
            this.colProductID.Caption = "colProductID";
            this.colProductID.FieldName = "ProductID";
            this.colProductID.Name = "colProductID";
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemRichTextEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemRichTextEdit1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemRichTextEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemRichTextEdit1.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemRichTextEdit1.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemRichTextEdit1.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemRichTextEdit1.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemRichTextEdit1.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemRichTextEdit1.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemRichTextEdit1.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemRichTextEdit1.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemRichTextEdit1.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemRichTextEdit1.DocumentFormat = DevExpress.XtraRichEdit.DocumentFormat.Html;
            this.repositoryItemRichTextEdit1.EncodingWebName = "utf-8";
            this.repositoryItemRichTextEdit1.MaxHeight = 750;
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            this.repositoryItemRichTextEdit1.ReadOnly = true;
            this.repositoryItemRichTextEdit1.ShowCaretInReadOnly = false;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemButtonEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemButtonEdit1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemButtonEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemButtonEdit1.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemButtonEdit1.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemButtonEdit1.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.ReadOnly = true;
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem2,
            this.layoutControlItem8,
            this.layoutLblCustomer});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(640, 996);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gControlGroup;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 222);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(620, 689);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblServer;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 51);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(57, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(620, 49);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCompleted;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(174, 911);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(127, 65);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(127, 65);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(446, 65);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lblTime;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(398, 123);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(53, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(222, 99);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblTable;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 123);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(35, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(398, 99);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnCompleteNoPrint;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(92, 911);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(82, 65);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(82, 65);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(82, 65);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCompleteAll;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 911);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(92, 65);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(92, 65);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(92, 65);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lblNumberOfOrder;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(620, 51);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutLblCustomer
            // 
            this.layoutLblCustomer.Control = this.lblCustomerName;
            this.layoutLblCustomer.CustomizationFormText = "layoutLblCustomer";
            this.layoutLblCustomer.Location = new System.Drawing.Point(0, 100);
            this.layoutLblCustomer.MaxSize = new System.Drawing.Size(301, 23);
            this.layoutLblCustomer.MinSize = new System.Drawing.Size(301, 23);
            this.layoutLblCustomer.Name = "layoutLblCustomer";
            this.layoutLblCustomer.Size = new System.Drawing.Size(620, 23);
            this.layoutLblCustomer.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutLblCustomer.Text = "layoutLblCustomer";
            this.layoutLblCustomer.TextSize = new System.Drawing.Size(0, 0);
            this.layoutLblCustomer.TextToControlDistance = 0;
            this.layoutLblCustomer.TextVisible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TicketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "TicketView";
            this.Size = new System.Drawing.Size(640, 996);
            this.Load += new System.EventHandler(this.TicketView_Load);
            this.VisibleChanged += new System.EventHandler(this.TicketView_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TicketView_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TicketView_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLblCustomer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl lblTable;
        private DevExpress.XtraEditors.LabelControl lblServer;
        private DevExpress.XtraGrid.GridControl gControlGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colQuality;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.SimpleButton btnCompleted;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colCompleted;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.LabelControl lblTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnCompleteAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colSaleItemID;
        private DevExpress.XtraGrid.Columns.GridColumn colToGo;
        private DevExpress.XtraGrid.Columns.GridColumn colCancelItem;
        private DevExpress.XtraGrid.Columns.GridColumn colIsChange;
        private DevExpress.XtraEditors.SimpleButton btnCompleteNoPrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn colBarCode;
        private DevExpress.XtraGrid.Columns.GridColumn colExtraName;
        private DevExpress.XtraGrid.Columns.GridColumn colOption;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSmall;
        private DevExpress.XtraGrid.Columns.GridColumn colMType;
        private System.Windows.Forms.Label lblNumberOfOrder;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.Columns.GridColumn colExtraWith;
        private DevExpress.XtraGrid.Columns.GridColumn colExtraWithout;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsBlink;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderBy;
        private DevExpress.XtraEditors.LabelControl lblCustomerName;
        private DevExpress.XtraLayout.LayoutControlItem layoutLblCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colEmergency;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colProductID;

    }
}
