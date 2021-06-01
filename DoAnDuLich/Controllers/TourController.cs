using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;
using PagedList;

namespace DoAnDuLich.Controllers
{
    public class TourController : Controller
    {
        //public double DonGia { get { return (double.Parse(Tour.Gia.ToString())*(100-int.Parse(KhuyenMai.PhanTram.ToString()))/100); } }

        DoAnDuLichEntities db = new DoAnDuLichEntities();
        // GET: Tour
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult _PartialTourTrongNuoc()
        //{
        //    return PartialView();
        //}
        //public ActionResult _PartialTourNuocNgoai()
        //{
        //    return PartialView();
        //}
        //public ActionResult _PartialDatNhieu()
        //{
        //    return PartialView();
        //}


        public ActionResult _PartialLienQuan()
        {
            return PartialView();
        }
        public ActionResult ChiTietTour(int maTour)
        {
            ViewBag.Tour = db.Tours.Where(n => n.MaTour == maTour);

            int maLoaiTour = LayMaLoaiTour(maTour);

            ViewBag.LienQuan = db.Tours.Where(n => n.MaLoaiTour == maLoaiTour &&  n.MaTour != maTour && n.TrangThai == true);

            ViewBag.BinhLuan = db.BinhLuans.Where(n => n.MaTour == maTour).OrderByDescending(n => n.MaKhachHang);

            var ctTour = db.CTTours.Where(n => n.MaTour == maTour && n.TrangThai == true);

            ViewBag.CountCTT = db.CTTours.Where(n => n.MaTour == maTour && n.TrangThai == true);

            return View(ctTour);
        }
        public int LayMaLoaiTour(int maTour)
        {
            Tour tour = db.Tours.SingleOrDefault(n => n.MaTour == maTour && n.TrangThai == true);
            return Convert.ToInt32(tour.MaLoaiTour);
        }
        public int LayMaDiemKT(int maTour)
        {
            Tour tour = db.Tours.SingleOrDefault(n => n.MaTour == maTour && n.TrangThai == true);
            return Convert.ToInt32(tour.MaDiemDen);
        }
        public ActionResult LoaiTour()
        {
            var loaiTour = db.LoaiTours;
            return View(loaiTour);
        }
        public ActionResult TourTheoLoai(int maLoaiTour, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.TenLoai = db.LoaiTours.Where(n => n.MaLoaiTour == maLoaiTour);
            var tour = db.Tours.Where(n => n.MaLoaiTour == maLoaiTour && n.TrangThai == true);
            if (tour.Count() <= 0)
            {
                ViewBag.ChuaCoTour = "Hiện tại chưa có Tour theo loại";
            }
            return View(tour.OrderBy(n => n.MaTour).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DanhGia(int maTour, int danhGia)
        {
            Tour tour = db.Tours.SingleOrDefault(n => n.MaTour == maTour);
            tour.DanhGia = ((tour.DanhGia * tour.LuotDanhGia) + (danhGia)) / (tour.LuotDanhGia + 1);
            tour.LuotDanhGia = tour.LuotDanhGia + 1;
            db.SaveChanges();
            return RedirectToAction("ChiTietTour", "Tour", new { maTour = maTour });
        }
        public ActionResult BinhLuan(FormCollection f)
        {
            if (Session["TaiKhoan"] == null)
            {
                Response.Write("<script language='javascript'>window.alert('Ban can phai dang nhap truoc moi co the dat tour')</script>");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                KhachHang kh = Session["TaiKhoan"] as KhachHang;
                BinhLuan bl = new BinhLuan();
                bl.MaTour = int.Parse(f["MaTour"].ToString());
                bl.MaKhachHang = kh.MaKhachHang;
                bl.NoiDung = f["NoiDung"];
                bl.DanhGia = int.Parse(f["DanhGia"].ToString());
                db.BinhLuans.Add(bl);

                Tour tour = db.Tours.SingleOrDefault(n => n.MaTour == bl.MaTour);
                tour.DanhGia = ((tour.DanhGia * tour.LuotDanhGia) + (bl.DanhGia)) / (tour.LuotDanhGia + 1);
                tour.LuotDanhGia = tour.LuotDanhGia + 1;
                db.SaveChanges();

                return RedirectToAction("ChiTietTour", "Tour", new { maTour = bl.MaTour });
            }
        }
    }
}