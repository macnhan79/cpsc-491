using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhoMac.Model;
using PhoMac.Main.Controller;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Froms;
using System.Threading;

namespace PhoMac.Main.GUI.Kitchen
{
    public partial class UCListTicketPrint : XtraForm
    {
        PhoHa7_ProcTickets ticket;
        public UCListTicketPrint()
        {
            InitializeComponent();
        }

        private void UCListTicketPrint_Load(object sender, EventArgs e)
        {
            gcTicket.DataSource = ClsPublic.ListPrintItem;
        }

        private void gTicket_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int ticketID = Convert.ToInt32(gvTicket.GetRowCellDisplayText(gvTicket.FocusedRowHandle, gvTicket.Columns["TicketID"]));
            //int temp = (int)(gTicket.GetFocusedRow() as DataRowView).Row["TicketID"];
            ticket = ClsPublic.ListPrintItem.First<PhoHa7_ProcTickets>(p => p.TicketID == ticketID);
            gcItems.DataSource = ticket.ListProduct;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ticket!=null)
            {
                PrintTicket printer = new PrintTicket();
                SplashScreenManager.ShowForm(typeof(WaitForm1));
                Thread thread = new Thread(delegate()
                {
                    printer.printItem(ticket);
                    //printItem(ticket);
                });
                thread.Start();
                SplashScreenManager.CloseForm();
               
               

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Dispose();
            this.Close();
        }

        private void gItems_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            //List<Product> dt = (DataTable)gcItems.DataSource;
            bool takeOut = Convert.ToBoolean(View.GetRowCellValue(e.RowHandle, colTakeOut));
            //draw take out item
            if (takeOut)
            {
                    e.Appearance.ForeColor = ClsPublic.ColorLetterToGo;
                    e.Appearance.BackColor = ClsPublic.ColorBackgroundToGo;
                    e.Appearance.BackColor2 = ClsPublic.ColorBackgroundToGo;
            }
            //cancel items
            bool cancel = Convert.ToBoolean(View.GetRowCellValue(e.RowHandle, colIsCancel));
            if (cancel)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
            }
        }




    }
}
