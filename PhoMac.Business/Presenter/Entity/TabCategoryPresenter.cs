using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Model;

namespace PhoMac.Business.Presenter
{
    public class TabCategoryPresenter
    {
        TabCategory dic;
        public List<TabCategoryPresenter> ListTabCategories;
        public TabCategory TabCategories
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

                this.CategoryID = dic.CategoryID;
                this.CategoryName = dic.CategoryName;
                this.Active = Convert.ToBoolean(dic.Active);
                this.Page = Convert.ToInt32(dic.Page);
                this.Col = Convert.ToInt32(dic.Col);
                this.Row = Convert.ToInt32(dic.Row);
                this.TickNumber = Convert.ToInt32(dic.TickNumber);
                this.TickNumDay = Convert.ToInt32(dic.TickNumDay);
                this.Rows = Convert.ToInt32(dic.Rows);
                this.Cols = Convert.ToInt32(dic.Cols);
                this.OnDay = Convert.ToInt32(dic.OnDay);
                this.OnMonth = Convert.ToInt32(dic.OnMonth);
                this.OnYear = Convert.ToInt32(dic.OnYear);
                this.OnDate = Convert.ToDateTime(dic.OnDate);
                this.DateKey = dic.DateKey;
                this.OnWeek = Convert.ToInt32(dic.OnWeek);
                this.WeekKey = dic.WeekKey;
                this.OnBiWeek = Convert.ToInt32(dic.OnBiWeek);
                this.BiWeekKey = dic.BiWeekKey;
                this.OnSimMonth = Convert.ToInt32(dic.OnSimMonth);
                this.SimMonthKey = dic.SimMonthKey;
                this.OnQuarter = Convert.ToInt32(dic.OnQuarter);
                this.QuarterKey = dic.QuarterKey;
                this.NewObj = Convert.ToBoolean(dic.NewObj);
                this.ChgeObj = Convert.ToBoolean(dic.ChgeObj);
                this.SentObj = Convert.ToBoolean(dic.SentObj);
                this.RecordState = Convert.ToInt32(dic.RecordState);
                this.RecordGUID = dic.RecordGUID;
                this.Color = dic.Color;
            }
        }

        public TabCategoryPresenter()
        {
            TabCategories = new TabCategory();
            ListTabCategories = new List<TabCategoryPresenter>();
        }

        public void CopyToList(List<TabCategory> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                TabCategoryPresenter obj = new TabCategoryPresenter();
                obj.TabCategories = pListDic[i];
                ListTabCategories.Add(obj);
            }
        }

        void copyInstance()
        {

            dic.CategoryID = CategoryID;
            dic.CategoryName = CategoryName;
            dic.Active = Active;
            dic.Page = Page;
            dic.Col = Col;
            dic.Row = Row;
            dic.TickNumber = TickNumber;
            dic.TickNumDay = TickNumDay;
            dic.Rows = Rows;
            dic.Cols = Cols;
            dic.OnDay = OnDay;
            dic.OnMonth = OnMonth;
            dic.OnYear = OnYear;
            dic.OnDate = OnDate;
            dic.DateKey = DateKey;
            dic.OnWeek = OnWeek;
            dic.WeekKey = WeekKey;
            dic.OnBiWeek = OnBiWeek;
            dic.BiWeekKey = BiWeekKey;
            dic.OnSimMonth = OnSimMonth;
            dic.SimMonthKey = SimMonthKey;
            dic.OnQuarter = OnQuarter;
            dic.QuarterKey = QuarterKey;
            dic.NewObj = NewObj;
            dic.ChgeObj = ChgeObj;
            dic.SentObj = SentObj;
            dic.RecordState = RecordState;
            dic.RecordGUID = RecordGUID;
            dic.Color = Color;
        }



        #region Property

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool Active { get; set; }
        public int Page { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public int TickNumber { get; set; }
        public int TickNumDay { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
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
        public int RecordState { get; set; }
        public string RecordGUID { get; set; }
        public string Color { get; set; }
        #endregion


    }
}
