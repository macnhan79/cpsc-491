namespace PhoHa7
{
    partial class Kitchen
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gControlImportant = new DevExpress.XtraGrid.GridControl();
            this.gViewFilter = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gControlGroup = new DevExpress.XtraGrid.GridControl();
            this.gViewGroup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colQtyGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.btnMin = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuit = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.btnMute = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetting = new DevExpress.XtraEditors.SimpleButton();
            this.panelTicketList = new DevExpress.XtraEditors.PanelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gControlImportant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gViewFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gViewGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTicketList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gControlImportant);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(161, 117);
            this.panelControl2.TabIndex = 8;
            // 
            // gControlImportant
            // 
            this.gControlImportant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gControlImportant.Location = new System.Drawing.Point(2, 2);
            this.gControlImportant.MainView = this.gViewFilter;
            this.gControlImportant.Name = "gControlImportant";
            this.gControlImportant.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2});
            this.gControlImportant.Size = new System.Drawing.Size(157, 113);
            this.gControlImportant.TabIndex = 0;
            this.gControlImportant.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gViewFilter});
            // 
            // gViewFilter
            // 
            this.gViewFilter.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gViewFilter.Appearance.Row.Options.UseFont = true;
            this.gViewFilter.Appearance.Row.Options.UseTextOptions = true;
            this.gViewFilter.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gViewFilter.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colQty,
            this.gridColumn2});
            this.gViewFilter.GridControl = this.gControlImportant;
            this.gViewFilter.Name = "gViewFilter";
            this.gViewFilter.OptionsSelection.MultiSelect = true;
            this.gViewFilter.OptionsSelection.UseIndicatorForSelection = false;
            this.gViewFilter.OptionsView.RowAutoHeight = true;
            this.gViewFilter.OptionsView.ShowGroupPanel = false;
            this.gViewFilter.RowHeight = 45;
            this.gViewFilter.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // colQty
            // 
            this.colQty.Caption = "Số lượng";
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.OptionsColumn.AllowEdit = false;
            this.colQty.OptionsColumn.ReadOnly = true;
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 0;
            this.colQty.Width = 39;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên";
            this.gridColumn2.ColumnEdit = this.repositoryItemMemoEdit2;
            this.gridColumn2.FieldName = "Description";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 125;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl1.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Appearance.Options.UseBorderColor = true;
            this.panelControl1.Controls.Add(this.gControlGroup);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(161, 329);
            this.panelControl1.TabIndex = 7;
            // 
            // gControlGroup
            // 
            this.gControlGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gControlGroup.Location = new System.Drawing.Point(2, 2);
            this.gControlGroup.MainView = this.gViewGroup;
            this.gControlGroup.Name = "gControlGroup";
            this.gControlGroup.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gControlGroup.Size = new System.Drawing.Size(157, 325);
            this.gControlGroup.TabIndex = 0;
            this.gControlGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gViewGroup});
            // 
            // gViewGroup
            // 
            this.gViewGroup.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gViewGroup.Appearance.Row.Options.UseFont = true;
            this.gViewGroup.Appearance.Row.Options.UseTextOptions = true;
            this.gViewGroup.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gViewGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colQtyGroup,
            this.colNameGroup});
            this.gViewGroup.GridControl = this.gControlGroup;
            this.gViewGroup.Name = "gViewGroup";
            this.gViewGroup.OptionsSelection.MultiSelect = true;
            this.gViewGroup.OptionsSelection.UseIndicatorForSelection = false;
            this.gViewGroup.OptionsView.RowAutoHeight = true;
            this.gViewGroup.OptionsView.ShowGroupPanel = false;
            this.gViewGroup.RowHeight = 45;
            this.gViewGroup.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // colQtyGroup
            // 
            this.colQtyGroup.Caption = "Số lượng";
            this.colQtyGroup.FieldName = "Quality";
            this.colQtyGroup.Name = "colQtyGroup";
            this.colQtyGroup.OptionsColumn.AllowEdit = false;
            this.colQtyGroup.OptionsColumn.ReadOnly = true;
            this.colQtyGroup.Visible = true;
            this.colQtyGroup.VisibleIndex = 0;
            this.colQtyGroup.Width = 39;
            // 
            // colNameGroup
            // 
            this.colNameGroup.Caption = "Tên";
            this.colNameGroup.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNameGroup.FieldName = "Description";
            this.colNameGroup.Name = "colNameGroup";
            this.colNameGroup.OptionsColumn.AllowEdit = false;
            this.colNameGroup.OptionsColumn.ReadOnly = true;
            this.colNameGroup.Visible = true;
            this.colNameGroup.VisibleIndex = 1;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnMin.Appearance.Options.UseFont = true;
            this.btnMin.Image = global::PhoHa7.Properties.Resources.minimize;
            this.btnMin.Location = new System.Drawing.Point(781, 17);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(102, 38);
            this.btnMin.TabIndex = 6;
            this.btnMin.Text = "ẨN";
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnQuit.Appearance.Options.UseFont = true;
            this.btnQuit.Image = global::PhoHa7.Properties.Resources.error;
            this.btnQuit.Location = new System.Drawing.Point(889, 17);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(102, 38);
            this.btnQuit.TabIndex = 5;
            this.btnQuit.Text = "THOÁT";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1174, 463);
            this.splitContainerControl1.SplitterPosition = 1000;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            this.splitContainerControl1.SplitterPositionChanged += new System.EventHandler(this.splitContainerControl1_SplitterPositionChanged);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl6);
            this.layoutControl1.Controls.Add(this.panelTicketList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(546, 183, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1000, 463);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl6
            // 
            this.panelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl6.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.panelControl6.Appearance.BackColor2 = System.Drawing.Color.Aqua;
            this.panelControl6.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.panelControl6.Appearance.Options.UseBackColor = true;
            this.panelControl6.Appearance.Options.UseBorderColor = true;
            this.panelControl6.Controls.Add(this.btnMute);
            this.panelControl6.Controls.Add(this.btnSetting);
            this.panelControl6.Controls.Add(this.btnMin);
            this.panelControl6.Controls.Add(this.btnQuit);
            this.panelControl6.Location = new System.Drawing.Point(2, 396);
            this.panelControl6.MinimumSize = new System.Drawing.Size(0, 65);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(996, 65);
            this.panelControl6.TabIndex = 1;
            // 
            // btnMute
            // 
            this.btnMute.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnMute.Appearance.Options.UseFont = true;
            this.btnMute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMute.Image = global::PhoHa7.Properties.Resources.volume_up_interface_symbol;
            this.btnMute.Location = new System.Drawing.Point(118, 17);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(102, 38);
            this.btnMute.TabIndex = 8;
            this.btnMute.Text = "Mở tiếng";
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetting.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnSetting.Appearance.Options.UseFont = true;
            this.btnSetting.Image = global::PhoHa7.Properties.Resources.setting;
            this.btnSetting.Location = new System.Drawing.Point(10, 17);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(102, 38);
            this.btnSetting.TabIndex = 7;
            this.btnSetting.Text = "Cài đặt";
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // panelTicketList
            // 
            this.panelTicketList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTicketList.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelTicketList.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.panelTicketList.Appearance.Options.UseBackColor = true;
            this.panelTicketList.Location = new System.Drawing.Point(2, 2);
            this.panelTicketList.Name = "panelTicketList";
            this.panelTicketList.Size = new System.Drawing.Size(996, 390);
            this.panelTicketList.TabIndex = 0;
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
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1000, 463);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelTicketList;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1000, 394);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl6;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 394);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 69);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(24, 69);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1000, 69);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.splitContainerControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(169, 463);
            this.panelControl3.TabIndex = 0;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(2, 2);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.panelControl4);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl5);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(165, 459);
            this.splitContainerControl2.SplitterPosition = 333;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.panelControl1);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(165, 333);
            this.panelControl4.TabIndex = 0;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.panelControl2);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(165, 121);
            this.panelControl5.TabIndex = 0;
            // 
            // Kitchen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 463);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "Kitchen";
            this.Text = "Kitchen";
            this.Load += new System.EventHandler(this.Kitchen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gControlImportant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gViewFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gViewGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelTicketList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TicketListView ticketListView1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.SimpleButton btnMin;
        private DevExpress.XtraEditors.SimpleButton btnQuit;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gControlImportant;
        private DevExpress.XtraGrid.Views.Grid.GridView gViewFilter;
        private DevExpress.XtraGrid.GridControl gControlGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gViewGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colNameGroup;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelTicketList;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnSetting;
        private DevExpress.XtraEditors.SimpleButton btnMute;
    }
}