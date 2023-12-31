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
    
    public partial class DaySale
    {
        public int DaySaleID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public string StoreName { get; set; }
        public string Location { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public Nullable<int> NumberEmp { get; set; }
        public Nullable<int> TickCount { get; set; }
        public Nullable<decimal> CashOnHand { get; set; }
        public Nullable<decimal> CashGCard { get; set; }
        public Nullable<decimal> CheckGCard { get; set; }
        public Nullable<decimal> CreditGCard { get; set; }
        public Nullable<decimal> GiftGCard { get; set; }
        public Nullable<decimal> PaidCash { get; set; }
        public Nullable<decimal> PaidCheck { get; set; }
        public Nullable<decimal> PaidCredit { get; set; }
        public Nullable<decimal> PaidGift { get; set; }
        public Nullable<decimal> PaidGCard { get; set; }
        public Nullable<decimal> PaidMisc { get; set; }
        public Nullable<decimal> Tips { get; set; }
        public Nullable<decimal> TotalSale { get; set; }
        public Nullable<decimal> GCardActivate { get; set; }
        public Nullable<decimal> TotalTicket { get; set; }
        public Nullable<decimal> GrandTotal { get; set; }
        public string Notes { get; set; }
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
        public Nullable<int> RecordState { get; set; }
        public string RecordGUID { get; set; }
        public Nullable<System.DateTime> EditTimestamp { get; set; }
    }
}
