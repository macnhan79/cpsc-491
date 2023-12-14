using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010.Views;
using PhoMac.Main.POS.Views;

namespace PhoMac.Main.POS
{
    public partial class FrmTest : DevExpress.XtraEditors.XtraForm, IMainForm
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            //UCTest1 obj = new UCTest1();
            //obj.MdiParent = this;
            //obj.Show();
        }

        private void tabbedView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            if (e.Document == document1)
            {
                document1.Caption = "Document 1";
                e.Control = new TableEditView() { };
            }
            else if (e.Document == document2)
            {
                document2.Caption = "Document 2";
                e.Control = new Pos_MainView() { };
            }
        }

       public delegate bool ActivateDocument(BaseDocument doucment);



       public void ShowHome()
       {
           //tabbedView1.Controller.Activate(document1);
       }
    }
}