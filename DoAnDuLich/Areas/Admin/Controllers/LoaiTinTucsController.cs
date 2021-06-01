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
    public class LoaiTinTucsController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/LoaiTinTucs
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                NhanVien nv = Session["Admin"] as NhanVien;
                NhanVien checkNV = db.NhanViens.SingleOrDefault(n => n.MaNV == nv.MaNV && n.Quyen == 0);
                if (checkNV != null)
                {
                    return View(db.LoaiTinTucs.ToList());
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

        // GET: Admin/LoaiTinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTinTuc loaiTinTuc = db.LoaiTinTucs.Find(id);
            if (loaiTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(loaiTinTuc);
        }

        // GET: Admin/LoaiTinTucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiTinTucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiTin,TenLoaiTin")] LoaiTinTuc loaiTinTuc)
        {
            if (ModelState.IsValid)
            {
                db.LoaiTinTucs.Add(loaiTinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiTinTuc);
        }

        // GET: Admin/LoaiTinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTinTuc loaiTinTuc = db.LoaiTinTucs.Find(id);
            if (loaiTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(loaiTinTuc);
        }

        // POST: Admin/LoaiTinTucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiTin,TenLoaiTin")] LoaiTinTuc loaiTinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiTinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiTinTuc);
        }

        // GET: Admin/LoaiTinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTinTuc loaiTinTuc = db.LoaiTinTucs.Find(id);
            if (loaiTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(loaiTinTuc);
        }

        // POST: Admin/LoaiTinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiTinTuc loaiTinTuc = db.LoaiTinTucs.Find(id);
            db.LoaiTinTucs.Remove(loaiTinTuc);
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
