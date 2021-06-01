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
    public class QLCTToursController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/QLCTTours
        public ActionResult Index()
        {
            var cTTours = db.CTTours.Include(c => c.Tour).Include(c => c.KhuyenMai);
            return View(cTTours.ToList());
        }

        // GET: Admin/QLCTTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTTour cTTour = db.CTTours.Find(id);
            if (cTTour == null)
            {
                return HttpNotFound();
            }
            return View(cTTour);
        }

        // GET: Admin/QLCTTours/Create
        public ActionResult Create()
        {
            ViewBag.MaTour = new SelectList(db.Tours, "MaTour", "TenTour");
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM");
            return View();
        }

        // POST: Admin/QLCTTours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTour,NgayXP,SoLuongCho,TrangThai,MaKM")] CTTour cTTour)
        {
            if (ModelState.IsValid)
            {
                db.CTTours.Add(cTTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTour = new SelectList(db.Tours, "MaTour", "TenTour", cTTour.MaTour);
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", cTTour.MaKM);
            return View(cTTour);
        }

        // GET: Admin/QLCTTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTTour cTTour = db.CTTours.Find(id);
            if (cTTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTour = new SelectList(db.Tours, "MaTour", "TenTour", cTTour.MaTour);
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", cTTour.MaKM);
            return View(cTTour);
        }

        // POST: Admin/QLCTTours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTour,NgayXP,SoLuongCho,TrangThai,MaKM")] CTTour cTTour)
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

        // GET: Admin/QLCTTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTTour cTTour = db.CTTours.Find(id);
            if (cTTour == null)
            {
                return HttpNotFound();
            }
            return View(cTTour);
        }

        // POST: Admin/QLCTTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTTour cTTour = db.CTTours.Find(id);
            db.CTTours.Remove(cTTour);
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
