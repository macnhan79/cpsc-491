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
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.Data.SqlClient;
using PhoHa7.Library.Classes.Common;
using System.IO;

namespace PhoMac.Main.GUI
{
    public partial class TicketListView : DevExpress.XtraEditors.XtraUserControl
    {
        int numOfTickets;
        string[] KitchenType { get; set; }
        DataSet ds = new DataSet();
        SqlHelper sql;
        //list control panel
        List<PanelControl> listPanelControl = new List<PanelControl>();
        //List coordinates
        List<Point> listCoordinates = new List<Point>();
        public int NumOfTickets
        {
            get { return numOfTickets; }
            set { numOfTickets = value; }
        }

        public TicketListView()
        {
            InitializeComponent();
            sql = new SqlHelper();
            KitchenType = ClsPublic.KITCHEN_TYPE.Split(',');
            timer1.Start();
        }

        private void TicketListView_Load(object sender, EventArgs e)
        {
            getListTicket();

            //get count of tickets
            numOfTickets = ds.Tables["ProcTickets"].Rows.Count;
            height = xtraScrollableControl1.Parent.Parent.Size.Height;
            loadTable();
            //int abc = this.Height;
        }

        int height = 0;

        public void countTime()
        {
            for (int i = 0; i < xtraScrollableControl1.Controls.Count; i++)
            {
                if (xtraScrollableControl1.Controls[i].HasChildren)
                {
                    if (xtraScrollableControl1.Controls[i].Controls[0].GetType() == typeof(TicketView))
                    {
                        TicketView uc = (TicketView)xtraScrollableControl1.Controls[i].Controls[0];
                        uc.countTime();
                    }
                }
            }
        }

        void loadTable()
        {
            //init panel, maximum = MAX_NUMBER_TICKET
            for (int i = 0; i < ClsPublic.MAX_NUMBER_TICKET; i++)
            {
                PanelControl panelControl = new DevExpress.XtraEditors.PanelControl();
                listPanelControl.Add(panelControl);
                this.xtraScrollableControl1.Controls.Add(panelControl);
                //Coordinates
                Point point = new System.Drawing.Point(i * (330 + 30), 0);
                panelControl.Location = point;
                //add coordinates to list
                listCoordinates.Add(point);
                panelControl.Name = "panelControl" + i;

                panelControl.Size = new System.Drawing.Size(330, height - 50);
                //set corlor
                // panelControl.Appearance.BackColor = Color.Red;
                // panelControl.Appearance.BackColor2 = Color.Red;
                panelControl.Appearance.Options.UseBackColor = true;

                panelControl.LookAndFeel.UseDefaultLookAndFeel = false;
                panelControl.LookAndFeel.Style = LookAndFeelStyle.Flat;
                if (i > (numOfTickets - 1))
                {
                    panelControl.Visible = false;
                }
            }
            //show ticket to panel
            for (int i = 0; i < numOfTickets; i++)
            {
                //get ticket from dataset
                int ticketID = Convert.ToInt32(ds.Tables["ProcTickets"].Rows[i]["ticketID"]);
                listPanelControl[i].Name = ticketID.ToString();
                //get sale item
                string s = "select s.*, OrderBy=0, p.PrintBoth from PhoHa7_ProcSaleItem s left join Products p on s.ProductID=p.ProductID where TicketID = @TicketID and Done = 0";
                sql.ExecuteDataSet(ds, ticketID.ToString(), s, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
                TicketView ucTicket = new TicketView();
                ucTicket.TableName = ds.Tables["ProcTickets"].Rows[i]["TableName"].ToString();

                listPanelControl[i].Controls.Add(ucTicket);

                //set ticket id
                ucTicket.TicketID = ticketID;
                //emp name
                ucTicket.EmpName = ds.Tables["ProcTickets"].Rows[i]["EmployeeName"].ToString();
                //set togo state
                ucTicket.IsToGoTicket = (bool)ds.Tables["ProcTickets"].Rows[i]["TakeOut"];
                //emp name
                ucTicket.CustomerName = ds.Tables["ProcTickets"].Rows[i]["CustomerName"].ToString();

                //number of order
                ucTicket.NumberOfOrder = (i + 1) + string.Empty;
                //set datasource
                ucTicket.DataSource = ds.Tables[ticketID.ToString()];
                //set DTicketNum
                ucTicket.DTicketNum = Convert.ToInt32(ds.Tables["ProcTickets"].Rows[i]["DTicketNum"]);
                //set DTicketNum
                ucTicket.DateTimeIssue = Convert.ToDateTime(ds.Tables["ProcTickets"].Rows[i]["DateTimeIssue"]);
                //set TicketID_Root
                ucTicket.TicketID_Root = Convert.ToInt32(ds.Tables["ProcTickets"].Rows[i]["TicketID_Root"]);
                //set emergency
                ucTicket.Emergency = Convert.ToBoolean(ds.Tables["ProcTickets"].Rows[i]["Emergency"] == DBNull.Value ? false : ds.Tables["ProcTickets"].Rows[i]["Emergency"]);
                //count total item in ticket
                s = "select count(*) from PhoHa7_ProcSaleItem where TicketID = @TicketID";
                ucTicket.CounTotalItems = Convert.ToInt32(sql.ExecuteScalar(s, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID }));
                //apprearance
                ucTicket.Dock = System.Windows.Forms.DockStyle.Fill;
                ucTicket.Location = new System.Drawing.Point(0, 0);
                ucTicket.Name = ticketID.ToString();
                UpdateTicketToKitchen(ticketID);
            }
            //reload group table
            Frm_Kitchen frmKitchen = (Frm_Kitchen)xtraScrollableControl1.Parent.TopLevelControl;
            frmKitchen.loadGroupAndImportant();
        }

