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
using PhoHa7.Library.Classes.Common;
using PhoMac.Main.PhoMac_System;
using System.Drawing.Printing;
using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Froms;
using PhoMac.Main.Entities;
using DevExpress.XtraGrid.Views.Grid;
using PhoMac.Model;
using PhoMac.Main.Controller;
using PhoMac.Main.GUI.Kitchen;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PhoMac.Model.Data;
using PhoMac.Model.Factory;
using PhoMac.Main.Test;
using SocketServerApp;

namespace PhoMac.Main.GUI
{
    public partial class Frm_Kitchen : DevExpress.XtraEditors.XtraForm
    {
        SqlHelper sqlHelper;
        string[] KitchenType { get; set; }
        TicketListView ticketListView2;
        DataSet ds;

        public Frm_Kitchen()
        {
            InitializeComponent();

            //
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(); ;
            //player.SoundLocation = @"C:\Windows\Media\Ring07.wav";
            //player.Play;

            timer1.Start();
            sqlHelper = new SqlHelper();
            KitchenType = ClsPublic.KITCHEN_TYPE.Split(',');
            loadGroupAndImportant();
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;


            //Thread t = new Thread(new ThreadStart(ClsPublic.handleCompleTicket));
            //t.Start();
            if (ClsPublic.HandleCompleteTicketFromSquare)
            {
                ClsPublic.threadHandleCompleteTicket = new Thread(delegate()
                {
                    ClsPublic.handleCompleTicket();
                });
                ClsPublic.threadHandleCompleteTicket.Start();
            }
           
            //get square product list
            Task.Run(() =>  ClsPublic.getSquareProductList()).Wait(20000);

            ClsPublic.socketServer.StartServer();
        }


        private SocketServer socketServer;
        void startSocketServer()
        {
            socketServer = new SocketServer();
            socketServer.MessageReceived += SocketServer_MessageReceived;

            socketServer.StartServer();
            //LogMessage("Server started. Waiting for connections...");
        }

        private void SocketServer_MessageReceived(object sender, string message)
        {
            // This event handler will be called whenever a message is received from any client
            //LogMessage("Received message: " + message);
            var msg = message.Split('|');


        }

        private void Kitchen_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            //// ticketListView2
            //// 
            ticketListView2 = new TicketListView();
            this.panelTicketList.Controls.Add(this.ticketListView2);
            this.ticketListView2.Dock = DockStyle.Fill;
            this.ticketListView2.AutoSize = true;
            this.ticketListView2.Location = new System.Drawing.Point(0, 0);
            this.ticketListView2.Name = "ticketListView2";
            //this.ticketListView2.NumOfTickets = 4;
            this.ticketListView2.Size = new System.Drawing.Size(769, 720);
            this.ticketListView2.TabIndex = 4;

            loadGroupAndImportant();

            //get screen resolution
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            if (ClsPublic.VerticalSplitGroupTablePosition == 0)
                this.splitContainerControl1.SplitterPosition = (screenWidth / 5) * 4;
            else
                this.splitContainerControl1.SplitterPosition = ClsPublic.VerticalSplitGroupTablePosition;

            if (ClsPublic.HorizontalSplitGroupTablePosition == 0)
                this.splitContainerControl2.SplitterPosition = screenHeight / 2;
            else
                this.splitContainerControl2.SplitterPosition = ClsPublic.HorizontalSplitGroupTablePosition;
            //show/hide Group Table and Filter Table
            ShowHideGroupFilterTable(screenHeight, screenWidth);


            //Print last bills
            ClsPublic.ListPrintItem = new List<PhoHa7_ProcTickets>();

            //test

            SplashScreenManager.CloseForm();
        }

