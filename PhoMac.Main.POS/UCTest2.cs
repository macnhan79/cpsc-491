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

namespace PhoMac.Main.POS
{
    public partial class UCTest2 : DevExpress.XtraEditors.XtraForm
    {
        public UCTest2()
        {
            InitializeComponent();
            

        }
      public  DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView TabbedView1;
        private void button1_Click(object sender, EventArgs e)
        {
            TabbedView1.Controller.Activate(TabbedView1.Documents[0]);
        }
    }
}