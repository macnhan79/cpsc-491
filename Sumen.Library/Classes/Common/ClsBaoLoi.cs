#region Main
//  File name    	:	ClsBaoLoi.cs
//  Purpose		    :	Thông báo lỗi chung trong hệ thống
//  Creater date	:	09-09-2009
//  Author		    :	Phạm Văn Duy
//  Version		    :	V1.0
//  Copyright		:	Cusc
#endregion

using System;
using PhoHa7.Library.Classes.Connection;
using PhoHa7.Library.Froms.MsgBox;
using System.Data.SqlClient;

namespace PhoHa7.Library.Classes.Common
{
    /// <summary>
    /// Lớp thông báo lỗi chung trong hệ thống
    /// </summary>
    public class ClsBaoLoi
    {
        //private static System.Data.OracleClient.OracleConnection conn;
        private static SqlConnection conn;
        /// <summary>
        /// Đưa ra MessageBox cảnh báo với MessageBoxIcon.Warning
        /// </summary>
        /// <param name="pThongBao">Câu cảnh báo</param>
        public static void CanhBao(string pThongBao)
        {
            MsgBox frmMsg = new MsgBox(pThongBao);
            frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.CanhBao;
            frmMsg.Text = "Thông báo";
            frmMsg.ShowDialog();
        }

        /// <summary>
        /// Hiển thị thông tin
        /// </summary>
        /// <param name="strTilteThongBao">Chuỗi tiêu đề thông báo</param>
        /// <param name="strMsg">Chuỗi thông tin cần thông báo</param>
        public static void ThongTin(string strTilteThongBao, string strMsg)
        {
            MsgBox frmMsg = new MsgBox(strMsg);
            frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.ThongTin;
            frmMsg.Text = strTilteThongBao;
            frmMsg.ShowDialog();
        }

        /// <summary>
        /// Hiển thị thông tin
        /// </summary>
        /// <param name="strMsg">Chuỗi thông tin cần thông báo</param>
        public static void ThongTin(string strMsg)
        {
            MsgBox frmMsg = new MsgBox(strMsg);
            frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.ThongTin;
            frmMsg.Text = "Thông tin";
            frmMsg.ShowDialog();
        }

        /// <summary>
        /// Yêu cầu xác nhận đồng ý hay không từ người dùng
        /// </summary>
        /// <param name="strTilteXacNhan">Chuỗi tiêu đề hộp thoại xác nhận</param>
        /// <param name="strMsg">Chuỗi thông báo</param>
        /// <returns>Chấp nhận: True; Không chập nhận: False</returns>
        public static bool XacNhan(string strTilteXacNhan, string strMsg)
        {
            FrmAccept frmMsg = new FrmAccept(strMsg);
            frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.Hoi;
            frmMsg.Text = strTilteXacNhan;
            frmMsg.ShowDialog();
            return frmMsg.bAccept;
        }

        /// <summary>
        /// Yêu cầu xác nhận đồng ý hay không từ người dùng
        /// </summary>
        /// <param name="strMsg">Chuỗi thông báo</param>
        /// <returns>Chấp nhận: True; Không chập nhận: False</returns>
        public static bool XacNhan(string strMsg)
        {
            FrmAccept frmMsg = new FrmAccept(strMsg);
            frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.Hoi;
            frmMsg.Text = "Xác nhận";
            frmMsg.ShowDialog();
            return frmMsg.bAccept;
        }

        /// <summary>
        /// Thông báo lỗi cho người dùng và ghi vào nhật ký ngoại lệ dp hệ thống phát sinh
        /// </summary>
        /// <param name="strTilteLoi">Chuỗi tiêu đề thông báo của hộp thoại lỗi</param>
        /// <param name="strMsgShow">Chuỗi thông báo sẽ hiển thị</param>
        /// <param name="expDetail">Ngoại lệ nhận được, sẽ được ghi nhật ký</param>
        public static void Loi(string strTilteLoi, string strMsgShow, Exception expDetail)
        {
            FrmError frmMsg = new FrmError(strMsgShow, expDetail);
            frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.Loi;
            if (expDetail.Message.IndexOf("ORA-12152: TNS:unable to send break message") != -1)
            {
                //ClsConnection.Conn = null;
                ClsConnection.MySqlConn = null;
                //conn = ClsConnection.Conn;
                conn = ClsConnection.MySqlConn;
                return;
            }
            frmMsg.Text = strTilteLoi;
            frmMsg.emailTo = "mac.nhan79@gmail.com";
            frmMsg.ShowDialog();
        }

        /// <summary>
        /// Thông báo lỗi cho người dùng và ghi vào nhật ký ngoại lệ dp hệ thống phát sinh
        /// </summary>        
        /// <param name="strMsgShow">Chuỗi thông báo sẽ hiển thị</param>
        /// <param name="expDetail">Ngoại lệ nhận được, sẽ được ghi nhật ký</param>
        public static void Loi(string strMsgShow, Exception expDetail)
        {
            try
            {
                expDetail = new ClsException(expDetail);
            }
            catch (ClsException ex)
            {
                FrmError frmMsg = new FrmError(ex.Message, expDetail);
                frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.Loi;
                frmMsg.Text = "Lỗi";
                frmMsg.emailTo = "mac.nhan79@gmail.com";
                frmMsg.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmError frmMsg = new FrmError(strMsgShow, expDetail);
                frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.Loi;
                frmMsg.Text = "Lỗi";
                frmMsg.emailTo = "mac.nhan79@gmail.com";
                frmMsg.ShowDialog();
            }
        }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>        
        /// <param name="strMsgShow">Chuỗi thông báo sẽ hiển thị</param>        
        public static void Loi(string strMsgShow)
        {
            FrmError frmMsg = new FrmError(strMsgShow);
            frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.Loi;
            frmMsg.ShowDialog();
        }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        /// <param name="strTilteLoi">Chuỗi tiêu đề thông báo của hộp thoại lỗi</param>
        /// <param name="strMsgShow">Chuỗi thông báo sẽ hiển thị</param>        
        public static void Loi(string strTilteLoi, string strMsgShow)
        {
            FrmError frmMsg = new FrmError(strMsgShow);
            frmMsg.ptbIcon.Image = PhoHa7.Library.Properties.Resources.Loi;
            frmMsg.Text = strTilteLoi;
            frmMsg.ShowDialog();
        }
    }
}
