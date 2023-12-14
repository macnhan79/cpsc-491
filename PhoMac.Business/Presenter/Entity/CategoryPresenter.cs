using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Business.Data;

namespace PhoMac.Business.Presenter
{
    public class CategoryPresenter
    {
        public List<CategoryPresenter> ListCategories;
        public Category cat;
        public Category Categories
        {
            //entity to database
            get
            {
                copyInstance();
                return cat;
            }
            //database to entity
            set
            {
                cat = value;
                CategoryID = cat.CategoryID;
                CategoryName = cat.CategoryName;
                Active = Convert.ToBoolean(cat.Active);
                Page = Convert.ToInt32(cat.Page);
                Col = Convert.ToInt32(cat.Col);
                Row = Convert.ToInt32(cat.Row);
                ChoicPage = Convert.ToInt32(cat.ChoicPage);
                OnDay = Convert.ToInt32(cat.OnDay);
                OnMonth = Convert.ToInt32(cat.OnMonth);
                OnYear = Convert.ToInt32(cat.OnYear);
                OnDate = Convert.ToDateTime(cat.OnDate);
                DateKey = cat.DateKey;
                OnWeek = Convert.ToInt32(cat.OnWeek);
                WeekKey = cat.WeekKey;
                OnBiWeek = Convert.ToInt32(cat.OnBiWeek);
                BiWeekKey = cat.BiWeekKey;
                OnSimMonth = Convert.ToInt32(cat.OnSimMonth);
                SimMonthKey = cat.SimMonthKey;
                OnQuarter = Convert.ToInt32(cat.OnQuarter);
                QuarterKey = cat.QuarterKey;
                NewObj = Convert.ToBoolean(cat.NewObj);
                ChgeObj = Convert.ToBoolean(cat.ChgeObj);
                SentObj = Convert.ToBoolean(cat.SentObj);
                iPage = Convert.ToInt32(cat.iPage);
                iRow = Convert.ToInt32(cat.iRow);
                iCol = Convert.ToInt32(cat.iCol);
                RecordState = Convert.ToInt32(cat.RecordState);
                RecordGUID = cat.RecordGUID;

                //WebShowTypeName = cat.WebShowTypeName;
                WebActive = Convert.ToBoolean(cat.WebActive);
                WebName = cat.WebName;
                WebOrderBy = Convert.ToInt32(cat.WebOrderBy);
                WebShowType = cat.WebShowType;
                WebFlag = cat.WebFlag;
            }
        }

        public CategoryPresenter()
        {
            cat = new Category();
            ListCategories = new List<CategoryPresenter>();
        }

        void copyInstance()
        {
            cat.CategoryID = CategoryID;
            cat.CategoryName = CategoryName;
            cat.Active = Active;
            cat.Page = Page == 0 ? -1 : Page;
            cat.Col = Col == 0 ? -1 : Col;
            cat.Row = Row == 0 ? -1 : Row;
            cat.ChoicPage = ChoicPage == 0 ? -1 : ChoicPage;
            cat.OnDay = OnDay == 0 ? -1 : OnDay;
            cat.OnMonth = OnMonth == 0 ? -1 : OnMonth;
            cat.OnYear = OnYear == 0 ? -1 : OnYear;
            cat.OnDate = OnDate;
            cat.DateKey = DateKey;
            cat.OnWeek = OnWeek == 0 ? -1 : OnWeek;
            cat.WeekKey = WeekKey;
            cat.OnBiWeek = OnBiWeek == 0 ? -1 : OnBiWeek;
            cat.BiWeekKey = BiWeekKey;
            cat.OnSimMonth = OnSimMonth == 0 ? -1 : OnSimMonth;
            cat.SimMonthKey = SimMonthKey;
            cat.OnQuarter = OnQuarter == 0 ? -1 : OnQuarter;
            cat.QuarterKey = QuarterKey;
            cat.NewObj = NewObj;
            cat.ChgeObj = ChgeObj;
            cat.SentObj = SentObj;
            cat.iPage = iPage == 0 ? -1 : iPage;
            cat.iRow = iRow == 0 ? -1 : iRow;
            cat.iCol = iCol == 0 ? -1 : iCol;
            cat.RecordState = RecordState;
            cat.RecordGUID = RecordGUID;

            //cat.WebShowTypeName = WebShowTypeName;
            cat.WebActive = WebActive;
            cat.WebName = WebName;
            cat.WebOrderBy = WebOrderBy == 0 ? -1 : Page;
            cat.WebShowType = WebShowType;
            cat.WebFlag = WebFlag;
        }


        public void CopyToList(List<Category> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                CategoryPresenter obj = new CategoryPresenter();
                obj.Categories = pListDic[i];
                ListCategories.Add(obj);
            }
        }

        #region Property
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool Active { get; set; }
        public int Page { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public int ChoicPage { get; set; }
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
        public int iPage { get; set; }
        public int iRow { get; set; }
        public int iCol { get; set; }
        public int RecordState { get; set; }
        public string RecordGUID { get; set; }

        public string WebShowTypeName { get; set; }
        public bool WebActive { get; set; }
        public string WebName { get; set; }
        public int WebOrderBy { get; set; }
        public string WebShowType { get; set; }
        public string WebFlag { get; set; }


        #endregion




    }
}
