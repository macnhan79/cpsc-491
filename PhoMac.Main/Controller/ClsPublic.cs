using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PhoHa7.Library.Classes.Connection;
using System.Data;
using PhoMac.Main.Entities;
using PhoMac.Model.Data;
using PhoMac.Model;
using PhoMac.Main.Controller;
using PhoMac.Model.Factory;
using System.Data.Entity;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading;
using SocketServerApp;

namespace PhoMac.Main
{
    public static class ClsPublic
    {
        #region Machine Info
        // public static MachinePresenter machinePresneter = new MachinePresenter();
        // public static PhoHa7_Machine machineInfo = machinePresneter.getMachineInfo();
        //public static PhoHa7_Machine machineInfo = new PhoHa7_Machine();
        public static SocketServer socketServer = new SocketServer();

        public static string KITCHEN_TYPE
        {
            //get
            //{
            //    string kitchenType = machineInfo.MachineType;
            //    if (kitchenType == null)
            //    {
            //        kitchenType = "1";
            //    }
            //    return kitchenType;
            //}
            //set
            //{
            //    machineInfo.MachineType = value;
            //    //machinePresneter.update(machineInfo);
            //}
            get
            {
                string kitchenType = global::PhoMac.Main.Properties.Settings.Default.KitchenType;
                if (kitchenType == null)
                {
                    kitchenType = "1";
                }
                return kitchenType;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.KitchenType = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static int TIMER_ORDER_COUNT_DOWN
        {
            get
            {
                int time = global::PhoMac.Main.Properties.Settings.Default.TimeCounter;
                //int time = Convert.ToInt32(machineInfo.Timer);
                //if (time == 0)
                //{
                //    time = 15;
                //}
                return time;
            }
            set
            {
                //machineInfo.Timer = value;
                //machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.TimeCounter = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static Color ColorLetterToGo
        {
            get
            {
                //Color color;
                //string strColor = machineInfo.ForceColorTogo + string.Empty;
                //if (strColor == string.Empty)
                //{
                //    color = Color.FromArgb(255, 128, 0);
                //}
                //else
                //{
                //    color = Color.FromArgb(Convert.ToInt32(strColor));
                //}
                //return color;
                return global::PhoMac.Main.Properties.Settings.Default.ForceColorToGo;
            }
            set
            {
                //machineInfo.ForceColorTogo = value.ToArgb().ToString();
                // machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.ForceColorToGo = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static Color ColorBackgroundToGo
        {
            get
            {
                //Color color;
                //string strColor = machineInfo.BackgroundColorToGo + string.Empty;
                //if (strColor == string.Empty)
                //{
                //    color = Color.FromArgb(0, 0, 192);
                //}
                //else
                //{
                //    color = Color.FromArgb(Convert.ToInt32(strColor));
                //}
                //return color;
                return global::PhoMac.Main.Properties.Settings.Default.ColorBackgroundToGo;
            }
            set
            {
                //machineInfo.BackgroundColorToGo = value.ToArgb().ToString();
                //machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.ColorBackgroundToGo = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static Color ColorLetterItemChange
        {
            get
            {
                //Color color;
                //string strColor = machineInfo.ForceColorChange + string.Empty;
                //if (strColor == string.Empty)
                //{
                //    color = Color.Black;
                //}
                //else
                //{
                //    color = Color.FromArgb(Convert.ToInt32(strColor));
                //}
                //return color;
                return global::PhoMac.Main.Properties.Settings.Default.ColorItemChange;
            }
            set
            {
                //machineInfo.ForceColorChange = value.ToArgb().ToString();
                // machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.ColorItemChange = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static Color ColorBackgroundItemChange
        {
            get
            {
                //Color color;
                //string strColor = machineInfo.BackgroundColorChange + string.Empty;
                //if (strColor == string.Empty)
                //{
                //    color = Color.FromArgb(0, 192, 0);
                //}
                //else
                //{
                //    color = Color.FromArgb(Convert.ToInt32(strColor));
                //}
                //return color;
                return global::PhoMac.Main.Properties.Settings.Default.ColorBackgroundItemChange;
            }
            set
            {
                // machineInfo.BackgroundColorChange = value.ToArgb().ToString();
                // machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.ColorBackgroundItemChange = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static int PaddingSizePrint
        {
            get
            {
                int padding = global::PhoMac.Main.Properties.Settings.Default.PaddingSizePrint;
                //int padding = Convert.ToInt32(machineInfo.PaddingSizeHeader);
                //if (padding == null || padding == 0)
                //{
                //    padding = 40;
                //}
                return padding;
            }
            set
            {
                // machineInfo.PaddingSizeHeader = value;
                //  machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.PaddingSizePrint = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static int FontSizePrint
        {
            get
            {
                int size = global::PhoMac.Main.Properties.Settings.Default.FontSizePrint;
                //int size = Convert.ToInt32(machineInfo.FontSizePrint);
                //if (size == null || size == 0)
                //{
                //    size = 18;
                //}
                return size;
            }
            set
            {
                // machineInfo.FontSizePrint = value;
                //  machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.FontSizePrint = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static bool AllowMultiInstance
        {
            get
            {
                //bool allow = Convert.ToBoolean(machineInfo.MultiInstance);
                //return allow;
                return global::PhoMac.Main.Properties.Settings.Default.AllowMultiInstance;
            }
            set
            {
                // machineInfo.MultiInstance = value;
                //machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.AllowMultiInstance = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static bool AutoCompleteAndPrint
        {
            get
            {
                //bool allow = Convert.ToBoolean(machineInfo.AutoPrinter);
                //return allow;
                return global::PhoMac.Main.Properties.Settings.Default.AutoCompleteAndPrint;
            }
            set
            {
                // machineInfo.AutoPrinter = value;
                //   machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.AutoCompleteAndPrint = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static bool AutoCompleteAndPrintSpecialItem
        {
            get
            {
                //bool allow = Convert.ToBoolean(machineInfo.AutoPrinter);
                //return allow;
                return global::PhoMac.Main.Properties.Settings.Default.AutoCompleteAndPrintSpecialItem;
            }
            set
            {
                // machineInfo.AutoPrinter = value;
                //   machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.AutoCompleteAndPrintSpecialItem = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }


        public static int AutoPrintAfterSecond
        {
            get
            {
                //int allow = Convert.ToInt32(machineInfo.AutoPrinterAfter);
                //if (allow == 0)
                //{
                //    allow = 3;
                //}
                //return allow;
                return global::PhoMac.Main.Properties.Settings.Default.AutoPrintAfterSecond;
            }
            set
            {
                //machineInfo.AutoPrinterAfter = value;
                //  machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.AutoPrintAfterSecond = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static string Printers
        {
            get
            {
                //string name = machineInfo.PrinterName + string.Empty;
                //return name;
                return global::PhoMac.Main.Properties.Settings.Default.Printers;
            }
            set
            {
                // machineInfo.PrinterName = value;
                //   machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.Printers = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static bool PrintBarCode
        {
            get
            {
                //  bool allow = Convert.ToBoolean(machineInfo.PrinterBarcode);
                // return allow;
                return global::PhoMac.Main.Properties.Settings.Default.PrintBarCode;
            }
            set
            {
                //machineInfo.PrinterBarcode = value;
                //    machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.PrintBarCode = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static int NumOfGroupTable
        {
            get
            {
                //int num = Convert.ToInt32(machineInfo.GroupTable);
                //if (num == 0)
                //{
                //    num = 4;
                //}
                //return num;
                return global::PhoMac.Main.Properties.Settings.Default.NumOfGroupTable;
            }
            set
            {
                //machineInfo.GroupTable = value;
                //   machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.NumOfGroupTable = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static bool ShowGroupTable
        {
            get
            {
                //bool allow = Convert.ToBoolean(machineInfo.ShowGroup);
                //return allow;
                return global::PhoMac.Main.Properties.Settings.Default.ShowGroupTable;
            }
            set
            {
                //machineInfo.ShowGroup = value;
                //  machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.ShowGroupTable = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static bool ShowFilterTable
        {
            get
            {
                //bool allow = Convert.ToBoolean(machineInfo.ShowFilterGroup);
                //return allow;
                return global::PhoMac.Main.Properties.Settings.Default.ShowFilterTable;
            }
            set
            {
                //machineInfo.ShowFilterGroup = value;
                //    machinePresneter.update(machineInfo);
                global::PhoMac.Main.Properties.Settings.Default.ShowFilterTable = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static string SoundUrl
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.SoundUrl;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.SoundUrl = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }
        public static int TicketWidthSize
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.TicketWidthSize;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.TicketWidthSize = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }
        public static int TicketHeightSize
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.TicketHeightSize;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.TicketHeightSize = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }
        public static int TicketFontSize
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.TicketFontSize;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.TicketFontSize = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }
        public static Color ForceColor
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.ForceColor;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.ForceColor = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }
        public static Color ForceColor1
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.ForceColor1;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.ForceColor1 = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static Color BackgroundColorEmergency
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.BackgroundColorEmergency;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.BackgroundColorEmergency = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static Color BackgrdColorQty
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.BackgrdColorQty;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.BackgrdColorQty = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static Color ForceColorQty
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.ForceColorQty;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.ForceColorQty = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static Color ForceColorQty1
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.ForceColorQty1;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.ForceColorQty1 = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static int FontSizeQty
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.FontSizeQty;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.FontSizeQty = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }



        public static Color ForceColorLageSize
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.ForceColorLageSize;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.ForceColorLageSize = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static string SizeSmall
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.SizeSmall;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.SizeSmall = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static string SizeLarge
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.SizeLarge;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.SizeLarge = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }



        public static int MAX_NUMBER_TICKET = 40;

        public static string WebsiteLocation
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.WebSiteLocation; ;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.WebSiteLocation = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static string ImageURL
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.ImagePath;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.ImagePath = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static int VerticalSplitGroupTablePosition
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.VerticalSplitGroupTablePosition;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.VerticalSplitGroupTablePosition = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static int HorizontalSplitGroupTablePosition
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.HorizontalSplitGroupTablePosition;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.HorizontalSplitGroupTablePosition = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static bool HandleCompleteTicketFromSquare
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.HandleTicketFromSquare;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.HandleTicketFromSquare = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        //public static void updateMachine()
        //{
        //    Dao dao = new Dao();
        //    machineInfo.MachineName = Environment.MachineName;
        //    using (PhoMac.Model.Entities obj = new PhoMac.Model.Entities())
        //    {
        //        int id = machineInfo.MachineID;
        //        PhoHa7_Machine temp = obj.PhoHa7_Machine.First(p => p.MachineID == id);

        //        temp.MachineType = machineInfo.MachineType;
        //        temp.BackgroundColorToGo = machineInfo.BackgroundColorToGo;//.ToArgb() + string.Empty;
        //        //color letter togo
        //        temp.ForceColorTogo = machineInfo.ForceColorTogo;//.ToArgb() + string.Empty;
        //        //font size print
        //        temp.FontSizePrint = machineInfo.FontSizePrint;
        //        //padding print
        //        temp.PaddingSizeHeader = machineInfo.PaddingSizeHeader;
        //        //color item change
        //        temp.ForceColorChange = machineInfo.ForceColorChange;//.ToArgb() + string.Empty;
        //        //color background item change
        //        temp.BackgroundColorChange = machineInfo.BackgroundColorChange;//.ToArgb() + string.Empty;
        //        //background emergency
        //        temp.BackgroundColorEmergency = machineInfo.BackgroundColorEmergency;//.ToArgb() + string.Empty;
        //        //forceColor Large Size
        //        //machineInfo.ForceColorLageSize = ClsPublic.ForceColorLageSize;
        //        //time count down
        //        temp.Timer = machineInfo.Timer;
        //        //auto print
        //        temp.AutoPrinter = machineInfo.AutoPrinter;
        //        temp.AutoPrinterAfter = machineInfo.AutoPrinterAfter;
        //        //printer
        //        temp.PrinterName = machineInfo.PrinterName;
        //        //print barcode
        //        temp.PrinterBarcode = machineInfo.PrinterBarcode;
        //        //number of group table
        //        temp.GroupTable = machineInfo.GroupTable;
        //        //show/hide Group and Filter Tables
        //        temp.ShowGroup = machineInfo.ShowGroup;
        //        temp.ShowFilterGroup = machineInfo.ShowFilterGroup;
        //        //txt small, large size
        //        //machineInfo.SizeSmall = ClsPublic.SizeSmall;
        //        //machineInfo.SizeLarge = ClsPublic.SizeLarge;
        //        //txt sound play url
        //        temp.SoundUrl = machineInfo.SoundUrl;
        //        //color inKitchen and NotKitchen
        //        temp.ForceColor = machineInfo.ForceColor;//.ToArgb() + string.Empty;
        //        temp.ForceColor1 = machineInfo.ForceColor1;//.ToArgb() + string.Empty;
        //        //qty column
        //        temp.BackgrdColorQty = machineInfo.BackgrdColorQty;//.ToArgb() + string.Empty;
        //        temp.ForceColorQty = machineInfo.ForceColorQty;//.ToArgb() + string.Empty;
        //        temp.ForceColorQty1 = machineInfo.ForceColorQty1;//.ToArgb() + string.Empty;
        //        temp.FontSizeQty = machineInfo.FontSizeQty;
        //        obj.SaveChanges();
        //    }
        //}

        //public static void getMachineInfo()
        //{
        //    Dao dao = new Dao();
        //    string machineName = Environment.MachineName;
        //    PhoHa7_Machine machine;
        //    var list = dao.FindByMultiColumnAnd<PhoHa7_Machine>(new[] { "MachineName" }, machineName);
        //    if (list.Count == 0)
        //    {
        //        machine = new PhoHa7_Machine();
        //        machine.MachineName = machineName;
        //        machine.MachineActive = true;
        //        using (PhoMac.Model.Entities obj = new PhoMac.Model.Entities())
        //        {
        //            obj.PhoHa7_Machine.Attach(machine);
        //            obj.Entry(machine).State = EntityState.Added;
        //            obj.SaveChanges();
        //        }
        //        //dao.Add<PhoHa7_Machine>(machine);
        //    }
        //    else
        //    {
        //        machine = list.First();
        //    }
        //    machineInfo.MachineID = machine.MachineID;
        //    machineInfo.MachineName = machineName;
        //    Color color;

        //    KITCHEN_TYPE = machine.MachineType;
        //    TIMER_ORDER_COUNT_DOWN = Convert.ToInt32(machine.Timer);
        //    //
        //    string strColor = machine.ForceColorTogo + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.FromArgb(255, 128, 0);
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    ColorLetterToGo = color;
        //    //
        //    strColor = machine.BackgroundColorToGo + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.FromArgb(0, 0, 192);
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    ColorBackgroundToGo = color;
        //    //
        //    strColor = machine.ForceColorChange + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.Black;
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    ColorLetterItemChange = color;
        //    //
        //    strColor = machine.BackgroundColorChange + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.FromArgb(0, 192, 0);
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    ColorBackgroundItemChange = color;
        //    PaddingSizePrint = Convert.ToInt32(machine.PaddingSizeHeader);
        //    FontSizePrint = Convert.ToInt32(machine.FontSizePrint);
        //    //AllowMultiInstance=machine.
        //    AutoPrint = Convert.ToBoolean(machine.AutoPrinter);
        //    AutoPrintAfterSecond = Convert.ToInt32(machine.AutoPrinterAfter);
        //    Printers = machine.PrinterName;
        //    PrintBarCode = Convert.ToBoolean(machine.PrinterBarcode);
        //    NumOfGroupTable = Convert.ToInt32(machine.GroupTable);
        //    ShowGroupTable = Convert.ToBoolean(machine.ShowGroup);
        //    ShowFilterTable = Convert.ToBoolean(machine.ShowFilterGroup);
        //    SoundUrl = machine.SoundUrl;
        //    TicketWidthSize = Convert.ToInt32(machine.TicketWidthSize);
        //    TicketHeightSize = Convert.ToInt32(machine.TicketHeightSize);
        //    TicketFontSize = Convert.ToInt32(machine.TicketFontSize);
        //    //
        //    strColor = machine.ForceColor + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.Black;
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    ForceColor = color;
        //    //
        //    strColor = machine.ForceColor1 + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.LightGray;
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    ForceColor1 = color;
        //    //
        //    strColor = machine.BackgroundColorEmergency + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.Red;
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    BackgroundColorEmergency = color;
        //    //
        //    strColor = machine.BackgrdColorQty + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.Transparent;
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    BackgrdColorQty = color;
        //    //
        //    strColor = machine.ForceColorQty + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.Black;
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    ForceColorQty = color;
        //    //
        //    strColor = machine.ForceColorQty1 + string.Empty;
        //    if (strColor == string.Empty)
        //    {
        //        color = Color.Red;
        //    }
        //    else
        //    {
        //        color = Color.FromArgb(Convert.ToInt32(strColor));
        //    }
        //    ForceColorQty1 = color;
        //    FontSizeQty = Convert.ToInt32(machine.FontSizeQty);
        //}

        #endregion

        #region Printer

        static List<PhoHa7_ProcTickets> listPrintItem = new List<PhoHa7_ProcTickets>();
        public static List<PhoHa7_ProcTickets> ListPrintItem
        {
            get { return listPrintItem; }
            set { listPrintItem = value; }
        }

        static int printPosition = 0;

        public static int PrintPosition
        {
            get
            {
                return printPosition;
            }
            set
            {
                printPosition = value;
            }
        }

        public static int NumberOfReprint
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.NumberOfReprint;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.NumberOfReprint = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static string DateKeyNow
        {
            get
            {
                DateTime today = DateTime.Now;
                return string.Empty + today.Day + today.Month + today.Year;
            }
        }

        public static string DateKey { get; set; }

        public static void addListPrintItem(PhoHa7_ProcTickets source)
        {
            try
            {
                PhoHa7_ProcTickets des = new PhoHa7_ProcTickets();
                des.TicketID = source.TicketID;
                des.TableName = source.TableName;
                des.DTicketNum = Convert.ToInt32(source.DTicketNum);
                des.TakeOut = Convert.ToBoolean(source.TakeOut);
                des.EmployeeName = source.EmployeeName;
                des.DateTimeIssue = source.DateTimeIssue;
                des.CountItemDonePrint = source.CountItemDonePrint;
                des.CountTotalItemPrint = source.CountTotalItemPrint;
                des.ListProduct = new List<Product>();
                foreach (Product p in source.ListProduct)
                {
                    Product product = new Product();
                    product.PrintName = p.PrintName;
                    product.SaleItemID = p.SaleItemID;
                    product.TakeOut = p.TakeOut;
                    product.IsCancel = p.IsCancel;
                    product.IsChange = p.IsChange;
                    product.PrintFrontStyle = p.PrintFrontStyle;
                    des.ListProduct.Add(product);
                }
                //set datekey
                if (DateKey == string.Empty)
                {
                    DateKey = DateKeyNow;
                }
                else
                {
                    if (DateKey != DateKeyNow)
                    {
                        DateKey = DateKeyNow;
                        ListPrintItem.Clear();
                    }
                }
                //add ticket to list
                ListPrintItem.Add(des);
                PrintPosition = ListPrintItem.Count - 1;

            }
            catch (Exception ex)
            {
                WriteException(ex);
            }

        }

        public static string LookUpPrinterForSpecialItem
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.LookUpPrinterForSpecialItem;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.LookUpPrinterForSpecialItem = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }

        public static string PrinterOpenCashDrawerName
        {
            get
            {
                return global::PhoMac.Main.Properties.Settings.Default.PrinterOpenCashDrawerName;
            }
            set
            {
                global::PhoMac.Main.Properties.Settings.Default.PrinterOpenCashDrawerName = value;
                global::PhoMac.Main.Properties.Settings.Default.Save();
            }
        }
        #endregion

        #region Write exception

        public static int WriteException(Exception ex)
        {
            int count = 0;

            try
            {
                using (PhoMac.Model.Entities obj = new PhoMac.Model.Entities())
                {
                    //Dao dao = new Dao();
                    Error er = new Error();
                    er.ErrorText = ex.Message + string.Empty;
                    er.ErrorDetails = ex.ToString() + string.Empty;
                    er.ErrorDate = DateTime.Now;
                    er.ErrorType = ex.InnerException == null ? "" : ex.InnerException.GetType().ToString();
                    er.InnerException = ex.InnerException == null ? "" : ex.InnerException.Message;
                    er.InnerSource = ex.InnerException == null ? "" : ex.InnerException.Source;
                    er.InnerExceptionStackTrace = ex.InnerException == null ? "" : ex.InnerException.StackTrace + string.Empty;
                    //count = dao.Add<Error>(er);
                    obj.Set<Error>().Add(er);

                    count = obj.SaveChanges();
                }
            }
            catch (System.Exception ex1)
            {
                SqlHelper sqlHelper = new SqlHelper();
                Error er = new Error();
                er.ErrorText = ex1.Message + string.Empty;
                er.ErrorDetails = ex1 + string.Empty;
                er.ErrorDate = DateTime.Now;
                er.ErrorType = ex1.InnerException == null ? "" : ex1.InnerException.GetType().ToString();
                er.InnerException = ex1.InnerException == null ? "" : ex1.InnerException.Message;
                er.InnerSource = ex1.InnerException == null ? "" : ex1.InnerException.Source;
                er.InnerExceptionStackTrace = ex1.InnerException == null ? "" : ex1.InnerException.StackTrace + string.Empty;

                string sql = "INSERT INTO [Error] ([ErrorText],[ErrorDetails],[ErrorDate],[ErrorType],[InnerException],[InnerSource],[InnerExceptionStackTrace]) " +
                                " VALUES (@ErrorText,@ErrorDetails,@ErrorDate,@ErrorType,@InnerException,@InnerSource,@InnerExceptionStackTrace)";
                sqlHelper.ExecuteNonQuery(sql, CommandType.Text, new object[] { "@ErrorText", "@ErrorDetails", "@ErrorDate", "@ErrorType", "@InnerException", "@InnerSource", "@InnerExceptionStackTrace" },
                    new object[] { er.ErrorText, er.ErrorDetails, er.ErrorDate, er.ErrorType, er.InnerException, er.InnerSource, er.InnerExceptionStackTrace });
            }


            return count;

        }

        #endregion


        public static Thread threadHandleCompleteTicket;

        static Dictionary<string, PhoHa7_Sys_Option> _listSystemOption = new Dictionary<string, PhoHa7_Sys_Option>();
        public static Dictionary<string, PhoHa7_Sys_Option> ListSystemOption
        {
            get
            {
                if (_listSystemOption == null || _listSystemOption.Count == 0)
                {
                    Dao dao = new Dao();
                    var obj = dao.GetAll<PhoHa7_Sys_Option>().ToList();
                    foreach (var item in obj)
                    {
                        _listSystemOption.Add(item.Opt_ID, item);
                    }
                    return _listSystemOption;
                }
                else
                {
                    return _listSystemOption;
                }
            }
        }
        public static string responseOrderDetails = "";
        public static async Task getSquareOrderDetails()
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
                responseOrderDetails = await response.Content.ReadAsStringAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }


        public static string SquareProductList = "";
        public static async Task getSquareProductList()
        {
            try
            {
                HttpClient client = new HttpClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://connect.squareup.com/v2/catalog/search");

                request.Headers.Add("Square-Version", "2023-06-08");
                request.Headers.Add("Authorization", "Bearer EAAAEBkTIFhqhIKegI5ih6bRhV_b0bfyhQyg-VPvUBH20wGiLM9kHLLBA7KQFgi1");
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                SquareProductList = await response.Content.ReadAsStringAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public static string SquareOnline = "";
        public static async Task getSquareOrderOnline()
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


                request.Content = new StringContent("{\n    \"location_ids\": [\n      \"LS7M0EPVEJZNB\"\n    ],\n    \"query\": {\n      \"filter\": {\n        \"source_filter\": {\n          \"source_names\": [\n            \"Square Online\"\n          ]\n        },\n        \"date_time_filter\": {\n          \"created_at\": {\n            \"start_at\": \"" + stoday + "\"\n          }\n        }\n      }\n    }\n  }");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                SquareOnline = await response.Content.ReadAsStringAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public static void handleCompleTicket()
        {
            while (true)
            {
                Thread.Sleep(10000);
                if (responseOrderDetails == "")
                {
                    try
                    {
                        Task.Run(() => getSquareOrderDetails()).Wait(20000);
                        Task.Run(() => getSquareOrderOnline()).Wait(20000);
                    }
                    catch (System.Exception ex)
                    {
                        ClsPublic.WriteException(ex);
                    }

                }
                else
                {
                    if (!isRunning)
                    {
                        try
                        {
                            Task.Run(() => updateCompletedTicket()).Wait(20000);
                            var json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(ClsPublic.SquareOnline.ToString());
                            Task.Run(() => addTicketOnline(json)).Wait(20000);
                        }
                        catch (System.Exception ex)
                        {
                            ClsPublic.WriteException(ex);
                        }
                    }


                }
            }



            //var result = response.Content.ReadAsStringAsync();
            //response.Wait();
            //var json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseOrderDetails.ToString());
            //foreach (var item in json["orders"])
            //{
            //    var a = item;
            //}
            //var note = json.SelectToken("orders[0].line_items[0].note");
        }

        static bool isRunning = false;

        static int addTicketOnline(Newtonsoft.Json.Linq.JToken ticketDetails)
        {
            //////////////////////////////////////////////////////////////////////////
            //Online Order
            //////////////////////////////////////////////////////////////////////////
            ///sL7TC8NqdNE5sue3zQJjBZS0eUJZY
            try
            {
                Dao dao = new Dao();
                foreach (var ticket in ticketDetails["orders"])
                {
                    string idXferToOnlinePickUP = ticket.SelectToken("id").ToString();
                    if (idXferToOnlinePickUP == "aiZKuOiSz9Pp0d8U5xNGmALSRCHZY")
                    {
                        string a = "";
                    }
                    string state = ticket.SelectToken("state").ToString();
                    //if ( (idXferToOnlinePickUP != string.Empty || idXferToOnlinePickUP != null))
                    if ((state == "OPEN" || state == "COMPLETED") && (idXferToOnlinePickUP != string.Empty || idXferToOnlinePickUP != null))
                    {
                        //check is added to PhoHa7_Procticket by EmploymentName -- id
                        Ticket tempPH7Proc = dao.FindByMultiColumnAnd<Ticket>(new[] { "XferTo" }, ticket.SelectToken("id").ToString()).FirstOrDefault();
                        if (tempPH7Proc == null)
                        {
                            var productList = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(ClsPublic.SquareProductList + string.Empty);
                            PhoHa7_ProcTickets proc = new PhoHa7_ProcTickets();
                            try
                            {
                                proc.CustomerName = ticket.SelectToken("fulfillments[0].pickup_details.recipient.display_name").ToString();
                            }
                            catch
                            {
                                try
                                {
                                    proc.CustomerName = ticket.SelectToken("fulfillments[0].delivery_details.recipient.display_name").ToString();
                                }
                                catch
                                {
                                }
                            }

                            proc.transaction_id = ticket.SelectToken("id").ToString();
                            proc.TableName = "Square Online - " + proc.CustomerName;
                            proc.TableID = 0;
                            proc.EmployeeName = "Online API";
                            proc.TicketNum = 0;
                            proc.TakeOut = true;
                            proc.ToKitchen = false;
                            proc.Done = false;
                            proc.IsChange = false;
                            proc.Emergency = false;
                            proc.DateTimeIssue = DateTime.Now;
                            proc.TicketID_Root = 0;
                            proc.DTicketNum = 0;
                            PhoHa7_ProcTickets newProcTicket = dao.Add1<PhoHa7_ProcTickets>(proc);

                            Ticket t = new Ticket();
                            t.TicketNum = 0;
                            t.CustomerID = 0;
                            t.EmployeeID = 0;
                            t.DateTimeIssue = DateTime.Now;
                            t.PaidType = 1;
                            t.ContractType = 1;


                            t.PaidCash = 0;
                            t.Discount = 0;


                            t.Voided = false;
                            t.CustomerName = proc.CustomerName;
                            t.TableName = "Square Online - " + t.CustomerName;
                            t.DTicketNum = 0;
                            t.TableID = 0;
                            t.TabCatID = 0;
                            t.SplitCheck = false;
                            t.ModDate = DateTime.Now;
                            t.EditTimestamp = DateTime.Now;
                            t.RecordGUID = Guid.NewGuid().ToString();

                            try
                            {
                                t.CardCode = ticket.SelectToken("tenders[0].card_details.card.last_4").ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                t.Tax = Convert.ToDecimal(ticket.SelectToken("taxes[0].applied_money.amount").ToString()) / 100;
                                t.PaidCredit = Convert.ToDecimal(ticket.SelectToken("tenders[0].amount_money.amount").ToString()) / 100;
                            }
                            catch
                            {

                            }

                            try
                            {

                                t.Tips = Convert.ToDecimal(ticket.SelectToken("total_tip_money.amount").ToString()) / 100;
                            }
                            catch (System.Exception ex)
                            {
                                t.Tips = 0;
                            }
                            t.PaidCredit = t.PaidCredit - t.Tips;
                            t.TotalP = t.PaidCredit - t.Tips;
                            t.XferTo = ticket.SelectToken("id").ToString();
                            dao.Add<Ticket>(t);


                            var lineItems = ticket.SelectToken("line_items");
                            foreach (var item in lineItems)
                            {
                                PhoHa7_ProcSaleItem saleItem = new PhoHa7_ProcSaleItem();
                                saleItem.ProductID = 0;
                                string itemName = item["name"] + string.Empty;
                                decimal basePrice = Convert.ToDecimal(item.SelectToken("base_price_money.amount")) / 100;
                                //get kitchen name

                                    foreach (var product in productList.SelectToken("objects"))
                                    {

                                        saleItem.Category = 1;
                                        var productName = "";
                                        try
                                        {
                                            productName = product.SelectToken("item_data.name").ToString();
                                        }
                                        catch (System.Exception ex)
                                        {
                                            continue;
                                        }
                                       

                                        if (productName == itemName)
                                        {
                                            saleItem.Description = product.SelectToken("item_data.kitchen_name").ToString();
                                            //set category PHO
                                            if (product.SelectToken("item_data.category_id").ToString() == "TIJH4DAWJKQ2TGVH34UTP7BQ" || product.SelectToken("item_data.category_id").ToString() == "DES3VOQLCGRJBQLWVMVZNWDN")
                                            {
                                                saleItem.Category = 4;
                                            }
                                            if (itemName.StartsWith("1.") || itemName.StartsWith("2.") || itemName.StartsWith("3.") || itemName.StartsWith("4.") || itemName.StartsWith("5.") ||
                                                itemName.StartsWith("6.") || itemName.StartsWith("7.") || itemName.StartsWith("8.") || itemName.StartsWith("9.") || itemName.StartsWith("10.") ||
                                                itemName.StartsWith("11.") || itemName.StartsWith("12.") || itemName.StartsWith("13.") || itemName.StartsWith("14.") || itemName.StartsWith("15.") ||
                                                itemName.StartsWith("16.") || itemName.StartsWith("17.") || itemName.StartsWith("18.") || itemName.StartsWith("19.") || itemName.StartsWith("20.") ||
                                                itemName.StartsWith("21.") || itemName.StartsWith("22.") || itemName.StartsWith("23.") || itemName.StartsWith("65.")
                                                || itemName.StartsWith("83.") || itemName.StartsWith("83A.") || itemName.StartsWith("83B.")
                                                || itemName.StartsWith("64.") || itemName.StartsWith("70.") || itemName.StartsWith("72.") || itemName.StartsWith("73.") || itemName.StartsWith("82.") || itemName.StartsWith("102.")
                                                || itemName.StartsWith("98.") || itemName.StartsWith("101.")
                                                || itemName.StartsWith("45.") || itemName.StartsWith("45A.") || itemName.StartsWith("45B.") || itemName.StartsWith("45C.")
                                                || itemName.StartsWith("46.") || itemName.StartsWith("46A.")
                                                || itemName.StartsWith("50.") || itemName.StartsWith("50A."))
                                            {
                                                saleItem.Category = 4;
                                            }
                                            if (product.SelectToken("item_data.category_id").ToString() == "KRESCPKTMTZOTIRKW4CBO2L2"
                                                || product.SelectToken("item_data.category_id").ToString() == "YM47R7TJQRVBXJDBZ743BWA6"
                                                || product.SelectToken("item_data.category_id").ToString() == "KRESCPKTMTZOTIRKW4CBO2L2")
                                            {
                                                saleItem.ProductID = 854;
                                            }
                                            break;
                                        }
                                    }
                                if (saleItem.Description == null || saleItem.Description == string.Empty)
                                {
                                    saleItem.Description = itemName;
                                }

                                //modifer
                                try
                                {
                                    var modifiers = item.SelectToken("modifiers");
                                    foreach (var modifier in modifiers)
                                    {
                                        saleItem.ExtraWith += modifier["name"].ToString() + "|";
                                        var modifierAmount = modifier.SelectToken("base_price_money.amount").ToString();
                                        saleItem.Extra += Convert.ToDecimal(modifierAmount) / 100;
                                    }
                                }
                                catch (System.Exception ex)
                                {

                                }

                                //set other field
                                saleItem.Qty = Convert.ToInt32(item["quantity"].ToString());
                                saleItem.TicketID = newProcTicket.TicketID;
                                
                                saleItem.TicketNum = 0;
                                saleItem.Price = basePrice;
                                saleItem.Extra = 0;
                                saleItem.Employee = "Square Online";
                                saleItem.Done = false;
                                saleItem.ToKitchen = false;
                                saleItem.TakeOut = false;
                                saleItem.NotSaleItem = false;
                                saleItem.Done1 = false;
                                //saleItem.Category = 1;
                                saleItem.Cancel = false;
                                saleItem.IsChange = false;
                                saleItem.BarCode = "";
                                saleItem.ExtraName = "";
                                saleItem.ExtraPrice = "";
                                saleItem.OptionRequire = "";
                                saleItem.IsSmall = false;
                                saleItem.MType = 0;
                                saleItem.SaleItemID_Root = 0;
                                saleItem.ExtraWithout = "";
                                saleItem.CustomSelect = "";


                                dao.Add<PhoHa7_ProcSaleItem>(saleItem);

                                SaleItem s = new SaleItem();
                                s.TicketID = t.TicketID;
                                s.ProductID = Convert.ToInt32(saleItem.ProductID);
                                s.TicketNum = saleItem.TicketNum;
                                s.Description = saleItem.Description;
                                s.Qty = saleItem.Qty;
                                s.Price = saleItem.Price;
                                s.TPrice = (saleItem.Price + saleItem.Extra) * saleItem.Qty;
                                s.Extra = saleItem.Extra;
                                s.ProcDiscount = 0;
                                s.SmallSize = false;
                                s.SizeChoiceTxt = "";
                                s.TranID = 0;
                                s.MType = 0;
                                s.OrgPrice = 0;
                                s.CPrice = 0;
                                s.SPrice = 0;
                                s.TakeOut = saleItem.TakeOut;
                                s.ItemCode = saleItem.BarCode;
                                s.KitchenName = saleItem.Description;
                                s.Voided = false;
                                s.OnDate = DateTime.Now;
                                s.ProductName = itemName;
                                s.EditTimestamp = DateTime.Now;
                                s.RecordGUID = Guid.NewGuid().ToString();
                                dao.Add<SaleItem>(s);

                            }
                        }

                        continue;
                    }
                }


            }
            catch
            {

            }




            return 0;
        }

        public static void updateCompletedTicket()
        {
            try
            {
                isRunning = true;
                var json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(ClsPublic.responseOrderDetails.ToString());


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




                isRunning = false;
                ClsPublic.responseOrderDetails = "";
            }
            catch (System.Exception ex)
            {
                isRunning = false;
                ClsPublic.WriteException(ex);
                //EntityFactory.getInstance().rollBack();
            }
            finally
            {
                isRunning = false;
            }
        }



        //static SaleItem copyProcSaleItem2SaleItem(ProcSaleItem procSaleItem)
        //{
        //    SaleItem saleItem = new SaleItem();
        //    saleItem.ProductID = procSaleItem.ProductID;
        //    saleItem.TicketNum = procSaleItem.TicketNum;
        //    saleItem.Description = procSaleItem.Description;
        //    saleItem.Type = procSaleItem.Type;
        //    saleItem.Qty = procSaleItem.Qty;
        //    saleItem.Price = procSaleItem.Price;
        //    saleItem.TPrice = (procSaleItem.Price + procSaleItem.Extra) * procSaleItem.Qty;
        //    saleItem.Extra = procSaleItem.Extra;
        //    saleItem.ByWho = procSaleItem.ByWho;
        //    saleItem.ProcDiscount = procSaleItem.ProcDiscount;
        //    saleItem.SmallSize = procSaleItem.SmallSize;
        //    saleItem.SizeChoiceTxt = procSaleItem.SizeChoiceTxt;
        //    saleItem.TranID = procSaleItem.TranID;
        //    saleItem.MType = procSaleItem.MType;
        //    saleItem.OrgPrice = procSaleItem.ProductID;
        //    saleItem.CPrice = procSaleItem.CPrice;
        //    saleItem.SPrice = procSaleItem.SPrice;
        //    saleItem.TakeOut = procSaleItem.TakeOut;
        //    saleItem.ItemCode = procSaleItem.ItemCode;
        //    saleItem.KitchenName = procSaleItem.KitchenName;
        //    saleItem.Voided = procSaleItem.Voided;
        //    saleItem.OnDate = DateTime.Now;
        //    saleItem.ProductName = procSaleItem.ProductName;
        //    saleItem.EditTimestamp = DateTime.Now;
        //    saleItem.RecordGUID = Guid.NewGuid().ToString();
        //    return saleItem;
        //}

        //static Ticket copyProcTicket2Ticket(ProcTicket procTicket)
        //{
        //    Ticket ticket = new Ticket();
        //    ticket.TicketNum = procTicket.TicketID;
        //    ticket.CustomerID = procTicket.CustomerID;
        //    ticket.EmployeeID = procTicket.EmployeeID;
        //    ticket.DateTimeIssue = DateTime.Now;
        //    ticket.PaidType = 1;
        //    ticket.ContractType = 1;
        //    ticket.Tax = procTicket.Tax;
        //    ticket.TotalP = procTicket.TotalP;
        //    ticket.PaidCash = procTicket.PaidCash;
        //    ticket.Discount = procTicket.Discount;
        //    ticket.CardCode = procTicket.CardCode;
        //    ticket.PaidCredit = procTicket.PaidCredit;
        //    ticket.Voided = false;
        //    ticket.CustomerName = procTicket.CustomerName;
        //    ticket.TableName = procTicket.TableName;
        //    ticket.DTicketNum = procTicket.DTicketNum;
        //    ticket.TableID = procTicket.TableID;
        //    ticket.TabCatID = procTicket.TabCatID;
        //    ticket.SplitCheck = false;
        //    ticket.ModDate = DateTime.Now;
        //    ticket.EditTimestamp = DateTime.Now;
        //    ticket.RecordGUID = Guid.NewGuid().ToString();
        //    return ticket;
        //}




    }
}
