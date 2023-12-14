using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PhoHa7.Library.Froms.MsgBox
{
    public partial class FrmError : DevExpress.XtraEditors.XtraForm
    {
        string strPath = Application.StartupPath;
        Exception lexpDetail;
        Point p = new Point();
        ClsConfig config = new ClsConfig();
        public string emailTo = "support@cuscsoft.com";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsgShow"></param>
        public FrmError(string strMsgShow)
        {
            InitializeComponent();

            this.btnDetail.Visible = false;
            this.btnWrite.Visible = false;
            this.btnEmail.Visible = false;
            this.pnlDetail.Visible = false;

            ResizeForm(strMsgShow, this);
            if (strMsgShow == "")
            {
                this.txtMsg.Text = "Lỗi";
            }
            else
            {
                this.txtMsg.Text = strMsgShow;
            }
            this.Height = this.Height - pnlDetail.Height;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsgShow"></param>
        /// <param name="exDetail"></param>
        public FrmError(string strMsgShow, Exception exDetail)
        {
            InitializeComponent();
            this.lexpDetail = exDetail;
            ResizeForm(strMsgShow, this);
            if (strMsgShow == "")
            {
                this.txtMsg.Text = "Lỗi";
            }
            else
            {
                this.txtMsg.Text = strMsgShow;
            }
            this.Height = this.Height - pnlDetail.Height;
            this.rtxtDetail.Text = ExToString(exDetail);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (btnDetail.Text == "&Chi tiết")
            {
                btnDetail.Text = "Cơ &bản";
                this.pnlDetail.Visible = true;
                this.btnWrite.Visible = true;
                this.btnEmail.Visible = true;
                this.Height = this.Height + pnlDetail.Height;
            }
            else
            {
                btnDetail.Text = "&Chi tiết";
                this.pnlDetail.Visible = false;
                this.btnWrite.Visible = false;
                this.btnEmail.Visible = false;
                this.Height = this.Height - pnlDetail.Height;
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            folderDialog.SelectedPath = strPath;
            folderDialog.ShowDialog();
            strPath = folderDialog.SelectedPath;
            WriteToFile();
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            FrmEmail frmemail = new FrmEmail("Phòng Tổ chức Cán bộ - CTU", this.rtxtDetail.Text);
            frmemail.ShowDialog();
        }

        /// <summary>
        /// Ghi thông tin lỗi ra file
        /// </summary>
        private void WriteToFile()
        {
            System.IO.FileStream file;
            string fileName = strPath + "\\" + "ErrorDetail.log";
            if (!System.IO.File.Exists(fileName))
            {
                file = System.IO.File.Create(fileName);
                file.Close();
            }

            System.IO.StreamWriter wr = System.IO.File.AppendText(fileName);
            wr.Write(ExToString(lexpDetail) + "\n\n----------------------------------------------------\n\n");
            wr.Close();
        }

        /// <summary>
        /// Đổi một ngoại lệ thành chuỗi
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string ExToString(Exception ex)
        {
            StringBuilder formattedException = new StringBuilder();

            if (ex != null)
            {
                formattedException.Append(Environment.NewLine)
                    .Append("---------------------Error view---------------------")
                    .Append(Environment.NewLine)
                    .Append("----------------------------------------------------")
                    .Append(Environment.NewLine)
                    .Append(Environment.NewLine)
                    .Append("EXCEPTION INFORMATION")
                    .Append(Environment.NewLine)
                    .Append(Environment.NewLine)
                    .Append("Date/Time: ").Append(DateTime.Now.ToString("F")).Append(Environment.NewLine)
                    .Append("Type: ").Append(ex.GetType().FullName).Append(Environment.NewLine)
                    .Append("Message: ").Append(ex.Message).Append(Environment.NewLine)
                    .Append("Source: ").Append(ex.Source).Append(Environment.NewLine)
                    .Append("Target Method: ")
                    .Append(ex.TargetSite.ToString())
                    .Append(Environment.NewLine).Append(Environment.NewLine)
                    .Append("Call Stack:").Append(Environment.NewLine);

                StackTrace exceptionStack = new StackTrace(ex);

                for (int i = 0; i < exceptionStack.FrameCount; i++)
                {
                    StackFrame exceptionFrame = exceptionStack.GetFrame(i);

                    formattedException.Append("\t").Append("Method Name: ").Append(exceptionFrame.GetMethod().ToString()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("File Name: ").Append(exceptionFrame.GetFileName()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("Column: ").Append(exceptionFrame.GetFileColumnNumber()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("Line: ").Append(exceptionFrame.GetFileLineNumber()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("CIL Offset: ").Append(exceptionFrame.GetILOffset()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("Native Offset: ").Append(exceptionFrame.GetNativeOffset()).Append(Environment.NewLine)
                        .Append(Environment.NewLine);
                }

                formattedException.Append("Inner Exception(s)").Append(Environment.NewLine);

                Exception innerException = ex.InnerException;

                while (innerException != null)
                {
                    formattedException.Append("\t").Append("Exception: ")
                        .Append(innerException.GetType().FullName).Append(Environment.NewLine);
                    innerException = innerException.InnerException;
                }

                formattedException.Append(Environment.NewLine).Append("Custom Properties")
                    .Append(Environment.NewLine);

                Type exceptionType = typeof(Exception);

                foreach (PropertyInfo propertyInfo in ex.GetType().GetProperties())
                {
                    if (exceptionType.GetProperty(propertyInfo.Name) == null)
                    {
                        formattedException.Append("\t").Append(propertyInfo.Name).Append(": ")
                            .Append(propertyInfo.GetValue(ex, null))
                            .Append(Environment.NewLine);
                    }
                }
            }

            return formattedException.ToString();
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
            p.X = lenOfPart - 30;
            p.Y = numOfPart - 3;
            return p;
        }

        private void txtMsg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Display the appropriate link based on the value of the 
            // LinkData property of the Link object.
            string target = e.Link.LinkData as string;

            // If the value looks like a URL, navigate to it.
            // Otherwise, display it in a message box.
            if (null != target)
            {
                System.Diagnostics.Process.Start(target);
            }
        }

        private void FrmError_Load(object sender, EventArgs e)
        {
            int iBegin = txtMsg.Text.IndexOf("<url>");
            txtMsg.Text = txtMsg.Text.Replace("<url>", "");
            int iEnd = txtMsg.Text.IndexOf("</url>");
            txtMsg.Text = txtMsg.Text.Replace("</url>", "");
            if (iBegin > 0)
            {
                this.txtMsg.LinkArea = new LinkArea(iBegin, iEnd - iBegin);
                this.txtMsg.Links[0].LinkData = txtMsg.Text.Substring(iBegin, iEnd - iBegin);
            }
        }
    }
}