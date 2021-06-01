using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;

namespace DoAnDuLich.Areas.Admin.Controllers
{
    public class QLDiemDensController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/QLDiemDens
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                NhanVien nv = Session["Admin"] as NhanVien;
                NhanVien checkNV = db.NhanViens.SingleOrDefault(n => n.MaNV == nv.MaNV && n.Quyen == 0);
                if (checkNV != null)
                {
                    var diemDens = db.DiemDens.Include(d => d.HanhTrinh);
                    return View(diemDens.ToList());
                }
                else
                {
                    return RedirectToAction("KhongCoQuyen", "NhanVien");
                }

            }
            else
            {
                return RedirectToAction("Login", "NhanVien");
            }
            
        }

        // GET: Admin/QLDiemDens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemDen diemDen = db.DiemDens.Find(id);
            if (diemDen == null)
            {
                return HttpNotFound();
            }
            return View(diemDen);
        }

        // GET: Admin/QLDiemDens/Create
        public ActionResult Create()
        {
            ViewBag.MaHanhTrinh = new SelectList(db.HanhTrinhs, "MaHanhTrinh", "TenHanhTrinh");
            return View();
        }

        // POST: Admin/QLDiemDens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDiemDen,TenDiemDen,MaHanhTrinh,GioiThieu,Anh")] DiemDen diemDen)
        {
            if (ModelState.IsValid)
            {
                db.DiemDens.Add(diemDen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHanhTrinh = new SelectList(db.HanhTrinhs, "MaHanhTrinh", "TenHanhTrinh", diemDen.MaHanhTrinh);
            return View(diemDen);
        }

        // GET: Admin/QLDiemDens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemDen diemDen = db.DiemDens.Find(id);
            if (diemDen == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHanhTrinh = new SelectList(db.HanhTrinhs, "MaHanhTrinh", "TenHanhTrinh", diemDen.MaHanhTrinh);
            return View(diemDen);
        }

        // POST: Admin/QLDiemDens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDiemDen,TenDiemDen,MaHanhTrinh,GioiThieu,Anh")] DiemDen diemDen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemDen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHanhTrinh = new SelectList(db.HanhTrinhs, "MaHanhTrinh", "TenHanhTrinh", diemDen.MaHanhTrinh);
            return View(diemDen);
        }

        // GET: Admin/QLDiemDens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemDen diemDen = db.DiemDens.Find(id);
            if (diemDen == null)
            {
                return HttpNotFound();
            }
            return View(diemDen);
        }

        // POST: Admin/QLDiemDens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemDen diemDen = db.DiemDens.Find(id);
            db.DiemDens.Remove(diemDen);
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
    }
}
