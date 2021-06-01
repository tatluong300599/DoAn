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
    public class QLTinTucsController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/QLTinTucs
        public ActionResult Index()
        {
            var tinTucs = db.TinTucs.Include(t => t.LoaiTinTuc);
            return View(tinTucs.ToList());
        }

        // GET: Admin/QLTinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: Admin/QLTinTucs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiTin = new SelectList(db.LoaiTinTucs, "MaLoaiTin", "TenLoaiTin");
            return View();
        }

        // POST: Admin/QLTinTucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTin,TieuDe,Anh,TGDangTin,MaLoaiTin,NoiDung,GioiThieu")] TinTuc tinTuc, HttpPostedFileBase fileUpload)
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
                    tinTuc.Anh = fileUpload.FileName;
                }
                tinTuc.TGDangTin = DateTime.Now;
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiTin = new SelectList(db.LoaiTinTucs, "MaLoaiTin", "TenLoaiTin", tinTuc.MaLoaiTin);
            return View(tinTuc);
        }

        // GET: Admin/QLTinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiTin = new SelectList(db.LoaiTinTucs, "MaLoaiTin", "TenLoaiTin", tinTuc.MaLoaiTin);
            return View(tinTuc);
        }

        // POST: Admin/QLTinTucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTin,TieuDe,Anh,TGDangTin,MaLoaiTin,NoiDung,GioiThieu")] TinTuc tinTuc, HttpPostedFileBase fileUpload)
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
                    tinTuc.Anh = fileUpload.FileName;
                }
                tinTuc.TGDangTin = DateTime.Now;
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiTin = new SelectList(db.LoaiTinTucs, "MaLoaiTin", "TenLoaiTin", tinTuc.MaLoaiTin);
            return View(tinTuc);
        }

        // GET: Admin/QLTinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/QLTinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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