        private void ShowHideGroupFilterTable(int screenHeight, int screenWidth)
        {
            gControlGroup.Visible = ClsPublic.ShowGroupTable;
            gControlImportant.Visible = ClsPublic.ShowFilterTable;


            //hide filter table
            if (!ClsPublic.ShowFilterTable)
                this.splitContainerControl2.SplitterPosition = screenHeight;
            //hide group table
            if (!ClsPublic.ShowGroupTable)
                this.splitContainerControl2.SplitterPosition = 0;

            if (!ClsPublic.ShowGroupTable && !ClsPublic.ShowFilterTable)
            {
                splitContainerControl2.Visible = false;
                this.splitContainerControl1.SplitterPosition = screenWidth;
                ClsPublic.VerticalSplitGroupTablePosition = screenWidth;
            }
            else
            {
                //hori
                if (ClsPublic.VerticalSplitGroupTablePosition == screenWidth || (ClsPublic.VerticalSplitGroupTablePosition - screenWidth) < 0)
                {
                    this.splitContainerControl1.SplitterPosition = (screenWidth / 5) * 4;
                }
                //vertical
                if (splitContainerControl2.SplitterPosition == screenHeight || splitContainerControl2.SplitterPosition == 0 || (splitContainerControl2.SplitterPosition - screenHeight) < 0)
                {
                    this.splitContainerControl2.SplitterPosition = screenHeight / 2;
                }

            }

            if (!ClsPublic.ShowGroupTable && ClsPublic.ShowFilterTable)
            {
                this.splitContainerControl2.SplitterPosition = 0;
                if (ClsPublic.VerticalSplitGroupTablePosition == screenWidth || (ClsPublic.VerticalSplitGroupTablePosition - screenWidth) < 0)
                {
                    this.splitContainerControl1.SplitterPosition = (screenWidth / 5) * 4;
                }
            }

            if (ClsPublic.ShowGroupTable && !ClsPublic.ShowFilterTable)
            {
                this.splitContainerControl2.SplitterPosition = screenHeight;
                if (ClsPublic.VerticalSplitGroupTablePosition == screenWidth || (ClsPublic.VerticalSplitGroupTablePosition - screenWidth) < 0)
                {
                    this.splitContainerControl1.SplitterPosition = (screenWidth / 5) * 4;
                }
            }

        }

        //load gridview group and important
        List<List<int>> listTicketGroup = new List<List<int>>();
        public void loadGroupAndImportant()
        {
            ds = new DataSet();
            //clear all table
            if (ds.Tables["GroupTable"] != null)
            {
                ds.Tables["GroupTable"].Clear();
            }
            if (ds.Tables["GroupImportant"] != null)
            {
                ds.Tables["GroupImportant"].Clear();
            }
            object[] cat = new object[KitchenType.Length];

            string sql = "select * from PhoHa7_ProcTickets";
            DataTable dtProcTickets = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);

            int tempOrderBy = 0;
            int tempNumberOfGroupTable = 0;

