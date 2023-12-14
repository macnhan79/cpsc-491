using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhoHa7.Library.Classes.Connection;
using PhoHa7.Library.Froms.MsgBox;
using PhoHa7.Library.Froms;
using PhoHa7.Library.Froms.Connection;
using PhoMac.Model.Data;
using PhoMac.Model;
using System.Threading;
using DevExpress.XtraSplashScreen;

namespace PhoMac.Main.PhoMac_System
{
    public partial class Frm_Setting : XtraUserControlKira
    {
        SqlHelper sqlHelper;
        DataSet ds;
        public Frm_Setting()
        {
            InitializeComponent();
            sqlHelper = new SqlHelper();
            ds = new DataSet();
            load();
        }

        void load()
        {
            loadKitchenType();
            lblMachineName.Text = Environment.MachineName;
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
            //background emergency
            colorBackgrdEmerg.EditValue = ClsPublic.BackgroundColorEmergency;
            //forceColor Large Size
            colorForceColorLageSize.EditValue = ClsPublic.ForceColorLageSize;
            //font size print
            trackBarFontSize.Value = ClsPublic.FontSizePrint;
            //padding print
            trackBarPadding.Value = ClsPublic.PaddingSizePrint;
            //timer for bill
            trackBarTimer.Value = ClsPublic.TIMER_ORDER_COUNT_DOWN;
            //website location
            txtWebsiteLocation.Text = ClsPublic.WebsiteLocation;
            //image
            txtImageURL.Text = ClsPublic.ImageURL;
            //auto print
            chkAutoPrint.Checked = ClsPublic.AutoCompleteAndPrint;
            spinPrintAfterSecond.Value = ClsPublic.AutoPrintAfterSecond;
            ckAllowMulInstance.Checked = ClsPublic.AllowMultiInstance;
            //txt small, large size
            txtSmallSize.Text = ClsPublic.SizeSmall;
            txtLargeSize.Text = ClsPublic.SizeLarge;
            //txt sound play url
            txtSoundPlayURL.Text = ClsPublic.SoundUrl;
            //color inKitchen and NotKitchen
            colorForceColorInKitchen.EditValue = ClsPublic.ForceColor;
            colorForceColorNotKitchen.EditValue = ClsPublic.ForceColor1;
            //qty column
            colorBackgrdQty.EditValue = ClsPublic.BackgrdColorQty;
            colorForceColorQty.EditValue = ClsPublic.ForceColorQty;
            colorForceColorQty1.EditValue = ClsPublic.ForceColorQty1;
            trackBarFontSizeQty.Value = ClsPublic.FontSizeQty;
            ckHandleTicket.EditValue = ClsPublic.HandleCompleteTicketFromSquare;
            ckAutoPrintSpecialItem.EditValue = ClsPublic.AutoCompleteAndPrintSpecialItem;

            //load list printers
            List<string> printers = new List<string>();
            foreach (string item in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                printers.Add(item);
            }
            lookUpEditPrinters.Properties.DataSource = printers;
            lookUpPrinterForCashDrawer.Properties.DataSource = printers;
            lookUpPrinterForSpecialItem.Properties.DataSource = printers;
            if (ClsPublic.Printers != "")
            {
                lookUpEditPrinters.EditValue = ClsPublic.Printers;

            }
            if (ClsPublic.PrinterOpenCashDrawerName != "")
            {
                lookUpPrinterForCashDrawer.EditValue = ClsPublic.PrinterOpenCashDrawerName;
            }
            if (ClsPublic.LookUpPrinterForSpecialItem != "")
            {
                lookUpPrinterForSpecialItem.EditValue = ClsPublic.LookUpPrinterForSpecialItem;
            }
            //is print barcode
            ckPrintBarCode.Checked = ClsPublic.PrintBarCode;
            spinNumberOfGroupTable.EditValue = ClsPublic.NumOfGroupTable;
            //show/hide Group and Filter Tables
            chkShowGroupTable.Checked = ClsPublic.ShowGroupTable;
            chkShowFilterTable.Checked = ClsPublic.ShowFilterTable;
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
            if (validationAddItem())
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
        }

