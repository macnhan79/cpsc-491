using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Business.Presenter
{
    public class TablePresenter
    {
        Table dic;
        public List<TablePresenter> ListTables;
        public Table Tables
        {
            //entity to database
            get
            {
                copyInstance();
                return dic;
            }
            //database to entity
            set
            {
                dic = value;
                if (dic == null)
                {
                    dic = new Table();
                }
                this.TableID = dic.TableID;
                this.CategoryID = Convert.ToInt32(dic.CategoryID);
                this.CurTicketNum = Convert.ToInt32(dic.CurTicketNum);
                this.CurDTicketNum = Convert.ToInt32(dic.CurDTicketNum);
                this.Active = Convert.ToBoolean(dic.Active);
                this.TableName = dic.TableName;
                this.ImageID = Convert.ToInt32(dic.ImageID);
                this.ImageFPath = dic.ImageFPath;
                this.Page = Convert.ToInt32(dic.Page);
                this.Row = Convert.ToInt32(dic.Row);
                this.Col = Convert.ToInt32(dic.Col);
                this._ButtonColor = Convert.ToInt32(dic.ButtonColor);
                this.ButtonColor = (_ButtonColor == 0 ? Color.Yellow : Color.FromArgb(_ButtonColor));
                this._TextColor = Convert.ToInt32(dic.TextColor);
                this.TextColor = (_TextColor == 0 ? Color.Black : Color.FromArgb(_TextColor));
                this.Delivery = Convert.ToBoolean(dic.Delivery);
                this.ZOrder = Convert.ToInt32(dic.ZOrder);
                this.TabNum = Convert.ToInt32(dic.TabNum);
                this.SeatNum = Convert.ToInt32(dic.SeatNum);
                this.TakeOut = Convert.ToBoolean(dic.TakeOut);
                this.StartTime = Convert.ToDateTime(dic.StartTime);
                this.TActive = Convert.ToBoolean(dic.TActive);
                this.OnDay = Convert.ToInt32(dic.OnDay);
                this.OnMonth = Convert.ToInt32(dic.OnMonth);
                this.OnYear = Convert.ToInt32(dic.OnYear);
                this.OnDate = Convert.ToDateTime(dic.OnDate);
                this.DateKey = dic.DateKey;

                this.oTextAlign = Convert.ToInt32(dic.oTextAlign);
                this.oFontName = dic.oFontName;
                this.oFontPoint = (float)Convert.ToDouble(dic.oFontPoint);
                this.oFontStyle = Convert.ToInt32(dic.oFontStyle);
                this._oForeColor = Convert.ToInt32(dic.oForeColor);
                this._oBackColor = Convert.ToInt32(dic.oBackColor);
                this.oLocX = Convert.ToInt32(dic.oLocX);
                this.oLocY = Convert.ToInt32(dic.oLocY);
                this.oWidth = Convert.ToInt32(dic.oWidth);
                this.oHeight = Convert.ToInt32(dic.oHeight);
                this.oTableStyle = Convert.ToInt32(dic.oTableStyle);
                this.oUpperColor = Convert.ToInt32(dic.oUpperColor);
                this.oLowerColor = Convert.ToInt32(dic.oLowerColor);

                this.TableType = Convert.ToInt32(dic.TableType);
                this.TableStatus = Convert.ToInt32(dic.TableStatus);
                this.BarTab = Convert.ToBoolean(dic.BarTab);
                this.FastCash = Convert.ToBoolean(dic.FastCash);
                this.EmployeeID = Convert.ToInt32(dic.EmployeeID);
                this.EmployeeName = dic.EmployeeName;
                this.OrderBy = Convert.ToInt32(dic.OrderBy);
            }
        }

        public TablePresenter()
        {
            dic = new Table();
            ListTables = new List<TablePresenter>();
        }

        public void CopyToList(List<Table> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                TablePresenter obj = new TablePresenter();
                obj.Tables = pListDic[i];
                ListTables.Add(obj);
            }
        }

        void copyInstance()
        {

            dic.TableID = TableID;
            dic.CategoryID = CategoryID;
            dic.CurTicketNum = CurTicketNum;
            dic.CurDTicketNum = CurDTicketNum;
            dic.Active = Active;
            dic.TableName = TableName;
            dic.ImageID = ImageID;
            dic.ImageFPath = ImageFPath;
            dic.Page = Page;
            dic.Row = Row;
            dic.Col = Col;
            dic.ButtonColor = _ButtonColor;
            dic.TextColor = _TextColor;
            dic.Delivery = Delivery;
            dic.ZOrder = ZOrder;
            dic.TabNum = TabNum;
            dic.SeatNum = SeatNum;
            dic.TakeOut = TakeOut;
            dic.StartTime = StartTime;
            dic.TActive = TActive;
            dic.OnDay = OnDay;
            dic.OnMonth = OnMonth;
            dic.OnYear = OnYear;
            dic.OnDate = OnDate;
            dic.DateKey = DateKey;

            dic.oTextAlign = oTextAlign;
            dic.oFontName = oFontName;
            dic.oFontPoint = oFontPoint;
            dic.oFontStyle = oFontStyle;
            dic.oForeColor = _oForeColor;
            dic.oBackColor = _oBackColor;
            dic.oLocX = oLocX;
            dic.oLocY = oLocY;
            dic.oWidth = oWidth;
            dic.oHeight = oHeight;
            dic.oTableStyle = oTableStyle;
            dic.oUpperColor = oUpperColor;
            dic.oLowerColor = oLowerColor;

            dic.TableType = TableType;
            dic.TableStatus = TableStatus;
            dic.BarTab = BarTab;
            dic.FastCash = FastCash;
            dic.EmployeeID = EmployeeID;
            dic.EmployeeName = EmployeeName;
            dic.OrderBy = OrderBy;
        }



        #region Property

        public int TableID { get; set; }
        public int CategoryID { get; set; }
        public int CurTicketNum { get; set; }
        public int CurDTicketNum { get; set; }
        public bool Active { get; set; }
        public string TableName { get; set; }
        public int ImageID { get; set; }
        public string ImageFPath { get; set; }
        public int Page { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int _ButtonColor { get; set; }
        public System.Drawing.Color ButtonColor { get; set; }
        public int _TextColor { get; set; }
        public System.Drawing.Color TextColor { get; set; }
        public bool Delivery { get; set; }
        public int ZOrder { get; set; }
        public int TabNum { get; set; }
        public int SeatNum { get; set; }
        public bool TakeOut { get; set; }
        public System.DateTime StartTime { get; set; }
        public bool TActive { get; set; }
        public int OnDay { get; set; }
        public int OnMonth { get; set; }
        public int OnYear { get; set; }
        public System.DateTime OnDate { get; set; }
        public string DateKey { get; set; }
        public int OnWeek { get; set; }
        public string WeekKey { get; set; }
        public int OnBiWeek { get; set; }
        public string BiWeekKey { get; set; }
        public int OnSimMonth { get; set; }
        public string SimMonthKey { get; set; }
        public int OnQuarter { get; set; }
        public string QuarterKey { get; set; }
        public bool NewObj { get; set; }
        public bool ChgeObj { get; set; }
        public bool SentObj { get; set; }
        public float BottleOff { get; set; }
        public int objType { get; set; }
        public string oName { get; set; }
        public int oTextAlign { get; set; }
        public string oFontName { get; set; }
        public float oFontPoint { get; set; }
        public int oFontStyle { get; set; }
        public int _oForeColor { get; set; }
        public System.Drawing.Color oForeColor { get; set; }
        public int _oBackColor { get; set; }
        public System.Drawing.Color oBackColor { get; set; }
        public int oLocX { get; set; }
        public int oLocY { get; set; }
        public int oWidth { get; set; }
        public int oHeight { get; set; }
        public int oTableStyle { get; set; }
        public int oUpperColor { get; set; }
        public int oLowerColor { get; set; }
        public int RecordState { get; set; }
        public string RecordGUID { get; set; }
        public int TableType { get; set; }
        public int TableStatus { get; set; }
        public bool BarTab { get; set; }
        public bool FastCash { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int OrderBy { get; set; }
        #endregion
    }
}
