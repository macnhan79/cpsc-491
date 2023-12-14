using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhoHa7.Library.Froms;
using DevExpress.XtraBars;
using PhoMac.Model.Data;
using PhoMac.Model;
using PhoMac.Main.POS.Data;


namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCPaymentType : XtraUserControlKira
    {
        PopupControlContainer popupControlContainer1;
        DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;

        bool isLoad = false;

        public UCPaymentType()
        {
            InitializeComponent();
        }

        public override void LoadForm()
        {
            base.LoadForm();
            //init();
        }

        public override void init()
        {
            base.init();
            if (!DesignMode)
            {
                loadPaymentType();
                initPopupCash();
                isLoad = true;
            }
            if (!isLoad)
            {

            }
        }

        void initTableLayout(int colCount)
        {
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.ColumnCount = colCount;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.RowCount = 1;

            float percentage = 100 / colCount;
            for (int i = 0; i < colCount; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentage));
            }

            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1);
        }

        void loadPaymentType()
        {
            //using (PhoMac.Model.Entities entity = new PhoMac.Model.Entities())
            //{
            Dao dao = new Dao();
            var list = dao.FindByMultiColumnAnd<PaymentType>(new[] { "Active" }, true).ToList();
            //var list = (from p in entity.PaymentTypes where p.Active == true select p).ToList();

            initTableLayout(list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                //create button
                UCPaymentButton btn = new UCPaymentButton();
                btn.Name = list[i].PayTypeStr;
                btn.ImageButton = ImageManager.GetImage(list[i].ImgURL);
                btn.Dock = DockStyle.Fill;
                btn.ButtonClick += btn_ButtonClick;

                //add button to layout
                tableLayoutPanel1.Controls.Add(btn, i, 0);
            }
            //  }
        }

        void btn_ButtonClick(object sender, EventArgs e)
        {
            popupControlContainer1.ShowPopup(Cursor.Position);
            //popupControlContainer1.HidePopup();
        }

        void initPopupCash()
        {
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            popupControlContainer1 = new DevExpress.XtraBars.PopupControlContainer();
            this.popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            UCCashInput passCode = new UCCashInput(0);
            passCode.Dock = DockStyle.Fill;
            this.popupControlContainer1.Controls.Add(passCode);
            this.popupControlContainer1.Location = new System.Drawing.Point(7, 136);
            this.popupControlContainer1.Name = "popupControlContainer1";
            this.popupControlContainer1.Ribbon = this.ribbonControl1;
            this.popupControlContainer1.Size = passCode.Size;
            this.popupControlContainer1.TabIndex = 4;
            this.popupControlContainer1.Visible = false;
        }

        private void LabelControl3_Click(object sender, EventArgs e)
        {

        }
    }
}
