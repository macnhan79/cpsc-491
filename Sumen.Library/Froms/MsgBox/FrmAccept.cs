using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhoHa7.Library.Froms.MsgBox
{
    public partial class FrmAccept : DevExpress.XtraEditors.XtraForm
    {
        public bool bAccept = false;
        Point p = new Point();
        ClsConfig config = new ClsConfig();

        public FrmAccept(string strMessage)
        {
            InitializeComponent();
            ResizeForm(strMessage, this);
            if (strMessage == "")
            {
                this.txtMsg.Text = "Xác nhận";
            }
            else
            {
                this.txtMsg.Text = strMessage;
            }
        }

        /// <summary>
        /// Thay đổi kích thướt fom theo thông tin đưa vào
        /// </summary>
        /// <param name="strMessage">Chuỗi hiện thị</param>
        /// <param name="frm">Đối tượng form tham chiếu</param>
        private void ResizeForm(string strMessage, Form frm)
        {
            p = getSize(strMessage);
            if (p.X > 0)
            {
                if ((frm.Width + p.X * 4) < config.MaxWidth)
                {
                    frm.Width = frm.Width + p.X * 4;
                }
                else
                {
                    frm.Width = config.MaxWidth;
                    p.Y = p.Y + 1;
                }
            }

            if (p.Y > 0)
            {
                if ((frm.Height + p.Y * 16) < config.MaxHeight)
                {
                    frm.Height = frm.Height + p.Y * 16;
                }
                else
                {
                    frm.Height = config.MaxHeight;
                }
            }            
        }

        /// <summary>
        /// Xác đinh độ rộng  của hộp thoại, mượn lớp point để lưu giữ độ x = rông, y = chieu cao
        /// </summary>
        /// <param name="str">Chuỗi thông báo</param>
        private System.Drawing.Point getSize(string str)
        {
            System.Drawing.Point p = new System.Drawing.Point();
            int numOfPart = 0;
            int lenOfPart = 0;
            string[] lstr;            
            lstr = str.Split('\n');
            numOfPart = lstr.Length;

            for (int i = 0; i < numOfPart; i++)
            {
                if (lstr[i].Length > lenOfPart)
                {
                    lenOfPart = lstr[i].Length;
                }
            }
            p.X = lenOfPart - 25;
            p.Y = numOfPart - 3;
            return p;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.bAccept = true;
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}