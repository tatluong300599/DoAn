//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnDuLich.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTDonDatTour
    {
        public int MaDon { get; set; }
        public int MaNV { get; set; }
        public Nullable<System.DateTime> TGDuyetDon { get; set; }
        public Nullable<int> TTThanhToan { get; set; }
        public Nullable<System.DateTime> TGThanhToan { get; set; }
    
        public virtual DonDatTour DonDatTour { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
