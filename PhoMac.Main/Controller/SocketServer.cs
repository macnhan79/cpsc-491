using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Froms;
using PhoMac.Main;
using PhoMac.Main.Controller;
using PhoMac.Main.Test;
using PhoMac.Model;
using PhoMac.Model.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServerApp
{
    public class SocketServer
    {
        private TcpListener server;
        private Thread listenerThread;
        private List<Thread> clientThreads;

        public event EventHandler<string> MessageReceived;

        public bool IsRunning { get; private set; }

        public void StartServer()
        {
            if (IsRunning)
                return;

            try
            {
                // Set the IP address and port number for the server
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 8888;

                server = new TcpListener(ipAddress, port);
                server.Start();
                IsRunning = true;

                clientThreads = new List<Thread>();
                listenerThread = new Thread(ListenForClients);
                listenerThread.Start();
            }
            catch (Exception ex)
            {
                // Handle any initialization errors
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ListenForClients()
        {
            try
            {
                while (IsRunning)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected: " + client.Client.RemoteEndPoint);

                    // Handle the client connection in a separate thread
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThreads.Add(clientThread);
                    clientThread.Start();
                }
            }
            catch (SocketException)
            {
                // This exception is thrown when the server is stopped, so we can ignore it.
            }
            catch (Exception ex)
            {
                // Handle any other errors
                Console.WriteLine("Error in ListenForClients: " + ex.Message);
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var msg = message.Split('|');
                    ///       printReceipt|ProcTicket|12345
                    ///       printReceipt|Ticket|12345
                    if (msg.Length > 0 && msg[0] == "printReceipt")
                    {
                        Dao dao = new Dao();
                        Ticket ticket;
                        List<SaleItem> saleItem = new List<SaleItem>();
                        if (msg[1] == "ProcTicket")
                        {
                            ProcTicket procTicket = dao.GetById<ProcTicket>(Convert.ToInt32(msg[2]));
                            var procSaleItem = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(msg[2]));
                            ticket = copyProcTicket2Ticket(procTicket);
                            foreach (var item in procSaleItem)
                            {
                                saleItem.Add(copyProcSaleItem2SaleItem(item));
                            }
                            printTicket(ticket, saleItem);
                        }
                        else if (msg[1] == "Ticket")
                        {
                            ticket = dao.GetById<Ticket>(Convert.ToInt32(msg[2]));
                            saleItem = (List<SaleItem>)dao.FindByMultiColumnAnd<SaleItem>(new[] { "TicketID" }, Convert.ToInt32(msg[2]));
                            printTicket(ticket, saleItem);
                        }

                        //print ticket



                    }
                    else if (msg.Length > 0 && msg[0] == "OpenCashDrawer")
                    {
                        //check passcode
                        //CashDrawer.OpenCashDrawer(ClsPublic.PrinterOpenCashDrawerName);
                    }
                    // Raise the MessageReceived event to notify listeners about the received message
                    //OnMessageReceived(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling client: " + ex.Message);
            }
            finally
            {
                client.Close();
                //Console.WriteLine("Client disconnected: " + client.Client.RemoteEndPoint);
            }
        }

        public void StopServer()
        {
            IsRunning = false;
            server.Stop();

            // Wait for all client threads to complete
            foreach (Thread clientThread in clientThreads)
            {
                clientThread.Join();
            }

            clientThreads.Clear();
        }

        protected virtual void OnMessageReceived(string message)
        {
            MessageReceived.Invoke(this, message);
        }

        void printTicket(Ticket ticket, List<SaleItem> s){
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            Dao dao = new Dao();

            //CashDrawer.OpenCashDrawer("MCP30 - Ethernet:TCP:");
            if (ClsPublic.ListPrintItem != null)
            {
                try
                {
                    PrintTicket printer = new PrintTicket();
                    printer.ticketInfo = ticket;
                    printer.listSaleItem = s;
                    //Thread t = new Thread(new ThreadStart(printer.printLastItem));
                    Thread t = new Thread(new ThreadStart(printer.PrintTickets));
                    t.Start();
                    //printer.printLastItem();
                }
                catch (System.Exception ex)
                {
                    ClsPublic.WriteException(ex);
                }
            }
            SplashScreenManager.CloseForm();
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



        public string RunClient(string address, int port)
        {
            string response = "";
            // Set up a listener on that address/port
            TcpClient tcpClient = new TcpClient(address, port);
            if (tcpClient != null)
            {
                string message = "Hello there";
                // Translate the passed message into UTF8ASCII and store it as a Byte array.
                byte[] bytes = Encoding.ASCII.GetBytes(message);

                NetworkStream stream = tcpClient.GetStream();

                // Send the message to the connected TcpServer. 
                // The write flushes the stream automatically here
                stream.Write(bytes, 0, bytes.Length);

                // Get the response from the server

                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                try
                {
                    response = reader.ReadToEnd();
                }
                finally
                {
                    // Close the reader
                    reader.Close();
                }

                // Close the client
                tcpClient.Close();
            }
            // Return the response text
            return response;
        }




    }
}
