using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PhoHa7.Library.Classes.Connection;
using System.Data.SqlClient;
using PhoHa7.Library.Classes.Common;
using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Froms;
using PhoMac.Main.Entities;
using DevExpress.XtraEditors;
using System.Threading;
using PhoMac.Model;
using DevExpress.Utils;
using PhoMac.Model.Data;

namespace PhoMac.Main.GUI
{
    public partial class TicketView : DevExpress.XtraEditors.XtraUserControl
    {
        SqlHelper sqlHelper;
        int ticketID;
        int ticketID_Root;
        string tableName;
        string tableNameOld = "";
        int blinkTableName = 0;
        string empName;
        string customerName;
        bool isToGoTicket = false;
        string numberOfOrder;
        int dTicketNum;
        int counTotalItems;
        bool emergency;
        public object DataSource
        {
            get { return gControlGroup.DataSource; }
            set
            {
                DataTable dt = (DataTable)value;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int categoryID = (int)dt.Rows[i]["Category"];
                    foreach (string item in KitChenType)
                    {
                        if (categoryID == Convert.ToInt32(item))
                        {
                            dt.Rows[i]["OrderBy"] = 1;
                            break;
                        }
                    }
                }
                DataTable temp = dt.Select(null, "OrderBy desc").CopyToDataTable();
                gControlGroup.DataSource = temp;
                gridView1.ClearSorting();





                //gridView1.Columns["OrderBy"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            }
        }

        public int TicketID
        {
            get { return ticketID; }
            set { ticketID = value; }

        }

        public int TicketID_Root
        {
            get { return ticketID_Root; }
            set
            {
                ticketID_Root = value;
                ticketInfo.TicketID_Root = value;
            }

        }

        public string[] KitChenType
        {
            get;
            set;
        }

        public string TableName
        {
            get { return tableName; }
            set
            {
                tableName = value;
                lblTable.Text = "Table: " + value;
                if (ticketInfo != null)
                {
                    ticketInfo.TableName = tableName;
                }

            }
        }

        public bool IsToGoTicket
        {
            get
            {
                return isToGoTicket;
            }
            set
            {
                isToGoTicket = value;
                if (isToGoTicket)
                {
                    this.BackColor = ClsPublic.ColorBackgroundToGo;
                }
                else
                {
                    this.BackColor = Color.Transparent;
                }
                ticketInfo.TakeOut = isToGoTicket;
            }
        }

