using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PhoHa7.Library.Classes.Connection;
using System.Data;
using PhoMac.Model.Data;
using PhoMac.Model;

namespace PhoMac.Main.POS.Data
{
    public static class ClsPublic
    {
        //public static int MAX_NUMBER_TICKET = 40;

        //public static string KITCHEN_TYPE
        //{
        //    get
        //    {
        //        string kitchenType = global::PhoMac.Main.Properties.Settings.Default.KitchenType;
        //        if (kitchenType == null)
        //        {
        //            kitchenType = "1";
        //        }
        //        return kitchenType;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.KitchenType = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static int TIMER_ORDER_COUNT_DOWN
        //{
        //    get
        //    {
        //        int time = global::PhoMac.Main.Properties.Settings.Default.TimeCounter;
        //        if (time == null || time == 0)
        //        {
        //            time = 15;
        //        }
        //        return time;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.TimeCounter = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static Color ColorLetterToGo
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.ColorDisplayToGo;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.ColorDisplayToGo = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static Color ColorBackgroundToGo
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.ColorBackgroundToGo;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.ColorBackgroundToGo = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static Color ColorLetterItemChange
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.ColorItemChange;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.ColorItemChange = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static Color ColorBackgroundItemChange
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.ColorBackgroundItemChange;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.ColorBackgroundItemChange = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static int PaddingSizePrint
        //{
        //    get
        //    {
        //        int padding = global::PhoMac.Main.Properties.Settings.Default.PaddingSizePrint;
        //        if (padding == null || padding == 0)
        //        {
        //            padding = 15;
        //        }
        //        return padding;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.PaddingSizePrint = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static int FontSizePrint
        //{
        //    get
        //    {
        //        int size = global::PhoMac.Main.Properties.Settings.Default.FontSizePrint;
        //        if (size == null || size == 0)
        //        {
        //            size = 15;
        //        }
        //        return size;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.FontSizePrint = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static string WebsiteLocation
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.WebSiteLocation; ;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.WebSiteLocation = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static string ImageURL
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.ImagePath;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.ImagePath = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static bool AllowMultiInstance
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.AllowMultiInstance;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.AllowMultiInstance = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static bool AutoPrint
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.AutoPrint;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.AutoPrint = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static int AutoPrintAfterSecond
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.AutoPrintAfterSecond;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.AutoPrintAfterSecond = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static int NumberOfReprint
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.NumberOfReprint;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.NumberOfReprint = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //static List<TicketPrintInfo> listPrintItem = new List<TicketPrintInfo>();
        //public static List<TicketPrintInfo> ListPrintItem
        //{
        //    get { return listPrintItem; }
        //    set { listPrintItem = value; }
        //}

        //static int printPosition = 0;
        //public static int PrintPosition
        //{
        //    get
        //    {
        //        return printPosition;
        //    }
        //    set
        //    {
        //        printPosition = value;
        //    }
        //}

        //public static string Printers
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.Printers;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.Printers = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static bool PrintBarCode
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.PrintBarCode;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.PrintBarCode = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static int NumOfGroupTable
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.NumOfGroupTable;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.NumOfGroupTable = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static int VerticalSplitGroupTablePosition
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.VerticalSplitGroupTablePosition;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.VerticalSplitGroupTablePosition = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static int HorizontalSplitGroupTablePosition
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.HorizontalSplitGroupTablePosition;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.HorizontalSplitGroupTablePosition = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static bool ShowGroupTable
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.ShowGroupTable;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.ShowGroupTable = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static bool ShowFilterTable
        //{
        //    get
        //    {
        //        return global::PhoMac.Main.Properties.Settings.Default.ShowFilterTable;
        //    }
        //    set
        //    {
        //        global::PhoMac.Main.Properties.Settings.Default.ShowFilterTable = value;
        //        global::PhoMac.Main.Properties.Settings.Default.Save();
        //    }
        //}

