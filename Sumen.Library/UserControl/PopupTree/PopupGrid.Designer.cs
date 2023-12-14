namespace PhoHa7.Library.UserControl.PopupTree
{
    partial class PopupGrid
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
            this.dgKhoa = new DevExpress.XtraGrid.GridControl();
            this.gvKhoa = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Title = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PARENTID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgKhoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // dgKhoa
            // 
            this.dgKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgKhoa.Location = new System.Drawing.Point(0, 0);
            this.dgKhoa.MainView = this.gvKhoa;
            this.dgKhoa.Name = "dgKhoa";
            this.dgKhoa.Size = new System.Drawing.Size(190, 165);
            this.dgKhoa.TabIndex = 4;
            this.dgKhoa.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvKhoa});
            // 
            // gvKhoa
            // 
            this.gvKhoa.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Title,
            this.PARENTID});
            this.gvKhoa.GridControl = this.dgKhoa;
            this.gvKhoa.Name = "gvKhoa";
            this.gvKhoa.OptionsBehavior.Editable = false;
            this.gvKhoa.OptionsCustomization.AllowFilter = false;
            this.gvKhoa.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvKhoa.OptionsView.ShowGroupPanel = false;
            // 
            // Title
            // 
            this.Title.Caption = "Title";
            this.Title.FieldName = "ProductName";
            this.Title.Name = "Title";
            this.Title.Visible = true;
            this.Title.VisibleIndex = 0;
            // 
            // PARENTID
            // 
            this.PARENTID.FieldName = "ExpandCategoryID";
            this.PARENTID.Name = "PARENTID";
            // 
            // PopupGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgKhoa);
            this.Name = "PopupGrid";
            this.Size = new System.Drawing.Size(190, 165);
            ((System.ComponentModel.ISupportInitialize)(this.dgKhoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKhoa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.Views.Grid.GridView gvKhoa;
        private DevExpress.XtraGrid.Columns.GridColumn Title;
        private DevExpress.XtraGrid.Columns.GridColumn PARENTID;
        public DevExpress.XtraGrid.GridControl dgKhoa;
    }
}
