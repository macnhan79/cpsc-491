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
    
    public partial class PhoHa7_Sys_User_Permission
    {
        public int UP_User_ID { get; set; }
        public string UP_Object_ID { get; set; }
        public Nullable<int> UP_Permission { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual PhoHa7_Sys_Object PhoHa7_Sys_Object { get; set; }
    }
}
