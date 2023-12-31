using System;
using System.Net.Mail;
using System.Windows.Forms;
using PhoHa7.Library.Classes.Common;

namespace PhoHa7.Library.Froms.MsgBox
{
    public partial class FrmEmail : DevExpress.XtraEditors.XtraForm
    {
        string errorDetail = "";
        ClsConfig config = new ClsConfig();

        public FrmEmail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gửi thông tin lỗi từ người dùng về đơn vị
        /// </summary>
        /// <param name="TenCongTy">Tên công ty áp dụng</param>
        /// <param name="detail">Lỗi</param>
        /// <param name="ServerMail">Server mail để gủi mail</param>
        /// <param name="DiaChiMail">Địa chỉ gủi/nhận</param>
        public FrmEmail(string TenCongTy, string NoiDung)
        {
            InitializeComponent();
            this.txtCongTy.Text = TenCongTy;
            errorDetail = NoiDung;            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // change cursor into wait cursor
            Cursor cr = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                //sendMail();
                SmtpClient smtp = new SmtpClient(config.MailServer);
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(config.EmailSend, config.PassWord);
                smtp.Credentials = credentials;
                MailMessage mailMessage = new MailMessage(config.EmailSend, config.EmailRec);

                mailMessage.Subject = "[APP0903]Thông tin lỗi Chương trình Quản lý Kho Sumen";
                mailMessage.Body = this.txtCongTy.Text + "\n" + this.txtYKien.Text + "\n" + errorDetail;
                smtp.Send(mailMessage);
                this.Close();

                // back to normal cursor
                Cursor.Current = cr;
            }
            catch (Exception ex)
            {
                ClsBaoLoi.Loi("Lỗi", "Không gởi mail được", ex);
            }
        }       
    }
}