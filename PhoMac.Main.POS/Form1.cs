using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhoMac.Business.Data;
using PhoMac.Business.Presenter;
using PhoMac.Model;
using PhoMac.Main.POS.Views.UC;
using DevExpress.Skins;
using DevExpress.XtraEditors;

namespace PhoMac.Main.POS
{
    public partial class Form1 : XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            //ViewManager view = ViewManager.getInstance();
            //UCPanelLayoutTable panel = view.ListTableTab[0];
            //panel.Dock = DockStyle.Fill;
            //this.Controls.Add(panel);
        }

        private void pos_MainView1_Load(object sender, EventArgs e)
        {
            //ucPaymentType1.init();
          //  ucTable2.test();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ucTable1.test();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // ucTable2.TableID = 10;
            ucTable1.TableID = 10;
           // ucTable2.init();
            ucTable1.init();
        }




        //ucPanelProduct1.ProductID = 39;
        //ucPanelProduct2.ProductID = 726;
        //ucPanelProduct1.FrmClick += HandleCustomEvent;
        //void HandleCustomEvent(object sender, PhoMac.Main.POS.Views.UC.UCPanelProduct.FrmClickEventArgs frmClickInfo)
        //{
        //   string abc= frmClickInfo._name;
        //}











    }
}
