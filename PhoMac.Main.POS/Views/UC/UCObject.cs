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

namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCObject : XtraUserControlKira
    {
        public ObjectInfo ObjectInfos { get; set; }

        public UCObject()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            ObjectInfos = new ObjectInfo();
        }

        /// <summary>
        /// Must be declare ObjectInfo first
        /// </summary>
        public override void init()
        {
            base.init();
            lblName.Text = ObjectInfos.Name;
        }





        public class ObjectInfo
        {
            public int ID { get; set; }
            public string IDString { get; set; }
            public string Name { get; set; }
            public object ObjectReference { get; set; }
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            OnFrmClick(this, new FrmClickEventArgs(this));
            Selected = true;
        }

        public bool Selected
        {
            get
            {
                return IsSelected;
            }
            set
            {
                IsSelected = value;
                if (IsSelected)
                {
                    lblName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                    lblName.Appearance.BorderColor = Color.Red;
                }
                else
                {
                    lblName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    lblName.Appearance.BorderColor = Color.Transparent;
                }
            }
        }

        public Color BackgroundColor
        {
            get { return lblName.BackColor; }
            set { lblName.BackColor = value; }
        }

        public Color ForeColor
        {
            get { return lblName.ForeColor; }
            set { lblName.ForeColor = value; }
        }



    }
}
