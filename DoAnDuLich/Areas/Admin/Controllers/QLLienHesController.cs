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
    public class QLLienHesController : Controller
    {
        private DoAnDuLichEntities db = new DoAnDuLichEntities();

        // GET: Admin/QLLienHes
        public ActionResult Index()
        {
            return View(db.LienHes.ToList().OrderByDescending(n=>n.ThoiGian));
        }
        public ActionResult DanhSachChuaLienHe()
        {
            var listLienHe = db.LienHes.Where(n => n.TrangThai == false).OrderByDescending(n => n.ThoiGian);
            return View(listLienHe);
        }
        public ActionResult DaLienHe(int maLienHe)
        {
            try
            {
                LienHe lh = db.LienHes.Single(n => n.MaLienHe == maLienHe);
                lh.TrangThai = true;
                db.SaveChanges();
                return RedirectToAction("DanhSachChuaLienHe", "QLLienHes");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "QLLienHes");
            }
            
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
