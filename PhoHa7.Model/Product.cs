//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoMac.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.Product_SizeDetails = new HashSet<Product_SizeDetails>();
        }
    
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> UnitsInStock { get; set; }
        public Nullable<int> InStockLimit { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> TeenDiscount { get; set; }
        public Nullable<decimal> SeniorDiscount { get; set; }
        public Nullable<int> DiscountDays { get; set; }
        public Nullable<int> TeenSeniorDays { get; set; }
        public Nullable<int> ButtonColor { get; set; }
        public Nullable<int> TextColor { get; set; }
        public Nullable<int> Page { get; set; }
        public Nullable<int> Col { get; set; }
        public Nullable<int> Row { get; set; }
        public string ProductImage { get; set; }
        public Nullable<int> TeenDays { get; set; }
        public Nullable<int> SeniorDays { get; set; }
        public Nullable<bool> InItem { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> InCatID { get; set; }
        public Nullable<System.DateTime> StockedDate { get; set; }
        public Nullable<int> RecordLevel { get; set; }
        public string BarCode { get; set; }
        public Nullable<bool> PrintBoth { get; set; }
        public Nullable<bool> NoTax { get; set; }
        public Nullable<bool> StockMatch { get; set; }
        public Nullable<bool> ChoiceProd { get; set; }
        public Nullable<bool> NoPicture { get; set; }
        public string Location { get; set; }
        public string VendorName { get; set; }
        public string SizeChoiceTxt { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> VendorID { get; set; }
        public Nullable<int> MType { get; set; }
        public Nullable<int> StockCount { get; set; }
        public Nullable<int> WorkTimeSec { get; set; }
        public Nullable<decimal> BPrice { get; set; }
        public Nullable<decimal> LPrice { get; set; }
        public Nullable<decimal> DPrice { get; set; }
        public Nullable<decimal> Price1 { get; set; }
        public Nullable<decimal> Price2 { get; set; }
        public Nullable<bool> ComProd { get; set; }
        public Nullable<decimal> CPrice { get; set; }
        public Nullable<bool> NotSaleItem { get; set; }
        public Nullable<int> PrintStation { get; set; }
        public string KitchenName { get; set; }
        public Nullable<bool> OutOrder { get; set; }
        public Nullable<decimal> Price3 { get; set; }
        public Nullable<decimal> Price4 { get; set; }
        public Nullable<decimal> Price5 { get; set; }
        public Nullable<decimal> Price6 { get; set; }
        public Nullable<decimal> Price7 { get; set; }
        public Nullable<int> Units { get; set; }
        public Nullable<int> LinkProduct { get; set; }
        public Nullable<int> RewardPoints { get; set; }
        public string ProdNo { get; set; }
        public Nullable<int> QBuy { get; set; }
        public Nullable<int> QFree { get; set; }
        public Nullable<int> OnDay { get; set; }
        public Nullable<int> OnMonth { get; set; }
        public Nullable<int> OnYear { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public string DateKey { get; set; }
        public Nullable<int> OnWeek { get; set; }
        public string WeekKey { get; set; }
        public Nullable<int> OnBiWeek { get; set; }
        public string BiWeekKey { get; set; }
        public Nullable<int> OnSimMonth { get; set; }
        public string SimMonthKey { get; set; }
        public Nullable<int> OnQuarter { get; set; }
        public string QuarterKey { get; set; }
        public Nullable<bool> NewObj { get; set; }
        public Nullable<bool> ChgeObj { get; set; }
        public Nullable<bool> SentObj { get; set; }
        public Nullable<float> InitAmount { get; set; }
        public Nullable<float> CurrAmount { get; set; }
        public Nullable<float> MiniAmount { get; set; }
        public Nullable<float> UnitAmount { get; set; }
        public Nullable<float> JarWeight { get; set; }
        public Nullable<int> ImageID { get; set; }
        public Nullable<int> QAccountType { get; set; }
        public Nullable<int> QAccountID { get; set; }
        public string QAccountName { get; set; }
        public byte[] ProdImage { get; set; }
        public byte[] ProdActImage { get; set; }
        public string Description { get; set; }
        public Nullable<bool> OrderPending { get; set; }
        public Nullable<bool> InstallUnit { get; set; }
        public string VendorNo { get; set; }
        public Nullable<int> Reserved { get; set; }
        public Nullable<int> ReservedStock { get; set; }
        public Nullable<bool> SerialNeeded { get; set; }
        public string ProductUrl { get; set; }
        public string PType { get; set; }
        public Nullable<int> SubLevel { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string ParentName { get; set; }
        public string EditSequence { get; set; }
        public Nullable<int> iPage { get; set; }
        public Nullable<float> ixPos { get; set; }
        public Nullable<float> iyPos { get; set; }
        public Nullable<float> iWidth { get; set; }
        public Nullable<float> iHeight { get; set; }
        public Nullable<float> iFontSize { get; set; }
        public string iButColor { get; set; }
        public string iTextColor { get; set; }
        public Nullable<System.DateTime> EditTimestamp { get; set; }
        public Nullable<int> RecordState { get; set; }
        public string RecordGUID { get; set; }
        public Nullable<int> LType { get; set; }
        public string FoodTypeName { get; set; }
        public Nullable<int> FoodTypeID { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> ExpandCategoryID { get; set; }
    
        public virtual ICollection<Product_SizeDetails> Product_SizeDetails { get; set; }
    }
}
