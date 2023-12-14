using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Froms.Connection;
using System.Data.SqlClient;

namespace PhoHa7.Library.Classes.Common
{
    public class ClsMsgBox
    {

        public static void ThongTin(string info)
        {
            XtraMessageBox.Show(info, "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void CanhBao(string info)
        {
            XtraMessageBox.Show(info, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static bool XacNhan(string info)
        {
            return (XtraMessageBox.Show(info, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }

        public static bool XacNhanXoaThongTin()
        {
            return (XtraMessageBox.Show("Các dữ liệu đã bị xóa sẽ không thể phục hồi được nữa.\nBạn có chắc muốn xóa dữ liệu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }

        public static bool XacNhanThemThongTinCBThoiViec()
        {
            return (XtraMessageBox.Show("Cán bộ đã thôi việc - nghỉ hưu.\nBạn có chắc muốn thêm hoặc cập nhật thông tin cho cán bộ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }

        public static bool XacNhanChuyenTab()
        {
            return (XtraMessageBox.Show("Bạn có muốn lưu dữ liệu trước khi chuyển sang tab khác không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }

        public static bool XacNhanDongTab()
        {
            return (XtraMessageBox.Show("Dữ liệu có thay đổi, bạn có chắc đóng tab không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }

        public static void Loi(string info)
        {
            XtraMessageBox.Show(info, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Thong bao loi
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="justOK"></param>
        /// <returns>1 la ghi log loi. 0 la khong ghi log</returns>
        public static int LoiChung(Exception ex, bool justOK = false)
        {
            string strResult = "Lỗi khi lưu thông tin. " + ex.Message;
            int reload = 0;
            MessageBoxIcon errorType = MessageBoxIcon.Error;
            SqlException myEx = null;
            if (ex.InnerException != null)
            {
                if (ex.InnerException is SqlException)
                {
                    myEx = ex.InnerException as SqlException;
                }
                if (ex.InnerException.InnerException != null)
                {
                    if (ex.InnerException.InnerException is SqlException)
                    {
                        myEx = ex.InnerException.InnerException as SqlException;
                    }
                }
            }

            if (myEx != null)
            {
                if (myEx.Number == 1451)
                {
                    errorType = MessageBoxIcon.Information;
                    strResult = "Thông tin đã được sử dụng bạn không thể xóa.\nChương trình sẽ tải lại dữ liệu mới?";
                }

                else if (myEx.Number == 1217)
                {
                    errorType = MessageBoxIcon.Information;
                    strResult = "Thông tin đã được thay đổi bởi người dùng khác.\nChương trình sẽ tải lại dữ liệu mới?";
                }

                else if (myEx.Number == 1042)
                {
                    errorType = MessageBoxIcon.Question;
                    strResult = "Không thể kết nối đến máy chủ.\nXin kiểm tra lại đường truyền.";
                }
                else if (myEx.Number == 1062)
                {
                    errorType = MessageBoxIcon.Information;
                    strResult = "Bị trùng khóa.\nXin kiểm tra lại.";
                }
            }
            else
            {
                if (ex.Message.StartsWith("The specified named connection is either not found in the configuration, not intended to be used with the EntityClient provider, or not valid.") || ex.Message.StartsWith("The underlying provider failed on Open"))
                {
                    FrmCauHinhCSDL frm = new FrmCauHinhCSDL();
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ThongTin("Thay đổi cấu hình thành công, vui lòng thoát chương trình để cập nhật lại thông tin.");
                        Application.Exit();
                    }
                }
                else
                {
                    errorType = MessageBoxIcon.Error;
                    strResult = "Hệ thống phát sinh sự cố.\nVui lòng liên hệ quản trị hệ thống để được hỗ trợ.\n" + ex.Message + "Thông báo";
                    reload = 1;
                }
            }

            if (errorType == MessageBoxIcon.Information)
            {
                XtraMessageBox.Show(strResult.ToString(), "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (errorType == MessageBoxIcon.Question)
            {
                if (XtraMessageBox.Show(strResult.ToString(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    FrmCauHinhCSDL frmChSQL = new FrmCauHinhCSDL();
                    frmChSQL.ShowDialog();
                }
            }
            else
            {
                XtraMessageBox.Show(strResult.ToString(), "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return reload;

        }
    }
}