        private void getListTicket()
        {
            //get list ticket
            string s = "select * from  dbo.PhoHa7_ProcTickets p where (select COUNT(*) from dbo.PhoHa7_ProcSaleItem s where s.TicketID=p.TicketID and Done=0 and(";
            object[] cat = new object[KitchenType.Length];
            for (int i = 0; i < KitchenType.Length; i++)
            {
                if (i < KitchenType.Length - 1)
                {
                    s += " Category= " + Convert.ToInt32(KitchenType[i]) + " or";
                }
                else
                {
                    s += " Category= " + Convert.ToInt32(KitchenType[i]) + "))>0";
                }
            }

            sql.ExecuteDataSet(ds, "ProcTickets", s, CommandType.Text, null, null);
        }

        public void updateTicket()
        {
            //get new tickets
            DataTable newTickets = sql.ExecuteDataTable("select * from  dbo.PhoHa7_ProcTickets p where (select COUNT(*) from dbo.PhoHa7_ProcSaleItem s ) = 0", CommandType.Text, null, null);
            for (int i = 0; i < newTickets.Rows.Count; i++)
            {
                int ticketID = Convert.ToInt32(newTickets.Rows[i]["ticketID"]);
                //check item > 0 . if count = 0 --> delete ticket
                int isCount = Convert.ToInt32(sql.ExecuteScalar("select count(*) from PhoHa7_ProcSaleItem where TicketID=@TicketID", CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID }));
                if (isCount == 0)
                {
                    sql.ExecuteNonQuery("delete PhoHa7_ProcTickets where TicketID=@TicketID", CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
                }
            }
            //get new tickets
            int newTicketsCount = Convert.ToInt32(sql.ExecuteScalar("select count(*) from PhoHa7_ProcTickets where ToKitchen = 0", CommandType.Text, null, null));
            //check exist new ticketes
            if (newTicketsCount > 0)
            {
                insertTicket();
            }
        }

