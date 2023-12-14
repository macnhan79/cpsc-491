using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Froms;
using PhoMac.Model;
using PhoMac.Model.Data;
using PhoMac.Model.Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhoMac.Main.Controller
{
    public class PrintTicket
    {
        Dao dao;

        public PrintTicket()
        {
            dao = new Dao();
        }

        public void printAllTicketFromDb()
        {
            PhoMac.Model.Entities entity = EntityFactory.getInstance().CreateEntities();
            List<PhoHa7_ProcTickets> listTicketPrint = new List<PhoHa7_ProcTickets>();
            //get machine 
            ICollection<PhoHa7_Machine> machines = dao.GetAll<PhoHa7_Machine>();
            //get all tickets
            ICollection<PhoHa7_ProcTickets> listTickets = dao.GetAll<PhoHa7_ProcTickets>();
            //
            //go each machine
            foreach (PhoHa7_Machine machine in machines)
            {
                //if machine is not type of kitchen
                if ((machine.MachineType + string.Empty) == string.Empty)
                {
                    continue;
                }
                string[] kitChenTypes = machine.MachineType.Split(',');
                //go each tickets
                foreach (PhoHa7_ProcTickets ticket in listTickets)
                {
                    //get all item in ticket
                    ICollection<PhoHa7_ProcSaleItem> saleItems = dao.FindByMultiColumnAnd<PhoHa7_ProcSaleItem>(new[] { "TicketID" }, ticket.TicketID);
                    //count items in ticket
                    ticket.CountTotalItemPrint = saleItems.Count;

                    List<Product> listProduct = new List<Product>();

                    //copy item into Products
                    foreach (PhoHa7_ProcSaleItem saleItem in saleItems)
                    {
                        //check item is in kitchen type of this machine
                        bool isIn = false;
                        foreach (string kitchenType in kitChenTypes)
                        {
                            if (kitchenType == (saleItem.Category + string.Empty))
                            {
                                isIn = true;
                                break;
                            }
                        }
                        //if item is in kitchentype-->add to listProduct
                        if (isIn)
                        {
                            //item info
                            Product product = new Product();
                            string namePrint = saleItem.Qty + ")" + saleItem.customDisplayKitchenName();
                            bool takeOut = Convert.ToBoolean(saleItem.TakeOut);
                            if (ClsPublic.PrintBarCode)
                            {
                                namePrint = namePrint + "(" + saleItem.BarCode + ")";
                            }


                            product.PrintName = namePrint;
                            product.PrintFrontStyle = FontStyle.Bold;
                            //set cancel item
                            bool isCancel = Convert.ToBoolean(saleItem.Cancel + string.Empty);
                            if (isCancel)
                            {
                                product.PrintFrontStyle = FontStyle.Strikeout;
                            }
                            listProduct.Add(product);
                            //update done item
                            saleItem.Done = true;
                            dao.Update<PhoHa7_ProcSaleItem>(saleItem);
                        }

                    }
                    //if list product > 0 --> create a tickets
                    if (listProduct.Count > 0)
                    {
                        ticket.ListProduct = listProduct;
                        //count item done in ticket
                        ticket.CountItemDonePrint = dao.FindByMultiColumnAnd<PhoHa7_ProcSaleItem>(new[] { "TicketID", "Done" }, ticket.TicketID, true).Count;
                        listTicketPrint.Add(ticket);
                    }
                    //delete ticket to db if all items is done
                    int countItemNotDone = dao.FindByMultiColumnAnd<PhoHa7_ProcSaleItem>(new[] { "TicketID", "Done" }, ticket.TicketID, false).Count;
                    if (countItemNotDone == 0)
                    {
                        entity.PhoHa7_ProcSaleItem.RemoveRange(entity.PhoHa7_ProcSaleItem.Where(p => p.TicketID == ticket.TicketID));
                        entity.PhoHa7_ProcTickets.Remove(entity.PhoHa7_ProcTickets.FirstOrDefault(p => p.TicketID == ticket.TicketID));
                        entity.SaveChanges();
                    }
                }


            }
            //print all tickets
            foreach (PhoHa7_ProcTickets t in listTicketPrint)
            {
                printTicketThread(t);
            }

        }

        void printTicketThread(PhoHa7_ProcTickets ticket)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            Thread thread = new Thread(delegate()
            {
                printItem(ticket);
            });
            thread.Start();
            SplashScreenManager.CloseForm();
        }



        public void printLastItem()
        {
            PrintDocument pdoc = new PrintDocument();
            PrintController printController = new StandardPrintController();
            pdoc.PrintController = printController;
            if (ClsPublic.Printers != "")
            {
                pdoc.PrinterSettings.PrinterName = ClsPublic.Printers;
            }
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintLastItem);
            pdoc.Print();
        }

        PhoHa7_ProcTickets ticket;
        public void printItem(PhoHa7_ProcTickets t)
        {
            try
            {
                ticket = t;
                PrintDocument pdoc = new PrintDocument();
                PrintController printController = new StandardPrintController();
                pdoc.PrintController = printController;
                if (ClsPublic.Printers != "")
                {
                    pdoc.PrinterSettings.PrinterName = ClsPublic.Printers;
                }
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintItem);
                pdoc.Print();
            }
            catch (System.Exception ex)
            {
                ClsPublic.WriteException(ex);
            }

        }

        public Ticket ticketInfo;
        public ICollection<SaleItem> listSaleItem;
        void pdoc_PrintTickets(object sender, PrintPageEventArgs e)
        {
            try
            {
                Ticket ticket = ticketInfo;

                int fontSize = ClsPublic.FontSizePrint;
                Graphics graphics = e.Graphics;
                Font font = new Font("Time New Roman", 10);
                float fontHeight = font.GetHeight();
                //qua trai
                int startX = 0;
                //len xuong
                int startY = ClsPublic.PaddingSizePrint;
                //khoang cach
                //DateTime dt = DateTime.Now;
                RectangleF header2Rect = new RectangleF();

                Font useFont = new Font("Time New Roman", 9, FontStyle.Bold);
                //print Pho Minh
                using (Font useFont1 = new Font("Time New Roman", 16, FontStyle.Bold))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("PHO MINH", useFont1, 290, StringFormat.GenericTypographic).Height));
                    //graphics.DrawString(, new Font("Time New Roman", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString("PHO MINH", useFont1, Brushes.Black, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;
                string[] info = { "40573 Margarita Rd Unit D", "Temecula, CA 92591", "(951) 296-0885", "phominh.net", Convert.ToDateTime(ticket.DateTimeIssue).ToString("MM/dd/yyyy"),
                                Convert.ToDateTime(ticket.DateTimeIssue).ToString("hh:mm tt"), "Server: "+ ticket.EmployeeName, "---------------------------------------------------------------",
                                "Table: " + ticket.TableName};







                //print information
                foreach (var item in info)
                {
                    //using (Font useFont = new Font("Time New Roman", 12, FontStyle.Regular))
                    //{
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString(item, useFont, 290, StringFormat.GenericTypographic).Height));
                    graphics.DrawString(item, useFont, Brushes.Black, header2Rect);
                    //}
                    startY = startY + (int)header2Rect.Size.Height;
                }

                using (Font useFont1 = new Font("Arial Black", 10, FontStyle.Bold))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Ticket: " + ticket.DTicketNum.ToString(), useFont1, 290, StringFormat.GenericTypographic).Height));
                    //graphics.DrawString(, new Font("Time New Roman", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString("Ticket: " + ticket.DTicketNum.ToString(), useFont1, Brushes.Black, header2Rect);
                    startY = startY + (int)header2Rect.Size.Height;
                }
                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("---------------------------------------------------------------", useFont, 290, StringFormat.GenericTypographic).Height));
                //graphics.DrawString(, new Font("Time New Roman", 16), new SolidBrush(Color.Black), startX, startY);
                graphics.DrawString("---------------------------------------------------------------", useFont, Brushes.Black, header2Rect);
                startY = startY + (int)header2Rect.Size.Height;

                //startY = startY + (int)header2Rect.Size.Height;

                //var listSaleItem = dao.FindByMultiColumnAnd<SaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticket.TicketID));
                //print item
                double subtotal = 0;
                RectangleF header2RectPrice;
                foreach (SaleItem item in listSaleItem)
                {
                    //using (Font useFont = new Font("Time New Roman", 12, FontStyle.Regular))
                    //{
                    //print Qty
                    //RectangleF header2RectQty = new RectangleF();
                    //header2RectQty.Location = new Point(0, startY);
                    //header2RectQty.Size = new Size(40, ((int)e.Graphics.MeasureString(item.Qty.ToString(), useFont, 40, StringFormat.GenericTypographic).Height));
                    //graphics.DrawString(item.Qty.ToString(), useFont, Brushes.Black, header2RectQty);

                    //print name
                    header2Rect.Location = new Point(0, startY);
                    header2Rect.Size = new Size(225, ((int)e.Graphics.MeasureString(item.Description + " x " + item.Qty.ToString(), useFont, 215, StringFormat.GenericTypographic).Height + 5));
                    graphics.DrawString(item.Description + " x " + item.Qty.ToString(), useFont, Brushes.Black, header2Rect);

                    //print price
                    header2RectPrice = new RectangleF();
                    header2RectPrice.Location = new Point(220, startY);
                    subtotal += Convert.ToDouble(item.TPrice);
                    header2RectPrice.Size = new Size(90, ((int)e.Graphics.MeasureString(string.Format("{0:C}", Convert.ToDouble(item.TPrice)), useFont, 90, StringFormat.GenericTypographic).Height + 5));
                    if (item.Description.Contains("Discount"))
                        graphics.DrawString((Convert.ToDouble(item.TPrice)/10).ToString("P0"), useFont, Brushes.Black, header2RectPrice);
                    else
                        graphics.DrawString(string.Format("{0:C}", Convert.ToDouble(item.TPrice)), useFont, Brushes.Black, header2RectPrice);
                    startY = startY + (int)header2Rect.Size.Height;
                }



                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(280, ((int)e.Graphics.MeasureString("---------------------------------------------------------------", useFont, 280, StringFormat.GenericTypographic).Height));
                graphics.DrawString("---------------------------------------------------------------", useFont, Brushes.Black, header2Rect);
                startY = startY + (int)header2Rect.Size.Height;

                //print subtotal
                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(220, ((int)e.Graphics.MeasureString("Subtotal", useFont, 220, StringFormat.GenericTypographic).Height));
                graphics.DrawString("Subtotal", useFont, Brushes.Black, header2Rect);
                //print price
                header2Rect.Location = new Point(220, startY);
                header2Rect.Size = new Size(90, ((int)e.Graphics.MeasureString(string.Format("{0:C}", subtotal), useFont, 90, StringFormat.GenericTypographic).Height));
                graphics.DrawString(string.Format("{0:C}", subtotal), useFont, Brushes.Black, header2Rect);



                startY = startY + (int)header2Rect.Size.Height;
                //print sale tax
                double saleTax = 0;
                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(220, ((int)e.Graphics.MeasureString("Sale Tax", useFont, 220, StringFormat.GenericTypographic).Height));
                graphics.DrawString("Sale Tax", useFont, Brushes.Black, header2Rect);
                //print price
                header2RectPrice = new RectangleF();
                header2RectPrice.Location = new Point(220, startY);
                saleTax = subtotal * 0.0875;
                header2RectPrice.Size = new Size(90, ((int)e.Graphics.MeasureString(string.Format("{0:C}", Convert.ToDouble(saleTax)), useFont, 90, StringFormat.GenericTypographic).Height));
                graphics.DrawString(string.Format("{0:C}", Convert.ToDouble(saleTax)), useFont, Brushes.Black, header2RectPrice);
                startY = startY + (int)header2Rect.Size.Height;


                //tips
                double tips = 0;
                try
                {
                    tips = Convert.ToDouble(ticketInfo.Tips);
                }
                catch
                {

                }

                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(220, ((int)e.Graphics.MeasureString("Tip", useFont, 220, StringFormat.GenericTypographic).Height));
                graphics.DrawString("Tip", useFont, Brushes.Black, header2Rect);
                //print price
                header2RectPrice = new RectangleF();
                header2RectPrice.Location = new Point(220, startY);
                header2RectPrice.Size = new Size(90, ((int)e.Graphics.MeasureString(string.Format("{0:C}", tips), useFont, 90, StringFormat.GenericTypographic).Height));
                graphics.DrawString(string.Format("{0:C}", tips), useFont, Brushes.Black, header2RectPrice);
                startY = startY + (int)header2Rect.Size.Height;


                //discount
                //tips
                double discount = 0;
                try
                {
                    discount = Convert.ToDouble(ticketInfo.Discount);
                }
                catch
                {

                }
                if (discount>0)
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(220, ((int)e.Graphics.MeasureString("Discount", useFont, 220, StringFormat.GenericTypographic).Height));
                    graphics.DrawString("Discount", useFont, Brushes.Black, header2Rect);
                    //print price
                    header2RectPrice = new RectangleF();
                    header2RectPrice.Location = new Point(220, startY);
                    header2RectPrice.Size = new Size(90, ((int)e.Graphics.MeasureString(string.Format("{0:C}", discount), useFont, 90, StringFormat.GenericTypographic).Height));
                    graphics.DrawString(string.Format("{0:C}", discount), useFont, Brushes.Black, header2RectPrice);
                    startY = startY + (int)header2Rect.Size.Height;
                }
                



                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(280, ((int)e.Graphics.MeasureString("---------------------------------------------------------------", useFont, 280, StringFormat.GenericTypographic).Height));
                graphics.DrawString("---------------------------------------------------------------", useFont, Brushes.Black, header2Rect);
                startY = startY + (int)header2Rect.Size.Height;


                //print total
                header2Rect.Location = new Point(startX, startY);
                header2Rect.Size = new Size(220, ((int)e.Graphics.MeasureString("Total", useFont, 220, StringFormat.GenericTypographic).Height + 10));
                graphics.DrawString("Total", useFont, Brushes.Black, header2Rect);
                //print price
                header2RectPrice.Location = new Point(220, startY);
                header2RectPrice.Size = new Size(90, ((int)e.Graphics.MeasureString(string.Format("{0:C}", Convert.ToDouble(subtotal + saleTax + tips)), useFont, 90, StringFormat.GenericTypographic).Height + 10));
                graphics.DrawString(string.Format("{0:C}", Convert.ToDouble(subtotal + saleTax + tips)), useFont, Brushes.Black, header2RectPrice);

                startY = startY + (int)header2Rect.Size.Height + 10;

                if (Convert.ToDouble(ticket.PaidCash) > 0 || Convert.ToDouble(ticket.PaidCredit) > 0)
                {
                    //print cash
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(220, ((int)e.Graphics.MeasureString("Cash", useFont, 220, StringFormat.GenericTypographic).Height));
                    graphics.DrawString("Cash", useFont, Brushes.Black, header2Rect);
                    //print price
                    header2RectPrice.Location = new Point(220, startY);
                    header2RectPrice.Size = new Size(90, ((int)e.Graphics.MeasureString(string.Format("{0:C}", ticket.PaidCash), useFont, 90, StringFormat.GenericTypographic).Height));
                    graphics.DrawString(string.Format("{0:C}", ticket.PaidCash), useFont, Brushes.Black, header2RectPrice);
                    startY = startY + (int)header2Rect.Size.Height;



                    //print credit
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(220, ((int)e.Graphics.MeasureString("Card " + ticket.CardCode, useFont, 220, StringFormat.GenericTypographic).Height));
                    graphics.DrawString("Card " + ticket.CardCode, useFont, Brushes.Black, header2Rect);
                    //print price
                    header2RectPrice.Location = new Point(220, startY);
                    header2RectPrice.Size = new Size(90, ((int)e.Graphics.MeasureString(string.Format("{0:C}", Convert.ToDouble(ticket.PaidCredit) + tips), useFont, 90, StringFormat.GenericTypographic).Height));
                    graphics.DrawString(string.Format("{0:C}", Convert.ToDouble(ticket.PaidCredit) + tips), useFont, Brushes.Black, header2RectPrice);
                    startY = startY + (int)header2Rect.Size.Height;



                    header2Rect.Location = new Point(startX, startY + 10);
                    header2Rect.Size = new Size(220, ((int)e.Graphics.MeasureString("", useFont, 220, StringFormat.GenericTypographic).Height));
                    graphics.DrawString("", useFont, Brushes.Black, header2Rect);
                }

            }

            catch (System.Exception ex)
            {
                ClsPublic.WriteException(ex);
            }
        }


        public void PrintTickets()
        {
            PrintDocument pdoc = new PrintDocument();
            PrintController printController = new StandardPrintController();
            pdoc.PrintController = printController;
            if (ClsPublic.Printers != "")
            {

                pdoc.PrinterSettings.PrinterName = ClsPublic.PrinterOpenCashDrawerName;

            }
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintTickets);
            pdoc.Print();
        }

        public void OpenDrawer()
        {
            PrintDocument pdoc = new PrintDocument();
            PrintController printController = new StandardPrintController();
            pdoc.PrintController = printController;
            if (ClsPublic.Printers != "")
            {

                pdoc.PrinterSettings.PrinterName = ClsPublic.Printers;
                pdoc.PrinterSettings.PrinterName = "MCP30 - Ethernet:TCP:";
            }
            pdoc.PrintPage += new PrintPageEventHandler(handleOpenDrawer);
            pdoc.Print();
        }

        void handleOpenDrawer(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                string text = Convert.ToChar(27).ToString() + Convert.ToChar(7).ToString() + Convert.ToChar(11).ToString() + Convert.ToChar(55).ToString() + Convert.ToChar(7).ToString();
                System.Drawing.Font printFont = new System.Drawing.Font
                    ("Arial", 35, System.Drawing.FontStyle.Regular);

                // Draw the content.
                e.Graphics.DrawString(text, printFont,
                    System.Drawing.Brushes.Black, 10, 10);
            }
            catch (Exception)
            {

                throw;
            }
        }



        void pdoc_PrintItem(object sender, PrintPageEventArgs e)
        {
            try
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
                RectangleF header2Rect = new RectangleF();
                //print reprint
                using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("***Reprint***", useFont, 290, StringFormat.GenericTypographic).Height));
                    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString("***Reprint***", useFont, Brushes.Black, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;
                PhoHa7_ProcTickets ticketInfo = ticket;
                if (Convert.ToBoolean(ticketInfo.TakeOut))
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
                }
                //in thông tin của ticket
                //print reprint
                //using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                //{
                //    header2Rect.Location = new Point(startX, startY);
                //    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("***Reprint***", useFont, 290, StringFormat.GenericTypographic).Height));
                //    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                //    graphics.DrawString("***Reprint***", useFont, Brushes.Black, header2Rect);
                //}
                //startY = startY + (int)header2Rect.Size.Height;
                //print DTicketNum
                if (Convert.ToBoolean(ticketInfo.TakeOut))
                {
                    //customer name
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
                    else
                    {
                        using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                        {
                            header2Rect.Location = new Point(startX, startY);
                            header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Table: " + ticketInfo.TableName, useFont, 290, StringFormat.GenericTypographic).Height));
                            //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                            graphics.DrawString("Table: " + ticketInfo.TableName, useFont, Brushes.Black, header2Rect);
                        }
                        startY = startY + (int)header2Rect.Size.Height;
                    }
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
                    //table
                    using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                    {
                        header2Rect.Location = new Point(startX, startY);
                        header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Table: " + ticketInfo.TableName, useFont, 290, StringFormat.GenericTypographic).Height));
                        //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                        graphics.DrawString("Table: " + ticketInfo.TableName, useFont, Brushes.Black, header2Rect);
                    }
                    startY = startY + (int)header2Rect.Size.Height;
                }
                //emp name
                using (Font useFont = new Font("Courier New", fontSize, FontStyle.Regular))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Server: " + ticketInfo.EmployeeName, useFont, 290, StringFormat.GenericTypographic).Height));
                    graphics.DrawString("Server: " + ticketInfo.EmployeeName, useFont, Brushes.Black, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;
                using (Font useFont = new Font("Courier New", 12, FontStyle.Regular))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Date: " + String.Format("{0:M/d/yyyy HH:mm:ss}", dt), useFont, 290, StringFormat.GenericTypographic).Height));
                    graphics.DrawString("Date: " + String.Format("{0:M/d/yyyy HH:mm}", dt), useFont, Brushes.Black, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;


                //print item
                foreach (Product item in ticketInfo.ListProduct)
                {
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

            catch (System.Exception ex)
            {
                ClsPublic.WriteException(ex);
            }
        }

        void pdoc_PrintLastItem(object sender, PrintPageEventArgs e)
        {
            try
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
                RectangleF header2Rect = new RectangleF();
                //print reprint
                using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                {
                    header2Rect.Location = new Point(startX, startY);
                    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("***Reprint***", useFont, 290, StringFormat.GenericTypographic).Height));
                    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                    graphics.DrawString("***Reprint***", useFont, Brushes.Black, header2Rect);
                }
                startY = startY + (int)header2Rect.Size.Height;
                if (ClsPublic.ListPrintItem.Count > 0)
                {
                    PhoHa7_ProcTickets ticketInfo = ClsPublic.ListPrintItem[ClsPublic.PrintPosition];
                    if (Convert.ToBoolean(ticketInfo.TakeOut))
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
                    }
                    //in thông tin của ticket
                    //print reprint
                    //using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                    //{
                    //    header2Rect.Location = new Point(startX, startY);
                    //    header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("***Reprint***", useFont, 290, StringFormat.GenericTypographic).Height));
                    //    //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                    //    graphics.DrawString("***Reprint***", useFont, Brushes.Black, header2Rect);
                    //}
                    //startY = startY + (int)header2Rect.Size.Height;
                    //print DTicketNum
                    if (Convert.ToBoolean(ticketInfo.TakeOut))
                    {
                        //customer name
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
                        else
                        {
                            using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                            {
                                header2Rect.Location = new Point(startX, startY);
                                header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Table: " + ticketInfo.TableName, useFont, 290, StringFormat.GenericTypographic).Height));
                                //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                                graphics.DrawString("Table: " + ticketInfo.TableName, useFont, Brushes.Black, header2Rect);
                            }
                            startY = startY + (int)header2Rect.Size.Height;
                        }
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
                        //table
                        using (Font useFont = new Font("Courier New", fontSize, FontStyle.Bold))
                        {
                            header2Rect.Location = new Point(startX, startY);
                            header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Table: " + ticketInfo.TableName, useFont, 290, StringFormat.GenericTypographic).Height));
                            //graphics.DrawString(, new Font("Courier New", 16), new SolidBrush(Color.Black), startX, startY);
                            graphics.DrawString("Table: " + ticketInfo.TableName, useFont, Brushes.Black, header2Rect);
                        }
                        startY = startY + (int)header2Rect.Size.Height;
                    }
                    //emp name
                    using (Font useFont = new Font("Courier New", fontSize, FontStyle.Regular))
                    {
                        header2Rect.Location = new Point(startX, startY);
                        header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Server: " + ticketInfo.EmployeeName, useFont, 290, StringFormat.GenericTypographic).Height));
                        graphics.DrawString("Server: " + ticketInfo.EmployeeName, useFont, Brushes.Black, header2Rect);
                    }
                    startY = startY + (int)header2Rect.Size.Height;
                    using (Font useFont = new Font("Courier New", 12, FontStyle.Regular))
                    {
                        header2Rect.Location = new Point(startX, startY);
                        header2Rect.Size = new Size(290, ((int)e.Graphics.MeasureString("Date: " + String.Format("{0:M/d/yyyy HH:mm:ss}", dt), useFont, 290, StringFormat.GenericTypographic).Height));
                        graphics.DrawString("Date: " + String.Format("{0:M/d/yyyy HH:mm}", dt), useFont, Brushes.Black, header2Rect);
                    }
                    startY = startY + (int)header2Rect.Size.Height;


                    //print item
                    foreach (Product item in ticketInfo.ListProduct)
                    {
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
                    ClsPublic.PrintPosition--;
                    if (ClsPublic.PrintPosition < 0)
                    {
                        ClsPublic.PrintPosition = ClsPublic.NumberOfReprint - 1;
                    }
                    if (ClsPublic.PrintPosition > (ClsPublic.ListPrintItem.Count - 1))
                    {
                        ClsPublic.PrintPosition = ClsPublic.ListPrintItem.Count - 1;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ClsPublic.WriteException(ex);
            }
        }

    }
}
