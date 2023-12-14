namespace PhoMac.Main.GUI.Kitchen
{
    partial class UCListTicketPrint
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.gcItems = new DevExpress.XtraGrid.GridControl();
            this.gItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTakeOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsCancel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTicket = new DevExpress.XtraGrid.GridControl();
            this.gvTicket = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDTicketNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTableName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colServer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTicketID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateTimeIssue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTicket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTicket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnPrint);
            this.layoutControl1.Controls.Add(this.gcItems);
            this.layoutControl1.Controls.Add(this.gcTicket);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1013, 170, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(681, 465);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = global::PhoMac.Main.Properties.Resources.Remove_32x32;
            this.btnClose.Location = new System.Drawing.Point(507, 404);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(162, 49);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Image = global::PhoMac.Main.Properties.Resources.printer_;
            this.btnPrint.Location = new System.Drawing.Point(342, 404);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(161, 49);
            this.btnPrint.StyleController = this.layoutControl1;
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "In";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // gcItems
            // 
            this.gcItems.Location = new System.Drawing.Point(345, 15);
            this.gcItems.MainView = this.gItems;
            this.gcItems.Name = "gcItems";
            this.gcItems.Size = new System.Drawing.Size(321, 382);
            this.gcItems.TabIndex = 5;
            this.gcItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gItems});
            // 
            // gItems
            // 
            this.gItems.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gItems.Appearance.Row.Options.UseFont = true;
            this.gItems.Appearance.Row.Options.UseTextOptions = true;
            this.gItems.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colTakeOut,
            this.colIsChange,
            this.colIsCancel});
            this.gItems.GridControl = this.gcItems;
            this.gItems.Name = "gItems";
            this.gItems.OptionsSelection.UseIndicatorForSelection = false;
            this.gItems.OptionsView.RowAutoHeight = true;
            this.gItems.OptionsView.ShowGroupPanel = false;
            this.gItems.RowHeight = 45;
            this.gItems.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gItems.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gItems_RowStyle);
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.Caption = "Name";
            this.colName.FieldName = "PrintName";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // colTakeOut
            // 
            this.colTakeOut.FieldName = "TakeOut";
            this.colTakeOut.Name = "colTakeOut";
            // 
            // colIsChange
            // 
            this.colIsChange.FieldName = "IsChange";
            this.colIsChange.Name = "colIsChange";
            // 
            // colIsCancel
            // 
            this.colIsCancel.FieldName = "IsCancel";
            this.colIsCancel.Name = "colIsCancel";
            // 
            // gcTicket
            // 
            this.gcTicket.Location = new System.Drawing.Point(15, 15);
            this.gcTicket.MainView = this.gvTicket;
            this.gcTicket.Name = "gcTicket";
            this.gcTicket.Size = new System.Drawing.Size(320, 435);
            this.gcTicket.TabIndex = 4;
            this.gcTicket.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTicket});
            // 
            // gvTicket
            // 
            this.gvTicket.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gvTicket.Appearance.Row.Options.UseFont = true;
            this.gvTicket.Appearance.Row.Options.UseTextOptions = true;
            this.gvTicket.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvTicket.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDTicketNum,
            this.colTableName,
            this.colServer,
            this.colTicketID,
            this.colDateTimeIssue});
            this.gvTicket.GridControl = this.gcTicket;
            this.gvTicket.Name = "gvTicket";
            this.gvTicket.OptionsDetail.EnableMasterViewMode = false;
            this.gvTicket.OptionsSelection.UseIndicatorForSelection = false;
            this.gvTicket.OptionsView.RowAutoHeight = true;
            this.gvTicket.OptionsView.ShowGroupPanel = false;
            this.gvTicket.RowHeight = 45;
            this.gvTicket.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gvTicket.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDateTimeIssue, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvTicket.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gTicket_RowCellClick);
            // 
            // colDTicketNum
            // 
            this.colDTicketNum.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDTicketNum.AppearanceHeader.Options.UseFont = true;
            this.colDTicketNum.Caption = "#";
            this.colDTicketNum.FieldName = "DTicketNum";
            this.colDTicketNum.Name = "colDTicketNum";
            this.colDTicketNum.OptionsColumn.AllowEdit = false;
            this.colDTicketNum.OptionsColumn.ReadOnly = true;
            this.colDTicketNum.Width = 38;
            // 
            // colTableName
            // 
            this.colTableName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTableName.AppearanceHeader.Options.UseFont = true;
            this.colTableName.Caption = "Table";
            this.colTableName.FieldName = "TableName";
            this.colTableName.Name = "colTableName";
            this.colTableName.OptionsColumn.AllowEdit = false;
            this.colTableName.OptionsColumn.ReadOnly = true;
            this.colTableName.Visible = true;
            this.colTableName.VisibleIndex = 0;
            this.colTableName.Width = 149;
            // 
            // colServer
            // 
            this.colServer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colServer.AppearanceHeader.Options.UseFont = true;
            this.colServer.Caption = "Server";
            this.colServer.FieldName = "EmployeeName";
            this.colServer.Name = "colServer";
            this.colServer.OptionsColumn.AllowEdit = false;
            this.colServer.OptionsColumn.ReadOnly = true;
            this.colServer.Visible = true;
            this.colServer.VisibleIndex = 1;
            this.colServer.Width = 153;
            // 
            // colTicketID
            // 
            this.colTicketID.FieldName = "TicketID";
            this.colTicketID.Name = "colTicketID";
            this.colTicketID.OptionsColumn.AllowEdit = false;
            this.colTicketID.OptionsColumn.ShowCaption = false;
            // 
            // colDateTimeIssue
            // 
            this.colDateTimeIssue.Caption = "gridColumn1";
            this.colDateTimeIssue.FieldName = "DateTimeIssue";
            this.colDateTimeIssue.Name = "colDateTimeIssue";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(681, 465);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcTicket;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(330, 445);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcItems;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(330, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(331, 392);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnPrint;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(330, 392);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(165, 53);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnClose;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(495, 392);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(166, 53);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // UCListTicketPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 465);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCListTicketPrint";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UCListTicketPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTicket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTicket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gItems;
        private DevExpress.XtraGrid.GridControl gcTicket;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTicket;
        private DevExpress.XtraGrid.Columns.GridColumn colDTicketNum;
        private DevExpress.XtraGrid.Columns.GridColumn colTableName;
        private DevExpress.XtraGrid.Columns.GridColumn colServer;
        private DevExpress.XtraGrid.Columns.GridColumn colTicketID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colDateTimeIssue;
        private DevExpress.XtraGrid.Columns.GridColumn colTakeOut;
        private DevExpress.XtraGrid.Columns.GridColumn colIsChange;
        private DevExpress.XtraGrid.Columns.GridColumn colIsCancel;
    }
}
