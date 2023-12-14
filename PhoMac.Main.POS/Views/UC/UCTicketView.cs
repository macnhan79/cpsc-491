using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCTicketView : UserControl
    {
        PopupControlContainer popupControlContainer1;
        DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;

        public UCTicketView()
        {
            InitializeComponent();
            initPopupCash();
        }

        private void btnFastInput_Click(object sender, EventArgs e)
        {
            popupControlContainer1.ShowPopup(Cursor.Position);
        }

        void initPopupCash()
        {
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            popupControlContainer1 = new DevExpress.XtraBars.PopupControlContainer();
            RibbonStatusBar ribbonStatusBar2 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            ribbonControl1.StatusBar = ribbonStatusBar2;
            this.popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            UCKeypadItem passCode = new UCKeypadItem();
            passCode.Dock = DockStyle.Fill;
            this.popupControlContainer1.Controls.Add(passCode);
            this.popupControlContainer1.Location = new System.Drawing.Point(7, 136);
            this.popupControlContainer1.Name = "popupControlContainer1";
            this.popupControlContainer1.Ribbon = this.ribbonControl1;
            this.popupControlContainer1.Size = new Size(420, 300);
            this.popupControlContainer1.TabIndex = 4;
            this.popupControlContainer1.Visible = false;
        }


    }
}
