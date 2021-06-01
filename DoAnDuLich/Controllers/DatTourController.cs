using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;
using PagedList;
namespace DoAnDuLich.Controllers
{
    //public double ThanhTien { get { return (double.Parse(CTTour.DonGia.ToString()) * int.Parse(SoLuongKhach.ToString()))-(double.Parse(CTTour.DonGia.ToString()) * 0.25 * int.Parse(TreEm.ToString())); } }
    public class DatTourController : Controller
    {
        DoAnDuLichEntities db = new DoAnDuLichEntities();
        // GET: DatTour
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DatTour(int maTour, DateTime ngayXP)
        {
            CTTour ctTour = db.CTTours.SingleOrDefault(n => n.MaTour == maTour && n.NgayXP == ngayXP);
            return View(ctTour);

        }
        public JsonResult DatTour(int maTour, string ngayXP, int tongSL, int treEm, string ghiChu, string thanhToan)
        {
            if (Session["TaiKhoan"] == null)
            {
                return Json(new { status = 0 });
            }
            else
            {
                KhachHang kh = Session["TaiKhoan"] as KhachHang;
                List<DonDatTour> listDon = db.DonDatTours.Where(n => n.MaKhachHang == kh.MaKhachHang && n.TrangThai==false).ToList();
                if (listDon.Count() >= 1)
                {
                    return Json(new { status = 2 });
                }
                else
                {
                    try
                    {
                        DonDatTour don = new DonDatTour();
                        don.MaKhachHang = kh.MaKhachHang;
                        don.MaTour = maTour;
                        don.NgayXP = DateTime.Parse(ngayXP);
                        don.SoLuongKhach = tongSL;
                        don.TreEm = treEm;
                        don.GhiChu = ghiChu;
                        don.TGDatTour = DateTime.Now;
                        don.PTThanhToan = thanhToan;
                        don.TrangThai = false;
                        don.TTThanhToan = false;
                        db.DonDatTours.Add(don);
                        db.SaveChanges();
                        return Json(new { status = 1 });
                    }
                    catch (Exception)
                    {

                        return Json(new { status = -1 });
                    }
                }
            }

        }
        public ActionResult _PartialMenuKhachHang()
        {
            return PartialView();
        }
        public ActionResult CapNhapDon(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                int maDon = int.Parse(f["maDon"]);
                int soLuong = int.Parse(f["TongKhach"]);
                int treEm = int.Parse(f["TreEm"]);
                string ghiChu = f["GhiChu"];

                DonDatTour don = db.DonDatTours.SingleOrDefault(n => n.MaDon == maDon);
                don.SoLuongKhach = soLuong;
                don.TreEm = treEm;
                don.GhiChu = ghiChu;
                db.SaveChanges();
                return RedirectToAction("DonChuaDuyet", "DatTour");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public ActionResult HuyDonDatTour(int maDon)
        {
            if (Session["TaiKhoan"] != null)
            {
                KhachHang kh = Session["TaiKhoan"] as KhachHang;
                DonDatTour don = db.DonDatTours.SingleOrDefault(n => n.MaDon == maDon && n.MaKhachHang == kh.MaKhachHang);
                db.DonDatTours.Remove(don);
                db.SaveChanges();
                return RedirectToAction("DonChuaDuyet", "DatTour");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult DonVuaDat()
        {
            KhachHang kh = Session["TaiKhoan"] as KhachHang;
            DonDatTour don = db.DonDatTours.Where(n => n.MaKhachHang == kh.MaKhachHang && n.TrangThai == false).OrderByDescending(n => n.TGDatTour).FirstOrDefault();
            return View(don);
        }
        public ActionResult DonChuaDuyet()
        {
            if (Session["TaiKhoan"] != null)
            {
                KhachHang kh = Session["TaiKhoan"] as KhachHang;
                var don = db.DonDatTours.Where(n => n.MaKhachHang == kh.MaKhachHang && n.TrangThai == false).OrderByDescending(n => n.TGDatTour);
                return View(don);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult DonChuaThanhToan()
        {
            KhachHang kh = Session["TaiKhoan"] as KhachHang;
            var don = db.DonDatTours.Where(n => n.MaKhachHang == kh.MaKhachHang && n.TTThanhToan == false && n.TrangThai==true);
            return View(don);
        }
        public ActionResult ThanhToan(int maDon)
        {
            CTDonDatTour ctd = db.CTDonDatTours.SingleOrDefault(n => n.MaDon == maDon);
            ctd.TTThanhToan = 1;
            DonDatTour don = db.DonDatTours.SingleOrDefault(n => n.MaDon == maDon);
            don.TTThanhToan = true;
            db.SaveChanges();
            return RedirectToAction("DonChuaThanhToan", "DatTour");
        }
        public ActionResult LichSuDatTour(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            KhachHang kh = Session["TaiKhoan"] as KhachHang;
            var don = db.DonDatTours.Where(n => n.MaKhachHang == kh.MaKhachHang && n.TrangThai == true && n.TTThanhToan == true);
            return View(don.OrderByDescending(n => n.TGDatTour).ToPagedList(pageNumber, pageSize));
        }

    }
}