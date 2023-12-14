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
using PhoHa7.Library.Enum;
using DevExpress.Utils;
using PhoHa7.Library.Classes.Connection;
using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Classes.Common;

namespace PhoMac.Main.GUI
{
    public partial class Frm_Product : XtraUserControlKira
    {
        private EnumFormCode _fromCode = EnumFormCode.FrmCategory;

        private Frm_Product_Edit _frmProductCategoryEdit;
        private WaitDialogForm _wait;
        private EnumFormStatus _enumForm;


        public Frm_Product()
        {
            InitializeComponent();
            //buttonsArray1.capQuyen(new Permission(_fromCode.ToString(), ClsPublic.User.User_Username));
            //_categoryPresenter = new CategoryPresenter();
            //if (ClsPublic.SYSTEM_WRITELOG == "1")
            //{
            //    SysLogPresenter log = new SysLogPresenter();
            //    log.Add(_fromCode, EnumFormStatus.View, "", "");
            //}
            this.Disposed += Frm_Customer_Type_Disposed;
        }

        void Frm_Customer_Type_Disposed(object sender, System.EventArgs e)
        {
            //ClsPublic.WriteLog(_fromCode, EnumFormStatus.Modify, "", "");
        }

        public EnumFormStatus EnumForm
        {
            get { return _enumForm; }
            set
            {
                _enumForm = value;
            }
        }

        private void Frm_Product_Category_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        public override void LoadForm()
        {
            SqlHelper sqlHelper = new SqlHelper();
            string sql = "select p.*,t.* from Products p left join PhoHa7_ProductType t on p.MType = t.ProductTypeCode";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
            treeList1.DataSource = dt;
            treeList1.ExpandAll();
            base.LoadForm();
        }

        private void buttonsArray1_btnEventAdd_click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (_frmProductCategoryEdit == null)
            {
                _frmProductCategoryEdit = new Frm_Product_Edit();
                _frmProductCategoryEdit.EnumStatus = EnumFormStatus.Add;
                SplashScreenManager.CloseForm();
                _frmProductCategoryEdit.ShowDialog();
            }
            else
            {
                _frmProductCategoryEdit.EnumStatus = EnumFormStatus.Add;
                SplashScreenManager.CloseForm();
                _frmProductCategoryEdit.ShowDialog();
            }
            Refresh();
        }

        private void buttonsArray1_btnEventDelete_click(object sender, EventArgs e)
        {
            object id = treeList1.FocusedNode[treeList1.KeyFieldName];
            if (id != null && id != "")
            {
                if (ClsMsgBox.XacNhanXoaThongTin())
                {
                    //delete sql
                    SqlHelper sqlhelper = new SqlHelper();
                    string sql = "delete from products where ProductID = @ProductID";
                    sqlhelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@ProductID" }, new object[] { id });
                    Refresh();
                }
            }
            else
            {
                ClsBaoLoi.ThongTin("Vui lòng chọn sản phẩm");
            }
        }

        private void buttonsArray1_btnEventClose_click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonsArray1_btnEventUpdate_click_1(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            object id = treeList1.FocusedNode[treeList1.KeyFieldName];
            if (id != null || id != "")
            {
                if (_frmProductCategoryEdit == null)
                {
                    _frmProductCategoryEdit = new Frm_Product_Edit();
                    _frmProductCategoryEdit.ProductID = Convert.ToInt32(id);
                    _frmProductCategoryEdit.EnumStatus = EnumFormStatus.Modify;
                    SplashScreenManager.CloseForm();
                    _frmProductCategoryEdit.ShowDialog();
                }
                else
                {
                    _frmProductCategoryEdit.ProductID = Convert.ToInt32(id);
                    _frmProductCategoryEdit.EnumStatus = EnumFormStatus.Modify;
                    SplashScreenManager.CloseForm();
                    _frmProductCategoryEdit.ShowDialog();
                }
                Refresh();
            }
            else
            {
                ClsMsgBox.ThongTin("Vui lòng chọn danh mục.");
            }
        }



    }
}