        public void insertTicket()
        {
            //get count of tickets
            numOfTickets = ds.Tables["ProcTickets"].Rows.Count;
            //get new tickets
            DataTable tempDS = new DataTable();
            string s = "select * from  dbo.PhoHa7_ProcTickets p where ToKitchen = 0 and (select COUNT(*) from dbo.PhoHa7_ProcSaleItem s where s.TicketID=p.TicketID and Done=0 and(";
            object[] cat = new object[KitchenType.Length];
            for (int i = 0; i < KitchenType.Length; i++)
            {
                if (i < KitchenType.Length - 1)
                {
                    s += " Category= " + Convert.ToInt32(KitchenType[i]) + " or";
                }
                else
                {
                    s += " Category= " + Convert.ToInt32(KitchenType[i]) + "))>0";
                }
            }

            tempDS = sql.ExecuteDataTable(s, CommandType.Text, null, null);
            if (tempDS.Rows.Count > 0)
            {
                ds.Tables["ProcTickets"].Clear();
                getListTicket();
                numOfTickets = ds.Tables["ProcTickets"].Rows.Count;
                //add each tickets
                for (int i = 0; i < tempDS.Rows.Count; i++)
                {
                    int ticketID = Convert.ToInt32(tempDS.Rows[i]["ticketID"]);
                    
                    //show up panel o vi tri ticket cu + 1
                    for (int j = 0; j < listPanelControl.Count; j++)
                    {
                        if (listPanelControl[j].Controls.Count > 0)
                        {
                            if (listPanelControl[j].Name == ticketID.ToString())
                            {
                                TicketView ucTicketView = (TicketView)listPanelControl[j].Controls[0];
                                ucTicketView.updateGridview();
                                //reload group table
                                Frm_Kitchen frmKitchen = (Frm_Kitchen)xtraScrollableControl1.Parent.TopLevelControl;
                                frmKitchen.loadGroupAndImportant();
                                break;
                            }
                        }
                        if (listPanelControl[j].Controls.Count == 0)
                        {
                            listPanelControl[j].Visible = true;
                            listPanelControl[j].Name = ticketID.ToString();
                            //get sale item
                            s = "select s.*, OrderBy=0, p.PrintBoth from PhoHa7_ProcSaleItem s left join Products p on s.ProductID=p.ProductID where TicketID = @TicketID and Done = 0";
                            if (ds.Tables[ticketID.ToString()] != null)
                            {
                                ds.Tables[ticketID.ToString()].Rows.Clear();
                            }
                            sql.ExecuteDataSet(ds, ticketID.ToString(), s, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
                            TicketView ucTicket = new TicketView();
                            //fill sale item to panel
                            listPanelControl[j].Controls.Add(ucTicket);
                            //set ticket id
                            ucTicket.TicketID = ticketID;
                            //set togo state
                            ucTicket.IsToGoTicket = (bool)tempDS.Rows[i]["TakeOut"];
                            //number of order
                            ucTicket.NumberOfOrder = (j + 1) + string.Empty;
                            //table name
                            ucTicket.TableName = tempDS.Rows[i]["TableName"].ToString();
                            //emp name
                            ucTicket.EmpName = tempDS.Rows[i]["EmployeeName"].ToString();
                            //customer name
                            ucTicket.CustomerName = tempDS.Rows[i]["CustomerName"].ToString();
                            //set datasource
                            ucTicket.DataSource = ds.Tables[ticketID.ToString()];
                            //set DTicketNum
                            ucTicket.DTicketNum = Convert.ToInt32(tempDS.Rows[i]["DTicketNum"].ToString());
                            //set TicketID_Root
                            ucTicket.TicketID_Root = Convert.ToInt32(tempDS.Rows[i]["TicketID_Root"]);
                            //set emergency
                            ucTicket.Emergency = Convert.ToBoolean(tempDS.Rows[i]["Emergency"] == DBNull.Value ? false : tempDS.Rows[i]["Emergency"]);
                            //set datetimeissue
                            //set TicketID_Root
                            ucTicket.DateTimeIssue = Convert.ToDateTime(tempDS.Rows[i]["DateTimeIssue"]);
                            //count total item in ticket
                            s = "select count(*) from PhoHa7_ProcSaleItem where TicketID = @TicketID";
                            ucTicket.CounTotalItems = Convert.ToInt32(sql.ExecuteScalar(s, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID }));
                            //appearance
                            ucTicket.Dock = System.Windows.Forms.DockStyle.Fill;
                            ucTicket.Location = new System.Drawing.Point(0, 0);
                            ucTicket.Name = ticketID.ToString();
                            //ring bell

                            
                            string curFile = ClsPublic.SoundUrl;
                            if (!File.Exists(curFile))
                            {
                                curFile = @"C:\Windows\Media\Ring07.wav";
                            }
                            if (File.Exists(curFile))
                            {
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                                player.SoundLocation = curFile;
                                player.Play();
                            }
                            //reload group table
                            Frm_Kitchen frmKitchen = (Frm_Kitchen)xtraScrollableControl1.Parent.TopLevelControl;
                            frmKitchen.loadGroupAndImportant();
                            break;
                        }
                    }

                    // ucTicket.Size = new System.Drawing.Size(284, 261);

                    //update to ProcTickets to kitchen
                    UpdateTicketToKitchen(ticketID);
                    //update ProcSaleItem to kitchen
                    //UpdateSaleItemToKitchen(ticketID);
                }
            }



            //update number of tickets
            numOfTickets = ds.Tables["ProcTickets"].Rows.Count;

        }

