using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Enum;
using PhoHa7.Library.Classes.Connection;
using PhoHa7.Library.Classes.Common;

namespace PhoMac.Main.PhoMac_System
{
    public partial class Frm_Users_Edit : DevExpress.XtraEditors.XtraForm
    {
        private EnumFormStatus _enumFormStatus;
        SqlHelper sqlHelper;
        public int EmployeeID { get; set; }

        public EnumFormStatus EnumFormStatus
        {
            get { return _enumFormStatus; }
            set
            {
                _enumFormStatus = value;
                if (_enumFormStatus == EnumFormStatus.Add)
                {
                    buttonsArray1.btnSua.Enabled = false;
                    buttonsArray1.btnThem.Enabled = true;
                }
                else if (_enumFormStatus == EnumFormStatus.Modify)
                {
                    buttonsArray1.btnSua.Enabled = true;
                    buttonsArray1.btnThem.Enabled = false;
                }
            }
        }

        public Frm_Users_Edit()
        {
            InitializeComponent();
            sqlHelper = new SqlHelper();
        }

        private void Frm_Users_Edit_Load(object sender, EventArgs e)
        {
            if (_enumFormStatus == EnumFormStatus.Add)
            {
                txtName.EditValue = null;
                txtPassword.EditValue = null;
                txtContact.EditValue = null;
                txtPhone.EditValue = null;
                ckActive.EditValue = CheckState.Checked;
            }
            else if (_enumFormStatus == EnumFormStatus.Modify)
            {
                string sql = "select * from Employees where EmployeeID = @EmployeeID";
                DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@EmployeeID" }, new object[] { EmployeeID });
                //fill text box
                txtName.Text = dt.Rows[0]["FullName"].ToString();
                txtPassword.Text = dt.Rows[0]["SecurityCode"].ToString();
                txtContact.Text = dt.Rows[0]["Address"].ToString();
                txtPhone.Text = dt.Rows[0]["CellPhone"].ToString();
                ckActive.EditValue = dt.Rows[0]["Active"];
            }
        }

        private void buttonsArray1_btnEventAdd_click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (_enumFormStatus == EnumFormStatus.Add)
                {
                    //add
                    string sql = "INSERT INTO [Employees] ([Active],[FullName],[Address],[CellPhone],[SecurityCode])"
                                    + "VALUES (@Active,@FullName,@Address,@CellPhone,@SecurityCode)";
                    int count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text,
                        new object[] { "@Active", "@FullName", "@Address", "@CellPhone", "@SecurityCode" },
                        new object[] { ckActive.EditValue, txtName.EditValue, txtContact.EditValue, txtPhone.EditValue, txtPassword.EditValue });
                    if (count > 0)
                    {
                        ClsMsgBox.ThongTin("Thêm mới tài khoản thành công");
                        this.EnumFormStatus = EnumFormStatus.Modify;
                    }
                    else
                    {
                        ClsMsgBox.ThongTin("Có lỗi xảy ra. Vui lòng kiểm tra lại.");
                    }
                }
            }
        }

        private void buttonsArray1_btnEventClose_click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonsArray1_btnEventUpdate_click(object sender, EventArgs e)
        {
            update(); 
        }

        bool Validation()
        {
            dxErrorProvider1.ClearErrors();
            if (txtName.EditValue == null || txtName.EditValue == "")
            {
                dxErrorProvider1.SetError(txtName, "Vui lòng nhập tài khoản");
                return false;
            }
            else if (txtPassword.EditValue == null || txtPassword.EditValue == "")
            {
                dxErrorProvider1.SetError(txtPassword, "Vui lòng nhập mật khẩu");
                return false;
            }
            return true;

            //if (_enumFormStatus == EnumFormStatus.Add)
            //{
            //    if (txtPassword.EditValue == null || txtPassword.EditValue == "")
            //    {
            //        dxErrorProvider1.SetError(txtPassword, "Vui lòng nhập mật khẩu");
            //        return false;
            //    }
            //}
        }

        private void update()
        {
            if (Validation())
            {
                if (_enumFormStatus == EnumFormStatus.Modify)
                {
                    string sql = "update [Employees] set [Active]=@Active,[FullName]=@FullName,[Address]=@Address,[CellPhone]=@CellPhone,[SecurityCode]=@SecurityCode"
                                    + " where EmployeeID = @EmployeeID";
                    int count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text,
                        new object[] { "@Active", "@FullName", "@Address", "@CellPhone", "@SecurityCode", "@EmployeeID" },
                        new object[] { ckActive.EditValue, txtName.EditValue, txtContact.EditValue, txtPhone.EditValue, txtPassword.EditValue, EmployeeID });
                    if (count > 0)
                    {
                        ClsMsgBox.ThongTin("Cập nhật tài khoản thành công.");
                        this.EnumFormStatus = EnumFormStatus.Modify;
                    }
                    else
                    {
                        ClsMsgBox.ThongTin("Có lỗi xảy ra. Vui lòng kiểm tra lại.");
                    }
                }
            }
        }


    }
}