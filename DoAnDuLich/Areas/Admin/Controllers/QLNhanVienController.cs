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
    public class QLNhanVienController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/QLNhanVien
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                NhanVien nv = Session["Admin"] as NhanVien;
                NhanVien checkNV = db.NhanViens.SingleOrDefault(n => n.MaNV == nv.MaNV && n.Quyen == 0);
                if (checkNV != null)
                {
                    return View(db.NhanViens.ToList());
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

        // GET: Admin/QLNhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/QLNhanVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLNhanVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoTen,SoDienThoai,Email,DiaChi,MatKhau,Quyen")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhanVien);
        }

        // GET: Admin/QLNhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/QLNhanVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoTen,SoDienThoai,Email,DiaChi,MatKhau,Quyen")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: Admin/QLNhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/QLNhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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


        public ActionResult KhoaNV(int maNV)
        {
            try
            {
                NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNV == maNV);
                nv.Quyen = -1;
                db.SaveChanges();
                return RedirectToAction("Index", "QLNhanVien");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult MoKhoaNV(int maNV)
        {
            try
            {
                NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNV == maNV);
                nv.Quyen = 1;
                db.SaveChanges();
                return RedirectToAction("Index","QLNhanVien");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
