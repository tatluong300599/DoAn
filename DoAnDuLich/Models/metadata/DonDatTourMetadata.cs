using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DoAnDuLich.Models;

namespace DoAnDuLich.Models
{
    
    [MetadataType(typeof(DonDatTourMetadata))]
    public partial class DonDatTour
    {
        internal sealed class DonDatTourMetadata
        {
            
            public int MaDon { get; set; }
            public Nullable<int> MaKhachHang { get; set; }
            public Nullable<int> MaTour { get; set; }
            public Nullable<System.DateTime> NgayXP { get; set; }
            public Nullable<System.DateTime> TGDatTour { get; set; }
            public Nullable<int> SoLuongKhach { get; set; }
            public string GhiChu { get; set; }
            public string PTThanhToan { get; set; }
            public Nullable<bool> TrangThai { get; set; }
            public Nullable<bool> TTThanhToan { get; set; }
            public Nullable<int> TreEm { get; set; }
            //public double ThanhTien { get { return (double.Parse(CTTour.Tour.Gia.ToString()) * int.Parse(SoLuongKhach.ToString())) - (double.Parse(CTTour.Tour.Gia.ToString()) * 0.25 * int.Parse(TreEm.ToString())); } }

        }
    }
}