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
    
    public partial class DiemDen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiemDen()
        {
            this.Tours = new HashSet<Tour>();
        }
    
        public int MaDiemDen { get; set; }
        public string TenDiemDen { get; set; }
        public Nullable<int> MaHanhTrinh { get; set; }
        public string GioiThieu { get; set; }
        public string Anh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours { get; set; }
        public virtual HanhTrinh HanhTrinh { get; set; }
    }
}
