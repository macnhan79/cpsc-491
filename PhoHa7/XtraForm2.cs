using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Connection;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

namespace PhoHa7
{
    public partial class XtraForm2 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm2()
        {
            InitializeComponent();
            SqlHelper sql = new SqlHelper();


            DataSet ds = new DataSet();
            sql.ExecuteDataSet(ds, "test", "select * from ProcSaleItem");
    
        }

        private void XtraForm2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DbProviderDataSet.ProcSaleItem' table. You can move, or remove it, as needed.
           // this.ProcSaleItemTableAdapter.Fill(this.DbProviderDataSet.ProcSaleItem);

            //this.reportViewer1.RefreshReport();
            myPrinters.SetDefaultPrinter("");
        }
        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDocument pdoc = new PrintDocument();
            pdoc.DocumentName = "PhoHa7.Report1.rdlc";
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            pdoc.Print();
        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Welcome to MSST", new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ticket No:" , new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ticket Date :", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            //String Source = this.source;
            graphics.DrawString("From " + " To ", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String Grosstotal = "Total Amount to Pay = ";

            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(Grosstotal, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            //String DrawnBy = ;
            graphics.DrawString("Conductor - ", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);





        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int a = 0;
        }
    }
}