        public string EmpName
        {
            get { return empName; }
            set
            {
                empName = value;
                lblServer.Text = "Server: " + value;
                ticketInfo.EmployeeName = empName;
            }
        }

        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                if (!IsToGoTicket)
                {
                    layoutLblCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    if (customerName == string.Empty)
                    {
                        layoutLblCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }
                    lblCustomerName.Text = "Customer: " + customerName;
                }
                ticketInfo.CustomerName = customerName;
            }
        }

        public string NumberOfOrder
        {
            get { return numberOfOrder; }
            set
            {
                numberOfOrder = value;
                lblNumberOfOrder.Text = value;
            }
        }

        public int DTicketNum
        {
            get
            {
                return dTicketNum;
            }
            set
            {
                dTicketNum = value;
                ticketInfo.DTicketNum = value;
            }
        }

        public bool Emergency
        {
            get
            {
                return emergency;
            }
            set
            {
                emergency = value;
                if (emergency)
                {
                    tableNameOld = tableName;
                }
                ticketInfo.Emergency = value;
            }
        }

        public int CounTotalItems
        {
            get
            {
                return counTotalItems;
            }
            set
            {
                counTotalItems = value;
                ticketInfo.CountTotalItemPrint = value;
            }
        }

        DateTime dateTimeIssue;
        public DateTime DateTimeIssue
        {
            get
            {
                return dateTimeIssue;
            }
            set
            {
                dateTimeIssue = value;
                ticketInfo.DateTimeIssue = value;
            }
        }

        PhoHa7_ProcTickets ticketInfo;
        Dao dao;

        System.Windows.Forms.Timer flashingTimer;
        Dictionary<int, int> rowChange;

        public TicketView()
        {
            InitializeComponent();
            
            sqlHelper = new SqlHelper();
            //
            KitChenType = ClsPublic.KITCHEN_TYPE.Split(',');
            timer1.Start();
            flashingTimer = new System.Windows.Forms.Timer();
            flashingTimer.Interval = 1000;
            flashingTimer.Enabled = false;
            flashingTimer.Tick += new EventHandler(flashingTimer_Tick);
            rowChange = new Dictionary<int, int>();
            dao = new Dao();
            //this.repositoryItemMemoEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        private void TicketView_Load(object sender, EventArgs e)
        {
            ticketInfo = new PhoHa7_ProcTickets();
            ticketInfo.TicketID = TicketID;
            ticketInfo.TicketID_Root = ticketID_Root;
            ticketInfo.TableName = TableName;
            ticketInfo.DTicketNum = DTicketNum;
            ticketInfo.TakeOut = IsToGoTicket;
            ticketInfo.EmployeeName = empName;
            ticketInfo.CustomerName = customerName;
            ticketInfo.DateTimeIssue = DateTimeIssue;
            ticketInfo.Emergency = Emergency;
            ticketInfo.ListProduct = new List<Product>();
        }


        #region Timer

        int timer = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int timeLeft = ClsPublic.TIMER_ORDER_COUNT_DOWN;
            double secDif = (DateTime.Now - DateTimeIssue).TotalSeconds;
            int min = (int)secDif / 60;
            int sec = (int)secDif - (min * 60);
            lblTime.Text = min + ":" + sec;

            if (min > timeLeft)
            {
                //change text to red corlor
                this.lblTime.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            if (timer < 3)
            {
                timer++;
            }
            else
            {
                countTime();
                refreshGridIfChanged();
                timer = 0;
            }

        }

        public void countTime()
        {

            timer++;
            //every second
            if (timer > ClsPublic.AutoPrintAfterSecond)
            {
                if (ClsPublic.AutoCompleteAndPrint)
                {
                    completeAll();
                }

            }
            if (timer == 4)
            {
                if (ClsPublic.AutoCompleteAndPrintSpecialItem)
                {
                    //SplashScreenManager.ShowForm(typeof(WaitForm1));
                    DataTable dt = (DataTable)gControlGroup.DataSource;
                    bool isPrint = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            if ((bool)dt.Rows[i]["PrintBoth"])
                            {
                                isPrint = true;
                                break;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        
                    }
                    if (isPrint)
                    {
                        int count = autoCompleteSpecialItems();
                        if (count > 0)
                        {
                            Thread t = new Thread(new ThreadStart(printItemSpecial));
                            t.Start();
                            // printItem();
                        }
                    }
                    
                    //SplashScreenManager.CloseForm();
                }
                double secDif = (DateTime.Now - DateTimeIssue).TotalSeconds;


                //blinking table name
                if (tableNameOld != "")
                {
                    //if (blinkTableName < 30)
                    //{
                    if (blinkTableName % 2 == 0)
                    {
                        if (emergency)
                            lblTable.Appearance.BackColor = ClsPublic.BackgroundColorEmergency;
                        else
                            lblTable.Appearance.BackColor = Color.Green;
                        blinkTableName++;
                    }
                    else
                    {
                        lblTable.Appearance.BackColor = Color.Transparent;
                        blinkTableName++;
                    }
                    //}
                    //else
                    //{
                    //    tableNameOld = "";
                    //}
                }
                //////////
            }



        }


        void flashingTimer_Tick(object sender, EventArgs e)
        {
            if (rowChange.Count > 0)
            {
                foreach (var item in rowChange)
                {
                    gridView1.RefreshRow(item.Key);
                }
            }

        }

        #endregion

        #region Button event

        //done ticket-->delete ticket
        private void btnCompleted_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            int count = completeTicket();
            if (count > 0)
            {
                Thread t = new Thread(new ThreadStart(printItem));
                t.Start();
                // printItem();
            }
            SplashScreenManager.CloseForm();
        }

        private void btnCompleteNoPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            completeTicket();

            SplashScreenManager.CloseForm();
        }

        private void btnCompleteAll_Click(object sender, EventArgs e)
        {
            completeAll();
        }

        #endregion

        #region Grid event

        //change done state item
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataTable dt = (DataTable)gControlGroup.DataSource;
            int category;
            try
            {
                category = Convert.ToInt32(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, gridView1.Columns["Category"]));
            }
            catch (System.Exception ex)
            {
                category = -1;
            }
            //check item is in Category
            bool isInCategory = false;
            foreach (string item in KitChenType)
            {
                if (category == Convert.ToInt32(item))
                {
                    isInCategory = true;
                    break;
                }
            }
            if (isInCategory)
            {
                bool temp = !(bool)(gridView1.GetFocusedRow() as DataRowView).Row["Done"];
                (gridView1.GetFocusedRow() as DataRowView).Row["Done"] = temp;
                DataTable dt1 = (DataTable)gControlGroup.DataSource;
                gControlGroup.RefreshDataSource();
                string a = "";
            }

        }
        bool isBlink = false;
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //  ColumnView a = (ColumnView)gridView1.GetRow(gridView1.FocusedRowHandle);

            GridView View = sender as GridView;
            DataTable dt = (DataTable)gControlGroup.DataSource;
            object abc = View.DataSource;
            if (e.RowHandle >= 0)
            {
                string name = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Description"]).ToString();
                int category = Convert.ToInt32(View.GetRowCellDisplayText(e.RowHandle, View.Columns["Category"]));
                //disable item is not in Category
                bool isInCategory = false;
                foreach (string item in KitChenType)
                {
                    if (category == Convert.ToInt32(item))
                    {
                        isInCategory = true;
                        break;
                    }
                }
                if (!isInCategory)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = ClsPublic.ForceColor1;
                    //e.Appearance.ForeColor = Color.LightGray;
                    gridView1.SelectRow(e.RowHandle + 1);
                }
                else
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = ClsPublic.ForceColor;
                }


                //draw is change - item is changed
                bool isChange = Convert.ToBoolean(View.GetRowCellValue(e.RowHandle, colIsChange));
                if (isChange)
                {
                    if (isInCategory)
                    {
                        int blink = 0;
                        try
                        {
                            blink = rowChange[e.RowHandle];
                        }
                        catch (System.Exception ex)
                        {

                        }
                        if (blink % 2 == 0 && blink < 30)
                        {
                            e.Appearance.ForeColor = ClsPublic.ForceColor;
                            e.Appearance.BackColor = Color.Transparent;
                            e.Appearance.BackColor2 = Color.Transparent;
                        }
                        else
                        {
                            e.Appearance.ForeColor = ClsPublic.ColorLetterItemChange;
                            e.Appearance.BackColor = ClsPublic.ColorBackgroundItemChange;
                            e.Appearance.BackColor2 = ClsPublic.ColorBackgroundItemChange;
                        }
                        try
                        {
                            rowChange[e.RowHandle] = rowChange[e.RowHandle] + 1;
                        }
                        catch (System.Exception ex)
                        {

                        }
                    }
                }
                bool takeOut = Convert.ToBoolean(View.GetRowCellValue(e.RowHandle, colToGo));
                //draw take out item
                if (takeOut)
                {
                    if (isInCategory)
                    {
                        e.Appearance.ForeColor = ClsPublic.ColorLetterToGo;
                        e.Appearance.BackColor = ClsPublic.ColorBackgroundToGo;
                        e.Appearance.BackColor2 = ClsPublic.ColorBackgroundToGo;
                    }
                }
                //cancel items
                bool cancel = Convert.ToBoolean(View.GetRowCellValue(e.RowHandle, colCancelItem));
                if (cancel)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                }
            }

        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column == null) return;
            GridView CurrentView = sender as GridView;
            if (e.Column.FieldName == "Qty")
            {
                int qty = (int)CurrentView.GetRowCellValue(e.RowHandle, e.Column);
                if (qty > 1)
                {
                    e.Appearance.ForeColor = ClsPublic.ForceColorQty1;
                    e.Appearance.BackColor = ClsPublic.BackgrdColorQty;
                    e.Appearance.BackColor2 = ClsPublic.BackgrdColorQty;
                    float fontSize = (float)ClsPublic.FontSizeQty;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, fontSize, FontStyle.Bold);
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {

        }

        private void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Description" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string kitcheName = gridView1.GetRowCellValue(e.ListSourceRowIndex, colName) + string.Empty;
                string extraName = gridView1.GetRowCellValue(e.ListSourceRowIndex, colExtraName) + string.Empty;
                string option = gridView1.GetRowCellValue(e.ListSourceRowIndex, colOption) + string.Empty;
                string extraWith = gridView1.GetRowCellValue(e.ListSourceRowIndex, colExtraWith) + string.Empty;
                string extraWithout = gridView1.GetRowCellValue(e.ListSourceRowIndex, colExtraWithout) + string.Empty;
                string customSelect = gridView1.GetRowCellValue(e.ListSourceRowIndex, colCustomSelect) + string.Empty;
                bool isSmall = Convert.ToBoolean(gridView1.GetRowCellValue(e.ListSourceRowIndex, colIsSmall) + string.Empty);
                int mType = Convert.ToInt32(gridView1.GetRowCellValue(e.ListSourceRowIndex, colMType) + string.Empty);
                e.DisplayText = customDisplayKitchenName(kitcheName, option, extraName, isSmall, mType, extraWith, extraWithout, customSelect);
            }
        }

        private void gControlGroup_DataSourceChanged(object sender, EventArgs e)
        {
            rowChange.Clear();
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                bool isChange = (bool)gridView1.GetRowCellValue(i, colIsChange);
                if (isChange)
                {
                    rowChange.Add(i, 0);
                    if (!flashingTimer.Enabled)
                    {
                        flashingTimer.Enabled = true;
                    }
                }
            }
        }

        private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            int category;

            try
            {
                category = Convert.ToInt32(gridView1.GetRowCellDisplayText(e.RowHandle, gridView1.Columns["Category"]));
            }
            catch (System.Exception ex)
            {
                category = -1;
            }
            //check item is in Category
            bool isInCategory = KitChenType.Contains(category + string.Empty);
            if (isInCategory)
            {
                int filterTextIndex = e.DisplayText.IndexOf("(" + ClsPublic.SizeLarge + ")", StringComparison.CurrentCultureIgnoreCase);
                if (filterTextIndex == -1)
                    return;

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Center;
                Size size = new Size(168, ((int)e.Graphics.MeasureString(e.DisplayText, e.Appearance.Font, 168, stringFormat).Height));
                Rectangle header2Rect = e.Bounds;
                // header2Rect.Size = size;

                StringFormat sf = new StringFormat(StringFormatFlags.NoClip);

                sf.LineAlignment = StringAlignment.Center;

                // e.Appearance.TextOptions.

                DevExpress.Utils.Paint.XPaint.Graphics.DrawMultiColorString(e.Cache, header2Rect, e.DisplayText, "(" + ClsPublic.SizeLarge + ")",
                    e.Appearance, sf, Color.Black, ClsPublic.ForceColorLageSize, true, filterTextIndex);
                e.Handled = true;

            }



        }

        #endregion


        #region Method

        public void completeAll()
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            TicketListView parent = (TicketListView)this.Parent.Parent.Parent;
            int count = 0;
            SqlTransaction transaction;
            transaction = ClsConnection.MySqlConn.BeginTransaction();
            try
            {
                //go each row and update item is done
                if (!IsToGoTicket)
                {
                    ticketInfo.ListProduct.Clear();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        foreach (string item in KitChenType)
                        {

                            if (Convert.ToInt32(gridView1.GetRowCellValue(i, colCategory)) == Convert.ToInt32(item))
                            {
                                string namePrint = gridView1.GetRowCellValue(i, colQuality) + ")" +
                                customDisplayKitchenName(gridView1.GetRowCellValue(i, colName) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colOption) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colExtraName) + string.Empty,
                                                         Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsSmall)),
                                                         Convert.ToInt32(gridView1.GetRowCellValue(i, colMType)),
                                                         gridView1.GetRowCellValue(i, colExtraWith) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colExtraWithout) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colCustomSelect) + string.Empty);
                                bool takeOut = Convert.ToBoolean(gridView1.GetRowCellValue(i, colToGo) + string.Empty);
                                if (ClsPublic.PrintBarCode)
                                {
                                    namePrint = namePrint + "(" + gridView1.GetRowCellValue(i, colBarCode).ToString() + ")";
                                }
                                namePrint = namePrint + (takeOut ? "(GO)" : "");
                                object saleItemID = gridView1.GetRowCellValue(i, colSaleItemID);
                                Product product = new Product();
                                product.SaleItemID = Convert.ToInt32(saleItemID);
                                product.PrintName = namePrint;
                                product.TakeOut = takeOut;
                                product.IsCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem));
                                product.IsChange = Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsChange));

                                product.PrintFrontStyle = FontStyle.Bold;
                                //set cancel item
                                bool isCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem) + string.Empty);
                                if (isCancel)
                                {
                                    product.PrintFrontStyle = FontStyle.Strikeout;
                                }
                                //ticketInfo.CountItemPrint++;
                                ticketInfo.ListProduct.Add(product);
                                //saleItemsName += namePrint + "|";
                                //update done item
                                string sql1 = "update PhoHa7_ProcSaleItem set Done = 1 where SaleItemID  = @SaleItemID";
                                count += sqlHelper.ExecuteNonQuery(sql1, CommandType.Text, transaction, new object[] { "@SaleItemID" }, new object[] { saleItemID });

                                break;
                            }
                        }
                    }
                }
                //ticket togo
                else
                {
                    ticketInfo.ListProduct.Clear();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        string namePrint = gridView1.GetRowCellValue(i, colQuality) + ")" +
                            customDisplayKitchenName(gridView1.GetRowCellValue(i, colName) + string.Empty,
                                                     gridView1.GetRowCellValue(i, colOption) + string.Empty,
                                                     gridView1.GetRowCellValue(i, colExtraName) + string.Empty,
                                                     Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsSmall)),
                                                     Convert.ToInt32(gridView1.GetRowCellValue(i, colMType)),
                                                     gridView1.GetRowCellValue(i, colExtraWith) + string.Empty,
                                                     gridView1.GetRowCellValue(i, colExtraWithout) + string.Empty,
                                                     gridView1.GetRowCellValue(i, colCustomSelect) + string.Empty);
                        bool takeOut = Convert.ToBoolean(gridView1.GetRowCellValue(i, colToGo) + string.Empty);
                        if (ClsPublic.PrintBarCode)
                        {
                            namePrint = namePrint + "(" + gridView1.GetRowCellValue(i, colBarCode).ToString() + ")";
                        }
                        namePrint = namePrint + (takeOut ? "(GO)" : "");
                        object saleItemID = gridView1.GetRowCellValue(i, colSaleItemID);
                        foreach (string item in KitChenType)
                        {
                            if (Convert.ToInt32(gridView1.GetRowCellValue(i, colCategory)) == Convert.ToInt32(item))
                            {
                                Product product = new Product();
                                product.PrintName = namePrint;
                                product.SaleItemID = Convert.ToInt32(saleItemID);
                                product.TakeOut = takeOut;
                                product.IsCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem));
                                product.IsChange = Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsChange));
                                product.PrintFrontStyle = FontStyle.Bold;
                                //set print cancel item
                                bool isCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem) + string.Empty);
                                if (isCancel)
                                {
                                    product.PrintFrontStyle = FontStyle.Strikeout;
                                }

                                ticketInfo.ListProduct.Add(product);

                                string sql1 = "update PhoHa7_ProcSaleItem set Done = 1 where SaleItemID  = @SaleItemID";
                                count += sqlHelper.ExecuteNonQuery(sql1, CommandType.Text, transaction, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                                break;
                            }
                        }
                    }
                    string sql = "select coalesce(SUM(qty),0) from PhoHa7_ProcSaleItem where Done = 1 and TicketID = @TicketID";
                    ticketInfo.CountItemDonePrint = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                    ticketInfo.CountTotalItemPrint = Convert.ToInt32(sqlHelper.ExecuteScalar("select coalesce(SUM(qty),0) from PhoHa7_ProcSaleItem where TicketID = @TicketID", CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                    //print all item
                    if (ticketInfo.CountItemDonePrint == ticketInfo.CountTotalItemPrint)
                    {
                        DataTable listAllItems = sqlHelper.ExecuteDataTable("select * from PhoHa7_ProcSaleItem where TicketID = @TicketID", CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID });
                        for (int k = 0; k < listAllItems.Rows.Count; k++)
                        {
                            int tempSaleItemID = Convert.ToInt32(listAllItems.Rows[k]["SaleItemID"]);
                            bool isFindProduct = false;
                            foreach (Product p in ticketInfo.ListProduct)
                            {
                                if (p.SaleItemID == tempSaleItemID)
                                {
                                    isFindProduct = true;
                                    break;
                                }
                            }
                            if (!isFindProduct)
                            {
                                Product p = new Product();
                                p.PrintName = listAllItems.Rows[k]["Qty"] + ") " + listAllItems.Rows[k]["Description"];
                                if (ClsPublic.PrintBarCode)
                                {
                                    p.PrintName += "(" + listAllItems.Rows[k]["BarCode"] + ")";
                                }
                                p.SaleItemID = tempSaleItemID;
                                p.PrintFrontStyle = FontStyle.Regular;
                                //set print cancel item
                                bool isCancel = Convert.ToBoolean(listAllItems.Rows[k]["Cancel"]);
                                if (isCancel)
                                {
                                    p.PrintFrontStyle = FontStyle.Strikeout;
                                }
                                ticketInfo.ListProduct.Add(p);
                            }
                        }
                        //payment
                        sql = "select count(*) from ProcTickets where TicketID = @TicketID";
                        int existProcTickets = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID_Root }));
                        if (existProcTickets == 1)
                            ticketInfo.Payment = "No";
                        else
                        {
                            sql = "select count(*) from Tickets where TicketNum = @TicketID";
                            existProcTickets = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID_Root }));
                            if (existProcTickets == 1)
                                ticketInfo.Payment = "Paid";
                            else
                                ticketInfo.Payment = "Unknown";
                        }
                    }
                }
                ticketInfo.TakeOut = isToGoTicket;
                ticketInfo.DateTimeIssue = DateTime.Now;
                ticketInfo.CustomerName = CustomerName;
                ticketInfo.TicketID = DateTime.Now.GetHashCode();
                ticketInfo.DTicketNum = DTicketNum;
                ClsPublic.addListPrintItem(ticketInfo);
                //delete ticket
                //kiỂm tra tất cả item trong ticket. Còn item chưa Done thì không xóa ticket, ngược lại thì xóa
                string sql2 = "select count(*) from dbo.PhoHa7_ProcSaleItem where Done = 0 and TicketID = @TicketID";
                int result = Convert.ToInt32(sqlHelper.ExecuteScalar(sql2, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                if (result == 0)
                {
                    count += deleteTicketToDatabase(transaction);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                count = 0;
                try
                {
                    transaction.Rollback();
                    ClsPublic.WriteException(ex);
                }
                catch (Exception ex1)
                {
                    if (ClsMsgBox.LoiChung(ex1, false) == 1)
                    {
                        ClsPublic.WriteException(ex1);
                    }
                }
            }
            //reload group table
            Frm_Kitchen frmKitchen = (Frm_Kitchen)this.Parent.TopLevelControl;
            frmKitchen.loadGroupAndImportant();
            //delete ticketview
            if (count > 0)
            {
                //hide ticket
                parent.ticketDone(TicketID);
                //print completed items
                Thread t = new Thread(new ThreadStart(printItem));
                t.Start();
                //printItem();
            }
            this.Dispose();
            SplashScreenManager.CloseForm();
        }

        private int completeTicket()
        {
            int count = 0;
            SqlTransaction transaction;
            transaction = ClsConnection.MySqlConn.BeginTransaction();
            try
            {
                string sql;
                if (!IsToGoTicket)
                {
                    //go each row and update item is done
                    ticketInfo.ListProduct.Clear();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        string namePrint = "";
                        DataTable dt1 = (DataTable)gControlGroup.DataSource;
                        if ((bool)gridView1.GetRowCellValue(i, colCompleted))
                        {
                            namePrint = gridView1.GetRowCellValue(i, colQuality) + ")" +
                                customDisplayKitchenName(gridView1.GetRowCellValue(i, colName) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colOption) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colExtraName) + string.Empty,
                                                         Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsSmall)),
                                                         Convert.ToInt32(gridView1.GetRowCellValue(i, colMType)),
                                                         gridView1.GetRowCellValue(i, colExtraWith) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colExtraWithout) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colCustomSelect) + string.Empty);
                            bool takeOut = Convert.ToBoolean(gridView1.GetRowCellValue(i, colToGo) + string.Empty);

                            if (ClsPublic.PrintBarCode)
                            {
                                namePrint = namePrint + "(" + gridView1.GetRowCellValue(i, colBarCode).ToString() + ")";
                            }
                            namePrint = namePrint + (takeOut ? "(GO)" : "");
                            object saleItemID = gridView1.GetRowCellValue(i, colSaleItemID);
                            Product product = new Product();
                            product.SaleItemID = Convert.ToInt32(gridView1.GetRowCellValue(i, colSaleItemID) + string.Empty);
                            product.PrintName = namePrint;
                            product.TakeOut = takeOut;
                            product.IsCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem));
                            product.IsChange = Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsChange));
                            product.PrintFrontStyle = FontStyle.Bold;
                            //set print cancel item
                            bool isCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem) + string.Empty);
                            if (isCancel)
                            {
                                product.PrintFrontStyle = FontStyle.Strikeout;
                            }
                            //ticketInfo.CountItemPrint++;
                            ticketInfo.ListProduct.Add(product);
                            //saleItemsName += namePrint + "|";
                            //update done item
                            sql = "update PhoHa7_ProcSaleItem set Done = 1 where SaleItemID  = @SaleItemID";
                            count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                            //count++;
                        }
                    }
                }
                //ticket takeout
                else
                {
                    ticketInfo.ListProduct.Clear();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        string namePrint = "";
                        Product productTogo = new Product();
                        namePrint = gridView1.GetRowCellValue(i, colQuality) + ". " +
                                        customDisplayKitchenName(gridView1.GetRowCellValue(i, colName) + string.Empty,
                                                                 gridView1.GetRowCellValue(i, colOption) + string.Empty,
                                                                 gridView1.GetRowCellValue(i, colExtraName) + string.Empty,
                                                                 Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsSmall)),
                                                                 Convert.ToInt32(gridView1.GetRowCellValue(i, colMType)),
                                                                 gridView1.GetRowCellValue(i, colExtraWith) + string.Empty,
                                                                 gridView1.GetRowCellValue(i, colExtraWithout) + string.Empty,
                                                                 gridView1.GetRowCellValue(i, colCustomSelect) + string.Empty);
                        bool takeOut = Convert.ToBoolean(gridView1.GetRowCellValue(i, colToGo) + string.Empty);
                        if (ClsPublic.PrintBarCode)
                        {
                            namePrint = namePrint + "(" + gridView1.GetRowCellValue(i, colBarCode).ToString() + ")";
                        }
                        namePrint = namePrint + (takeOut ? "(GO)" : "");
                        productTogo.PrintName = namePrint;
                        productTogo.SaleItemID = Convert.ToInt32(gridView1.GetRowCellValue(i, colSaleItemID) + string.Empty);
                        productTogo.TakeOut = takeOut;
                        productTogo.IsCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem));
                        productTogo.IsChange = Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsChange));
                        if ((bool)gridView1.GetRowCellValue(i, colCompleted))
                        {
                            productTogo.PrintFrontStyle = FontStyle.Bold;
                            //set print cancel item
                            bool isCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem) + string.Empty);
                            if (isCancel)
                            {
                                productTogo.PrintFrontStyle = FontStyle.Strikeout;
                            }
                            ticketInfo.ListProduct.Add(productTogo);
                            //}
                            //update done item
                            object saleItemID = gridView1.GetRowCellValue(i, colSaleItemID);
                            sql = "update PhoHa7_ProcSaleItem set Done = 1 where SaleItemID  = @SaleItemID";
                            count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                            //count++;
                        }
                    }

                    sql = "select coalesce(SUM(qty),0) from dbo.PhoHa7_ProcSaleItem where Done = 1 and TicketID = @TicketID";
                    ticketInfo.CountItemDonePrint = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                    ticketInfo.CountTotalItemPrint = Convert.ToInt32(sqlHelper.ExecuteScalar("select coalesce(SUM(qty),0) from PhoHa7_ProcSaleItem where TicketID = @TicketID", CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                    //print all item
                    if (ticketInfo.CountItemDonePrint == ticketInfo.CountTotalItemPrint)
                    {
                        DataTable listAllItems = sqlHelper.ExecuteDataTable("select * from PhoHa7_ProcSaleItem where TicketID = @TicketID", CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID });
                        for (int k = 0; k < listAllItems.Rows.Count; k++)
                        {
                            int tempSaleItemID = Convert.ToInt32(listAllItems.Rows[k]["SaleItemID"]);
                            bool isFindProduct = false;
                            foreach (Product p in ticketInfo.ListProduct)
                            {
                                if (p.SaleItemID == tempSaleItemID)
                                {
                                    isFindProduct = true;
                                    break;
                                }
                            }
                            if (!isFindProduct)
                            {
                                Product p = new Product();
                                p.PrintName = listAllItems.Rows[k]["Qty"] + ") " + listAllItems.Rows[k]["Description"];
                                if (ClsPublic.PrintBarCode)
                                {
                                    p.PrintName += " (" + listAllItems.Rows[k]["BarCode"] + ")";
                                }
                                p.SaleItemID = tempSaleItemID;
                                p.PrintFrontStyle = FontStyle.Regular;
                                //set print cancel item
                                bool isCancel = Convert.ToBoolean(listAllItems.Rows[k]["Cancel"]);
                                if (isCancel)
                                {
                                    p.PrintFrontStyle = FontStyle.Strikeout;
                                }
                                ticketInfo.ListProduct.Add(p);
                            }
                        }
                        //payment
                        sql = "select count(*) from ProcTickets where TicketID = @TicketID";
                        int existProcTickets = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID_Root }));
                        if (existProcTickets == 1)
                            ticketInfo.Payment = "No";
                        else
                        {
                            sql = "select count(*) from Tickets where TicketNum = @TicketID";
                            existProcTickets = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID_Root }));
                            if (existProcTickets == 1)
                                ticketInfo.Payment = "Paid";
                            else
                                ticketInfo.Payment = "Unknown";
                        }
                    }
                }
                ticketInfo.TakeOut = isToGoTicket;
                ticketInfo.DateTimeIssue = DateTime.Now;
                ticketInfo.CustomerName = CustomerName;
                ticketInfo.TicketID = DateTime.Now.GetHashCode();
                ticketInfo.DTicketNum = DTicketNum;
                ClsPublic.addListPrintItem(ticketInfo);

                //kiỂm tra tất cả item trong ticket. Còn item chưa Done thì không xóa ticket, ngược lại thì xóa
                sql = "select count(*) from dbo.PhoHa7_ProcSaleItem where Done = 0 and TicketID = @TicketID";
                count = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                if (count == 0)
                {
                    count += deleteTicketToDatabase(transaction);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                count = 0;
                try
                {
                    transaction.Rollback();
                    ClsPublic.WriteException(ex);
                }
                catch (Exception e)
                {
                    if (ClsMsgBox.LoiChung(ex, false) == 1)
                    {
                        ClsPublic.WriteException(e);
                    }
                }
            }
            //reload group table
            Frm_Kitchen frmKitchen = (Frm_Kitchen)this.Parent.TopLevelControl;
            frmKitchen.loadGroupAndImportant();
            //delete ticketview
            TicketListView parent = (TicketListView)this.Parent.Parent.Parent;
            string sql1 = "select count(*) from dbo.PhoHa7_ProcSaleItem where Done = 0 and TicketID = @TicketID and Category = @Category";
            int result = 0;
            foreach (string item in KitChenType)
            {
                result += Convert.ToInt32(sqlHelper.ExecuteScalar(sql1, CommandType.Text, transaction,
                new object[] { "@TicketID", "@Category" }, new object[] { ticketID, Convert.ToInt32(item) }));
            }
            if (result == 0)
            {
                parent.ticketDone(TicketID);
            }
            else
            {
                refreshGrid();
            }
            return count;
        }



        private int autoCompleteSpecialItems()
        {
            int count = 0;
            SqlTransaction transaction;
            transaction = ClsConnection.MySqlConn.BeginTransaction();
            try
            {
                string sql;
                if (!IsToGoTicket)
                {
                    //go each row and update item is done
                    ticketInfo.ListProduct.Clear();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        string namePrint = "";
                        int productID = Convert.ToInt32(gridView1.GetRowCellValue(i, colProductID).ToString());
                        Product p = dao.GetById<Product>(productID);
                        if (p != null && Convert.ToBoolean(p.PrintBoth))
                        {
                            namePrint = gridView1.GetRowCellValue(i, colQuality) + ")" +
                                customDisplayKitchenName(gridView1.GetRowCellValue(i, colName) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colOption) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colExtraName) + string.Empty,
                                                         Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsSmall)),
                                                         Convert.ToInt32(gridView1.GetRowCellValue(i, colMType)),
                                                         gridView1.GetRowCellValue(i, colExtraWith) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colExtraWithout) + string.Empty,
                                                         gridView1.GetRowCellValue(i, colCustomSelect) + string.Empty);
                            bool takeOut = Convert.ToBoolean(gridView1.GetRowCellValue(i, colToGo) + string.Empty);

                            if (ClsPublic.PrintBarCode)
                            {
                                namePrint = namePrint + "(" + gridView1.GetRowCellValue(i, colBarCode).ToString() + ")";
                            }
                            namePrint = namePrint + (takeOut ? "(GO)" : "");
                            object saleItemID = gridView1.GetRowCellValue(i, colSaleItemID);
                            Product product = new Product();
                            product.SaleItemID = Convert.ToInt32(gridView1.GetRowCellValue(i, colSaleItemID) + string.Empty);
                            product.PrintName = namePrint;
                            product.TakeOut = takeOut;
                            product.IsCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem));
                            product.IsChange = Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsChange));
                            product.PrintFrontStyle = FontStyle.Bold;
                            //set print cancel item
                            bool isCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem) + string.Empty);
                            if (isCancel)
                            {
                                product.PrintFrontStyle = FontStyle.Strikeout;
                            }
                            //ticketInfo.CountItemPrint++;
                            ticketInfo.ListProduct.Add(product);
                            //saleItemsName += namePrint + "|";
                            //update done item
                            sql = "update PhoHa7_ProcSaleItem set Done = 1, ToKitchen = 1 where SaleItemID  = @SaleItemID";
                            count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                            //count++;
                        }
                    }
                }
                //ticket takeout
                else
                {
                    ticketInfo.ListProduct.Clear();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        string namePrint = "";
                        Product productTogo = new Product();
                        namePrint = gridView1.GetRowCellValue(i, colQuality) + ". " +
                                        customDisplayKitchenName(gridView1.GetRowCellValue(i, colName) + string.Empty,
                                                                 gridView1.GetRowCellValue(i, colOption) + string.Empty,
                                                                 gridView1.GetRowCellValue(i, colExtraName) + string.Empty,
                                                                 Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsSmall)),
                                                                 Convert.ToInt32(gridView1.GetRowCellValue(i, colMType)),
                                                                 gridView1.GetRowCellValue(i, colExtraWith) + string.Empty,
                                                                 gridView1.GetRowCellValue(i, colExtraWithout) + string.Empty,
                                                                 gridView1.GetRowCellValue(i, colCustomSelect) + string.Empty);
                        bool takeOut = Convert.ToBoolean(gridView1.GetRowCellValue(i, colToGo) + string.Empty);
                        if (ClsPublic.PrintBarCode)
                        {
                            namePrint = namePrint + "(" + gridView1.GetRowCellValue(i, colBarCode).ToString() + ")";
                        }
                        namePrint = namePrint + (takeOut ? "(GO)" : "");
                        productTogo.PrintName = namePrint;
                        productTogo.SaleItemID = Convert.ToInt32(gridView1.GetRowCellValue(i, colSaleItemID) + string.Empty);
                        productTogo.TakeOut = takeOut;
                        productTogo.IsCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem));
                        productTogo.IsChange = Convert.ToBoolean(gridView1.GetRowCellValue(i, colIsChange));


                        int productID = Convert.ToInt32(gridView1.GetRowCellValue(i, colProductID).ToString());
                        Product p = dao.GetById<Product>(productID);
                        if (p != null && Convert.ToBoolean(p.PrintBoth))
                        {
                            productTogo.PrintFrontStyle = FontStyle.Bold;
                            //set print cancel item
                            bool isCancel = Convert.ToBoolean(gridView1.GetRowCellValue(i, colCancelItem) + string.Empty);
                            if (isCancel)
                            {
                                productTogo.PrintFrontStyle = FontStyle.Strikeout;
                            }
                            ticketInfo.ListProduct.Add(productTogo);
                            //}
                            //update done item
                            object saleItemID = gridView1.GetRowCellValue(i, colSaleItemID);
                            sql = "update PhoHa7_ProcSaleItem set Done = 1, ToKitchen = 1 where SaleItemID  = @SaleItemID";
                            count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                            //count++;
                        }
                    }

                    sql = "select coalesce(SUM(qty),0) from dbo.PhoHa7_ProcSaleItem where Done = 1 and TicketID = @TicketID";
                    ticketInfo.CountItemDonePrint = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                    ticketInfo.CountTotalItemPrint = Convert.ToInt32(sqlHelper.ExecuteScalar("select coalesce(SUM(qty),0) from PhoHa7_ProcSaleItem where TicketID = @TicketID", CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                    //print all item
                    if (ticketInfo.CountItemDonePrint == ticketInfo.CountTotalItemPrint)
                    {
                        DataTable listAllItems = sqlHelper.ExecuteDataTable("select * from PhoHa7_ProcSaleItem where TicketID = @TicketID", CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID });
                        for (int k = 0; k < listAllItems.Rows.Count; k++)
                        {
                            int tempSaleItemID = Convert.ToInt32(listAllItems.Rows[k]["SaleItemID"]);
                            bool isFindProduct = false;
                            foreach (Product p in ticketInfo.ListProduct)
                            {
                                if (p.SaleItemID == tempSaleItemID)
                                {
                                    isFindProduct = true;
                                    break;
                                }
                            }
                            if (!isFindProduct)
                            {
                                Product p = new Product();
                                p.PrintName = listAllItems.Rows[k]["Qty"] + ") " + listAllItems.Rows[k]["Description"];
                                if (ClsPublic.PrintBarCode)
                                {
                                    p.PrintName += " (" + listAllItems.Rows[k]["BarCode"] + ")";
                                }
                                p.SaleItemID = tempSaleItemID;
                                p.PrintFrontStyle = FontStyle.Regular;
                                //set print cancel item
                                bool isCancel = Convert.ToBoolean(listAllItems.Rows[k]["Cancel"]);
                                if (isCancel)
                                {
                                    p.PrintFrontStyle = FontStyle.Strikeout;
                                }
                                ticketInfo.ListProduct.Add(p);
                            }
                        }
                        //payment
                        sql = "select count(*) from ProcTickets where TicketID = @TicketID";
                        int existProcTickets = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID_Root }));
                        if (existProcTickets == 1)
                            ticketInfo.Payment = "No";
                        else
                        {
                            sql = "select count(*) from Tickets where TicketNum = @TicketID";
                            existProcTickets = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID_Root }));
                            if (existProcTickets == 1)
                                ticketInfo.Payment = "Paid";
                            else
                                ticketInfo.Payment = "Unknown";
                        }
                    }
                }
                if (ticketInfo.ListProduct.Count > 0)
                {
                    ticketInfo.TakeOut = isToGoTicket;
                    ticketInfo.DateTimeIssue = DateTime.Now;
                    ticketInfo.CustomerName = CustomerName;
                    ticketInfo.TicketID = DateTime.Now.GetHashCode();
                    ticketInfo.DTicketNum = DTicketNum;
                    ClsPublic.addListPrintItem(ticketInfo);

                    //kiỂm tra tất cả item trong ticket. Còn item chưa Done thì không xóa ticket, ngược lại thì xóa
                    sql = "select count(*) from dbo.PhoHa7_ProcSaleItem where Done = 0 and TicketID = @TicketID";
                    count = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                    if (count == 0)
                    {
                        count += deleteTicketToDatabase(transaction);
                    }
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }

            }
            catch (System.Exception ex)
            {
                count = 0;
                try
                {
                    transaction.Rollback();
                    ClsPublic.WriteException(ex);
                }
                catch (Exception e)
                {
                    if (ClsMsgBox.LoiChung(ex, false) == 1)
                    {
                        ClsPublic.WriteException(e);
                    }
                }
            }

            ////reload group table
            //Frm_Kitchen frmKitchen = (Frm_Kitchen)this.Parent.TopLevelControl;
            //frmKitchen.loadGroupAndImportant();
            ////delete ticketview
            //if (count > 0)
            //{
            //    //hide ticket
            //    parent.ticketDone(TicketID);
            //    //print completed items
            //    Thread t = new Thread(new ThreadStart(printItem));
            //    t.Start();
            //    //printItem();
            //}

            //delete ticketview
            //reload group table
            Frm_Kitchen frmKitchen = (Frm_Kitchen)this.Parent.TopLevelControl;
            frmKitchen.loadGroupAndImportant();
            //delete ticketview
            TicketListView parent = (TicketListView)this.Parent.Parent.Parent;
            string sql1 = "select count(*) from dbo.PhoHa7_ProcSaleItem where Done = 0 and TicketID = @TicketID and Category = @Category";
            int result = 0;
            foreach (string item in KitChenType)
            {
                result += Convert.ToInt32(sqlHelper.ExecuteScalar(sql1, CommandType.Text, transaction,
                new object[] { "@TicketID", "@Category" }, new object[] { ticketID, Convert.ToInt32(item) }));
            }
            if (result == 0)
            {
                parent.ticketDone(TicketID);
            }
            else
            {
                refreshGrid();
            }
            return count;
        }


        public void refreshGrid()
        {
            string sql = "select s.*, OrderBy=0, p.PrintBoth from PhoHa7_ProcSaleItem s left join Products p on s.ProductID=p.ProductID where TicketID = @TicketID and Done = 0";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
            gControlGroup.DataSource = null;
            gControlGroup.DataSource = dt;
        }

        public void refreshGridIfChanged()
        {
            string sql = "select s.*, OrderBy=0, p.PrintBoth from PhoHa7_ProcSaleItem s left join Products p on s.ProductID=p.ProductID where TicketID = @TicketID";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
            bool isChange = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable gridDatatable = (DataTable)gControlGroup.DataSource;
                for (int j = 0; j < gridDatatable.Rows.Count; j++)
                {
                    bool isInCategory = false;
                    foreach (string item in KitChenType)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["Category"].ToString()) == Convert.ToInt32(item))
                        {
                            isInCategory = true;
                            break;
                        }
                    }
                    if (dt.Rows[i]["SaleItemID"].ToString() == gridDatatable.Rows[j]["SaleItemID"].ToString() && !isInCategory)
                    {
                        if (dt.Rows[i]["Done"].ToString() != gridDatatable.Rows[j]["Done"].ToString() ||
                            dt.Rows[i]["Done1"].ToString() != gridDatatable.Rows[j]["Done1"].ToString() ||
                            dt.Rows[i]["ToKitchen"].ToString() != gridDatatable.Rows[j]["ToKitchen"].ToString() ||
                            dt.Rows[i]["Cancel"].ToString() != gridDatatable.Rows[j]["Cancel"].ToString() ||
                            dt.Rows[i]["IsChange"].ToString() != gridDatatable.Rows[j]["IsChange"].ToString())
                        {
                            isChange = true;
                        }
                    }
                }
            }
            if (isChange)
            {
                DataTable gridDatatable = (DataTable)gControlGroup.DataSource;
                sql = "select s.*, OrderBy=0, p.PrintBoth from PhoHa7_ProcSaleItem s left join Products p on s.ProductID=p.ProductID where TicketID = @TicketID and Done = 0";
                DataTable newdt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
                for (int i = 0; i < newdt.Rows.Count; i++)
                {
                    int categoryID = (int)newdt.Rows[i]["Category"];
                    foreach (string item in KitChenType)
                    {
                        if (categoryID == Convert.ToInt32(item))
                        {
                            newdt.Rows[i]["OrderBy"] = 1;
                            break;
                        }
                    }
                    for (int j = 0; j < gridDatatable.Rows.Count;j++ )
                    {
                        if (newdt.Rows[i]["SaleItemID"].ToString() == gridDatatable.Rows[j]["SaleItemID"].ToString())
                        {
                            newdt.Rows[i]["Done"] = gridDatatable.Rows[j]["Done"];
                            break;
                        }
                    }
                }
                DataTable temp = newdt.Select(null, "OrderBy desc").CopyToDataTable();

                gControlGroup.DataSource = null;
                gControlGroup.DataSource = temp;
            }

        }

        //string KitchenType = ClsPublic.KITCHEN_TYPE;
        public void updateGridview()
        {
            int count = 0;

            //change table
            string sql = "select * from PhoHa7_ProcTickets where TicketID = @TicketID";
            DataRow tableInfo = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID }).Rows[0];
            //change table name
            if (tableInfo["TableName"].ToString() != TableName)
            {
                tableNameOld = TableName;
                TableName = tableInfo["TableName"].ToString();
                blinkTableName = 0;
            }
            //change employee name
            EmpName = tableInfo["EmployeeName"].ToString();
            //change togo
            IsToGoTicket = (bool)tableInfo["TakeOut"];
            //change emergency
            Emergency = (bool)tableInfo["Emergency"];
            //customer name
            CustomerName = tableInfo["CustomerName"].ToString();
            //customer name
            DateTimeIssue = Convert.ToDateTime(tableInfo["DateTimeIssue"]);

            //check sale item is tokitchen
            sql = "select COUNT(*) from dbo.PhoHa7_ProcSaleItem s where TicketID=@TicketID and ToKitchen = 0 and (";
            for (int i = 0; i < KitChenType.Length; i++)
            {
                if (i < KitChenType.Length - 1)
                {
                    sql += " Category= " + Convert.ToInt32(KitChenType[i]) + " or";
                }
                else
                {
                    sql += " Category= " + Convert.ToInt32(KitChenType[i]) + ")";
                }
            }
            int countItemToKitchen = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID }));
            if (countItemToKitchen > 0)
            {
                SqlTransaction transaction;
                transaction = ClsConnection.MySqlConn.BeginTransaction();
                try
                {

                    sql = "select s.*, OrderBy=0, p.PrintBoth from PhoHa7_ProcSaleItem s left join Products p on s.ProductID=p.ProductID where TicketID = @TicketID and Done = 0";
                    DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID });
                    //this.DataSource = null;
                    this.DataSource = dt;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int saleItemID = Convert.ToInt32(dt.Rows[i]["SaleItemID"]);
                        string category = dt.Rows[i]["Category"] + string.Empty;
                        for (int j = 0; j < KitChenType.Length; j++)
                        {
                            if (category == KitChenType[j])
                            {
                                sql = "UPDATE PhoHa7_ProcSaleItem set ToKitchen = 1 where SaleItemID = @SaleItemID";
                                count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                                break;
                            }
                        }
                    }
                    //update ticket that is show to kitchen
                    string s = "select count(*) from PhoHa7_ProcSaleItem where TicketID=@TicketID and ToKitchen = 0";
                    count = Convert.ToInt32(sqlHelper.ExecuteScalar(s, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
                    if (count == 0)
                    {
                        int result = sqlHelper.ExecuteNonQuery("update PhoHa7_Proctickets set ToKitChen = 1 where TicketID = @TicketID", CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID });
                    }

                    sql = "update PhoHa7_ProcTickets set [ToKitchen] = 0, IsChange = 0 where TicketID = @TicketID";
                    sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID });
                    transaction.Commit();
                }
                catch (System.Exception ex)
                {
                    count = 0;
                    try
                    {
                        transaction.Rollback();
                        if (ClsMsgBox.LoiChung(ex, false) == 1)
                        {
                            ClsPublic.WriteException(ex);
                        }
                    }
                    catch (Exception e)
                    {
                        if (ClsMsgBox.LoiChung(ex, false) == 1)
                        {
                            ClsPublic.WriteException(ex);
                        }
                    }
                }
            }


        }

        private int deleteTicketToDatabase(SqlTransaction transaction)
        {
            //xoa ticket trong database
            string sql = "delete from PhoHa7_ProcSaleItem where TicketID = @TicketID";
            int result = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID });
            sql = "delete from PhoHa7_ProcTickets where TicketID = @TicketID";
            result = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID });
            return result;
        }

        string customDisplayKitchenName(string kitchenName, string optionRequire, string extraName, bool isSmall, int mType, string extraWith, string extraWithout, string customSelect)
        {
            string name = "";
            //custom select
            if (customSelect != "")
            {
                string[] arrCustomSelect = customSelect.ToString().Split('|');
                for (int i = 0; i < arrCustomSelect.Length; i++)
                {
                    name = name + " " + arrCustomSelect[i];
                }
            }
            if (extraName != "")
            {
                name += "(";
                string[] arrExtraName = extraName.ToString().Split('|');
                for (int i = 0; i < arrExtraName.Length; i++)
                {
                    name += arrExtraName[i];
                    if (i == (arrExtraName.Length - 1))
                    {
                        name += ")";
                    }
                    else
                    {
                        name += ", ";
                    }
                }
            }
            //extra with
            if (extraWith != "")
            {
                name += "(";
                string[] arrExtraWith = extraWith.ToString().Split('|');
                for (int i = 0; i < arrExtraWith.Length; i++)
                {
                    name += arrExtraWith[i];
                    if (i == (arrExtraWith.Length - 1))
                    {
                        name += ")";
                    }
                    else
                    {
                        name += ", ";
                    }
                }
            }
            //extra without
            if (extraWithout != "")
            {
                name += "(";
                string[] arrExtraWithout = extraWithout.ToString().Split('|');
                for (int i = 0; i < arrExtraWithout.Length; i++)
                {
                    name += arrExtraWithout[i];
                    if (i == (arrExtraWithout.Length - 1))
                    {
                        name += ")";
                    }
                    else
                    {
                        name += ", ";
                    }
                }
            }
            if (optionRequire != "")
            {
                name += "(" + optionRequire + string.Empty + ")";
            }
            if (mType == 4)
            {
                if (isSmall)
                {
                    name += "(" + ClsPublic.SizeSmall + ")";
                }
                else
                {
                    name += "(" + ClsPublic.SizeLarge + ")";
                }
            }
            //if (e.Column.FieldName == "Description")
            //{
            //    string text = string.Format(e.DisplayText.Replace("(" + ClsPublic.SizeLarge + ")", "<color=" + ClsPublic.ForceColorLageSize.Name + ">(" + ClsPublic.SizeLarge + ")</color>"));
            //    e.DisplayText = text;
            //    //e.Handled = true;
            //}
            return kitchenName + " " + name;
        }



        #endregion

        #region Print item

        void printItem()
        {
            PrintDocument pdoc = new PrintDocument();
            PrintController printController = new StandardPrintController();
            pdoc.PrintController = printController;
            if (ClsPublic.Printers != "")
            {
                pdoc.PrinterSettings.PrinterName = ClsPublic.Printers;
            }
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            pdoc.Print();
        }

        void printItemSpecial()
        {
            PrintDocument pdoc = new PrintDocument();
            PrintController printController = new StandardPrintController();
            pdoc.PrintController = printController;
            if (ClsPublic.LookUpPrinterForSpecialItem != "")
            {
                pdoc.PrinterSettings.PrinterName = ClsPublic.LookUpPrinterForSpecialItem;
            }
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            pdoc.Print();
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int fontSize = ClsPublic.FontSizePrint;

            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            //qua trai
            int startX = 0;
            //len xuong
            int startY = ClsPublic.PaddingSizePrint;
            //khoang cach
            DateTime dt = DateTime.Now;
            //in thông tin của ticket
            //table
            RectangleF header2Rect = new RectangleF();
            //print table name
            if (IsToGoTicket)
            {

            }

            //print DTicketNum
            if (IsToGoTicket)
            {
                //count item done
                using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                {
                    string text1 = ticketInfo.CountItemDonePrint + " of " + ticketInfo.CountTotalItemPrint;
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    //
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(280, ((int)e.Graphics.MeasureString(text1, useFont, 290, stringFormat).Height));
                    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString(text1, useFont, Brushes.Black, header2Rect, stringFormat);
                }
                startY = startY + (int)header2Rect.Size.Height;
                //print customer name
                if ((ticketInfo.CustomerName + string.Empty) != string.Empty)
                {
                    using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                    {
                        header2Rect.Location = new Point(startX, startY);
                        header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Customer: " + ticketInfo.CustomerName, useFont, 290, StringFormat.GenericTypographic).Height));
                        //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                        graphics.DrawString("Customer: " + ticketInfo.CustomerName, useFont, Brushes.Black, header2Rect);
                    }
                    startY = startY + (int)header2Rect.Size.Height;
                }
                //print table name
                using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Table: " + tableName, useFont, 290, StringFormat.GenericTypographic).Height));
                    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString("Table: " + tableName, useFont, Brushes.Black, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;
                //DTicketNum
                using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Ticket: #" + ticketInfo.DTicketNum, useFont, 290, StringFormat.GenericTypographic).Height));
                    graphics.FillRectangle(Brushes.Black, header2Rect);
                    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString("Ticket: #" + ticketInfo.DTicketNum, useFont, Brushes.White, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;
                //payment
                using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Payment: " + ticketInfo.Payment, useFont, 290, StringFormat.GenericTypographic).Height));
                    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString("Payment: " + ticketInfo.Payment, useFont, Brushes.Black, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;
            }
            else
            {
                using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Table: " + tableName, useFont, 290, StringFormat.GenericTypographic).Height));
                    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString("Table: " + tableName, useFont, Brushes.Black, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;
                //print customer name
                if ((ticketInfo.CustomerName + string.Empty) != string.Empty)
                {
                    using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                    {
                        header2Rect.Location = new Point(startX, startY);
                        header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Customer: " + ticketInfo.CustomerName, useFont, 290, StringFormat.GenericTypographic).Height));
                        //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                        graphics.DrawString("Customer: " + ticketInfo.CustomerName, useFont, Brushes.Black, header2Rect);
                    }
                    startY = startY + (int)header2Rect.Size.Height;
                }
            }

            //emp name
            using (Font useFont = new Font("Courier New", fontSize, FontStyle.Regular))
            {
                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Server: " + empName, useFont, 290, StringFormat.GenericTypographic).Height));
                graphics.DrawString("Server: " + empName, useFont, Brushes.Black, header2Rect);
            }
            startY = startY + (int)header2Rect.Size.Height;
            using (Font useFont = new Font("Courier New", 12, FontStyle.Regular))
            {
                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Date: " + String.Format("{0:M/d/yyyy HH:mm:ss}", dt), useFont, 290, StringFormat.GenericTypographic).Height));
                graphics.DrawString("Date: " + String.Format("{0:M/d/yyyy HH:mm}", dt), useFont, Brushes.Black, header2Rect);
            }
            startY = startY + (int)header2Rect.Size.Height;

            //print items
            foreach (Product item in ticketInfo.ListProduct)
            {
                //print item
                using (Font useFont = new Font("Courier New", fontSize, item.PrintFrontStyle))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(280, ((int)e.Graphics.MeasureString(item.PrintName, useFont, 280, StringFormat.GenericTypographic).Height));
                    if (item.PrintName.EndsWith("(GO)"))
                    {
                        graphics.FillRectangle(Brushes.Black, header2Rect);
                        graphics.DrawString(item.PrintName, useFont, Brushes.White, header2Rect);
                    }
                    else
                    {
                        graphics.DrawString(item.PrintName, useFont, Brushes.Black, header2Rect);
                    }

                }
                startY = startY + (int)header2Rect.Size.Height;
            }
            using (Font useFont = new Font("Courier New", 12, FontStyle.Bold))
            {
                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(280, ((int)e.Graphics.MeasureString("----------------------", useFont, 280, StringFormat.GenericTypographic).Height));
                graphics.DrawString("----------------------", useFont, Brushes.Black, header2Rect);
            }


        }
        #endregion

        private void TicketView_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                timer1.Stop();
            }else if (Visible)
            {
                timer1.Start();
            }
        }

        private void TicketView_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void TicketView_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(e.KeyChar.ToString());
            //if (lblNumberOfOrder.Text == "1")
            //{
            //    if (e.KeyChar == ' ')
            //    {
            //        completeAll();
            //    }
            //}
        }

















    }
}
