using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Connection;

namespace PhoHa7
{
    public partial class Frm_Kitchen : DevExpress.XtraEditors.XtraForm
    {
        SqlHelper sql;
        string[] KitchenType
        {
            get;
            set;
        }
        DataSet ds;

        public Frm_Kitchen()
        {
            InitializeComponent();
            
            //
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(); ;
            //player.SoundLocation = @"C:\Windows\Media\Ring07.wav";
            //player.Play;

            timer1.Start();
            sql = new SqlHelper();
            ds = new DataSet();
            KitchenType = ClsPublic.KITCHEN_TYPE.Split(',');
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
        TicketListView ticketListView2;
        private void Kitchen_Load(object sender, EventArgs e)
        {
            //// ticketListView2
            //// 
            ticketListView2 = new PhoHa7.TicketListView();
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
            this.splitContainerControl1.SplitterPosition = (screenWidth / 5) * 4;
            this.splitContainerControl2.SplitterPosition = screenHeight / 2;
        }

        //load gridview group and important
        private void loadGroupAndImportant()
        {
            //load grouping table
            string s = "select COUNT(description) as Quality , Description from PhoHa7_ProcSaleItem where Category = @Category and Done = 0 group by description order by Quality desc ";
            if (ds.Tables["GroupTable"] != null)
            {
                ds.Tables["GroupTable"].Clear();
            }
            //get all item in a category
            foreach (string item in KitchenType)
            {
                sql.ExecuteDataSet(ds, "GroupTable", s, CommandType.Text, new object[] { "@Category" }, new object[] { item });
            }


            gControlGroup.DataSource = ds.Tables["GroupTable"];
            //load important table
            s = "select count(p.Description) as Qty,p.Description from PhoHa7_ProcSaleItem p" +
                " left JOIN (select * from PhoHa7_FilterSaleItem) f ON p.ProductID = f.ProductID" +
                " where f.ProductID > 0 and p.Category = @Category and p.Done = 0" +
                " group by p.Description";
            if (ds.Tables["GroupImportant"] != null)
            {
                ds.Tables["GroupImportant"].Clear();
            }
            //get all item in a category
            foreach (string item in KitchenType)
            {
                sql.ExecuteDataSet(ds, "GroupImportant", s, CommandType.Text, new object[] { "@Category" }, new object[] { item });
            }
            gControlImportant.DataSource = ds.Tables["GroupImportant"];
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
                loadGroupAndImportant();
                ticketListView2.updateTicket();
                ticketListView2.countTime();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            PhoHa7.frmSetting s = new frmSetting();
            s.ShowDialog();
        }

        //flag volume: 0 = mute , 1 = sound
        int flagVolume = 1;
        private void btnMute_Click(object sender, EventArgs e)
        {
            if (flagVolume == 0)
            {
                this.btnMute.Image = global::PhoHa7.Properties.Resources.volume_up_interface_symbol;
                this.btnMute.Text = "Mở tiếng";
                flagVolume = 1;
            }
            else if (flagVolume == 1)
            {
                this.btnMute.Image = global::PhoHa7.Properties.Resources.mute;
                this.btnMute.Text = "Tắt tiếng";
                flagVolume = 0;
            }
        }

        private void splitContainerControl1_SplitterPositionChanged(object sender, EventArgs e)
        {
            //textBox2.Text = splitContainerControl1.SplitterPosition.ToString();
        }
    }
}