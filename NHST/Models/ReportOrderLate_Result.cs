//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NHST.Models
{
    using System;
    
    public partial class ReportOrderLate_Result
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string MainOrderCode { get; set; }
        public string Barcode { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> DepostiDate { get; set; }
        public Nullable<System.DateTime> DateBuy { get; set; }
        public string StaffNote { get; set; }
        public Nullable<int> totalRow { get; set; }
    }
}
