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
    
    public partial class PaymentType
    {
        public int PaymentTypeID { get; set; }
        public Nullable<bool> Selected { get; set; }
        public Nullable<bool> Active { get; set; }
        public string PayTypeStr { get; set; }
        public Nullable<int> RecordState { get; set; }
        public string RecordGUID { get; set; }
        public string ImgURL { get; set; }
    }
}
