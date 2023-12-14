using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Connection;
using PhoHa7.Library.Froms.MsgBox;
using PhoHa7.Library.Froms.Connection;

namespace PhoHa7.System
{
    public partial class frmSetting : DevExpress.XtraEditors.XtraForm
    {
        SqlHelper sqlHelper;
        DataSet ds;
        public frmSetting()
        {
            InitializeComponent();
            sqlHelper = new SqlHelper();
            ds = new DataSet();
            load();
        }

        void load()
        {
            loadKitchenType();

            //lookUpKitchenType.EditValue = ClsPublic.KITCHEN_TYPE;

            //load filter item gridview
            string sql = "select * from PhoHa7_FilterSaleItem";
            if (ds.Tables["FilterSaleItem"] != null)
            {
                ds.Tables.Clear();
            }
            sqlHelper.ExecuteDataSet(ds, "FilterSaleItem", sql, CommandType.Text, null, null);
            gItems.DataSource = ds.Tables["FilterSaleItem"];

            //load background color to go
            colorBackgroundToGo.EditValue = ClsPublic.ColorBackgroundToGo;
            //load letter color to go
            colorLetterToGo.EditValue = ClsPublic.ColorLetterToGo;
            //load background color change
            colorBackgroundItemChange.EditValue = ClsPublic.ColorBackgroundItemChange;
            //load letter color change
            colorLetterItemChange.EditValue = ClsPublic.ColorLetterItemChange;
            //font size print
            trackBarFontSize.Value = ClsPublic.FontSizePrint;
            //padding print
            trackBarPadding.Value = ClsPublic.PaddingSizePrint;
            //timer for bill
            trackBarTimer.Value = ClsPublic.TIMER_ORDER_COUNT_DOWN;
        }

        private void loadKitchenType()
        {
            //load kitchen type
            //string sql = "select * from PhoHa7_Category";
            //if (ds.Tables["Category"] != null)
            //{
            //    ds.Tables.Clear();
            //}
            //sqlHelper.ExecuteDataSet(ds, "Category", sql, CommandType.Text, null, null);
            //lookUpKitchenType.Properties.DataSource = ds.Tables["Category"];
            //lookUpKitchenTypeAdd.Properties.DataSource = ds.Tables["Category"];
            //lookUpKitchenType.Properties.DisplayMember = "CategoryName";
            //lookUpKitchenType.Properties.ValueMember = "CategoryID";

            //

            //load kitchen type
            string sql = "select * from Categories";
            if (ds.Tables["Category"] != null)
            {
                ds.Tables.Clear();
            }
            sqlHelper.ExecuteDataSet(ds, "Category", sql, CommandType.Text, null, null);
            checkedListBoxKitchenType.DataSource = ds.Tables["Category"];
            //datasource for add filter item
            lookUpKitchenTypeAdd.Properties.DataSource = ds.Tables["Category"];
            checkedListBoxKitchenType.DisplayMember = "CategoryName";
            checkedListBoxKitchenType.ValueMember = "CategoryID";

            DataTable dataTable = (DataTable)checkedListBoxKitchenType.DataSource;
            //go for each row list Category
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //go each Category saved
                string[] kitType = ClsPublic.KITCHEN_TYPE.Split(',');
                //compare
                foreach (string item in kitType)
                {
                    //check item in list check box
                    if (Convert.ToInt32(checkedListBoxKitchenType.GetItemValue(i)) == Convert.ToInt32(item))
                    {
                        checkedListBoxKitchenType.SetItemChecked(i, true);
                    }
                }

            }
        }

        #region Filter items

        //filter items
        int idFilterItem = 0;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            try
            {
                idFilterItem = Convert.ToInt32(gridView1.GetRowCellValue(e.FocusedRowHandle, colID));
            }
            catch (System.Exception ex)
            {

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sql = "select ProductID from Products where BarCode = @BarCode";
            int productID = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@BarCode" }, new object[] { txtID.Text }));
            sql = "insert into PhoHa7_FilterSaleItem (Description,ProductID,CategoryID,BarCode) values (@Description,@ProductID,@CategoryID,@BarCode)";
            int result = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@Description", "@ProductID", "@CategoryID", "@BarCode" },
                new object[] { txtItemAdd.Text, productID, lookUpKitchenTypeAdd.EditValue, txtID.Text });
            if (result > 0)
            {
                MsgBox msg = new MsgBox("Thêm thành công.");
                msg.Show();
                txtItemAdd.Text = "";
                txtID.Text = "";
                lookUpKitchenTypeAdd.EditValue = "";
            }
            load();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string sql = "delete PhoHa7_FilterSaleItem where ID = @ID";
                int resultSQL = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@ID" }, new object[] { idFilterItem });
                if (resultSQL > 0)
                {
                    MessageBox.Show("Đã xóa", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            load();
        }

        #endregion


        private void btnSave_Click(object sender, EventArgs e)
        {
            //kitchen type change
            string kType = "";
            DevExpress.XtraEditors.BaseCheckedListBoxControl.CheckedItemCollection checkItems = checkedListBoxKitchenType.CheckedItems;// = {DevExpress.XtraEditors.BaseCheckedListBoxControl.}
            for (int i = 0; i < checkItems.Count; i++)
            {
                int categoryID = (int)checkItems[i];
                if (i != checkItems.Count - 1)
                {
                    kType += categoryID + ",";
                }
                else
                {
                    kType += categoryID;
                }
            }
            string[] temp = kType.Split(',');
            ClsPublic.KITCHEN_TYPE = kType;

            //color background Togo
            ClsPublic.ColorBackgroundToGo = (Color)colorBackgroundToGo.EditValue;
            //color letter togo
            ClsPublic.ColorLetterToGo = (Color)colorLetterToGo.EditValue;
            //font size print
            ClsPublic.FontSizePrint = Convert.ToInt32(trackBarFontSize.EditValue);
            //padding print
            ClsPublic.PaddingSizePrint = Convert.ToInt32(trackBarPadding.EditValue);
            //color item change
            ClsPublic.ColorLetterItemChange = (Color)colorLetterItemChange.EditValue;
            //color background item change
            ClsPublic.ColorBackgroundItemChange = (Color)colorBackgroundItemChange.EditValue;
            //time count down
            ClsPublic.TIMER_ORDER_COUNT_DOWN = Convert.ToInt32(trackBarTimer.EditValue);
        }


        private void colorBackgroundToGo_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void colorLetterToGo_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void trackBarFontSize_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void trackBarPadding_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void colorLetterItemChange_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void colorBackgroundItemChange_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void trackBarTimer_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnServerConfig_Click(object sender, EventArgs e)
        {
            FrmCauHinhCSDL frm = new FrmCauHinhCSDL();
            frm.ShowDialog();
        }

        private void btnEmp_Click(object sender, EventArgs e)
        {

        }















    }
}