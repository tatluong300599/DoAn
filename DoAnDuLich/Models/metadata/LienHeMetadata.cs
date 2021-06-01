using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using thu vien metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoAnDuLich.Models
{
    [MetadataType(typeof(LienHeMetadata))]
    public partial class LienHe
    { }
    public partial class LienHeMetadata
    {
        [Display(Name = "Ma lien he")]
        public int MaLienHe { get; set; }
        [Display(Name = "Thoi Gian lien he den")]
        public Nullable<System.DateTime> ThoiGian { get; set; }
        [Display(Name = "Ho ten khach hang")]
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string NoiDung { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public string DiaChi { get; set; }
    }
}