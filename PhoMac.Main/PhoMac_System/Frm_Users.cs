using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Connection;
using PhoHa7.Library.Enum;
using PhoHa7.Library.Classes.Common;
using PhoHa7.Library.Froms;

namespace PhoMac.Main.PhoMac_System
{
    public partial class Frm_Users : XtraUserControlKira
    {
        SqlHelper sqlHelper;
        Frm_Users_Edit _frmUserEdit;
        int EmployeeID { get; set; }

        public Frm_Users()
        {
            InitializeComponent();
            sqlHelper = new SqlHelper();
        }

        private void buttonsArray1_btnEventAdd_click(object sender, EventArgs e)
        {
            if (_frmUserEdit == null)
            {
                _frmUserEdit = new Frm_Users_Edit();
                _frmUserEdit.EnumFormStatus = EnumFormStatus.Add;
                _frmUserEdit.ShowDialog();
            }
            else
            {
                _frmUserEdit.EnumFormStatus = EnumFormStatus.Add;
                _frmUserEdit.ShowDialog();
            }
        }

        private void buttonsArray1_btnEventClose_click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonsArray1_btnEventDelete_click(object sender, EventArgs e)
        {
            string sUsername = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colID) + string.Empty;
            {
                if (ClsMsgBox.XacNhanXoaThongTin())
                {
                    //sql delete
                    string sql = "delete from Employees where EmployeeID = @EmployeeID";
                    int count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@EmployeeID" }, new object[] { EmployeeID });
                    if (count > 0)
                    {
                        ClsMsgBox.ThongTin("Xóa nhân viên thành công,");
                        //if (ClsPublic.SYSTEM_WRITELOG == "1")
                        //{
                        //    SysLogPresenter log = new SysLogPresenter();
                        //    log.Add(FormCode, EnumFormStatus.Delete, ClsPublic.User.User_Username, "");
                        //}
                    }
                }
            }
        }

        private void buttonsArray1_btnEventUpdate_click(object sender, EventArgs e)
        {
            string sUsername = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colID) + string.Empty;
            {
                //user users = _userPresenter.GetById(sUsername);
                if (sUsername != null)
                {
                    if (_frmUserEdit == null)
                    {
                        _frmUserEdit = new Frm_Users_Edit();
                        _frmUserEdit.EmployeeID = Convert.ToInt32(sUsername);
                        _frmUserEdit.EnumFormStatus = EnumFormStatus.Modify;
                        _frmUserEdit.ShowDialog();
                    }
                    else
                    {
                        _frmUserEdit.EmployeeID = Convert.ToInt32(sUsername);
                        _frmUserEdit.EnumFormStatus = EnumFormStatus.Modify;
                        _frmUserEdit.ShowDialog();
                    }
                }
                else
                {
                    ClsMsgBox.Loi("Không tìm thấy nhân viên.");
                }
            }
        }

        private void Frm_Users_Load(object sender, EventArgs e)
        {
            string sql = "select * from Employees";
            gridControl1.DataSource = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EmployeeID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colID));
        }
    }
}