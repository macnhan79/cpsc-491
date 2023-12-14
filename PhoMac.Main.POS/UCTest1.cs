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
using DevExpress.XtraEditors;

namespace PhoMac.Main.POS
{
    public partial class UCTest1 : XtraForm
    {
        public UCTest1()
        {
            InitializeComponent();
        }
      public  DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView TabbedView1;
        private void button1_Click(object sender, EventArgs e)
        {
            TabbedView1.Controller.Activate(TabbedView1.Documents[1]);
        }
    }
}