        public void ticketDone(int ticketID)
        {
            //sort view
            bool isFind = false;
            for (int i = 0; i < xtraScrollableControl1.Controls.Count - 1; i++)
            {
                if (ticketID.ToString() == xtraScrollableControl1.Controls[i].Name)
                {
                    xtraScrollableControl1.Controls[i].Name = "panelControl";
                    xtraScrollableControl1.Controls[i].Controls[0].Dispose();
                    xtraScrollableControl1.Controls[i].Controls.Clear();
                    isFind = true;
                }
                if (isFind)
                {
                    TicketView ticket;
                    if (xtraScrollableControl1.Controls[i + 1].HasChildren)
                    {
                        ticket = (TicketView)xtraScrollableControl1.Controls[i + 1].Controls[0];
                        xtraScrollableControl1.Controls[i].Controls.Add(ticket);
                        xtraScrollableControl1.Controls[i].Name = ticket.TicketID.ToString();
                        ticket.NumberOfOrder = (i + 1) + string.Empty;
                        //xtraScrollableControl1.Controls[i + 1].Controls.RemoveAt(0);
                        xtraScrollableControl1.Controls[i + 1].Name = "panelControl";
                    }
                    else
                    {
                        xtraScrollableControl1.Controls[i].Visible = false;
                    }
                }
                numOfTickets--;
            }
        }

        private void UpdateTicketToKitchen(int ticketID)
        {
            //SqlTransaction transaction;
            //transaction = ClsConnection.MySqlConn.BeginTransaction("UpdateTicketToKitchen");
            //try
            //{
            //    //update ticket that is show to kitchen
            //    string s = "select count(*) from PhoHa7_ProcSaleItem where TicketID=@TicketID and ToKitchen = 0";
            //    int count = Convert.ToInt32(sql.ExecuteScalar(s, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { ticketID }));
            //    if (count == 0)
            //    {
            //        int result = sql.ExecuteNonQuery("update PhoHa7_Proctickets set ToKitChen = 1 where TicketID = @TicketID", CommandType.Text,transaction, new object[] { "@TicketID" }, new object[] { ticketID });
            //        if (result == 1)
            //        {
            //            //show susscess
            //        }
            //        else
            //        {
            //            //error
            //        }
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    try
            //    {
            //        transaction.Rollback();
            //    }
            //    catch (Exception e)
            //    {
            //        if (ClsMsgBox.LoiChung(ex, false) == 1)
            //        {
            //            ClsPublic.WriteException(ex);
            //        }
            //    }
            //}
        }

        private void UpdateSaleItemToKitchen(int ticketID)
        {
            //update ticket that is show to kitchen
            int result = sql.ExecuteNonQuery("update PhoHa7_ProcSaleItem set ToKitChen = 1 where TicketID = @TicketID", CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
            if (result == 1)
            {
                //show susscess
            }
            else
            {
                //error
            }
        }


        //timer 5 seconds
        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time < 5)
            {
                time++;
            }
            else
            {
                updateTicket();
                time = 0;
            }
        }




    }
}
