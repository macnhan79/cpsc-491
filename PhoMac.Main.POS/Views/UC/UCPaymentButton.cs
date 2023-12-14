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
using PhoMac.Model;

namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCPaymentButton : XtraUserControlKira
    {
        public string NamePayment
        {
            get { return simpleButton1.Text; }
            set
            {
                simpleButton1.Text = value;
            }
        }

        public Image ImageButton
        {
            get { return simpleButton1.Image; }
            set
            {
                simpleButton1.Image = value;
            }
        }

        decimal amount = 0;
        public decimal Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                labelControl1.Text = String.Format("{0:C}", amount);
                if (amount == 0)
                {
                    labelControl1.Appearance.ForeColor = System.Drawing.SystemColors.ActiveBorder;
                }
                else
                {
                    labelControl1.Appearance.ForeColor = System.Drawing.Color.LimeGreen;
                }
            }
        }

        public event EventHandler ButtonClick
        {
            add { simpleButton1.Click += value; }
            remove { simpleButton1.Click -= value; }
        }

        public PaymentType PayType { get; set; }

        public UCPaymentButton()
        {
            InitializeComponent();
        }


    }
}
