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
    public class QLLoaiToursController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/QLLoaiTours
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                NhanVien nv = Session["Admin"] as NhanVien;
                NhanVien checkNV = db.NhanViens.SingleOrDefault(n => n.MaNV == nv.MaNV && n.Quyen == 0);
                if (checkNV != null)
                {
                    return View(db.LoaiTours.ToList());
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

        // GET: Admin/QLLoaiTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTour loaiTour = db.LoaiTours.Find(id);
            if (loaiTour == null)
            {
                return HttpNotFound();
            }
            return View(loaiTour);
        }

        // GET: Admin/QLLoaiTours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLLoaiTours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiTour,TenLoaiTour,Anh")] LoaiTour loaiTour)
        {
            if (ModelState.IsValid)
            {
                db.LoaiTours.Add(loaiTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiTour);
        }

        // GET: Admin/QLLoaiTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTour loaiTour = db.LoaiTours.Find(id);
            if (loaiTour == null)
            {
                return HttpNotFound();
            }
            return View(loaiTour);
        }

        // POST: Admin/QLLoaiTours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiTour,TenLoaiTour,Anh")] LoaiTour loaiTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiTour);
        }

        // GET: Admin/QLLoaiTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTour loaiTour = db.LoaiTours.Find(id);
            if (loaiTour == null)
            {
                return HttpNotFound();
            }
            return View(loaiTour);
        }

        // POST: Admin/QLLoaiTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiTour loaiTour = db.LoaiTours.Find(id);
            db.LoaiTours.Remove(loaiTour);
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