            for (int i = 0; i < dtProcTickets.Rows.Count; i++)
            {
                int temp = i / ClsPublic.NumOfGroupTable;
                int ticketID = Convert.ToInt32(dtProcTickets.Rows[i]["TicketID"]);
                //check count item kitchentype
                sql = "select count(*) from PhoHa7_ProcSaleItem where TicketID = " + ticketID + " and ( ";
                for (int g = 0; g < KitchenType.Length; g++)
                {
                    if (g < KitchenType.Length - 1)
                    {
                        sql += " Category= " + Convert.ToInt32(KitchenType[g]) + " or";
                    }
                    else
                    {
                        sql += " Category= " + Convert.ToInt32(KitchenType[g]) + ")  and Done = 0";
                    }
                }
                int countItems = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, null, null));
                if (countItems == 0)
                {
                    continue;
                }

                bool isFind = false;
                foreach (List<int> item in listTicketGroup)
                {
                    if (item.Exists(x => x == ticketID))
                    {
                        isFind = true;
                        break;
                    }
                }
                if (!isFind)
                {
                    if (listTicketGroup.Count > 0)
                    {
                        tempNumberOfGroupTable = listTicketGroup[listTicketGroup.Count - 1].Count;
                        if (tempNumberOfGroupTable >= (ClsPublic.NumOfGroupTable))
                        {
                            tempNumberOfGroupTable = 0;
                        }
                    }
                    else
                        tempNumberOfGroupTable = 0;

                    if (tempNumberOfGroupTable == 0)
                    {
                        List<int> listItemTicketID = new List<int>();
                        listItemTicketID.Add(ticketID);
                        listTicketGroup.Add(listItemTicketID);
                    }
                    else
                    {
                        List<int> listItemTicketID = listTicketGroup[listTicketGroup.Count - 1];
                        listItemTicketID.Add(ticketID);
                    }
                }
            }
            List<List<int>> delItems = new List<List<int>>();
            foreach (List<int> item in listTicketGroup)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    //
                    if (i == 0)
                    {
                        if (item[i] % 2 == tempOrderBy % 2)
                            tempOrderBy = item[i] + 1;
                        else
                            tempOrderBy = item[i];
                        sql = "select SUM(Qty) as Quality , Description, IsSmall, MType, OrderBy = " + tempOrderBy + " from PhoHa7_ProcSaleItem where (TicketID = " + item[i];
                        if (i == (item.Count - 1))
                        {
                            sql += ") and ( ";
                            for (int g = 0; g < KitchenType.Length; g++)
                            {
                                if (g < KitchenType.Length - 1)
                                {
                                    sql += " Category= " + Convert.ToInt32(KitchenType[g]) + " or";
                                }
                                else
                                {
                                    sql += " Category= " + Convert.ToInt32(KitchenType[g]) + ")  and Done = 0 group by description, IsSmall, MType,OptionRequire,ExtraWith,ExtraWithout,CustomSelect order by Quality desc";
                                }
                            }
                            //exec
                            //DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
                            //if (dt.Rows.Count > 0)
                            //{
                            //    if (ds.Tables["GroupTable"] == null)
                            //        ds.Tables.Add("GroupTable");
                            //    ds.Tables["GroupTable"].Merge(dt);
                            //}
                            //else
                            //{
                            //    delItems.Add(item);
                            //}
                        }
                    }
                    else
                    {
                        sql = sql + " or TicketID = " + item[i];
                        //add kitchentype
                        if (i == (item.Count - 1))
                        {
                            sql += ") and ( ";
                            for (int g = 0; g < KitchenType.Length; g++)
                            {
                                if (g < KitchenType.Length - 1)
                                {
                                    sql += " Category= " + Convert.ToInt32(KitchenType[g]) + " or";
                                }
                                else
                                {
                                    sql += " Category= " + Convert.ToInt32(KitchenType[g]) + ")  and Done = 0 group by description, IsSmall, MType order by Quality desc";
                                }
                            }

                        }
                    }
                }
                //exc datatable
                //exec
                DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
                if (dt.Rows.Count > 0)
                {
                    if (ds.Tables["GroupTable"] == null)
                        ds.Tables.Add("GroupTable");
                    ds.Tables["GroupTable"].Merge(dt);
                }
                else
                {
                    delItems.Add(item);
                }

            }
            foreach (var item in delItems)
            {
                listTicketGroup.Remove(item);
            }
            delItems.Clear();

            gControlGroup.DataSource = ds.Tables["GroupTable"];
            gViewGroup.ClearSelection();


            //load important table
            sql = "select SUM(p.Qty) as Quality,p.Description, IsSmall, MType from PhoHa7_ProcSaleItem p" +
                " left JOIN (select * from PhoHa7_FilterSaleItem) f ON p.ProductID = f.ProductID" +
                " where f.ProductID > 0 and  p.Done = 0 and (";
            for (int i = 0; i < KitchenType.Length; i++)
            {
                if (i < KitchenType.Length - 1)
                {
                    sql += " p.Category = " + Convert.ToInt32(KitchenType[i]) + " or";
                }
                else
                {
                    sql += " p.Category= " + Convert.ToInt32(KitchenType[i]) + ")  group by p.Description,IsSmall, MType order by Quality desc";
                }
            }

            ////get all item in a category
            //foreach (string item in KitchenType)
            //{
            sqlHelper.ExecuteDataSet(ds, "GroupImportant", sql, CommandType.Text, null, null);
            //}
            gControlImportant.DataSource = ds.Tables["GroupImportant"];
        }

        //int time = 0;
        int timeResetPrintPosition = 0;
        bool isRunningHandleTicket = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (time < 50)
            //{
            //    time++;
            //}
            //else
            //{
            //    //loadGroupAndImportant();
            //    time = 0;
            //}
            if (timeResetPrintPosition < 7)
            {
                timeResetPrintPosition++;
            }
            else
            {
                timeResetPrintPosition = 0;
                if (ClsPublic.ListPrintItem.Count > 0)
                {
                    ClsPublic.PrintPosition = ClsPublic.ListPrintItem.Count - 1;
                }


                //Thread t;
                if (ClsPublic.responseOrderDetails != "")
                {

                }
                if (isRunningHandleTicket && ClsPublic.responseOrderDetails != "")
                {
                    //updateCompletedTicket();
                }
            }
        }

        void updateCompletedTicket()
        {
            var json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(ClsPublic.responseOrderDetails.ToString());

            Dao dao = new Dao(false, true);
            EntityFactory.getInstance().BeginTransactionEntities();
            var listProcTickets = dao.GetAll<ProcTicket>();
            foreach (var listProcTicket in listProcTickets)
            {
                foreach (var item in json["orders"])
                {
                    string ticketID = "";
                    try
                    {
                        ticketID = item.SelectToken("line_items[0].note").ToString();
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }
                    string[] ticketIDSaleItemID = ticketID.Split('|');
                    //ticketIDSaleItemID[0] = "111720";
                    if (ticketIDSaleItemID[0] == listProcTicket.TicketID.ToString())
                    {


                        //copy procticket to ticket table
                        ProcTicket procTicket = dao.GetById<ProcTicket>(Convert.ToInt32(ticketIDSaleItemID[0]));
                        //get tax, subtotal, credit amount, cash amount from Square to ProcTicket
                        procTicket.Tax = Convert.ToDecimal(item.SelectToken("line_items[0].total_tax_money.amount").ToString()) / 100;
                        procTicket.TotalP = Convert.ToDecimal(item.SelectToken("line_items[0].gross_sales_money.amount").ToString()) / 100;

                        var tenders = item.SelectToken("tenders");
                        foreach (var tender in tenders)
                        {
                            if (tender["type"].ToString() == "CASH")
                            {
                                procTicket.PaidCash += Convert.ToDecimal(tender.SelectToken("amount_money.amount").ToString()) / 100;
                            }
                            else if (tender["type"].ToString() == "CARD")
                            {
                                procTicket.CardCode = tender.SelectToken("card_details.card.last_4").ToString();
                                procTicket.PaidCredit += Convert.ToDecimal(tender.SelectToken("amount_money.amount").ToString()) / 100;
                            }
                        }
                        Ticket ticket = copyProcTicket2Ticket(procTicket);
                        ticket = dao.Add1<Ticket>(ticket);
                        //handle ticket pay all
                        if (ticketIDSaleItemID.Length == 1)
                        {
                            //copy procSaleItem to SaleItem table
                            var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));
                            foreach (var procSaleItem in procSaleItems)
                            {
                                SaleItem saleItem = copyProcSaleItem2SaleItem(procSaleItem);
                                saleItem.TicketID = ticket.TicketID;
                                dao.Add<SaleItem>(saleItem);
                                dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                            }
                            //delete ProcTicket and ProcSaleItem
                            dao.Delete<ProcTicket>(procTicket.TicketID);
                        }

                        //hand ticket  split bill
                        else
                        {
                            //copy procSaleItem to SaleItem table
                            var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));
                            foreach (var procSaleItem in procSaleItems)
                            {
                                if (ticketIDSaleItemID.Contains(procSaleItem.SaleItemID.ToString()))
                                {
                                    SaleItem saleItem = copyProcSaleItem2SaleItem(procSaleItem);
                                    saleItem.TicketID = ticket.TicketID;
                                    dao.Add<SaleItem>(saleItem);
                                    dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                                }
                            }
                            if (procSaleItems.Count == 0)
                            {
                                //delete ProcTicket and ProcSaleItem
                                dao.Delete<ProcTicket>(procTicket.TicketID);
                            }
                        }
                        break;
                    }


                }
            }
            EntityFactory.getInstance().commit();
            ClsPublic.responseOrderDetails = "";
            isRunningHandleTicket = false;
        }

        SaleItem copyProcSaleItem2SaleItem(ProcSaleItem procSaleItem)
        {
            SaleItem saleItem = new SaleItem();
            saleItem.ProductID = procSaleItem.ProductID;
            saleItem.TicketNum = procSaleItem.TicketNum;
            saleItem.Description = procSaleItem.Description;
            saleItem.Type = procSaleItem.Type;
            saleItem.Qty = procSaleItem.Qty;
            saleItem.Price = procSaleItem.Price;
            saleItem.TPrice = (procSaleItem.Price + procSaleItem.Extra) * procSaleItem.Qty;
            saleItem.Extra = procSaleItem.Extra;
            saleItem.ByWho = procSaleItem.ByWho;
            saleItem.ProcDiscount = procSaleItem.ProcDiscount;
            saleItem.SmallSize = procSaleItem.SmallSize;
            saleItem.SizeChoiceTxt = procSaleItem.SizeChoiceTxt;
            saleItem.TranID = procSaleItem.TranID;
            saleItem.MType = procSaleItem.MType;
            saleItem.OrgPrice = procSaleItem.ProductID;
            saleItem.CPrice = procSaleItem.CPrice;
            saleItem.SPrice = procSaleItem.SPrice;
            saleItem.TakeOut = procSaleItem.TakeOut;
            saleItem.ItemCode = procSaleItem.ItemCode;
            saleItem.KitchenName = procSaleItem.KitchenName;
            saleItem.Voided = procSaleItem.Voided;
            saleItem.OnDate = DateTime.Today;
            saleItem.ProductName = procSaleItem.ProductName;
            saleItem.EditTimestamp = DateTime.Today;
            saleItem.RecordGUID = Guid.NewGuid().ToString();
            return saleItem;
        }

        Ticket copyProcTicket2Ticket(ProcTicket procTicket)
        {
            Ticket ticket = new Ticket();
            ticket.TicketNum = procTicket.TicketID;
            ticket.CustomerID = procTicket.CustomerID;
            ticket.EmployeeID = procTicket.EmployeeID;
            ticket.DateTimeIssue = DateTime.Today;
            ticket.PaidType = 1;
            ticket.ContractType = 1;
            ticket.Tax = procTicket.Tax;
            ticket.TotalP = procTicket.TotalP;
            ticket.PaidCash = procTicket.PaidCash;
            ticket.Discount = procTicket.Discount;
            ticket.CardCode = procTicket.CardCode;
            ticket.PaidCredit = procTicket.PaidCredit;
            ticket.Voided = false;
            ticket.CustomerName = procTicket.CustomerName;
            ticket.TableName = procTicket.TableName;
            ticket.DTicketNum = procTicket.DTicketNum;
            ticket.TableID = procTicket.TableID;
            ticket.TabCatID = procTicket.TabCatID;
            ticket.SplitCheck = false;
            ticket.ModDate = DateTime.Today;
            ticket.EditTimestamp = DateTime.Today;
            ticket.RecordGUID = Guid.NewGuid().ToString();
            return ticket;
        }


        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (ClsBaoLoi.XacNhan("Bạn có chắc thoát chương trình?"))
            {
                if (ClsPublic.HandleCompleteTicketFromSquare && ClsPublic.threadHandleCompleteTicket != null && ClsPublic.threadHandleCompleteTicket.IsAlive)
                {
                    ClsPublic.threadHandleCompleteTicket.Abort();
                }
                ClsPublic.socketServer.StopServer();
                Application.Exit();
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Frm_PassCode f = new Frm_PassCode();
            f.ShowDialog();
        }

        //flag volume: 0 = mute , 1 = sound
        //int flagVolume = 1;
        private void btnMute_Click(object sender, EventArgs e)
        {
            //if (flagVolume == 0)
            //{
            //    this.btnMute.Image = global::PhoMac.Main.Properties.Resources.volume_up_interface_symbol;
            //    this.btnMute.Text = "Mở tiếng";
            //    flagVolume = 1;
            //}
            //else if (flagVolume == 1)
            //{
            //    this.btnMute.Image = global::PhoMac.Main.Properties.Resources.mute;
            //    this.btnMute.Text = "Tắt tiếng";
            //    flagVolume = 0;
            //}
            if (ClsBaoLoi.XacNhan("Bạn có chắc muốn in hết các thẻ?"))
            {
                PrintTicket printer = new PrintTicket();
                printer.printAllTicketFromDb();
            }
        }

        private void splitContainerControl1_SplitterPositionChanged(object sender, EventArgs e)
        {
            ClsPublic.VerticalSplitGroupTablePosition = splitContainerControl1.SplitterPosition;
        }

        private void splitContainerControl2_SplitterPositionChanged(object sender, EventArgs e)
        {
            ClsPublic.HorizontalSplitGroupTablePosition = splitContainerControl2.SplitterPosition;
        }

        private void btnPrintLastItem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            Dao dao = new Dao();

            //CashDrawer.OpenCashDrawer("MCP30 - Ethernet:TCP:");
            if (ClsPublic.ListPrintItem != null)
            {
                try
                {
                    //CashDrawer.OpenCashDrawer("MCP30 - Ethernet:TCP:");
                    PrintTicket printer = new PrintTicket();
                    //printer.ticketInfo = dao.GetById<Ticket>(132681);
                    //printer.listSaleItem = dao.FindByMultiColumnAnd<SaleItem>(new[] { "TicketID" }, 132681);
                    Thread t = new Thread(new ThreadStart(printer.printLastItem));
                    //Thread t = new Thread(new ThreadStart(printer.PrintTickets));
                    t.Start();
                    printer.printLastItem();
                }
                catch (System.Exception ex)
                {
                    ClsPublic.WriteException(ex);
                }
            }
            SplashScreenManager.CloseForm();
        }

        private void gViewGroup_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Description" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string kitcheName = gViewGroup.GetRowCellValue(e.ListSourceRowIndex, colNameGroup) + string.Empty;
                bool isSmall = Convert.ToBoolean(gViewGroup.GetRowCellValue(e.ListSourceRowIndex, colIsSmallGroup) + string.Empty);
                int mType = Convert.ToInt32(gViewGroup.GetRowCellValue(e.ListSourceRowIndex, colMTypeGroup) + string.Empty);

                if (mType == 4)
                {
                    if (isSmall)
                    {
                        e.DisplayText = kitcheName + "(N)";
                    }
                    else
                    {
                        e.DisplayText = kitcheName + "(XL)";
                    }
                }
            }
        }

        private void gViewFilter_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Description" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string kitcheName = gViewFilter.GetRowCellValue(e.ListSourceRowIndex, colNameFilter) + string.Empty;
                bool isSmall = Convert.ToBoolean(gViewFilter.GetRowCellValue(e.ListSourceRowIndex, colIsSmallFilter) + string.Empty);
                int mType = Convert.ToInt32(gViewFilter.GetRowCellValue(e.ListSourceRowIndex, colMTypeFilter) + string.Empty);

                if (mType == 4)
                {
                    if (isSmall)
                    {
                        e.DisplayText = kitcheName + "(N)";
                    }
                    else
                    {
                        e.DisplayText = kitcheName + "(XL)";
                    }
                }
            }
        }

        private void gViewGroup_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                int orderby = Convert.ToInt32(View.GetRowCellValue(e.RowHandle, colOrderByGroup));
                if (orderby % 2 == 0)
                {
                    e.Appearance.BackColor = ClsPublic.ColorBackgroundItemChange;
                    e.Appearance.BackColor2 = ClsPublic.ColorBackgroundItemChange;
                }
                else
                {
                    e.Appearance.BackColor = Color.Transparent;
                    e.Appearance.BackColor2 = Color.Transparent;
                }
            }
        }

        UCListTicketPrint uCListTicketPrint;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            uCListTicketPrint = new UCListTicketPrint();

            uCListTicketPrint.ShowDialog();
        }

        private void Frm_Kitchen_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.ToString() == "1")
                {
                    TicketView abc = (TicketView)panelTicketList.Controls[0].Controls[0].Controls[0].Controls[0];
                    abc.completeAll();
                }
                if (e.KeyChar.ToString() == "2")
                {
                    TicketView abc = (TicketView)panelTicketList.Controls[0].Controls[0].Controls[1].Controls[0];
                    abc.completeAll();
                }
                if (e.KeyChar.ToString() == "3")
                {
                    TicketView abc = (TicketView)panelTicketList.Controls[0].Controls[0].Controls[2].Controls[0];
                    abc.completeAll();
                }
                if (e.KeyChar.ToString() == "4")
                {
                    TicketView abc = (TicketView)panelTicketList.Controls[0].Controls[0].Controls[3].Controls[0];
                    abc.completeAll();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
            
        }



    }
}
