namespace PhoMac.Main.POS
{
    partial class RibbonForm1
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
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgModules = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiGrid = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiGridCardView = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiSpreadsheet = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiWord = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiSnap = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiReports = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiPivot = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiCharts = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiMaps = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiScheduler = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiPdf = new DevExpress.XtraNavBar.NavBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(2713, 282);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 2036);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(2713, 62);
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbgModules;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgModules});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiGrid,
            this.nbiSpreadsheet,
            this.nbiWord,
            this.nbiSnap,
            this.nbiReports,
            this.nbiPivot,
            this.nbiCharts,
            this.nbiMaps,
            this.nbiScheduler,
            this.nbiGridCardView,
            this.nbiPdf});
            this.navBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInControl;
            this.navBarControl1.Location = new System.Drawing.Point(0, 282);
            this.navBarControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneGroupClientHeight = 320;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 308;
            this.navBarControl1.OptionsNavPane.ShowOverflowButton = false;
            this.navBarControl1.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl1.OptionsNavPane.ShowSplitter = false;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(308, 1754);
            this.navBarControl1.StoreDefaultPaintStyleName = true;
            this.navBarControl1.TabIndex = 3;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbgModules
            // 
            this.nbgModules.Caption = "WinForms Products";
            this.nbgModules.Expanded = true;
            this.nbgModules.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            this.nbgModules.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.nbgModules.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiGrid),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiGridCardView),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiSpreadsheet),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiWord),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiSnap),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiReports),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPivot),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiCharts),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiMaps),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiScheduler),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPdf)});
            this.nbgModules.Name = "nbgModules";
            this.nbgModules.NavigationPaneVisible = false;
            this.nbgModules.SelectedLinkIndex = 9;
            // 
            // nbiGrid
            // 
            this.nbiGrid.Caption = "Grid: Tasks";
            this.nbiGrid.Name = "nbiGrid";
            // 
            // nbiGridCardView
            // 
            this.nbiGridCardView.Caption = "Grid: Contacts";
            this.nbiGridCardView.Name = "nbiGridCardView";
            // 
            // nbiSpreadsheet
            // 
            this.nbiSpreadsheet.Caption = "Spreadsheet";
            this.nbiSpreadsheet.Name = "nbiSpreadsheet";
            // 
            // nbiWord
            // 
            this.nbiWord.Caption = "Word Processing";
            this.nbiWord.Name = "nbiWord";
            // 
            // nbiSnap
            // 
            this.nbiSnap.Caption = "WYSIWYG Reports";
            this.nbiSnap.Name = "nbiSnap";
            // 
            // nbiReports
            // 
            this.nbiReports.Caption = "Banded Reports";
            this.nbiReports.Name = "nbiReports";
            // 
            // nbiPivot
            // 
            this.nbiPivot.Caption = "Pivot Table";
            this.nbiPivot.Name = "nbiPivot";
            // 
            // nbiCharts
            // 
            this.nbiCharts.Caption = "Analytics";
            this.nbiCharts.Name = "nbiCharts";
            // 
            // nbiMaps
            // 
            this.nbiMaps.Caption = "Weather Map";
            this.nbiMaps.Name = "nbiMaps";
            // 
            // nbiScheduler
            // 
            this.nbiScheduler.Caption = "Scheduler";
            this.nbiScheduler.Name = "nbiScheduler";
            // 
            // nbiPdf
            // 
            this.nbiPdf.Caption = "Pdf Viewer";
            this.nbiPdf.Name = "nbiPdf";
            // 
            // RibbonForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2713, 2098);
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "RibbonForm1";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "RibbonForm1";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbgModules;
        private DevExpress.XtraNavBar.NavBarItem nbiGrid;
        private DevExpress.XtraNavBar.NavBarItem nbiGridCardView;
        private DevExpress.XtraNavBar.NavBarItem nbiSpreadsheet;
        private DevExpress.XtraNavBar.NavBarItem nbiWord;
        private DevExpress.XtraNavBar.NavBarItem nbiSnap;
        private DevExpress.XtraNavBar.NavBarItem nbiReports;
        private DevExpress.XtraNavBar.NavBarItem nbiPivot;
        private DevExpress.XtraNavBar.NavBarItem nbiCharts;
        private DevExpress.XtraNavBar.NavBarItem nbiMaps;
        private DevExpress.XtraNavBar.NavBarItem nbiScheduler;
        private DevExpress.XtraNavBar.NavBarItem nbiPdf;
    }
}