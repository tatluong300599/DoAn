using DoAnDuLich.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DoAnDuLich.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        DoAnDuLichEntities db = new DoAnDuLichEntities();
        // GET: Admin/HomeAdmin
        public ActionResult _PartialHeaderAdmin()
        {
            ViewBag.NewDon = db.DonDatTours.Where(n => n.TrangThai == false);
            ViewBag.LienHe = db.LienHes.Where(n => n.TrangThai == false);
            return PartialView();
        }
        public ActionResult Index(DateTime? NgayA, DateTime? NgayB)
        {
            if (Session["Admin"] != null)
            {
                DateTime homNay = DateTime.Today;
                DateTime thangTruoc = homNay.AddMonths(-1);
                DateTime quyTruoc = homNay.AddMonths(-3);
                ViewBag.TongTienThang = db.DonDatTours.Where(n => n.TTThanhToan == true && (n.TGDatTour <= homNay && n.TGDatTour.Value.Month >= homNay.Month)).ToList().Sum(n => n.ThanhTien);
                ViewBag.TongTienQuy = db.DonDatTours.Where(n => n.TTThanhToan == true && (n.TGDatTour <= homNay && n.TGDatTour >= quyTruoc)).ToList().Sum(n => n.ThanhTien);

                ViewBag.CTTour = db.CTTours.Where(n => n.TrangThai == true).OrderByDescending(n => n.NgayXP);

                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, today.Day);
                var twoMonthsAgo = today.AddMonths(-1);
                var a = NgayA;
                var b = NgayB;
                //if (NgayA == null)
                //{
                //    b = today;
                //    a = twoMonthsAgo;
                //}
                TempData["NgayA"] = a;
                TempData["NgayB"] = b;

                string filePath = @"D:\nguoitruycap.txt";
                string testData = System.IO.File.ReadAllText(filePath);
                ViewBag.LuotTruyCap = testData;

                var lstKQ = db.DonDatTours.Where(n => n.TTThanhToan == true && (n.TGDatTour >= a && n.TGDatTour <= b)).ToList();
                if (a == null || b == null)
                {
                    TempData["DoanhThuTheoNgay"] = "0";
                    //return RedirectToAction("Index", "ThongKe");
                }
                else
                {

                    TempData["DoanhThuTheoNgay"] = lstKQ.Sum(n => n.ThanhTien);
                    //return RedirectToAction("Index", "ThongKe");
                }
                ViewBag.Don = lstKQ;
                SoDo();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "NhanVien");
            }
        }
        public ActionResult ListDonDatTour()
        {
            var donDatTour = db.DonDatTours.Where(n => n.TrangThai == false).OrderByDescending(n => n.TGDatTour);
            return View(donDatTour);
        }
        public ActionResult ListChuaThanhToan()
        {
            var chuaTT = db.CTDonDatTours.Where(n => n.TTThanhToan == 0);
            return View(chuaTT);
        }
        public ActionResult ThanhToan(int maDon)
        {
            CTDonDatTour don = db.CTDonDatTours.Single(n => n.MaDon == maDon);
            don.TTThanhToan = 1;
            db.SaveChanges();
            return RedirectToAction("ListChuaThanhToan");
        }
        public ActionResult DuyetDon(int maDon, int maTour, DateTime ngayXP, int soLuong, KhachHang kh)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "NhanVien");
            }
            else
            {
                try
                {
                    NhanVien nv = Session["Admin"] as NhanVien;
                    DonDatTour don = db.DonDatTours.Single(n => n.MaDon == maDon);
                    don.TrangThai = true;
                    CTTour ctTour = db.CTTours.Single(n => n.MaTour == maTour && n.NgayXP == ngayXP);
                    ctTour.SoLuongCho = ctTour.SoLuongCho - soLuong;
                    CTDonDatTour cTDon = new CTDonDatTour();
                    cTDon.MaDon = don.MaDon;
                    cTDon.MaNV = nv.MaNV;
                    cTDon.TTThanhToan = 0;
                    cTDon.TGDuyetDon = DateTime.Now;
                    cTDon.TGThanhToan = null;
                    db.CTDonDatTours.Add(cTDon);
                    db.SaveChanges();

                    string userContect = System.IO.File.ReadAllText(Server.MapPath("~/Template/MailKhachHang.html"));
                    userContect = userContect.Replace("{{MaTour}}", don.MaTour.ToString());
                    userContect = userContect.Replace("{{TenTour}}", don.CTTour.Tour.TenTour.ToString());
                    userContect = userContect.Replace("{{NgayXP}}", don.NgayXP.ToString());
                    userContect = userContect.Replace("{{SoLuongCho}}", don.SoLuongKhach.ToString());
                    new MailHelper().SendMail(don.KhachHang.Email, "Dat Tour Thanh Cong", userContect);
                    return RedirectToAction("ListDonDatTour");
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            

        }
        public ActionResult ChiTietDonDatTour(int maDon)
        {
            DonDatTour donDat = db.DonDatTours.SingleOrDefault(n => n.MaDon == maDon);
            return View(donDat);
        }
        public ActionResult ListDonALl()
        {
            var don = db.CTDonDatTours;
            return View(don);
        }
        public ActionResult XoaDonDat(int maDon)
        {
            DonDatTour don = db.DonDatTours.SingleOrDefault(n => n.MaDon == maDon);
            db.DonDatTours.Remove(don);
            db.SaveChanges();
            return RedirectToAction("ListDonDatTour");
        }

        public ActionResult DanhSachBinhLuan()
        {
            var binhLuan = db.BinhLuans;
            return View(binhLuan);
        }
        public ActionResult XoaBinhLuan(int maBinhLuan)
        {
            BinhLuan binhLuan = db.BinhLuans.Single(n => n.MaBinhLuan == maBinhLuan);
            db.BinhLuans.Remove(binhLuan);
            db.SaveChanges();
            return RedirectToAction("DanhSachBinhLuan");
        }
        //private string GenerateOrder(int orderID)
        //{
        //    var folderReport = ConfigurationManager.AppSettings["ReportFloder"];
        //    string filePath = Server.MapPath(folderReport);
        //    string templateDocument = Server.MapPath("~/Template/HoaDonDuLich.xlsx");

        //    string documentName = string.Format("Order-{0}-{1}", orderID, DateTime.Now.ToString("yyyyMMddhhmmsss"));
        //    string fullPath = Path.Combine(filePath,documentName);
        //    if (Directory.Exists(fullPath))
        //    {
        //        Directory.Delete(fullPath);
        //    }
        //    MemoryStream output = new MemoryStream();
            
        //    try
        //    {
        //        using (FileStream templateDocumentStream = File)
        //        {
        //            using(ExcelPackage package=new ExcelPackage(templateDocumentStream))
        //            {
        //                ExcelWorksheet sheet = package.Workbook.Worksheets["HuyenTrang"];
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return "";
        //}
        public ActionResult HoaDon(int maDon)
        {
            NhanVien nv = Session["Admin"] as NhanVien;
            var folderReport = ConfigurationManager.AppSettings["ReportFloder"];
            inExcel inx= new inExcel();
            inx.XuatExcel(maDon,nv.MaNV, Server.MapPath(folderReport));

            return RedirectToAction("Index", "HomeAdmin");
        }
        public JsonResult SoDo()
        {
           int thang = DateTime.Today.Month;
           int nam = DateTime.Today.Year;
            List<double> list = new List<double>();
            var don = db.DonDatTours.Where(n => n.TTThanhToan == true && (n.TGDatTour.Value.Month <= thang && n.TGDatTour.Value.Year >= nam));
            for (int i = 1; i <= thang; i++)
            {
                double tongkethang = 0;
                foreach (var item in don.Where(x => x.TGDatTour.Value.Month == i))
                {
                    tongkethang = tongkethang + (double)(item.ThanhTien);
                }
                list.Add(tongkethang);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult SoDo()
        //{
        //    List<CTDonDatTour> ctdon = db.CTDonDatTours.ToList();
        //    List<double> ls = new List<double> { 100, 1000, 100000, 20020200 };
        //    return Json(ls, JsonRequestBehavior.AllowGet);
        //}
        public List<double> ThongKeTheoThang(int thang, int nam)
        {
            List<double> list = new List<double>();
            var don = db.DonDatTours.Where(n => n.TTThanhToan == true && (n.TGDatTour.Value.Month <= thang && n.TGDatTour.Value.Year >= nam));
            //var result = from dh in db.DonDatTours.Where(x => x.TGDatTour.Value.Month <= thang & x.TGDatTour.Value.Year == nam & x.TrangThai == true)
            //             join dhd in db.CTDonDatTours on dh.MaDon equals dhd.MaDon
            //             select new ThongKeView()
            //             {
            //                 SoLuong = int.Parse(dh.SoLuongKhach.ToString()),
            //                 DonGia = double.Parse(dh.CTTour.Tour.Gia.ToString()),
            //                 Ngay = DateTime.Parse(dh.TGDatTour.ToString()),
            //             };
            for (int i = 1; i <= thang; i++)
            {
                double tongkethang = 0;
                foreach (var item in don.Where(x => x.TGDatTour.Value.Month == i))
                {
                    tongkethang = tongkethang + (double)(item.ThanhTien);
                }
                list.Add(tongkethang);
            }
            return list;
        }
    }
}