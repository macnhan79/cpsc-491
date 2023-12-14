using PhoHa7.Library.Classes.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data.SqlClient;
using PhoHa7.Library.Enum;
using PhoHa7.Library.Classes.Common;
using PhoMac.Model.Data;
using PhoMac.Model;

/// <summary>
/// Summary description for Tickets
/// </summary>
public class ProcTickets
{
    SqlHelperWeb sqlHelper;
    public ProcTickets()
    {
        sqlHelper = new SqlHelperWeb(DA_Connection.GetConnectionString());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productList">With 12 column: ProductID(int),BarCode[String], ProductName(string), KitchenName[string], CategoryID[int]
    /// LPrice[double], SPrice[double], Extra[double], Quality[int] SmallSize[bool], ToGo(boolean), EmployeeName[string]</param>
    /// <param name="tableID"></param>
    /// <param name="customerName"></param>
    /// <param name="tabParties"></param>
    /// <returns></returns>
    public int insertProcTicket(DataTable productList, DataTable productListDel, int tableID, string customerName, int tabParties, 
        int empID, string empName, bool ticketTogo, EnumFormStatus statusTicket, int oldTicketID, bool isEmergency)
    {
        int count = 0;
        try
        {
            sqlHelper.BeginTransaction();
            try
            {
                Dao dao = new Dao();
                //bool isFirstTicket = false;
                int ticketNum = 0;
                int dTicketNum = 0;
                double amount = 0;
                //current datetime
                DateTime date = DateTime.Today;
                //get TabCatID from TableID
                int tabCatID = Convert.ToInt32(sqlHelper.ExecuteScalar("select CategoryID from Tables where TableID = @TableID", CommandType.Text, true, new object[] { "@TableID" }, new object[] { tableID }));
                //get TableName
                string tableName = sqlHelper.ExecuteScalar("select TableName from Tables where TableID = @TableID", CommandType.Text, true, new object[] { "@TableID" }, new object[] { tableID }).ToString().Trim();

                //get max DTicketNum in ProcTicket, Tickets, DelItems
                List<int> listDTicketNum = new List<int>();
                //max and count dticketnum from ProcTickets table
                string oDTicketProcTicket = sqlHelper.ExecuteScalar("select max(DTicketNum) from (select * from ProcTickets where TabCatID=@TabCatID and DateTimeIssue > @DateTime) as T",
                    CommandType.Text, true, new object[] { "@TabCatID", "@DateTime" }, new object[] { tabCatID, DateTime.Today }).ToString();
                listDTicketNum.Add(Convert.ToInt32(oDTicketProcTicket == String.Empty ? "0" : oDTicketProcTicket));
                string oDTicketProcTicketCount = sqlHelper.ExecuteScalar("select count(*) from ProcTickets where TabCatID=@TabCatID and DateTimeIssue > @DateTime",
                    CommandType.Text, true, new object[] { "@TabCatID", "@DateTime" }, new object[] { tabCatID, DateTime.Today }).ToString();
                listDTicketNum.Add(Convert.ToInt32(oDTicketProcTicketCount == String.Empty ? "0" : oDTicketProcTicketCount));


                //max and count dticketnum from Tickets table
                string oDTicketTickets = sqlHelper.ExecuteScalar("select max(DTicketNum) from (select * from Tickets where TabCatID=@TabCatID and DateTimeIssue > @DateTime) as T",
                    CommandType.Text, true, new object[] { "@TabCatID", "@DateTime" }, new object[] { tabCatID, DateTime.Today }).ToString();
                listDTicketNum.Add(Convert.ToInt32(oDTicketTickets == String.Empty ? "0" : oDTicketTickets));
                string oDTicketTicketsCount = sqlHelper.ExecuteScalar("select count(*) from Tickets where TabCatID=@TabCatID and DateTimeIssue > @DateTime",
                    CommandType.Text, true, new object[] { "@TabCatID", "@DateTime" }, new object[] { tabCatID, DateTime.Today }).ToString();
                listDTicketNum.Add(Convert.ToInt32(oDTicketTicketsCount == String.Empty ? "0" : oDTicketTicketsCount));



                dTicketNum = listDTicketNum.Max() + 1;




                string sqlInsert = "INSERT INTO [ProcTickets] " +
                   "([CustomerID],[EmployeeID],[TicketNum],[DateTimeIssue],[PaidType],[ContractType],[Tax],[TotalS],[TotalP],[TotalG],[TotalE],[ShareOwn],[ShareEmp],[PaidCash],[PaidGift] " +
                   ",[Discount],[ByWho],[TimeSpan],[DriverLicense],[Tips],[TotalT],[TotalC],[PaidCredit] " +
                   ",[PaidCheck],[Voided],[SkipTax],[JackPot],[VNDisComm],[TableDone],[Delivery],[XferTo],[CustomerName] " +
                   ",[TableName],[BarCode],[VoiDelReason],[MemberType],[MealsType],[TabNum],[SeatNum],[ResInt4] " +
                   ",[Tax2],[ProdDiscount],[PaidMember],[CoGDiscount],[CoGDiscountP],[ResDate0],[ResDate1],[ResDate2],[CommPercent],[CommProduct],[TotalO] " +
                   ",[TranID],[DTicketNum],[TakeOut],[TableID],[TabCatID],[TabParties],[SplitCheck] " +
                   ",[CoG],[ServerCard],[IsHLProductSale],[IsHLMemberSale],[LibProduct],[DHLMealCharge] " +
                   ",[DHLCCardCharge],[DHLMCardCharge],[DUpGrade],[OnDay],[OnMonth],[OnYear],[OnDate],[DateKey],[OnWeek],[WeekKey] " +
                   ",[OnBiWeek],[BiWeekKey],[OnSimMonth],[SimMonthKey],[OnQuarter],[QuarterKey],[PaidGCard],[PaidMisc] " +
                   ",[AdminID],[PayrollID],[CoachID],[CoachName],[SaleRepID],[SaleRepName],[ModDate],[CardID],[IsExterm],[EditTimestamp],[RecordState],[EmployeeName]) VALUES " +
                   "(0,@EmployeeID" +
                   ",@TicketNum" +
                   ",@DateTimeIssue" +
                   ",1,1,0,0" +
                   ",@TotalP" +
                   ",@TotalG" +
                   ",@TotalE" +
                   ",@ShareOwn" +
                   ",0,0,0,0,0,0,'',0,0,0,0,0,0,0,0,0,0,0,''" +
                   ",@CustomerName" +//'Hien thi time tren table trong app(CustomerName)'
                   ",@TableName" +
                   ",'','',0,0,0,1,null,0,0,0,0,0,null,null,null,0,0,0,1" +
                   ",@DTicketNum" +
                   ",@TakeOut" +
                   ",@TableID" +
                   ",@TabCatID" +
                   ",1" + //<TabParties, int,>
                   ",0,0,0,0,0,0,0,0,0,0" +
                   ",@OnDay" +
                   ",@OnMonth" +
                   ",@OnYear" +
                   ",@OnDate" +
                   ",@DateKey" +
                   ",@OnWeek" +
                   ",@WeekKey" +
                   ",@OnBiWeek" +
                   ",@BiWeekKey" +
                   ",@OnSimMonth" +
                   ",@SimMonthKey" +
                   ",@OnQuarter" +
                   ",@QuarterKey" +
                   ",0,0,0,0" +
                   ",0,'Unknown',0,'Unknown'" +
                   ",@ModDate" +
                   ",0,0" +
                   ",@EditTimestamp" +
                   ",1,@EmployeeName) ; SELECT SCOPE_IDENTITY()";
                //calc Amount
                double totalE = 0;
                for (int i = 0; i < productList.Rows.Count; i++)
                {
                    double lPrice = Convert.ToDouble(productList.Rows[i]["LPrice"]);
                    double sPrice = Convert.ToDouble(productList.Rows[i]["SPrice"]);
                    double extraPrice = Convert.ToDouble(productList.Rows[i]["Extra"]);
                    bool smallSize = Convert.ToBoolean(productList.Rows[i]["SmallSize"]);
                    int qty = Convert.ToInt32(productList.Rows[i]["Qty"]);
                    //int mType = Convert.ToInt32(productList.Rows[i]["MType"]);
                    totalE = totalE + (extraPrice * qty);
                    if (smallSize)
                    {
                        if (sPrice == 0)
                            amount = amount + (lPrice * qty);
                        else
                            amount = amount + (sPrice * qty);
                    }
                    else
                    {
                        amount += (lPrice * qty);
                    }
                }
                amount += totalE;
                //week of year
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                DateTime onDate = DateTime.Now;
                System.Globalization.Calendar cal = dfi.Calendar;
                int onWeek = cal.GetWeekOfYear(onDate, dfi.CalendarWeekRule,
                                                  dfi.FirstDayOfWeek);
                int onDay = onDate.Day;
                int onMonth = onDate.Month;
                int onYear = onDate.Year;
                string dateKey = onMonth.ToString() + onDay.ToString() + onYear.ToString();
                string weekKey = onWeek.ToString() + onYear.ToString();
                int onBiWeek = onWeek / 2;
                string biWeekKey = onBiWeek.ToString() + onYear.ToString();
                int onSimMonth = onMonth * 2;
                if (onDay <= 15)
                {
                    onSimMonth = onSimMonth - 1;
                }
                string simMonthKey = onSimMonth.ToString() + onYear.ToString();
                int onQuarter = (onMonth + 2) / 3;
                string quarterKey = onQuarter.ToString() + onYear.ToString();



                int ticketID = 0;
                int PhoHa7_ProcTicketsID = 0;
                if (statusTicket == EnumFormStatus.Add)
                {
                    ticketID = Convert.ToInt32(sqlHelper.ExecuteScalar(sqlInsert, CommandType.Text, true,
                        new object[] { "@EmployeeID","@TicketNum", "@DateTimeIssue", "@TotalP", "@TotalG","@TotalE", "@ShareOwn", "@CustomerName", "@TableName", "@DTicketNum","@TakeOut", "@TableID","@TabCatID", "@OnDay", 
            "@OnMonth", "@OnYear","@OnDate", "@DateKey","@OnWeek","@WeekKey","@OnBiWeek","@BiWeekKey","@OnSimMonth","@SimMonthKey","@OnQuarter","@QuarterKey","@ModDate","@EditTimestamp","@EmployeeName" },
                        new object[] {empID, ticketNum, onDate, amount, amount,totalE, amount, customerName,tableName,dTicketNum,ticketTogo,tableID,tabCatID,onDay,
            onMonth,onYear,onDate,dateKey,onWeek,weekKey,onBiWeek,biWeekKey,onSimMonth,simMonthKey,onQuarter,quarterKey,onDate,onDate,empName}));
                    ticketNum = ticketID;
                    count += sqlHelper.ExecuteNonQuery("update ProcTickets set TicketNum = @TicketNum where TicketID = @TicketID", CommandType.Text, true, new object[] { "@TicketNum", "@TicketID" }, new object[] { ticketNum, ticketID });
                    //insert ProcTicket Kitchen [PhoHa7_ProcTickets]
                    PhoHa7_ProcTicketsID = insert_PhoHa7_ProcTickets(customerName, empName, ticketTogo, ticketNum, dTicketNum, tableID, tableName, onDate, ticketID, true,isEmergency);
                }
                else if (statusTicket == EnumFormStatus.Modify)
                {
                    int existPhoHa7_ProcTickets = Convert.ToInt32(sqlHelper.ExecuteScalar("select count(*) from PhoHa7_ProcTickets where TicketID_Root = @TicketID_Root",
                        CommandType.Text, true, new object[] { "@TicketID_Root" }, new object[] { oldTicketID }));
                    if (existPhoHa7_ProcTickets == 0)
                    {
                        PhoHa7_ProcTicketsID = insert_PhoHa7_ProcTickets(customerName, empName, ticketTogo, ticketNum, dTicketNum, tableID, tableName, onDate, oldTicketID, true, isEmergency);
                    }
                    //count += update_ProcTicket(transaction, oldTicketID, amount, customerName, ticketTogo);
                    count += update_ProcTicket(true, oldTicketID, amount, totalE, customerName, empName, ticketTogo, tableID, tableName,isEmergency);
                }

                //get PhoHa7_ProcTickets ID
                //int PhoHa7_ProcTicketsID = Convert.ToInt32(sqlHelper.ExecuteScalar("select TicketID from PhoHa7_ProcTickets where TicketNum=@TicketNum", CommandType.Text, new object[] { "@TicketNum" }, new object[] { ticketNum }));
                ///
                ///insert ProcSaleItem
                ///

                //go each item and insert it
                for (int i = 0; i < productList.Rows.Count; i++)
                {
                    string sqlInsertSaleItem = "INSERT INTO [ProcSaleItem] " +
                        "([TicketID],[ProductID],[TicketNum],[Description],[Type],[Qty],[Price],[TPrice],[Extra]" +
                        ",[ByWho],[ProcDiscount],[TeenDiscount],[SeniorDiscount],[ComProd],[PrintBoth],[SmallSize],[Done]" +
                        ",[ChociceNo],[SizeChoiceTxt],[TranID],[MType],[WorkTimeSec],[ItemPos],[ComTotal],[OrgPrice]" +
                        ",[Cost],[TCost],[CoGDiscount],[CPrice],[SPrice],[TakeOut],[Printed],[PrintStation]" +
                        ",[NotSaleItem],[ItemCode],[KitchenName],[TabParty],[NoTax],[CoG],[Voided],[IsHLProductSale]" +
                        ",[IsHLMemberSale],[MemberType],[MealsType],[RewardPoints],[QBuy],[QFree],[OnDay],[OnMonth]" +
                        ",[OnYear],[OnDate],[DateKey],[OnWeek],[WeekKey],[OnBiWeek],[BiWeekKey],[OnSimMonth]" +
                        ",[SimMonthKey],[OnQuarter],[QuarterKey],[OrgAmount],[TotAmount],[UnitAmount],[EmployeeID],[EmployeeName]" +
                        ",[CoachID],[CoachName],[SaleRepID],[SaleRepName],[PaidType],[QtyAdj],[IsGiftItem],[GiftItems]" +
                        ",[FreeItems],[VendorID],[PAccountType],[PAccountID],[SerialNeeded],[StockHold]" +
                        ",[QuotedItem],[bReserved],[bAccessory],[RecordState],[EditTimestamp],[AdminID],[Weight],[FoodTypeID],[ExtraName],[ExtraPrice],[OptionRequire],[ExtraWith],[ExtraWithout],[CustomSelect]) VALUES (" +
                        "@TicketID" +
                        ",@ProductID" +
                        ",@TicketNum" +
                        ",@Description" +
                        ",1" +
                        ",@Qty" +
                        ",@Price" +
                        ",@TPrice" +
                        ",@Extra" +
                        ",1,0,0,0,0,0" +
                        ",@SmallSize" +
                        ",0,0" +
                        ",'S'" +//@SizeChoiceTxt
                        ",1" +
                        ",@MType" +//@MType, int,>
                        ",0" +
                        ",0" +//@ItemPos, int,>
                        ",0" +
                        ",@OrgPrice" +
                        ",0,0,0" +
                        ",@CPrice" +
                        ",0" +//@SPrice, money,>
                        ",@TakeOut,1" +
                        ",2" +//@PrintStation, int,>
                        ",0" +
                        ",@ItemCode" +
                        ",@KitchenName" +
                        ",0,0,0,0,0,0,0,0,0,0,0" +
                        ",@OnDay" +
                        ",@OnMonth" +
                        ",@OnYear" +
                        ",@OnDate" +
                        ",@DateKey" +
                        ",@OnWeek" +
                        ",@WeekKey" +
                        ",@OnBiWeek" +
                        ",@BiWeekKey" +
                        ",@OnSimMonth" +
                        ",@SimMonthKey" +
                        ",@OnQuarter" +
                        ",@QuarterKey" +
                        ",0,0,0,0" +
                        ",''" +//@EmployeeName, nvarchar(512),>
                        ",0,'Unknown',0,'Unknown',1,1,0,0,0,0,0,0,0,0,0,0,0" +
                        ",1" +//<RecordState, int,>
                        ",@EditTimestamp" +
                        ",0,0,0,@ExtraName,@ExtraPrice,@OptionRequire,@ExtraWith,@ExtraWithout,@CustomSelect) ; SELECT SCOPE_IDENTITY()";

                    int saleItemID = Convert.ToInt32(productList.Rows[i]["SaleItemID"]);
                    int productID = Convert.ToInt32(productList.Rows[i]["ProductID"]);
                    string description = productList.Rows[i]["ProductName"].ToString();
                    int quality = Convert.ToInt32(productList.Rows[i]["Qty"]);
                    double lPrice = Convert.ToDouble(productList.Rows[i]["LPrice"]);
                    double sPrice = Convert.ToDouble(productList.Rows[i]["SPrice"]);
                    double extra = Convert.ToDouble(productList.Rows[i]["Extra"]);
                    bool smallSize = Convert.ToBoolean(productList.Rows[i]["SmallSize"]);
                    bool togo = Convert.ToBoolean(productList.Rows[i]["TakeOut"]);

                    int categoryID = 0;
                    try
                    {
                        categoryID = Convert.ToInt32(productList.Rows[i]["CategoryID"]);
                    }
                    catch
                    {
                        string sql = "select CategoryID from Products where ProductID = @ProductID";
                        categoryID = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, true, new object[] { "@ProductID" }, new object[] { productID }));
                    }

                    //SizeChoiceTxt//
                    string itemCode = productList.Rows[i]["BarCode"].ToString();
                    string kitchenName = productList.Rows[i]["KitchenName"].ToString();
                    string ExtraNameCode = productList.Rows[i]["ExtraName"].ToString();
                    string ExtraPrice = productList.Rows[i]["ExtraPrice"].ToString();
                    string OptionRequire = productList.Rows[i]["OptionRequire"].ToString();
                    string OptionRequireVNese = productList.Rows[i]["OptionRequireVNese"].ToString();
                    string extraWith = productList.Rows[i]["ExtraWith"].ToString();
                    string extraWithout = productList.Rows[i]["ExtraWithout"].ToString();
                    string customSelect = productList.Rows[i]["CustomSelect"].ToString();
                    object temp = productList.Rows[i]["Status"];
                    EnumFormStatus isChange = (EnumFormStatus)productList.Rows[i]["Status"];
                    int mType = Convert.ToInt32(productList.Rows[i]["MType"]);
                    //
                    //modify Description (Name in cashier)
                    if (extra > 0)
                    {
                        string[] arrExtraName = ExtraNameCode.Split('|');
                        if (arrExtraName.Length == 1)
                        {
                            Product product = dao.GetById<Product>(Convert.ToInt32(arrExtraName[0]));
                            description = description + "(" + product.ProductName + ")";
                        }
                        else
                        {
                            for (int k = 0; k < arrExtraName.Length; k++)
                            {
                                Product product = dao.GetById<Product>(Convert.ToInt32(arrExtraName[k]));
                                if (k == 0)
                                {
                                    description += "(";
                                }
                                description += product.ProductName;
                                //description += arrExtraName[k];
                                if (k == (arrExtraName.Length - 2))
                                {
                                    description += ")";
                                }
                                else
                                {
                                    description += ", ";
                                }
                            }
                        }
                    }

                    double price = 0;
                    double tPrice = 0;
                    double orgPrice = 0;
                    double cPrice = 0;
                    //
                    if (mType == 4)
                    {
                        if (smallSize)
                        {
                            price = lPrice;
                            tPrice = (sPrice + extra) * quality;
                            orgPrice = sPrice;
                            cPrice = sPrice;
                        }
                        else
                        {
                            price = lPrice;
                            tPrice = (lPrice + extra) * quality;
                            orgPrice = lPrice;
                            cPrice = sPrice;
                        }
                    }
                    else
                    {
                        price = lPrice;
                        tPrice = (lPrice + extra) * quality;
                        orgPrice = lPrice;
                        cPrice = 0;
                    }
                    if (statusTicket == EnumFormStatus.Add)
                    {
                        int SaleItemID_Root = Convert.ToInt32(sqlHelper.ExecuteScalar(sqlInsertSaleItem, CommandType.Text, true,
                new object[] { "@TicketID", "@ProductID", "@TicketNum", "@Description", "@Qty", "@Price", "@TPrice", "@Extra", 
                           "@SmallSize", "@MType","@OrgPrice", "@CPrice","@TakeOut", "@ItemCode", "@KitchenName", "@OnDay", "@OnMonth", "@OnYear", "@OnDate",
                            "@DateKey","@OnWeek","@WeekKey","@OnBiWeek","@BiWeekKey","@OnSimMonth","@SimMonthKey","@OnQuarter","@QuarterKey","@EditTimestamp",
                            "@ExtraName","@ExtraPrice","@OptionRequire","@ExtraWith","@ExtraWithout","@CustomSelect"},
                new object[] { ticketID, productID, ticketID, description, quality, price,tPrice,extra,
                            smallSize,mType,orgPrice,cPrice,togo,itemCode,kitchenName,onDay,onMonth,onYear,onDate,
                            dateKey,onWeek,weekKey,onBiWeek,biWeekKey,onSimMonth,simMonthKey,onQuarter,quarterKey,onDate,
                            ExtraNameCode,ExtraPrice,OptionRequire,extraWith,extraWithout,customSelect}));
                        //        //
                        //        //Insert to PhoHa7_ProcSaleItem kitchen
                        //        //
                        count += insert_PhoHa7_ProcSaleItem(empName, count, ticketNum, PhoHa7_ProcTicketsID, productID, quality, sPrice, extra,
                            togo, kitchenName, categoryID, itemCode, ExtraNameCode, ExtraPrice, OptionRequireVNese, smallSize, mType, SaleItemID_Root, true, extraWith, extraWithout, customSelect);
                    }
                    else if (statusTicket == EnumFormStatus.Modify)
                    {
                        if (isChange == EnumFormStatus.Add)
                        {
                            int SaleItemID_Root = Convert.ToInt32(sqlHelper.ExecuteScalar(sqlInsertSaleItem, CommandType.Text, true,
                        new object[] { "@TicketID", "@ProductID", "@TicketNum", "@Description", "@Qty", "@Price", "@TPrice", "@Extra", 
               "@SmallSize", "@MType","@OrgPrice", "@CPrice","@TakeOut", "@ItemCode", "@KitchenName", "@OnDay", "@OnMonth", "@OnYear", "@OnDate",
                "@DateKey","@OnWeek","@WeekKey","@OnBiWeek","@BiWeekKey","@OnSimMonth","@SimMonthKey","@OnQuarter","@QuarterKey","@EditTimestamp","@ExtraName","@ExtraPrice","@OptionRequire","@ExtraWith","@ExtraWithout","@CustomSelect"},
                        new object[] { oldTicketID, productID, ticketNum, description, quality, price,tPrice,extra,
                smallSize,mType,orgPrice,cPrice,togo,itemCode,kitchenName,onDay,onMonth,onYear,onDate,
                dateKey,onWeek,weekKey,onBiWeek,biWeekKey,onSimMonth,simMonthKey,onQuarter,quarterKey,onDate,ExtraNameCode,ExtraPrice,OptionRequire,extraWith, extraWithout, customSelect}));
                            //
                            //Insert to PhoHa7_ProcSaleItem kitchen
                            //
                            DataTable oldPhoHa7_ProcTicketID_dt = sqlHelper.ExecuteDataTable("select TicketID,TicketNum from PhoHa7_ProcTickets where TicketID_Root = @TicketID_Root",
                                                                            CommandType.Text, true, new object[] { "@TicketID_Root" }, new object[] { oldTicketID });
                            int oldPhoHa7_ProcTicketID = Convert.ToInt32(oldPhoHa7_ProcTicketID_dt.Rows[0]["TicketID"]);
                            int oldPhoHa7_TicketNum = Convert.ToInt32(oldPhoHa7_ProcTicketID_dt.Rows[0]["TicketNum"]);
                            count += insert_PhoHa7_ProcSaleItem(empName, count, oldPhoHa7_TicketNum, oldPhoHa7_ProcTicketID, productID, quality, sPrice, extra,
                                togo, kitchenName, categoryID, itemCode, ExtraNameCode, ExtraPrice, OptionRequireVNese, smallSize, mType, SaleItemID_Root, true, extraWith, extraWithout, customSelect);
                        }
                        else if (isChange == EnumFormStatus.Modify)
                        {
                            count += update_ProcSaleItem(true, saleItemID, quality, extra, smallSize, price, tPrice, orgPrice, cPrice, togo, ExtraNameCode, ExtraPrice, OptionRequire, extraWith, extraWithout, customSelect);
                            count += update_PhoHa7_ProcSaleItem(true, saleItemID, quality, empName, togo, ExtraNameCode, OptionRequireVNese, smallSize, extraWith, extraWithout, customSelect, oldTicketID, isEmergency);
                        }
                    }
                }
                //cancel item
                if (statusTicket == EnumFormStatus.Modify)
                {
                    if (productListDel != null)
                    {
                        for (int j = 0; j < productListDel.Rows.Count; j++)
                        {
                            int saleItemIDDelete = Convert.ToInt32(productListDel.Rows[j]["SaleItemID"]);
                            string description = productListDel.Rows[j]["ProductName"].ToString();
                            count += delete_ProcSaleItem(true, saleItemIDDelete, description, empName);
                            count += delete_PhoHa7_ProcSaleItem(true, saleItemIDDelete, oldTicketID);
                        }

                    }
                }
                sqlHelper.CommitTransaction();

            }
            catch (Exception ex)
            {
                count = 0;
                ExceptionUtility.LogException(ex, "ProcTickets");
                try
                {
                    sqlHelper.RollBack();
                }
                catch (Exception exc)
                {
                    ExceptionUtility.LogException(exc, "ProcTickets");
                }
            }
        }
        catch (System.Exception ex)
        {
            count = 0;
            ExceptionUtility.LogException(ex, "ProcTickets");
        }
        return count;
    }

    private int insert_PhoHa7_ProcSaleItem(string empName, int count, int ticketNum, int ticketID, int productID, int quality, double sPrice, double extra,
        bool togo, string kitchenName, int categoryID, string barCode, string extraName, string extraPrice, string optionRequire, bool isSmall, int mType, int SaleItemID_Root, bool hasTransaction,
        string extraWith, string extraWithout, string customSelect)
    {
        Dao dao = new Dao();
        //extra with
        string[] arrExtraWith = extraWith.ToString().Split('|');
        for (int i = 0; i < arrExtraWith.Length; i++)
        {
            Product pro = dao.GetById<Product>(Convert.ToInt32(arrExtraWith[i] == string.Empty ? "0" : arrExtraWith[i]));
            if (pro != null)
            {
                string nameExtraWith = pro.KitchenName;
                extraWith = extraWith.Replace(arrExtraWith[i], nameExtraWith);
            }
        }
        //extra without
        string[] arrExtraWithout = extraWithout.ToString().Split('|');
        for (int i = 0; i < arrExtraWithout.Length; i++)
        {
            Product pro = dao.GetById<Product>(Convert.ToInt32(arrExtraWithout[i] == string.Empty ? "0" : arrExtraWithout[i]));
            if (pro != null)
            {
                string nameExtraWithout = pro.KitchenName;
                extraWithout = extraWithout.Replace(arrExtraWithout[i], nameExtraWithout);
            }

        }
        //custom
        string[] arrCustomSelect = customSelect.ToString().Split('|');
        for (int i = 0; i < arrCustomSelect.Length; i++)
        {
            Product pro = dao.GetById<Product>(Convert.ToInt32(arrCustomSelect[i] == string.Empty ? "0" : arrCustomSelect[i]));
            if (pro != null)
            {
                string nameCustomSelect = pro.KitchenName;
                customSelect = customSelect.Replace(arrCustomSelect[i], nameCustomSelect);
            }
        }
        //extra name
        string[] arrExtraNameCode = extraName.ToString().Split('|');
        for (int i = 0; i < arrExtraNameCode.Length; i++)
        {
            Product pro = dao.GetById<Product>(Convert.ToInt32(arrExtraNameCode[i] == string.Empty ? "0" : arrExtraNameCode[i]));
            if (pro != null)
            {
                string nameExtraName = pro.KitchenName;
                extraName = extraName.Replace(arrExtraNameCode[i], nameExtraName);
            }
        }

        string sqlInsert_PhoHa7_ProcSaleItem = "INSERT INTO [PhoHa7_ProcSaleItem] " +
       "([TicketID],[ProductID],[TicketNum],[Description],[Qty],[Price],[Extra],[Employee],[TakeOut],[Category],[BarCode],[ExtraName],[ExtraPrice],[OptionRequire],[IsSmall],[MType],[SaleItemID_Root],[ExtraWith],[ExtraWithout],[CustomSelect]) VALUES" +
       "(@TicketID,@ProductID,@TicketNum,@Description,@Qty,@Price,@Extra,@Employee,@TakeOut,@Category,@BarCode,@ExtraName,@ExtraPrice,@OptionRequire,@IsSmall,@MType,@SaleItemID_Root,@ExtraWith,@ExtraWithout,@CustomSelect)";
        count += sqlHelper.ExecuteNonQuery(sqlInsert_PhoHa7_ProcSaleItem, CommandType.Text, true,
        new object[] { "@TicketID", "@ProductID", "@TicketNum", "@Description", "@Qty", "@Price", "@Extra", "@Employee", "@TakeOut", "@Category", "@BarCode", "@ExtraName", "@ExtraPrice", "@OptionRequire", "@IsSmall", "@MType", "@SaleItemID_Root", "@ExtraWith", "@ExtraWithout", "@CustomSelect" },
        new object[] { ticketID, productID, ticketNum, kitchenName, quality, sPrice, extra, empName, togo, categoryID, barCode, extraName, extraPrice, optionRequire, isSmall, mType, SaleItemID_Root, extraWith, extraWithout, customSelect });
        return count;
    }

    private int insert_PhoHa7_ProcTickets(string customerName, string empName, bool ticketTogo, int ticketNum, int dTicketNum,
        int tableID, string tableName, DateTime onDate, int TicketID_Root, bool pTransaction, bool isEmergency)
    {
        string sqlPhoHa7_ProcTickets = "INSERT INTO [PhoHa7_ProcTickets] " +
            "([CustomerName],[EmployeeName],[TicketNum]" +
           ",[DateTimeIssue],[Tax],[Total],[TableName],[DTicketNum]" +
           ",[TakeOut],[TableID],[TicketID_Root],Emergency) VALUES" +
           "(@CustomerName" +
           ",@EmployeeName" +
           ",@TicketNum" +
           ",@DateTimeIssue" +
           ",0,0" +
           ",@TableName" +
           ",@DTicketNum" +
           ",@TakeOut,@TableID,@TicketID_Root,@Emergency) ; SELECT SCOPE_IDENTITY()";
        int id = Convert.ToInt32(sqlHelper.ExecuteScalar(sqlPhoHa7_ProcTickets, CommandType.Text, pTransaction,
            new object[] { "@CustomerName", "@EmployeeName", "@TicketNum", "@DateTimeIssue", "@TableName", "@DTicketNum", "@TakeOut", "@TableID", "@TicketID_Root", "@Emergency" },
            new object[] { customerName, empName, ticketNum, onDate, tableName, dTicketNum, ticketTogo, tableID, TicketID_Root, isEmergency }));
        return id;
    }



    private int update_ProcTicket(bool transaction, int newTicketID, double amount, double amountE, string customerName, string empName, bool takeOut, 
        int tableID, string tableName,bool isEmergency)
    {
        int count = 0;
        //find table is combined
        string sql = "select TicketID, TotalP,TotalE, DTicketNum from ProcTickets where TableID = @TableID and TicketID != @TicketID";
        DataTable result = sqlHelper.ExecuteDataTable(sql, CommandType.Text, transaction, new object[] { "@TableID", "@TicketID" }, new object[] { tableID, newTicketID });


        if (result.Rows.Count == 1)
        {
            int oldTicketID = Convert.ToInt32(result.Rows[0]["TicketID"]);
            //************************combine table****************************
            //update ticketID in ProcSaleItem
            sql = "UPDATE ProcSaleItem SET TicketID = @TicketID where TicketID = @OldTicketID";
            count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID", "@OldTicketID" }, new object[] { newTicketID, oldTicketID });
            //update phoha7_ProcSaleItem
            //sql = "select TicketID from PhoHa7_ProcTickets where TicketID_Root = @TicketID_Root";
            //int phoha7_proctickets_ticketID = Convert.ToInt32(sqlHelper.ExecuteScalar(sql, CommandType.Text, transaction, new object[] { "@TicketID_Root" }, new object[] { oldTicketID }));
            //sql = "update PhoHa7_ProcSaleItem set TicketID = @TicketID where TicketID = (select TicketID from PhoHa7_ProcTickets where TicketID_Root = @TicketID_Root)";
            //count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID", "@TicketID_Root" }, new object[] { newTicketID, phoha7_proctickets_ticketID });

            //delete table in proctickets
            sql = "delete from ProcTickets where TicketID = @TicketID";
            sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID" }, new object[] { oldTicketID });


            //delete table in Phoha7_ProcTickets
            //sql = "delete from PhoHa7_ProcTickets where TicketID_Root = @TicketID_Root";
            //sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID_Root" }, new object[] { oldTicketID });
            //update table in Phoha7_ProcTickets
            sql = "update [PhoHa7_ProcTickets] SET [ToKitchen] = 0,[CustomerName]=@CustomerName, [EmployeeName] = @EmployeeName, " +
        "[TableName]=@TableName, [TakeOut] = @TakeOut, [TableID] = @TableID, [Emergency]=@Emergency where TicketID_Root = @TicketID_Root";
            count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction,
                new object[] { "@CustomerName", "@EmployeeName", "@TableName", "@TakeOut", "@TableID","@Emergency", "@TicketID_Root" },
                new object[] { customerName, empName, tableName, takeOut, tableID,isEmergency, oldTicketID });

            //

            sql = "update [PhoHa7_ProcTickets] SET [ToKitchen] = 0,[CustomerName]=@CustomerName, [EmployeeName] = @EmployeeName, " +
        "[TableName]=@TableName, [TakeOut] = @TakeOut, [TableID] = @TableID,[DTicketNum]=(select DTicketNum from PhoHa7_ProcTickets where TicketID_Root=@TicketID_Root1), TicketID_Root = @TicketID_RootNew where TicketID_Root = @TicketID_Root";
            count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction,
                new object[] { "@CustomerName", "@EmployeeName", "@TableName", "@TakeOut", "@TableID", "@TicketID_Root1", "@TicketID_RootNew", "@TicketID_Root" },
                new object[] { customerName, empName, tableName, takeOut, tableID, newTicketID, newTicketID, oldTicketID });
        }
        //update table
        sql = "UPDATE [ProcTickets] SET [CustomerName] = @CustomerName, [TakeOut] = @TakeOut, " +
           " [TotalP] = (select sum(TPrice*Qty) from ProcSaleItem where TicketID = @TicketIDP), " +
           " [TotalG] = (select sum(TPrice*Qty) from ProcSaleItem where TicketID = @TicketIDG) , " +
           " [TotalE] = (select sum(Extra*Qty) from ProcSaleItem where TicketID = @TicketIDE), " +
           " [ShareOwn] = (select sum(TPrice*Qty) from ProcSaleItem where TicketID = @TicketIDS) , [EditTimestamp] = @EditTimestamp, [TableID] = @TableID, [TableName] = @TableName where TicketID = @TicketID";
        count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction,
            new object[] { "@CustomerName", "@TakeOut", "@TicketIDP", "@TicketIDG", "@TicketIDE", "@TicketIDS", "@EditTimestamp", "@TableID", "@TableName", "@TicketID" },
            new object[] { customerName, takeOut, newTicketID, newTicketID, newTicketID, newTicketID, DateTime.Now, tableID, tableName, newTicketID });


        sql = "update [PhoHa7_ProcTickets] SET [ToKitchen] = 0,[CustomerName]=@CustomerName, [EmployeeName] = @EmployeeName, " +
        "[TableName]=@TableName, [TakeOut] = @TakeOut, [TableID] = @TableID,[Emergency]=@Emergency where TicketID_Root = @TicketID_Root";
        count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction,
            new object[] { "@CustomerName", "@EmployeeName", "@TableName", "@TakeOut", "@TableID", "@Emergency", "@TicketID_Root" },
            new object[] { customerName, empName, tableName, takeOut, tableID,isEmergency, newTicketID });
        return count;
    }

    private int update_ProcSaleItem(bool transaction, int SaleItemID, int qty, double extra, bool smallSize, double price, double tPrice, double orgPrice, double cPrice, bool takeOut,
        string extraname, string extraPrice, string option, string extraWith, string extraWithout, string customSelect)
    {
        int count = 0;
        string sql = "UPDATE [ProcSaleItem] SET [Qty] = @Qty, [Extra] = @Extra, [SmallSize] = @SmallSize , [Price]=@Price, [TPrice]=@TPrice, [OrgPrice] = @OrgPrice , [CPrice] = @CPrice ," +
            "        [TakeOut] = @TakeOut, [ExtraName]=@ExtraName , [ExtraPrice] = @ExtraPrice , [OptionRequire] = @OptionRequire, " +
            " [ExtraWith]=@ExtraWith, [ExtraWithout]=@ExtraWithout,[CustomSelect]=@CustomSelect where SaleItemID = @SaleItemID";
        count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction,
            new object[] { "@Qty", "@Extra", "@SmallSize", "@Price", "@TPrice", "@OrgPrice", "@CPrice", "@TakeOut", "@ExtraName", "@ExtraPrice", "@OptionRequire", "@ExtraWith", "@ExtraWithout", "@CustomSelect", "@SaleItemID" },
            new object[] { qty, extra, smallSize, price, tPrice, orgPrice, cPrice, takeOut, extraname, extraPrice, option, extraWith, extraWithout, customSelect, SaleItemID });
        return count;
    }

    private int delete_ProcSaleItem(bool transaction, int SaleItemID, string description, string emp)
    {
        int count = 0;
        string name = "Deleted by " + emp + "- " + description;
        string sql = "UPDATE [ProcSaleItem] SET [Description] = @Description,[KitchenName]=@KitchenName, [Extra] = 0, [Price] = 0, [TPrice] = 0 , [OrgPrice] = 0, [CPrice] = 0, ExtraPrice = 0 where SaleItemID = @SaleItemID";
        count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction,
            new object[] { "@Description", "@KitchenName", "@SaleItemID" },
            new object[] { name, name, SaleItemID });
        return count;
    }

    private int update_PhoHa7_ProcSaleItem(bool transaction, int saleItemID, int qty, string emp, bool takeOut,
        string extraName, string option, bool isSmall, string extraWith, string extraWithout, string customSelect, int TicketID_Root, bool isEmergency)
    {
        Dao dao = new Dao();
        //extra with
        string[] arrExtraWith = extraWith.ToString().Split('|');
        for (int i = 0; i < arrExtraWith.Length; i++)
        {
            Product pro = dao.GetById<Product>(Convert.ToInt32(arrExtraWith[i] == string.Empty ? "0" : arrExtraWith[i]));
            if (pro != null)
            {
                string nameExtraWith = pro.KitchenName;
                extraWith = extraWith.Replace(arrExtraWith[i], nameExtraWith);
            }

        }
        //extra without
        string[] arrExtraWithout = extraWithout.ToString().Split('|');
        for (int i = 0; i < arrExtraWithout.Length; i++)
        {
            Product pro = dao.GetById<Product>(Convert.ToInt32(arrExtraWithout[i] == string.Empty ? "0" : arrExtraWithout[i]));
            if (pro != null)
            {
                string nameExtraWithout = pro.KitchenName;
                extraWithout = extraWithout.Replace(arrExtraWithout[i], nameExtraWithout);
            }

        }
        //custom
        string[] arrCustomSelect = customSelect.ToString().Split('|');
        for (int i = 0; i < arrCustomSelect.Length; i++)
        {
            Product pro = dao.GetById<Product>(Convert.ToInt32(arrCustomSelect[i] == string.Empty ? "0" : arrCustomSelect[i]));
            if (pro != null)
            {
                string nameCustomSelect = pro.KitchenName;
                customSelect = customSelect.Replace(arrCustomSelect[i], nameCustomSelect);
            }

        }
        //extra name
        string[] arrExtraNameCode = extraName.ToString().Split('|');
        for (int i = 0; i < arrExtraNameCode.Length; i++)
        {
            Product pro = dao.GetById<Product>(Convert.ToInt32(arrExtraNameCode[i] == string.Empty ? "0" : arrExtraNameCode[i]));
            if (pro != null)
            {
                string nameExtraName = pro.KitchenName;
                extraName = extraName.Replace(arrExtraNameCode[i], nameExtraName);
            }
        }

        int count = 0;
        string sql = "UPDATE [PhoHa7_ProcTickets] set [ToKitchen] = 0,[IsChange] = 1, [Emergency] = @Emergency where [TicketID_Root] = @TicketID_Root";
        count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID_Root", "@Emergency" }, new object[] { TicketID_Root,isEmergency });

        sql = "UPDATE [PhoHa7_ProcSaleItem] SET [Qty] = @Qty ,[Employee] = @Employee, [ToKitchen] = 0,[TakeOut] = @TakeOut," +
        "[IsChange] = 1, [ExtraName] = @ExtraName, [OptionRequire] = @OptionRequire, [IsSmall] = @IsSmall, [ExtraWith]=@ExtraWith , [ExtraWithout]=@ExtraWithout , [CustomSelect]=@CustomSelect WHERE SaleItemID_Root = @SaleItemID";
        count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction,
            new object[] { "@Qty", "@Employee", "@TakeOut", "@ExtraName", "@OptionRequire", "@IsSmall", "@ExtraWith", "@ExtraWithout", "@CustomSelect", "@SaleItemID" },
            new object[] { qty, emp, takeOut, extraName, option, isSmall, extraWith, extraWithout, customSelect, saleItemID });
        return count;
    }

    private int delete_PhoHa7_ProcSaleItem(bool transaction, int SaleItemID_Root, int TicketID_Root)
    {
        int count = 0;

        string sql = "UPDATE [PhoHa7_ProcTickets] set [ToKitchen] = 0, [IsChange] = 1 where [TicketID_Root] = @TicketID_Root";
        count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction, new object[] { "@TicketID_Root" }, new object[] { TicketID_Root });

        sql = "UPDATE [PhoHa7_ProcSaleItem] SET [ToKitchen] = 0,[Cancel] = 1 where SaleItemID_Root = @SaleItemID_Root";
        count += sqlHelper.ExecuteNonQuery(sql, CommandType.Text, transaction,
            new object[] { "@SaleItemID_Root" },
            new object[] { SaleItemID_Root });
        return count;
    }

}