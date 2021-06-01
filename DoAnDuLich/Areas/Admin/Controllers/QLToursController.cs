using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;

namespace DoAnDuLich.Areas.Admin.Controllers
{
    public class QLToursController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/QLTours
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                var tours = db.Tours.Include(t => t.DiemDen).Include(t => t.DiemXP).Include(t => t.LoaiTour);
                return View(tours.ToList());
            }
            else
            {
                return RedirectToAction("Login", "NhanVien");
            }
            
        }

        // GET: Admin/QLTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Admin/QLTours/Create
        public ActionResult Create()
        {
            ViewBag.MaDiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen");
            ViewBag.MaDiemXP = new SelectList(db.DiemXPs, "MaDiemXP", "TenDiemXP");
            ViewBag.MaLoaiTour = new SelectList(db.LoaiTours, "MaLoaiTour", "TenLoaiTour");
            return View();
        }

        // POST: Admin/QLTours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTour,TenTour,MaLoaiTour,GioiThieu,ChuongTrinh,Anh,Anh1,Anh2,SoNgay,Gia,TrangThai,MaDiemXP,MaDiemDen,DanhGia,LuotDanhGia")] Tour tour, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var fileName = "";
                if (fileUpload != null)
                {
                    fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fileUpload.SaveAs(path);
                    }
                    tour.Anh = fileUpload.FileName;
                }
                tour.TrangThai = true;
                tour.DanhGia = 5;
                tour.LuotDanhGia = 1;
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen", tour.MaDiemDen);
            ViewBag.MaDiemXP = new SelectList(db.DiemXPs, "MaDiemXP", "TenDiemXP", tour.MaDiemXP);
            ViewBag.MaLoaiTour = new SelectList(db.LoaiTours, "MaLoaiTour", "TenLoaiTour", tour.MaLoaiTour);
            return View(tour);
        }

        // GET: Admin/QLTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen", tour.MaDiemDen);
            ViewBag.MaDiemXP = new SelectList(db.DiemXPs, "MaDiemXP", "TenDiemXP", tour.MaDiemXP);
            ViewBag.MaLoaiTour = new SelectList(db.LoaiTours, "MaLoaiTour", "TenLoaiTour", tour.MaLoaiTour);
            return View(tour);
        }

        // POST: Admin/QLTours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTour,TenTour,MaLoaiTour,GioiThieu,ChuongTrinh,Anh,Anh1,Anh2,SoNgay,Gia,TrangThai,MaDiemXP,MaDiemDen,DanhGia,LuotDanhGia")] Tour tour, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var fileName = "";
                if (fileUpload != null)
                {
                    fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fileUpload.SaveAs(path);
                    }
                    tour.Anh = fileUpload.FileName;
                }
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                TempData["thanhcong"] = "Sửa sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["kthanhcong"] = "Sửa sản phẩm thất bại!";
            }
            ViewBag.MaDiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen", tour.MaDiemDen);
            ViewBag.MaDiemXP = new SelectList(db.DiemXPs, "MaDiemXP", "TenDiemXP", tour.MaDiemXP);
            ViewBag.MaLoaiTour = new SelectList(db.LoaiTours, "MaLoaiTour", "TenLoaiTour", tour.MaLoaiTour);
            return View(tour);
        }

        // GET: Admin/QLTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/QLTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult ChiTiet(int maTour)
        {
            ViewData["MaTour"] = maTour;
            var ctTour = db.CTTours.Where(n => n.MaTour == maTour).OrderByDescending(n => n.NgayXP);
            return View(ctTour);
        }
        [HttpGet]
        public ActionResult TaoMoi(int maTour)
        {
            ViewData["MaTour"] = maTour;
            ViewBag.TenTour = db.Tours.SingleOrDefault(n => n.MaTour == maTour).TenTour;
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM");
            return View();
        }
        [HttpPost]
        public ActionResult TaoMoi(FormCollection f)
        {
            try
            {
                CTTour ctt = new CTTour();

                ctt.MaTour = int.Parse(f["MaTour"]);
                ctt.NgayXP = DateTime.Parse(f["NgayXP"]);
                ctt.SoLuongCho = int.Parse(f["SoLuongCho"]);
                ctt.TrangThai = bool.Parse(f["TrangThai"]);
                ctt.MaKM = int.Parse(f["MaKM"]);

                db.CTTours.Add(ctt);
                db.SaveChanges();
                return RedirectToAction("ChiTiet", "QLTours", new { maTour = ctt.MaTour });
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpGet]
        public ActionResult CapNhapNgay(int maTour,DateTime ngayXP)
        {
            CTTour cTTour = db.CTTours.SingleOrDefault(n => n.MaTour == maTour && n.NgayXP == ngayXP);
            if (cTTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTour = new SelectList(db.Tours, "MaTour", "TenTour", cTTour.MaTour);
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", cTTour.MaKM);
            return View(cTTour);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhapNgay([Bind(Include = "MaTour,NgayXP,SoLuongCho,TrangThai,MaKM")] CTTour cTTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTour = new SelectList(db.Tours, "MaTour", "TenTour", cTTour.MaTour);
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", cTTour.MaKM);
            return View(cTTour);
        }
        public ActionResult KhoaCTTour(int maTour,DateTime ngayXP)
        {
            CTTour ctTour = db.CTTours.SingleOrDefault(n => n.MaTour == maTour && n.NgayXP == ngayXP);
            ctTour.TrangThai = false;
            db.SaveChanges();
            return RedirectToAction("ChiTiet","QLTours",new {maTour=maTour });
        }
        public ActionResult MoKhoaCTTour(int maTour, DateTime ngayXP)
        {
            CTTour ctTour = db.CTTours.SingleOrDefault(n => n.MaTour == maTour && n.NgayXP == ngayXP);
            ctTour.TrangThai = true;
            db.SaveChanges();
            return RedirectToAction("ChiTiet", "QLTours", new { maTour = maTour });
        }


        //danh sách khách đặt Tour
        public ActionResult DanhSachKhachDat(int maTour, DateTime ngayXP)
        {
            var don = db.CTDonDatTours.Where(n => n.DonDatTour.MaTour == maTour && n.DonDatTour.NgayXP == ngayXP);
            return View(don);
        }
    }
}