        //public static void addListPrintItem(TicketPrintInfo source)
        //{
        //    TicketPrintInfo des = new TicketPrintInfo();
        //    des.TableName = source.TableName;
        //    des.DTicketNum = source.DTicketNum;
        //    des.TicketTakeOut = source.TicketTakeOut;
        //    des.EmpName = source.EmpName;
        //    des.Time = DateTime.Now;
        //    des.CountItemDonePrint = source.CountItemDonePrint;
        //    des.CountTotalItemPrint = source.CountTotalItemPrint;
        //    des.ListProduct = new List<Product>();
        //    foreach (Product p in source.ListProduct)
        //    {
        //        Product product = new Product();
        //        product.PrintName = p.PrintName;
        //        product.PrintFrontStyle = p.PrintFrontStyle;
        //        des.ListProduct.Add(product);
        //    }
        //    if (ListPrintItem.Count < NumberOfReprint)
        //    {
        //        ListPrintItem.Add(des);
        //        PrintPosition = ListPrintItem.Count - 1;
        //    }
        //    else
        //    {
        //        ListPrintItem.Add(des);
        //        ListPrintItem.RemoveAt(0);
        //        PrintPosition = ListPrintItem.Count - 1;
        //    }
        //}

        //public static int WriteException(Exception ex)
        //{
        //    SqlHelper sqlHelper = new SqlHelper();
        //    string sql = "INSERT INTO [Error]([ErrorText],[ErrorDetails],[ErrorDate])" +
        //                    " VALUES (@ErrorText,@ErrorDetails,@ErrorDate)";
        //    int count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@ErrorText", "@ErrorDetails", "@ErrorDate" },
        //        new object[] { ex.Message, ex.ToString(), DateTime.Now });
        //    return count;

        //}

        public static int WriteException(Exception ex)
        {
            SqlHelper sqlHelper = new SqlHelper();
            Dao dao = new Dao();
            Error er = new Error();
            er.ErrorText = ex.Message;
            er.ErrorDetails = ex.ToString();
            er.ErrorDate = DateTime.Now;
            er.ErrorType = ex.InnerException == null ? "" : ex.InnerException.GetType().ToString();
            er.InnerException = ex.InnerException == null ? "" : ex.InnerException.Message;
            er.InnerSource = ex.InnerException == null ? "" : ex.InnerException.Source;
            er.InnerExceptionStackTrace = ex.InnerException == null ? "" : ex.InnerException.StackTrace + string.Empty;

            int count = dao.Add<Error>(er);
            //string sql = "INSERT INTO [Error]([ErrorText],[ErrorDetails],[ErrorDate],[ErrorType],[InnerException],[InnerSource],[InnerExceptionStackTrace])" +
            //                " VALUES (@ErrorText,@ErrorDetails,@ErrorDate,@ErrorType,@InnerException,@InnerSource,@InnerExceptionStackTrace)";
            //int count = sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@ErrorText", "@ErrorDetails", "@ErrorDate", "@ErrorType", "@InnerException", "@InnerSource", "@InnerExceptionStackTrace" },
            //    new object[] { ex.Message, ex.ToString(), DateTime.Now, ex.InnerException.GetType().ToString(), ex.InnerException.Message, ex.InnerException.Source, ex.InnerException.StackTrace + string.Empty });
            return count;

        }

        static Dictionary<string,PhoHa7_Sys_Option> _listSystemOption = new Dictionary<string,PhoHa7_Sys_Option>();
        public static Dictionary<string,PhoHa7_Sys_Option> ListSystemOption
        {
            get
            {
                if (_listSystemOption == null || _listSystemOption.Count==0)
                {
                    Dao dao = new Dao();
                  var  obj = dao.GetAll<PhoHa7_Sys_Option>().ToList();
                    foreach (var item in obj)
                    {
                        _listSystemOption.Add(item.Opt_ID,item);
                    }
                    return _listSystemOption;
                }
                else
                {
                    return _listSystemOption;
                }
            }
        }

    }
}
