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
    public class QLDiemXPsController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/QLDiemXPs
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                NhanVien nv = Session["Admin"] as NhanVien;
                NhanVien checkNV = db.NhanViens.SingleOrDefault(n => n.MaNV == nv.MaNV && n.Quyen == 0);
                if (checkNV != null)
                {
                    return View(db.DiemXPs.ToList());
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

        // GET: Admin/QLDiemXPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemXP diemXP = db.DiemXPs.Find(id);
            if (diemXP == null)
            {
                return HttpNotFound();
            }
            return View(diemXP);
        }

        // GET: Admin/QLDiemXPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLDiemXPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDiemXP,TenDiemXP,DiaChi")] DiemXP diemXP)
        {
            if (ModelState.IsValid)
            {
                db.DiemXPs.Add(diemXP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diemXP);
        }

        // GET: Admin/QLDiemXPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemXP diemXP = db.DiemXPs.Find(id);
            if (diemXP == null)
            {
                return HttpNotFound();
            }
            return View(diemXP);
        }

        // POST: Admin/QLDiemXPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDiemXP,TenDiemXP,DiaChi")] DiemXP diemXP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemXP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diemXP);
        }

        // GET: Admin/QLDiemXPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemXP diemXP = db.DiemXPs.Find(id);
            if (diemXP == null)
            {
                return HttpNotFound();
            }
            return View(diemXP);
        }

        // POST: Admin/QLDiemXPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemXP diemXP = db.DiemXPs.Find(id);
            db.DiemXPs.Remove(diemXP);
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
