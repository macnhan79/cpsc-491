using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhoHa7.Library.Classes.Common;
using PhoHa7.Library.Froms;

namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCPassCode : XtraUserControlKira
    {
        string passCode = "";
        public UCPassCode()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            textEdit1.EditValue += "*";
            passCode += "0";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string str = textEdit1.EditValue.ToString();
            if (str.Length > 0)
            {
                textEdit1.EditValue = str.Substring(0, str.Length - 1);
            }

            if (passCode.Length > 0)
            {
                passCode = passCode.Substring(0, passCode.Length - 1);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (passCode == "")
            //{
            //    FormManager formManager = FormManager.getInstance();
            //    if (formManager._RibbonForm == null || formManager._RibbonForm.IsDisposed)
            //        formManager._RibbonForm = new Main();


            //    //Application.Run(formManager._RibbonForm);
            //    formManager._RibbonForm.Show();
            //    formManager._RibbonForm.Activate();
            //    this.Dispose();
            //}
            //else
            //{
            //    ClsBaoLoi.Loi("Mật khẩu không đúng");
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