        bool validationAddItem()
        {
            dxErrorProvider1.ClearErrors();
            if (txtID.EditValue == null || txtID.EditValue == "")
            {
                dxErrorProvider1.SetError(txtID, "Vui lòng nhập mã.");
                return false;
            }
            else if (txtItemAdd.EditValue == null || txtItemAdd.EditValue == "")
            {
                dxErrorProvider1.SetError(txtItemAdd, "Vui lòng nhập tên món.");
                return false;
            }
            else if (lookUpKitchenTypeAdd.EditValue == null || lookUpKitchenTypeAdd.EditValue == "")
            {
                dxErrorProvider1.SetError(lookUpKitchenTypeAdd, "Vui lòng chọn danh mục.");
                return false;
            }
            return true;
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
            SplashScreenManager.ShowForm(typeof(WaitForm1));
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
            //background emergency
            ClsPublic.BackgroundColorEmergency = (Color)colorBackgrdEmerg.EditValue;
            //forceColor Large Size
            ClsPublic.ForceColorLageSize = (Color)colorForceColorLageSize.EditValue;
            //time count down
            ClsPublic.TIMER_ORDER_COUNT_DOWN = Convert.ToInt32(trackBarTimer.EditValue);
            //web
            ClsPublic.WebsiteLocation = txtWebsiteLocation.Text;
            //image
            ClsPublic.ImageURL = txtImageURL.Text;
            //auto print
            ClsPublic.AutoCompleteAndPrint = chkAutoPrint.Checked;
            ClsPublic.AutoCompleteAndPrintSpecialItem = ckAutoPrintSpecialItem.Checked;
            ClsPublic.AutoPrintAfterSecond = (int)spinPrintAfterSecond.Value;
            ClsPublic.AllowMultiInstance = ckAllowMulInstance.Checked;
            //printer
            ClsPublic.Printers = lookUpEditPrinters.EditValue + string.Empty;
            ClsPublic.PrinterOpenCashDrawerName = lookUpPrinterForCashDrawer.EditValue + string.Empty;
            ClsPublic.LookUpPrinterForSpecialItem = lookUpPrinterForSpecialItem.EditValue + string.Empty;
            
            //print barcode
            ClsPublic.PrintBarCode = ckPrintBarCode.Checked;
            //number of group table
            ClsPublic.NumOfGroupTable = Convert.ToInt32(spinNumberOfGroupTable.EditValue);
            //show/hide Group and Filter Tables
            ClsPublic.ShowGroupTable = chkShowGroupTable.Checked;
            ClsPublic.ShowFilterTable = chkShowFilterTable.Checked;
            //txt small, large size
            ClsPublic.SizeSmall = txtSmallSize.Text + string.Empty;
            ClsPublic.SizeLarge = txtLargeSize.Text + string.Empty;
            //txt sound play url
            ClsPublic.SoundUrl = txtSoundPlayURL.Text + string.Empty;
            //color inKitchen and NotKitchen
            ClsPublic.ForceColor = (Color)colorForceColorInKitchen.EditValue;
            ClsPublic.ForceColor1 = (Color)colorForceColorNotKitchen.EditValue;
            //qty column
            ClsPublic.BackgrdColorQty = (Color)colorBackgrdQty.EditValue;
            ClsPublic.ForceColorQty = (Color)colorForceColorQty.EditValue;
            ClsPublic.ForceColorQty1 = (Color)colorForceColorQty1.EditValue;
            ClsPublic.FontSizeQty = trackBarFontSizeQty.Value;
            ClsPublic.HandleCompleteTicketFromSquare = Convert.ToBoolean(ckHandleTicket.Checked);
            //ClsPublic.MachineInfo
            //color background Togo
            SplashScreenManager.CloseForm();
        }


        private void colorBackgroundToGo_EditValueChanged(object sender, EventArgs e)
        {
            //var temp = ((Color)colorBackgroundToGo.EditValue).ToArgb().ToString();
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
            //this.Close();
        }

        private void btnServerConfig_Click(object sender, EventArgs e)
        {
            FrmCauHinhCSDL frm = new FrmCauHinhCSDL();
            frm.ShowDialog();
        }

        private void btnEmp_Click(object sender, EventArgs e)
        {

        }

        private void txtWebsiteLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtWebsiteLocation.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void txtImageURL_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtImageURL.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void txtSoundPlayURL_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSoundPlayURL.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dao dao = new Dao();
            count = dao.GetAll<Ticket>().Count;


            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
        }

        static int count = 0;
        static int step = 0;

        public static void ThreadProc()
        {
            Dao dao = new Dao();
            ICollection<PhoMac.Model.Ticket> listTicket = dao.GetAll<Ticket>();
            count = listTicket.Count;
            foreach (var itemTicket in listTicket)
            {
                ICollection<SaleItem> listSaleItem = dao.FindByMultiColumnAnd<SaleItem>(new[] { "TicketID" }, itemTicket.TicketID);
                decimal sumPrice = Convert.ToDecimal(listSaleItem.Sum(s => s.TPrice));
                itemTicket.TotalP = sumPrice;
                itemTicket.TotalG = sumPrice * Convert.ToDecimal(1.0875);
                if (itemTicket.PaidType == 1)
                {
                    itemTicket.PaidCash = itemTicket.TotalG;
                }
                else
                {
                    itemTicket.PaidCash = 0;
                }
                dao.Update<Ticket>(itemTicket);
                step++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //label1.Text = step + " of " + count;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkAutoPrint_CheckedChanged(object sender, EventArgs e)
        {

        }















    }
}
