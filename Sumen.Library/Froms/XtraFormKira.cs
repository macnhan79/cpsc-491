using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Common;

namespace PhoHa7.Library.Froms
{
    public class XtraFormKira : XtraForm
    {
        private System.ComponentModel.IContainer components = null;
        private XtraUserControlKira _xtraUserControl;
        public XtraUserControlKira XtraUserControl
        {
            get { return _xtraUserControl; }
            set { _xtraUserControl = value; }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public XtraFormKira()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.FormClosing += XtraFormKira_FormClosing;
        }

        public override void Refresh()
        {
            _xtraUserControl.Refresh();
            base.Refresh();
        }


        void XtraFormKira_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_xtraUserControl != null && _xtraUserControl.IsChange)
            {
                if (!ClsMsgBox.XacNhanDongTab())
                {
                    e.Cancel = true;
                    return;
                }
                this.Dispose();
            }
        }

        public void LoadUc<T>()
        {
            _xtraUserControl = Activator.CreateInstance<T>() as XtraUserControlKira;
            if (_xtraUserControl != null)
            {
                _xtraUserControl.Dock = DockStyle.Fill;
                Text = _xtraUserControl.Text;
                Controls.Add(_xtraUserControl);
                _xtraUserControl.Disposed += _xtraUserControl_Disposed;
            }
        }

        void _xtraUserControl_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormKira));
            this.SuspendLayout();
            // 
            // XtraFormKira
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XtraFormKira";
            this.ResumeLayout(false);

        }



    }
}
