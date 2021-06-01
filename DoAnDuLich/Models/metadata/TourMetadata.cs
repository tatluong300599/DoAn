using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using thu vien metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DoAnDuLich.Models
{
    [MetadataType(typeof(TourMetadata))]
    public partial class Tour
    { }
    public partial class TourMetadata
    {
        
        [Display(Name = "Số ngày tham gia Tour")]
        [Range(1,365,ErrorMessage ="Số ngày tham gia tour nhỏ nhất là 1 lớn nhất là 365")]
        public Nullable<int> SoNgay { get; set; }

        [Display(Name = "Tên Tour")]
        [Required(ErrorMessage = "không được bỏ trống")]
        [MinLength(10,ErrorMessage ="Độ dài ngắn nhất là 10")]
        public string TenTour { get; set; }
    }
}