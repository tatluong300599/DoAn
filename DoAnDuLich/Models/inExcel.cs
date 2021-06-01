using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnDuLich.Models;
using System.IO;
using OfficeOpenXml;

namespace DoAnDuLich.Models
{

    public class inExcel
    {
        public string XuatExcel(int iddonhang,int manv, string path)
        {
            DoAnDuLichEntities db = new DoAnDuLichEntities();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DonDatTour don = db.DonDatTours.SingleOrDefault(n => n.MaDon == iddonhang);

            NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNV == manv);
            //var donhangdetails = new DonHangDetailsDao().listAll(iddonhang);
            string templateDocument = HttpContext.Current.Server.MapPath("~/Template/HoaDonDuLich.xlsx");
            string documentName = string.Format("{0}-{1}-{2}.xlsx", iddonhang, don.KhachHang.HoTen, DateTime.Now.ToString("yyyyMMdd"));
            string fullpath = Path.Combine(path, documentName);
            if (Directory.Exists(fullpath))
            {
                Directory.Delete(fullpath);
            }
            MemoryStream output = new MemoryStream();
            try
            {
                using (FileStream fs = File.OpenRead(templateDocument))
                {
                    using (ExcelPackage package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets["HuyenTrang"];
                        sheet.Cells[4, 1].Value = "Tên khách hàng : " + don.KhachHang.HoTen;
                        sheet.Cells[5, 1].Value = "Địa chỉ : " + don.KhachHang.DiaChi;
                        sheet.Cells[6, 1].Value = "Số điện thoại: " + don.KhachHang.SoDienThoai;
                        sheet.Cells[7, 1].Value = "Email : " + don.KhachHang.Email;
                        sheet.Cells[8, 1].Value = "Thời gian đặt Tour : " + don.TGDatTour;

                        sheet.Cells[11, 2].Value = don.CTTour.Tour.TenTour;
                        sheet.Cells[12, 2].Value ="Ngày xuất phát:"+ don.NgayXP;

                        sheet.Cells[12, 3].Value = don.SoLuongKhach-don.TreEm;
                        sheet.Cells[15, 3].Value = don.TreEm;

                        sheet.Cells[12, 4].Value = don.CTTour.Tour.Gia * (100 - (don.CTTour.KhuyenMai.PhanTram)) / 100;
                        sheet.Cells[15, 4].Value = don.CTTour.Tour.Gia * (100 - (don.CTTour.KhuyenMai.PhanTram)) / 100;

                        sheet.Cells[23, 2].Value = don.KhachHang.HoTen;
                        sheet.Cells[23, 4].Value = nv.HoTen;
                        package.SaveAs(new FileInfo(fullpath));
                    }
                    return documentName;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string XuatDanhSach(int maTour,DateTime ngayXP)
        {
            return "";
        }
    }
}