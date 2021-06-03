using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;

namespace DoAnDuLich.Controllers
{
    public class HomeController : Controller
    {
        DoAnDuLichEntities db = new DoAnDuLichEntities();
        public ActionResult Index()
        {
            ViewBag.listTrongNuoc = db.Tours.Where(n => n.MaLoaiTour == 1 && n.TrangThai == true);
            ViewBag.listNuocNgoai = db.Tours.Where(n => n.MaLoaiTour == 2 && n.TrangThai == true);
            ViewBag.listHot = db.Tours.Where(n => n.MaLoaiTour == 3 && n.TrangThai == true);
            ViewBag.listTinTuc = db.TinTucs.OrderByDescending(n => n.TGDangTin);

            var datNhieu = from don in db.DonDatTours
                           group don.SoLuongKhach by don.MaTour into donGrourp
                           select new
                           {
                               MaTour = donGrourp.Key,
                               Count = donGrourp.Count()
                           };

            ViewBag.DatNhieu = from a in datNhieu
                               join tour in db.Tours on a.MaTour equals (tour.MaTour)
                               orderby a.Count descending
                               select tour;
            ViewBag.DiemXP = new SelectList(db.DiemXPs, "MaDiemXP", "TenDiemXP");
            ViewBag.DiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen");

            var allCTT = db.CTTours.Where(n => n.TrangThai == true && n.Tour.MaLoaiTour == 1);
            ViewBag.CTT = allCTT.GroupBy(p => p.MaTour)
                                .Select(g => g.FirstOrDefault())
                                .ToList();


            return View();
        }
        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LienHe(LienHe lh)
        {
            try
            {
                lh.HoTen = Request.Form["HoTen"].ToString();
                lh.DiaChi = Request.Form["DiaChi"].ToString();
                lh.SoDienThoai = Request.Form["SoDienThoai"].ToString();
                lh.Email = Request.Form["Email"].ToString();
                lh.NoiDung = Request.Form["NoiDung"].ToString();
                lh.ThoiGian = DateTime.Now;
                lh.TrangThai = false;
                db.LienHes.Add(lh);
                db.SaveChanges();
                ViewBag.ThongBao = "Cảm ơn bạn đã phản hồi thông tin.Cty sẽ liên hệ bạn trong thời gian sớm nhất";
                return View(lh);
            }
            catch (Exception)
            {
                return View(lh);
            }
            
        }
        public int diemDen()
        {
            return 1;
        }

        #region Hien Thi Home
        public ActionResult _PartialTourTrongNuoc()
        {
            return PartialView();
        }
        public ActionResult _PartialTourNuocNgoai()
        {
            return PartialView();
        }
        public ActionResult _PartialDatNhieu()
        {
            return PartialView();
        }
        public ActionResult _PartialTinTuc()
        {
            return PartialView();
        }
        #endregion

    }
}