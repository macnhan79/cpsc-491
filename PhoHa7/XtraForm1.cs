using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Connection;
using DevExpress.XtraGrid.Columns;
using DevExpress.LookAndFeel;

namespace PhoHa7
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
            //init timer
           // timer1.Start();

        }

        void fullScreen()
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        //load table
        void loadTable()
        {
            //clear
            foreach (Control item in xtraScrollableControl1.Controls)
            {
                xtraScrollableControl1.Controls.Remove(item);
            }
            SqlHelper sql = new SqlHelper();


            DataSet ds = new DataSet();
            sql.ExecuteDataSet(ds, "ProcTickets", "select * from ProcTickets");
            int numTable = ds.Tables["ProcTickets"].Rows.Count;
            for (int i = 0; i < numTable; i++)
            {

                PanelControl panelControl = new DevExpress.XtraEditors.PanelControl();
                this.xtraScrollableControl1.Controls.Add(panelControl);
                panelControl.Location = new System.Drawing.Point(i * (300 + 30), 0);
                panelControl.Name = "panelControl" + i;
                panelControl.Size = new System.Drawing.Size(300, 559);
                //set corlor
                //panelControl.Appearance.BackColor = Color.Red;
                //panelControl.Appearance.BackColor2 = Color.Red;
                panelControl.Appearance.Options.UseBackColor = true;

                panelControl.LookAndFeel.UseDefaultLookAndFeel = false;
                panelControl.LookAndFeel.Style = LookAndFeelStyle.Flat;
                int ticketID = Convert.ToInt32(ds.Tables["ProcTickets"].Rows[i]["ticketID"]);
                //get sale item
                string s = "select * from ProcSaleItem where TicketID = " + ticketID;
                sql.ExecuteDataSet(ds, "procSaleItem" + i, s, CommandType.Text, null, null);
                TicketView xtraUserControl21 = new TicketView();
                panelControl.Controls.Add(xtraUserControl21);
                xtraUserControl21.DataSource = ds.Tables["procSaleItem" + i];
                xtraUserControl21.Dock = System.Windows.Forms.DockStyle.Fill;
                xtraUserControl21.Location = new System.Drawing.Point(0, 0);
                xtraUserControl21.Name = ticketID.ToString();
                xtraUserControl21.Size = new System.Drawing.Size(284, 261);
                
            }
            //DataView dv;
            //dv = ds.Tables[0].DefaultView;
            //gridControl1.DataSource = dv;

            //for (int i = 0; i < ds.Tables["test"].Rows.Count; i++)
            //{
            //    int procTicketID = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"].ToString());
            //    DataSet dsProSaleItem = new DataSet();
            //    sql.ExecuteDataSet(ds, "dsProSaleItem", "select * from ProcSaleItem where TicketID = " + procTicketID);
            //    // add table
            //  //  object t = cardView1.GetDataRow(i).Table;
            //    //GridColumn gridColumn1 = new GridColumn();
            //    //cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { this.gridColumn1 });
            //    //this.gridColumn1.Caption = ds.Tables[1].Rows[i]["Description"].ToString();
            //    //this.gridColumn1.Name = "gridColumn1";
            //    //this.gridColumn1.Visible = true;
            //}




        }

        //timer 5 seconds
        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time < 50)
            {
                time++;
            }
            else
            {
                time = 0;
                loadTable();
            }


        }
    }
}