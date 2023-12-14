using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Model;
using PhoMac.Business.Data;
using PhoMac.Business.Presenter.Entity;

namespace PhoMac.Business.Presenter
{
    public class ProductPresenter
    {
        Product dic;
        Dao dao;
        public List<ProductPresenter> ListProducts;

        public Product Products
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
                if (dic ==null)
                {
                    dic = new Product();
                }

                this.ProductID = dic.ProductID;
                this.ProductName = dic.ProductName;
                this.CategoryID = Convert.ToInt32(dic.CategoryID);
                this.Price = Price;
                this.UnitsInStock = Convert.ToInt32(dic.UnitsInStock);
                this.InStockLimit = Convert.ToInt32(dic.InStockLimit);
                this.Active = Convert.ToBoolean(dic.Active);
                this.Type = Convert.ToInt32(dic.Type);
                this.ToDate = Convert.ToDateTime(dic.ToDate);
                this.Discount = Convert.ToDecimal(dic.Discount);
                this.TeenDiscount = Convert.ToDecimal(dic.TeenDiscount);
                this.SeniorDiscount = Convert.ToDecimal(dic.SeniorDiscount);
                this.DiscountDays = Convert.ToInt32(dic.DiscountDays);
                this.TeenSeniorDays = Convert.ToInt32(dic.TeenSeniorDays);
                this.ButtonColor = Convert.ToInt32(dic.ButtonColor);
                this.TextColor = Convert.ToInt32(dic.TextColor);
                this.Page = Convert.ToInt32(dic.Page);
                this.Col = Convert.ToInt32(dic.Col);
                this.Row = Convert.ToInt32(dic.Row);
                this.ProductImage = dic.ProductImage;
                this.TeenDays = Convert.ToInt32(dic.TeenDays);
                this.SeniorDays = Convert.ToInt32(dic.SeniorDays);
                this.InItem = Convert.ToBoolean(dic.InItem);
                this.Cost = Convert.ToDecimal(dic.Cost);
                this.InCatID = Convert.ToInt32(dic.InCatID);
                this.StockedDate = Convert.ToDateTime(dic.StockedDate);
                this.RecordLevel = Convert.ToInt32(dic.RecordLevel);
                this.BarCode = dic.BarCode;
                this.PrintBoth = Convert.ToBoolean(dic.PrintBoth);
                this.NoTax = Convert.ToBoolean(dic.NoTax);
                this.StockMatch = Convert.ToBoolean(dic.StockMatch);
                this.ChoiceProd = Convert.ToBoolean(dic.ChoiceProd);
                this.NoPicture = Convert.ToBoolean(dic.NoPicture);
                this.Location = dic.Location;
                this.VendorName = dic.VendorName;
                this.SizeChoiceTxt = dic.SizeChoiceTxt;
                this.CategoryName = dic.CategoryName;
                this.VendorID = Convert.ToInt32(dic.VendorID);
                this.MType = Convert.ToInt32(dic.MType);
                this.StockCount = Convert.ToInt32(dic.StockCount);
                this.WorkTimeSec = Convert.ToInt32(dic.WorkTimeSec);
                this.BPrice = Convert.ToDecimal(dic.BPrice);
                this.LPrice = Convert.ToDecimal(dic.LPrice);
                this.DPrice = Convert.ToDecimal(dic.DPrice);
                this.Price1 = Convert.ToDecimal(dic.Price1);
                this.Price2 = Convert.ToDecimal(dic.Price2);
                this.ComProd = Convert.ToBoolean(dic.ComProd);
                this.CPrice = Convert.ToDecimal(dic.CPrice);
                this.NotSaleItem = Convert.ToBoolean(dic.NotSaleItem);
                this.PrintStation = Convert.ToInt32(dic.PrintStation);
                this.KitchenName = dic.KitchenName;
                this.OutOrder = Convert.ToBoolean(dic.OutOrder);
                this.Price3 = Convert.ToDecimal(dic.Price3);
                this.Price4 = Convert.ToDecimal(dic.Price4);
                this.Price5 = Convert.ToDecimal(dic.Price5);
                this.Price6 = Convert.ToDecimal(dic.Price6);
                this.Price7 = Convert.ToDecimal(dic.Price7);
                this.Units = Convert.ToInt32(dic.Units);
                this.LinkProduct = Convert.ToInt32(dic.LinkProduct);
                this.RewardPoints = Convert.ToInt32(dic.RewardPoints);
                this.ProdNo = dic.ProdNo;
                this.QBuy = Convert.ToInt32(dic.QBuy);
                this.QFree = Convert.ToInt32(dic.QFree);
                this.OnDay = Convert.ToInt32(dic.OnDay);
                this.OnMonth = Convert.ToInt32(dic.OnMonth);
                this.OnYear = Convert.ToInt32(dic.OnYear);
                this.OnDate = Convert.ToDateTime(dic.OnDate);
                this.DateKey = dic.DateKey;
                //dic.OnWeek =   OnWeek          ;
                //dic.WeekKey =   WeekKey          ;
                //dic.OnBiWeek =   OnBiWeek          ;
                //dic.BiWeekKey =   BiWeekKey          ;
                //dic.OnSimMonth =             ;
                //dic.SimMonthKey =             ;
                //dic.OnQuarter =             ;
                //dic.QuarterKey =             ;
                //dic.NewObj =             ;
                //dic.ChgeObj =             ;
                //dic.SentObj =             ;
                this.InitAmount = (float)Convert.ToDouble(dic.InitAmount);
                this.CurrAmount = (float)Convert.ToDouble(dic.CurrAmount);
                this.MiniAmount = (float)Convert.ToDouble(dic.MiniAmount);
                this.UnitAmount = (float)Convert.ToDouble(dic.UnitAmount);
                this.JarWeight = (float)Convert.ToDouble(dic.JarWeight);
                this.ImageID = Convert.ToInt32(dic.ImageID);
                this.QAccountType = Convert.ToInt32(dic.QAccountType);
                this.QAccountID = Convert.ToInt32(dic.QAccountID);
                this.QAccountName = dic.QAccountName;
                //dic.ProdImage =   ProdImage          ;
                //dic.ProdActImage =   ProdActImage          ;
                this.Description = dic.Description;
                this.OrderPending = Convert.ToBoolean(dic.OrderPending);
                this.InstallUnit = Convert.ToBoolean(dic.InstallUnit);
                this.VendorNo = dic.VendorNo;
                this.Reserved = Convert.ToInt32(dic.Reserved);
                this.ReservedStock = Convert.ToInt32(dic.ReservedStock);
                this.SerialNeeded = Convert.ToBoolean(dic.SerialNeeded);
                this.ProductUrl = dic.ProductUrl;
                this.PType = dic.PType;
                this.SubLevel = Convert.ToInt32(dic.SubLevel);
                this.ParentID = Convert.ToInt32(dic.ParentID);
                this.ParentName = dic.ParentName;
                this.EditSequence = dic.EditSequence;
                this.iPage = Convert.ToInt32(dic.iPage);
                this.ixPos = (float)Convert.ToDouble(dic.ixPos);
                this.iyPos = (float)Convert.ToDouble(dic.iyPos);
                this.iWidth = (float)Convert.ToDouble(dic.iWidth);
                this.iHeight = (float)Convert.ToDouble(dic.iHeight);
                this.iFontSize = (float)Convert.ToDouble(dic.iFontSize);
                this.iButColor = dic.iButColor;
                this.iTextColor = dic.iTextColor;
                this.EditTimestamp = Convert.ToDateTime(dic.EditTimestamp);
                this.RecordState = Convert.ToInt32(dic.RecordState);
                this.RecordGUID = dic.RecordGUID;
                this.LType = Convert.ToInt32(dic.LType);
                this.FoodTypeName = dic.FoodTypeName;
                this.FoodTypeID = Convert.ToInt32(dic.FoodTypeID);
                this.OrderBy = Convert.ToInt32(dic.OrderBy);
                this.ExpandCategoryID = Convert.ToInt32(dic.ExpandCategoryID);
            }
        }

        public ProductPresenter()
        {
            dic = new Product();
            dao = new Dao();
            ListProducts = new List<ProductPresenter>();
        }

        public void CopyToList(List<Product> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                ProductPresenter obj = new ProductPresenter();
                obj.Products = pListDic[i];
                ListProducts.Add(obj);
            }
        }

        void copyInstance()
        {
            dic.ProductID = ProductID;
            dic.ProductName = ProductName;
            dic.CategoryID = CategoryID;
            dic.Price = Price;
            dic.UnitsInStock = UnitsInStock;
            dic.InStockLimit = InStockLimit;
            dic.Active = Active;
            dic.Type = Type;
            dic.ToDate = ToDate;
            dic.Discount = Discount;
            dic.TeenDiscount = TeenDiscount;
            dic.SeniorDiscount = SeniorDiscount;
            dic.DiscountDays = DiscountDays;
            dic.TeenSeniorDays = TeenSeniorDays;
            dic.ButtonColor = ButtonColor;
            dic.TextColor = TextColor;
            dic.Page = Page;
            dic.Col = Col;
            dic.Row = Row;
            dic.ProductImage = ProductImage;
            dic.TeenDays = TeenDays;
            dic.SeniorDays = SeniorDays;
            dic.InItem = InItem;
            dic.Cost = Cost;
            dic.InCatID = InCatID;
            dic.StockedDate = StockedDate;
            dic.RecordLevel = RecordLevel;
            dic.BarCode = BarCode;
            dic.PrintBoth = PrintBoth;
            dic.NoTax = NoTax;
            dic.StockMatch = StockMatch;
            dic.ChoiceProd = ChoiceProd;
            dic.NoPicture = NoPicture;
            dic.Location = Location;
            dic.VendorName = VendorName;
            dic.SizeChoiceTxt = SizeChoiceTxt;
            dic.CategoryName = CategoryName;
            dic.VendorID = VendorID;
            dic.MType = MType;
            dic.StockCount = StockCount;
            dic.WorkTimeSec = WorkTimeSec;
            dic.BPrice = BPrice;
            dic.LPrice = LPrice;
            dic.DPrice = DPrice;
            dic.Price1 = Price1;
            dic.Price2 = Price2;
            dic.ComProd = ComProd;
            dic.CPrice = CPrice;
            dic.NotSaleItem = NotSaleItem;
            dic.PrintStation = PrintStation;
            dic.KitchenName = KitchenName;
            dic.OutOrder = OutOrder;
            dic.Price3 = Price3;
            dic.Price4 = Price4;
            dic.Price5 = Price5;
            dic.Price6 = Price6;
            dic.Price7 = Price7;
            dic.Units = Units;
            dic.LinkProduct = LinkProduct;
            dic.RewardPoints = RewardPoints;
            dic.ProdNo = ProdNo;
            dic.QBuy = QBuy;
            dic.QFree = QFree;
            dic.OnDay = OnDay;
            dic.OnMonth = OnMonth;
            dic.OnYear = OnYear;
            dic.OnDate = OnDate;
            dic.DateKey = DateKey;
            //dic.OnWeek =   OnWeek          ;
            //dic.WeekKey =   WeekKey          ;
            //dic.OnBiWeek =   OnBiWeek          ;
            //dic.BiWeekKey =   BiWeekKey          ;
            //dic.OnSimMonth =             ;
            //dic.SimMonthKey =             ;
            //dic.OnQuarter =             ;
            //dic.QuarterKey =             ;
            //dic.NewObj =             ;
            //dic.ChgeObj =             ;
            //dic.SentObj =             ;
            dic.InitAmount = InitAmount;
            dic.CurrAmount = CurrAmount;
            dic.MiniAmount = MiniAmount;
            dic.UnitAmount = UnitAmount;
            dic.JarWeight = JarWeight;
            dic.ImageID = ImageID;
            dic.QAccountType = QAccountType;
            dic.QAccountID = QAccountID;
            dic.QAccountName = QAccountName;
            //dic.ProdImage =   ProdImage          ;
            //dic.ProdActImage =   ProdActImage          ;
            dic.Description = Description;
            dic.OrderPending = OrderPending;
            dic.InstallUnit = InstallUnit;
            dic.VendorNo = VendorNo;
            dic.Reserved = Reserved;
            dic.ReservedStock = ReservedStock;
            dic.SerialNeeded = SerialNeeded;
            dic.ProductUrl = ProductUrl;
            dic.PType = PType;
            dic.SubLevel = SubLevel;
            dic.ParentID = ParentID;
            dic.ParentName = ParentName;
            dic.EditSequence = EditSequence;
            dic.iPage = iPage;
            dic.ixPos = ixPos;
            dic.iyPos = iyPos;
            dic.iWidth = iWidth;
            dic.iHeight = iHeight;
            dic.iFontSize = iFontSize;
            dic.iButColor = iButColor;
            dic.iTextColor = iTextColor;
            dic.EditTimestamp = EditTimestamp;
            dic.RecordState = RecordState;
            dic.RecordGUID = RecordGUID;
            dic.LType = LType;
            dic.FoodTypeName = FoodTypeName;
            dic.FoodTypeID = FoodTypeID;
            dic.OrderBy = OrderBy;
            dic.ExpandCategoryID = ExpandCategoryID;

        }



        #region Property

        /// <summary>
        /// Get List product size details that belong to Product
        /// </summary>
        public PSizeDetailPresenter ProductSizeDetails
        {
            get
            {
                PSizeDetailPresenter _productSizeDetails = new PSizeDetailPresenter();
                _productSizeDetails.CopyToList(dao.FindByMultiColumnAnd<Product_SizeDetails>(new[] { "PSize_Product_ID" }, ProductID).ToList());
                return _productSizeDetails;
            }
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        public int InStockLimit { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public System.DateTime ToDate { get; set; }
        public decimal Discount { get; set; }
        public decimal TeenDiscount { get; set; }
        public decimal SeniorDiscount { get; set; }
        public int DiscountDays { get; set; }
        public int TeenSeniorDays { get; set; }
        public int ButtonColor { get; set; }
        public int TextColor { get; set; }
        public int Page { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public string ProductImage { get; set; }
        public int TeenDays { get; set; }
        public int SeniorDays { get; set; }
        public bool InItem { get; set; }
        public decimal Cost { get; set; }
        public int InCatID { get; set; }
        public System.DateTime StockedDate { get; set; }
        public int RecordLevel { get; set; }
        public string BarCode { get; set; }
        public bool PrintBoth { get; set; }
        public bool NoTax { get; set; }
        public bool StockMatch { get; set; }
        public bool ChoiceProd { get; set; }
        public bool NoPicture { get; set; }
        public string Location { get; set; }
        public string VendorName { get; set; }
        public string SizeChoiceTxt { get; set; }
        public string CategoryName { get; set; }
        public int VendorID { get; set; }
        public int MType { get; set; }
        public int StockCount { get; set; }
        public int WorkTimeSec { get; set; }
        public decimal BPrice { get; set; }
        public decimal LPrice { get; set; }
        public decimal DPrice { get; set; }
        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public bool ComProd { get; set; }
        public decimal CPrice { get; set; }
        public bool NotSaleItem { get; set; }
        public int PrintStation { get; set; }
        public string KitchenName { get; set; }
        public bool OutOrder { get; set; }
        public decimal Price3 { get; set; }
        public decimal Price4 { get; set; }
        public decimal Price5 { get; set; }
        public decimal Price6 { get; set; }
        public decimal Price7 { get; set; }
        public int Units { get; set; }
        public int LinkProduct { get; set; }
        public int RewardPoints { get; set; }
        public string ProdNo { get; set; }
        public int QBuy { get; set; }
        public int QFree { get; set; }
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
        public float InitAmount { get; set; }
        public float CurrAmount { get; set; }
        public float MiniAmount { get; set; }
        public float UnitAmount { get; set; }
        public float JarWeight { get; set; }
        public int ImageID { get; set; }
        public int QAccountType { get; set; }
        public int QAccountID { get; set; }
        public string QAccountName { get; set; }
        public byte[] ProdImage { get; set; }
        public byte[] ProdActImage { get; set; }
        public string Description { get; set; }
        public bool OrderPending { get; set; }
        public bool InstallUnit { get; set; }
        public string VendorNo { get; set; }
        public int Reserved { get; set; }
        public int ReservedStock { get; set; }
        public bool SerialNeeded { get; set; }
        public string ProductUrl { get; set; }
        public string PType { get; set; }
        public int SubLevel { get; set; }
        public int ParentID { get; set; }
        public string ParentName { get; set; }
        public string EditSequence { get; set; }
        public int iPage { get; set; }
        public float ixPos { get; set; }
        public float iyPos { get; set; }
        public float iWidth { get; set; }
        public float iHeight { get; set; }
        public float iFontSize { get; set; }
        public string iButColor { get; set; }
        public string iTextColor { get; set; }
        public System.DateTime EditTimestamp { get; set; }
        public int RecordState { get; set; }
        public string RecordGUID { get; set; }
        public int LType { get; set; }
        public string FoodTypeName { get; set; }
        public int FoodTypeID { get; set; }
        public int OrderBy { get; set; }
        public int ExpandCategoryID { get; set; }


        #endregion
    }
}
