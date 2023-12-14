using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid;
using System.Drawing.Printing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PhoHa7.Library.Classes.Connection;
using DevExpress.XtraGrid.Columns;

namespace PhoHa7
{
    public partial class TicketView : DevExpress.XtraEditors.XtraUserControl
    {
        SqlHelper sqlHelper;
        int ticketID;
        string tableName;
        string empName;
        public object DataSource
        {
            get { return gControlGroup.DataSource; }
            set
            {
                gControlGroup.DataSource = value;
            }
        }

        public int TicketID
        {
            get { return ticketID; }
            set { ticketID = value; }

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
            }
        }

        public string EmpName { get; set; }

        public TicketView()
        {
            InitializeComponent();
            sqlHelper = new SqlHelper();
            //
            KitChenType = ClsPublic.KITCHEN_TYPE.Split(',');
            timer1.Start();
        }

        int timer = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //countTime();
        }

        public void countTime()
        {
            int timeLeft = ClsPublic.TIMER_ORDER_COUNT_DOWN * 60;
            timer++;
            if (timer % 10 == 0)
            {
                int min = timer / 600;
                int sec = timer / 10 - (min * 60);
                lblTime.Text = min + ":" + sec;

                if (timer / 10 > timeLeft)
                {
                    //change text to red corlor
                    this.lblTime.Appearance.ForeColor = System.Drawing.Color.Red;
                }
            }
        }





        string saleItemsName = "";
        //done ticket-->delete ticket
        private void btnCompleted_Click(object sender, EventArgs e)
        {
            int count = completeTicket();
            if (count > 0)
            {
                printItem();
            }

        }

        private void btnCompleteNoPrint_Click(object sender, EventArgs e)
        {
            completeTicket();
        }

        private int completeTicket()
        {
            TicketListView parent = (TicketListView)this.Parent.Parent.Parent;
            string sql;
            int count = 0;
            //go each row and update item is done
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if ((bool)gridView1.GetRowCellValue(i, colCompleted))
                {
                    object saleItemID = gridView1.GetRowCellValue(i, colSaleItemID);
                    saleItemsName += gridView1.GetRowCellValue(i, colName).ToString() + "(" + gridView1.GetRowCellValue(i, colBarCode).ToString() + ")" + ";";
                    sql = "update PhoHa7_ProcSaleItem set Done = 1 where SaleItemID  = @SaleItemID";
                    sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@SaleItemID" }, new object[] { saleItemID });
                    count++;
                }
            }
            //check item that is not completed
            sql = "select count(*) from dbo.PhoHa7_ProcSaleItem where Done = 0 and TicketID = @TicketID and Category = @Category";
            int result = 0;
            foreach (string item in KitChenType)
            {
                result += Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text,
                new object[] { "@TicketID", "@Category" }, new object[] { ticketID, Convert.ToInt32(item) }));
            }
            if (result == 0)
            {
                parent.ticketDone(TicketID);
            }
            else
            {
                updateGridview();
            }

            //kiỂm tra tất cả item trong ticket. Còn item chưa Done thì không xóa ticket, ngược lại thì xóa
            sql = "select count(*) from dbo.PhoHa7_ProcSaleItem where Done = 0 and TicketID = @TicketID";
            result = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID }));
            if (result == 0)
            {
                deleteTicketToDatabase();
            }
            //print completed items
            return count;
        }



        private void btnCompleteAll_Click(object sender, EventArgs e)
        {
            TicketListView parent = (TicketListView)this.Parent.Parent.Parent;
            //delete ticket
            //kiỂm tra tất cả item trong ticket. Còn item chưa Done thì không xóa ticket, ngược lại thì xóa
            string sql = "select count(*) from dbo.PhoHa7_ProcSaleItem where Done = 0 and TicketID = @TicketID";
            int result = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID }));
            if (result == 0)
            {
                deleteTicketToDatabase();
            }
            //hide ticket
            parent.ticketDone(TicketID);
            //print completed items
            printItem();
        }

        private int deleteTicketToDatabase()
        {
            //xoa ticket trong database
            string sql = "delete from PhoHa7_ProcSaleItem where TicketID = @TicketID";
            int result = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
            sql = "delete from PhoHa7_ProcTickets where TicketID = @TicketID";
            result = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
            return result;
        }

        //change done state item
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            bool temp = !(bool)(gridView1.GetFocusedRow() as DataRowView).Row["Done"];
            (gridView1.GetFocusedRow() as DataRowView).Row["Done"] = temp;
            ////update value col temp
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "colTemp", temp);
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //  ColumnView a = (ColumnView)gridView1.GetRow(gridView1.FocusedRowHandle);
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
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
                    e.Appearance.ForeColor = Color.LightGray;
                    //e.Appearance.BackColor = Color.Gray;
                    // e.Appearance.BackColor2 = Color.Gray;
                    gridView1.SelectRow(e.RowHandle + 1);
                }
                //draw take out item
                bool takeOut = Convert.ToBoolean(View.GetRowCellValue(e.RowHandle, colToGo));
                if (takeOut)
                {
                    e.Appearance.ForeColor = ClsPublic.ColorLetterToGo;
                    e.Appearance.BackColor = ClsPublic.ColorBackgroundToGo;
                    e.Appearance.BackColor2 = ClsPublic.ColorBackgroundToGo;
                }
                //draw is change - item is changed
                bool isChange = Convert.ToBoolean(View.GetRowCellValue(e.RowHandle, colIsChange));
                if (takeOut)
                {
                    if (isInCategory)
                    {
                        e.Appearance.ForeColor = ClsPublic.ColorLetterItemChange;
                        e.Appearance.BackColor = ClsPublic.ColorBackgroundItemChange;
                        e.Appearance.BackColor2 = ClsPublic.ColorBackgroundItemChange;
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

        private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
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
            if (!isInCategory)
            {
                this.gridView1.FocusedRowHandle = e.PrevFocusedRowHandle;
            }

        }

        void updateGridview()
        {
            DataSet ds = new DataSet();
            string sql = "select * from PhoHa7_ProcSaleItem where TicketID = @TicketID and Done = 0";
            sqlHelper.ExecuteDataSet(ds, ticketID.ToString(), sql, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
            this.DataSource = ds.Tables[0];
        }

        #region Print item

        void printItem()
        {
            PrintDocument pdoc = new PrintDocument();
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
            int startY = 0;
            //khoang cach
            int Offset = ClsPublic.PaddingSizePrint;
            //in thông tin của ticket
            graphics.DrawString("Table: " + tableName, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Server: " + empName, new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            DateTime dt = DateTime.Now;
            graphics.DrawString("Date: " + String.Format("{0:M/d/yyyy HH:mm:ss}", dt), new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            string[] name = saleItemsName.Split(';');
            //duyÊt qua từng row xem item nào đã check
            foreach (string item in name)
            {
                //print item
                graphics.DrawString(item, new Font("Courier New", fontSize, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + ClsPublic.PaddingSizePrint;
                graphics.DrawString("", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            }

            //in footer
            graphics.DrawString("-", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            //clear name
            saleItemsName = "";
        }
        #endregion





    }
}
