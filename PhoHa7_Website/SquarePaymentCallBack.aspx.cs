using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PhoMac.Model.Data;
using PhoMac.Model;
using PhoMac.Model.Factory;
using System.Data.Entity;

public partial class SquarePaymentCallBack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Dao dao = new Dao();
            dynamic json = JsonConvert.DeserializeObject(Request.QueryString["data"]);
            HtmlGenericControl myDiv = (HtmlGenericControl)FindControl("callbackInformation");
            //Label1.Text = json["status"].ToString();
            if (json["status"] == "ok")
            {
                string orderID = "";
                try
                {
                    orderID = json["transaction_id"].ToString();
                    //check ticket already add to Ticket table
                    var ticket = dao.FindByMultiColumnAnd<Ticket>(new[] { "XferTo" }, orderID).FirstOrDefault();
                    if (ticket == null)
                    {
                        Task<string> response = getSquareOrderDetails(orderID);
                        response.Wait();
                        json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(response.Result.ToString());

                        Task<string> response1 = handleCompleteTicket(json);
                        response1.Wait();
                        string tableID = response1.Result.ToString();

                        //HyperLink1.NavigateUrl = "default.aspx?tableid=" + tableID + "&type=modify";
                        if (tableID != "")
                            Response.Redirect("default.aspx?tableid=" + tableID + "&type=modify");
                        else
                            Response.Redirect("table.aspx");
                    }
                    else
                    {
                        Response.Redirect("table.aspx");
                    }
                }
                catch (System.Exception ex)
                {
                }
            }
            else
            {
                Response.Redirect("table.aspx");
            }







        }

    }


    protected async Task<string> handleCompleteTicket(Newtonsoft.Json.Linq.JObject json)
    {
        try
        {
            //Dao dao = new Dao(false, true);
            Dao dao = new Dao();
            var ticketID = json.SelectToken("orders[0].line_items[0].note");
            string[] ticketIDSaleItemID = ticketID.ToString().Split('|');
            // var listProcTickets = dao.GetAll<ProcTicket>();
            string tableID = "";
            //EntityFactory.getInstance().BeginTransactionEntities();
            ProcTicket procTicket = dao.GetById<ProcTicket>(Convert.ToInt32(ticketIDSaleItemID[0]));



            if (procTicket != null)
            //if (ticketIDSaleItemID[0] == listProcTicket.TicketID.ToString())
            {
                using (var context = new PhoMac.Model.Entities())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {



                        //copy procticket to ticket table
                        //ProcTicket procTicket = dao.GetById<ProcTicket>(Convert.ToInt32(ticketIDSaleItemID[0]));
                        //get tax, subtotal, credit amount, cash amount from Square to ProcTicket
                        procTicket.Tax = Convert.ToDecimal(json.SelectToken("orders[0].line_items[0].total_tax_money.amount").ToString()) / 100;
                        procTicket.TotalP = Convert.ToDecimal(json.SelectToken("orders[0].line_items[0].gross_sales_money.amount").ToString()) / 100;
                        procTicket.XferTo = json.SelectToken("orders[0].id").ToString();
                        var tenders = json.SelectToken("orders[0].tenders");
                        procTicket.PaidCredit = 0;
                        procTicket.PaidCash = 0;
                        foreach (var tender in tenders)
                        {
                            if (tender["type"].ToString() == "CASH")
                            {
                                procTicket.PaidCash += Convert.ToDecimal(tender.SelectToken("amount_money.amount").ToString()) / 100;
                            }
                            else if (tender["type"].ToString() == "CARD")
                            {
                                procTicket.CardCode = tender.SelectToken("card_details.card.last_4").ToString();
                                procTicket.PaidCredit += (Convert.ToDecimal(tender.SelectToken("amount_money.amount").ToString()) / 100);

                                try
                                {
                                    procTicket.Tips = Convert.ToDecimal(tender.SelectToken("tip_money.amount").ToString()) / 100;
                                }
                                catch (System.Exception ex)
                                {
                                    procTicket.Tips = 0;
                                }
                                procTicket.PaidCredit = procTicket.PaidCredit - procTicket.Tips;
                            }
                        }
                        Ticket ticket = procTicket.copyProcTicket2Ticket();

                        var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));
                        //handle ticket pay all
                        if (ticketIDSaleItemID.Length - 1 == procSaleItems.Count)
                        {
                            //copy procSaleItem to SaleItem table


                            HashSet<decimal> discountPercent = new HashSet<decimal>();
                            decimal subtotal = 0;
                            decimal discount = 0;
                            //ticket = dao.Add1<Ticket>(ticket);
                            context.Tickets.Add(ticket);
                            context.SaveChanges();
                            foreach (var procSaleItem in procSaleItems)
                            {

                                SaleItem saleItem = procSaleItem.copyProcSaleItem2SaleItem();
                                ///handle discount
                                if (procSaleItem.TPrice < 0)
                                {
                                    discountPercent.Add(Convert.ToDecimal(procSaleItem.TPrice));
                                }
                                saleItem.TicketID = ticket.TicketID;
                                subtotal += Convert.ToDecimal(procSaleItem.TPrice);



                                //dao.Add<SaleItem>(saleItem);
                                //dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                                context.SaleItems.Add(saleItem);
                                context.ProcSaleItems.Attach(procSaleItem);
                                context.Entry(procSaleItem).State = EntityState.Deleted;
                            }
                            //calc discount 
                            foreach (var discountItem in discountPercent)
                            {
                                discount += subtotal * (discountItem / 100);
                            }
                            ticket.Discount = discount;


                            //dao.Update<Ticket>(ticket);
                            context.Tickets.Attach(ticket);
                            context.Entry(ticket).State = EntityState.Modified;
                            ////delete ProcTicket and ProcSaleItem
                            //dao.Delete<ProcTicket>(procTicket.TicketID);
                            context.ProcTickets.Attach(procTicket);
                            context.Entry(procTicket).State = EntityState.Deleted;
                            //EntityFactory.getInstance().commit();
                            context.SaveChanges();
                            dbContextTransaction.Commit();

                            //auto print ticket
                            string tID = ticket.TicketID.ToString();
                            SocketClient client = new SocketClient();
                            client.ConnectAndSendMsg("127.0.0.1", 8888, "printReceipt|Ticket|" + tID);
                        }

                        //hand ticket  split bill
                        else
                        {

                            //copy procSaleItem to SaleItem table
                            int counSaleItemCommit = 0;
                            //var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));
                            if (procSaleItems.Count > 0)
                            {

                            }
                            HashSet<decimal> discountPercent = new HashSet<decimal>();
                            //ticket = dao.Add1<Ticket>(ticket);
                            context.Tickets.Add(ticket);
                            context.SaveChanges();


                            //add ticket
                            List<SaleItem> listSaleItem = new List<SaleItem>();
                            foreach (var procSaleItem in procSaleItems)
                            {
                                if (ticketIDSaleItemID.Contains(procSaleItem.SaleItemID.ToString()))
                                {

                                    SaleItem saleItem = procSaleItem.copyProcSaleItem2SaleItem();
                                    saleItem.TicketID = ticket.TicketID;

                                    ///handle discount
                                    if (procSaleItem.TPrice < 0)
                                    {
                                        discountPercent.Add(Convert.ToDecimal(procSaleItem.TPrice));
                                    }

                                    //dao.Add<SaleItem>(saleItem);
                                    //dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                                    context.SaleItems.Add(saleItem);
                                    context.ProcSaleItems.Attach(procSaleItem);
                                    context.Entry(procSaleItem).State = EntityState.Deleted;


                                    listSaleItem.Add(saleItem);
                                    counSaleItemCommit++;
                                }
                            }                                                                                 //count = 0
                            //calc discount 
                            decimal subtotal = 0;
                            decimal discount = 0;
                            foreach (var discountItem in discountPercent)
                            {
                                discount += subtotal * (discountItem / 100);
                            }
                            ticket.Discount = discount;
                            //dao.Update<Ticket>(ticket);  
                            context.Entry(ticket).State = EntityState.Modified;//update ticket
                            if (procSaleItems.Count == counSaleItemCommit)
                            {
                                //delete ProcTicket and ProcSaleItem
                                try
                                {
                                    //dao.Delete<ProcTicket>(procTicket.TicketID);
                                    context.ProcTickets.Attach(procTicket);
                                    context.Entry(procTicket).State = EntityState.Deleted;
                                    
                                }
                                catch (System.Exception ex)
                                {
                                    //dao.Delete<Ticket>(ticket.TicketID);
                                    context.Tickets.Attach(ticket);
                                    context.Entry(ticket).State = EntityState.Deleted;
                                }
                                // saleitem = 3 = count
                            }
                            context.SaveChanges();
                            dbContextTransaction.Commit();

                            string tID = ticket.TicketID.ToString();
                            SocketClient client = new SocketClient();
                            client.ConnectAndSendMsg("127.0.0.1", 8888, "printReceipt|Ticket|" + tID);
                            tableID = procTicket.TableID.ToString();
                            //if (counSaleItemCommit > 0)
                            //{
                            //    //if (listSaleItem.Count==0)
                            //    //{
                            //    //    dao.Delete<Ticket>(ticket.TicketID);      
                            //    //}
                            //    EntityFactory.getInstance().commit();                                           //commit

                            //}
                            //else
                            //{
                            //    EntityFactory.getInstance().rollBack();
                            //}

                        }
                    }
                }
            }


            if (tableID != "")
                return tableID;
            //Response.Redirect("default.aspx?tableid=" + tableID + "&type=modify");
            else
                return "";
            //Response.Redirect("table.aspx");

        }
        catch (System.Exception ex)
        {
            ClsPublic.WriteException(ex);
            return "";
        }
    }

    protected async Task<string> getSquareOrderDetails(string orderID)
    {
        HttpClient client = new HttpClient();
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
               | SecurityProtocolType.Tls11
               | SecurityProtocolType.Tls12
               | SecurityProtocolType.Ssl3;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://connect.squareup.com/v2/orders/batch-retrieve");

        request.Headers.Add("Square-Version", "2023-06-08");
        request.Headers.Add("Authorization", "Bearer EAAAEBkTIFhqhIKegI5ih6bRhV_b0bfyhQyg-VPvUBH20wGiLM9kHLLBA7KQFgi1");

        request.Content = new StringContent("{\n    \"location_id\": \"LS7M0EPVEJZNB\",\n    \"order_ids\": [\n      \"" + orderID + "\"\n    ]\n  }");
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        return responseBody;
    }



    public async Task<string> getAllSquareOrderDetails()
    {
        try
        {
            HttpClient client = new HttpClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://connect.squareup.com/v2/orders/search");

            request.Headers.Add("Square-Version", "2023-06-08");
            request.Headers.Add("Authorization", "Bearer EAAAEBkTIFhqhIKegI5ih6bRhV_b0bfyhQyg-VPvUBH20wGiLM9kHLLBA7KQFgi1");
            DateTime today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            string stoday = today.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");

            //request.Content = new StringContent("{\n    \"location_ids\": [\n      \"LS7M0EPVEJZNB\"\n    ],\n    \"query\": {\n      \"filter\": {\n        \"date_time_filter\": {\n          \"created_at\": {\n            \"start_at\": \"2023-07-18T00:00:00-07:00\"\n          }\n        }\n      }\n    }\n  }");
            request.Content = new StringContent("{\n    \"location_ids\": [\n      \"LS7M0EPVEJZNB\"\n    ],\n    \"query\": {\n      \"filter\": {\n        \"date_time_filter\": {\n          \"created_at\": {\n            \"start_at\": \"" + stoday + "\"\n          }\n        }\n      }\n    }\n  }");

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseOrderDetails = await response.Content.ReadAsStringAsync();
            return responseOrderDetails;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }


    public string updateCompletedTicket()
    {
        string tableID = "";
        try
        {
            
            Task<string> response = getAllSquareOrderDetails();
            response.Wait();
            var json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(response.Result.ToString());


            //Dao dao = new Dao(false, true);
            Dao dao = new Dao();
            var listProcTickets = dao.GetAll<ProcTicket>();
            //EntityFactory.getInstance().BeginTransactionEntities();
            foreach (var item in json["orders"])
            {
                string idXferToOnlinePickUP = string.Empty;
                string ticketID = "";
                try
                {
                    ticketID = item.SelectToken("line_items[0].note").ToString();

                }
                catch
                {
                    continue;
                }
                string transactionID = item.SelectToken("id").ToString();
                var t = dao.FindByMultiColumnAnd<Ticket>(new[] { "XferTo" }, transactionID).FirstOrDefault();
                if (t != null)
                {
                    continue;
                }



                string[] ticketIDSaleItemID = ticketID.Split('|');
                ProcTicket procTicket = listProcTickets.FirstOrDefault(s => s.TicketID.ToString() == ticketIDSaleItemID[0]);

                //ticketIDSaleItemID[0] = "111720";
                //using (PhoMac.Model.Entities objEntities = new PhoMac.Model.Entities())
                //{
                //}
                if (procTicket != null)
                {
                    using (var context = new PhoMac.Model.Entities())
                    {
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {

                            //copy procticket to ticket table
                            //ProcTicket procTicket = dao.GetById<ProcTicket>(Convert.ToInt32(ticketIDSaleItemID[0]));
                            //get tax, subtotal, credit amount, cash amount from Square to ProcTicket
                            procTicket.Tax = Convert.ToDecimal(item.SelectToken("line_items[0].total_tax_money.amount").ToString()) / 100;
                            procTicket.TotalP = Convert.ToDecimal(item.SelectToken("line_items[0].gross_sales_money.amount").ToString()) / 100;
                            procTicket.XferTo = item.SelectToken("id").ToString();
                            var tenders = item.SelectToken("tenders");
                            procTicket.PaidCredit = 0;
                            procTicket.PaidCash = 0;
                            foreach (var tender in tenders)
                            {
                                if (tender["type"].ToString() == "CASH")
                                {
                                    procTicket.PaidCash += Convert.ToDecimal(tender.SelectToken("amount_money.amount").ToString()) / 100;
                                }
                                else if (tender["type"].ToString() == "CARD")
                                {
                                    procTicket.CardCode = tender.SelectToken("card_details.card.last_4").ToString();
                                    procTicket.PaidCredit += (Convert.ToDecimal(tender.SelectToken("amount_money.amount").ToString()) / 100);

                                    try
                                    {
                                        procTicket.Tips = Convert.ToDecimal(tender.SelectToken("tip_money.amount").ToString()) / 100;
                                    }
                                    catch (System.Exception ex)
                                    {
                                        procTicket.Tips = 0;
                                    }
                                    procTicket.PaidCredit = procTicket.PaidCredit - procTicket.Tips;
                                }
                            }
                            Ticket ticket = procTicket.copyProcTicket2Ticket();

                            var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));

                            //handle ticket pay all
                            if (ticketIDSaleItemID.Length - 1 == procSaleItems.Count)
                            {
                                //copy procSaleItem to SaleItem table
                                HashSet<decimal> discountPercent = new HashSet<decimal>();
                                decimal subtotal = 0;
                                decimal discount = 0;
                                //ticket = dao.Add1<Ticket>(ticket);
                                context.Tickets.Add(ticket);
                                context.SaveChanges();
                                foreach (var procSaleItem in procSaleItems)
                                {

                                    SaleItem saleItem = procSaleItem.copyProcSaleItem2SaleItem();
                                    ///handle discount
                                    if (procSaleItem.TPrice < 0)
                                    {
                                        discountPercent.Add(Convert.ToDecimal(procSaleItem.TPrice));
                                    }
                                    saleItem.TicketID = ticket.TicketID;
                                    subtotal += Convert.ToDecimal(procSaleItem.TPrice);




                                    //dao.Add<SaleItem>(saleItem);
                                    //dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                                    context.SaleItems.Add(saleItem);
                                    context.ProcSaleItems.Attach(procSaleItem);
                                    context.Entry(procSaleItem).State = EntityState.Deleted;
                                }
                                //calc discount 
                                foreach (var discountItem in discountPercent)
                                {
                                    discount += subtotal * (discountItem / 100);
                                }
                                ticket.Discount = discount;


                                //dao.Update<Ticket>(ticket);
                                context.Tickets.Attach(ticket);
                                context.Entry(ticket).State = EntityState.Modified;
                                ////delete ProcTicket and ProcSaleItem
                                //dao.Delete<ProcTicket>(procTicket.TicketID);
                                context.ProcTickets.Attach(procTicket);
                                context.Entry(procTicket).State = EntityState.Deleted;
                                //EntityFactory.getInstance().commit();
                                context.SaveChanges();
                                dbContextTransaction.Commit();
                            }

                            //hand ticket  split bill
                            else
                            {

                                //copy procSaleItem to SaleItem table
                                int counSaleItemCommit = 0;
                                //var procSaleItems = dao.FindByMultiColumnAnd<ProcSaleItem>(new[] { "TicketID" }, Convert.ToInt32(ticketIDSaleItemID[0]));

                                HashSet<decimal> discountPercent = new HashSet<decimal>();
                                //ticket = dao.Add1<Ticket>(ticket);
                                context.Tickets.Add(ticket);
                                context.SaveChanges();

                                List<SaleItem> listSaleItem = new List<SaleItem>();
                                foreach (var procSaleItem in procSaleItems)
                                {
                                    if (ticketIDSaleItemID.Contains(procSaleItem.SaleItemID.ToString()))
                                    {

                                        SaleItem saleItem = procSaleItem.copyProcSaleItem2SaleItem();
                                        saleItem.TicketID = ticket.TicketID;

                                        ///handle discount
                                        if (procSaleItem.TPrice < 0)
                                        {
                                            discountPercent.Add(Convert.ToDecimal(procSaleItem.TPrice));
                                        }

                                        //dao.Add<SaleItem>(saleItem);
                                        //dao.Delete<ProcSaleItem>(procSaleItem.SaleItemID);
                                        context.SaleItems.Add(saleItem);
                                        context.ProcSaleItems.Attach(procSaleItem);
                                        context.Entry(procSaleItem).State = EntityState.Deleted;


                                        listSaleItem.Add(saleItem);
                                        counSaleItemCommit++;
                                    }
                                }
                                //calc discount 
                                decimal subtotal = 0;
                                decimal discount = 0;
                                foreach (var discountItem in discountPercent)
                                {
                                    discount += subtotal * (discountItem / 100);
                                }
                                ticket.Discount = discount;
                                //dao.Update<Ticket>(ticket);
                                context.Tickets.Attach(ticket);
                                context.Entry(ticket).State = EntityState.Modified;

                                if (procSaleItems.Count == counSaleItemCommit)
                                {
                                    //delete ProcTicket and ProcSaleItem
                                    try
                                    {
                                        //dao.Delete<ProcTicket>(procTicket.TicketID);
                                        context.ProcTickets.Attach(procTicket);
                                        context.Entry(procTicket).State = EntityState.Deleted;
                                    }
                                    catch (System.Exception ex)
                                    {
                                        //dao.Delete<Ticket>(ticket.TicketID);
                                        context.Tickets.Attach(ticket);
                                        context.Entry(ticket).State = EntityState.Deleted;
                                    }
                                }
                                context.SaveChanges();
                                dbContextTransaction.Commit();
                                tableID = ticket.TableID.ToString();
                                //if (counSaleItemCommit > 0)
                                //{
                                //    EntityFactory.getInstance().commit();
                                //}
                                //else
                                //{
                                //    try
                                //    {
                                //        EntityFactory.getInstance().rollBack();
                                //    }
                                //    catch
                                //    {

                                //    }

                                //}

                            }

                            continue;
                        }
                    }
                }




            }
        }
        catch (System.Exception ex)
        {
            ClsPublic.WriteException(ex);
        }
        finally
        {
            
        }
        return tableID;
    }



}