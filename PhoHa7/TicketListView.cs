using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PhoHa7
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
            //get list ticket
            string s = "select * from  dbo.PhoHa7_ProcTickets p where (select COUNT(*) from dbo.PhoHa7_ProcSaleItem s where s.TicketID=p.TicketID and";
            object[] cat = new object[KitchenType.Length];
            for (int i = 0; i < KitchenType.Length; i++)
            {
                if (i < KitchenType.Length - 1)
                {
                    s += " Category= " + Convert.ToInt32(KitchenType[i]) + " or";
                }
                else
                {
                    s += " Category= " + Convert.ToInt32(KitchenType[i]) + ")>0";
                }
            }
            sql.ExecuteDataSet(ds, "ProcTickets", s, CommandType.Text, null, null);

            //get count of tickets
            numOfTickets = ds.Tables["ProcTickets"].Rows.Count;
            object p = xtraScrollableControl1.Parent.Size;
            loadTable();
        }

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

                panelControl.Size = new System.Drawing.Size(330, 650);
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
                string s = "select * from PhoHa7_ProcSaleItem where TicketID = @TicketID and Done = 0";
                sql.ExecuteDataSet(ds, ticketID.ToString(), s, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
                TicketView ucTicket = new TicketView();
                ucTicket.TableName = ds.Tables["ProcTickets"].Rows[i]["TableName"].ToString();
                ucTicket.EmpName = ds.Tables["ProcTickets"].Rows[i]["EmployeeName"].ToString();
                listPanelControl[i].Controls.Add(ucTicket);
                //set datasource
                ucTicket.DataSource = ds.Tables[ticketID.ToString()];
                //set ticket id
                ucTicket.TicketID = ticketID;
                //appearent
                ucTicket.Dock = System.Windows.Forms.DockStyle.Fill;
                ucTicket.Location = new System.Drawing.Point(0, 0);
                ucTicket.Name = ticketID.ToString();
                UpdateTicketToKitchen(ticketID);
            }
        }

        public void updateTicket()
        {
            //get new tickets
            int newTickets = Convert.ToInt32(sql.ExecuteScalar("select count(*) from PhoHa7_ProcTickets where ToKitchen = 0", CommandType.Text, null, null));
            //check exist new ticketes
            if (newTickets > 0)
            {
                //get count of tickets
                numOfTickets = ds.Tables["ProcTickets"].Rows.Count;
                //get new tickets
                DataSet tempDS = new DataSet();
                sql.ExecuteDataSet(tempDS, "ProcTickets", "select * from PhoHa7_ProcTickets where ToKitchen = 0");
                ds.Tables["ProcTickets"].Merge(tempDS.Tables[0]);


                //add each tickets
                for (int i = 0; i < tempDS.Tables["ProcTickets"].Rows.Count; i++)
                {
                    int ticketID = Convert.ToInt32(tempDS.Tables["ProcTickets"].Rows[i]["ticketID"]);
                    //show up panel o vi tri ticket cu + 1
                    listPanelControl[numOfTickets + i].Visible = true;
                    listPanelControl[numOfTickets + i].Name = ticketID.ToString();
                    //get sale item
                    string s = "select * from PhoHa7_ProcSaleItem where TicketID = @TicketID and Done = 0";
                    sql.ExecuteDataSet(ds, ticketID.ToString(), s, CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
                    TicketView ucTicket = new TicketView();
                    //fill sale item to panel
                    listPanelControl[numOfTickets + i].Controls.Add(ucTicket);
                    //set datasource
                    ucTicket.DataSource = ds.Tables[ticketID.ToString()];
                    //set ticket id
                    ucTicket.TicketID = ticketID;
                    ucTicket.Dock = System.Windows.Forms.DockStyle.Fill;
                    ucTicket.Location = new System.Drawing.Point(0, 0);
                    ucTicket.Name = ticketID.ToString();
                    // ucTicket.Size = new System.Drawing.Size(284, 261);

                    //update to ProcTickets to kitchen
                    UpdateTicketToKitchen(ticketID);
                    //update ProcSaleItem to kitchen
                    UpdateSaleItemToKitchen(ticketID);
                }
                //update number of tickets
                numOfTickets = ds.Tables["ProcTickets"].Rows.Count;
            }
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
                    xtraScrollableControl1.Controls[i].Controls.RemoveAt(0);
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
            //update ticket that is show to kitchen
            int result = sql.ExecuteNonQuery("update PhoHa7_Proctickets set ToKitChen = 1 where TicketID = @TicketID", CommandType.Text, new object[] { "@TicketID" }, new object[] { ticketID });
            if (result == 1)
            {
                //show susscess
            }
            else
            {
                //error
            }
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




    }
}
