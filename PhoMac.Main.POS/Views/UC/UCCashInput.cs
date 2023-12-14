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
    public partial class UCCashInput : XtraUserControlKira
    {
        #region Property

        public decimal Amount
        {
            get;
            set;
        }

        #endregion

        string passCode = "";

        public UCCashInput(decimal amount)
        {
            InitializeComponent();
            Amount = amount;
        }

        public override void LoadForm()
        {
            base.LoadForm();
            loadExpressButtonCash();
        }

        void loadExpressButtonCash()
        {
            decimal[] list = new decimal[6];
            list[0] = Amount;
            list[1] = 5;
            if (Amount >= 1 && Amount <= 5)
            {
                list[2] = 5;
                list[3] = 10;
                list[4] = 20;
                list[5] = 100;
            }
            else if (Amount >= 5 && Amount <= 10)
            {
                list[2] = 10;
                list[3] = 20;
                list[4] = 50;
                list[5] = 100;
            }
            else if (Amount >= 10 && Amount <= 15)
            {
                list[1] = 15;
                list[2] = 20;
                list[3] = 40;
                list[4] = 50;
                list[5] = 100;
            }
            else if (Amount >= 15 && Amount <= 20)
            {
                list[1] = 20;
                list[2] = 30;
                list[3] = 40;
                list[4] = 50;
                list[5] = 100;
            }
            else if (Amount >= 20 && Amount <= 25)
            {
                list[1] = 25;
                list[2] = 30;
                list[3] = 40;
                list[4] = 50;
                list[5] = 100;
            }
            else if (Amount >= 25 && Amount <= 30)
            {
                list[1] = 30;
                list[2] = 40;
                list[3] = 50;
                list[4] = 60;
                list[5] = 100;
            }
            else //if (Amount >= 30 && Amount <= 40)
            {
                list[1] = 30;
                list[2] = 40;
                list[3] = 50;
                list[4] = 60;
                list[5] = 100;
            }
            this.windowsUIButtonPanel1.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton(String.Format("{0:C}", list[0]), global::PhoMac.Main.POS.Properties.Resources.Accept, -1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, -1),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton(String.Format("{0:C}", list[1]), global::PhoMac.Main.POS.Properties.Resources.Accept, -1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, -1),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton(String.Format("{0:C}", list[2]), global::PhoMac.Main.POS.Properties.Resources.Accept, -1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, -1),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton(String.Format("{0:C}", list[3]), global::PhoMac.Main.POS.Properties.Resources.Accept, -1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, -1),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton(String.Format("{0:C}", list[4]), global::PhoMac.Main.POS.Properties.Resources.Accept, -1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, -1),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton(String.Format("{0:C}", list[5]), global::PhoMac.Main.POS.Properties.Resources.Accept, -1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, -1)});
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
