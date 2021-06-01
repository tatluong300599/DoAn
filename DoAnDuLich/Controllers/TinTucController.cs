using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;
using PagedList.Mvc;
using PagedList;

namespace DoAnDuLich.Controllers
{
    public class TinTucController : Controller
    {
        DoAnDuLichEntities db = new DoAnDuLichEntities();
        // GET: TinTuc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChiTietTin(int maTin)
        {
            ViewBag.LoaiTin = db.LoaiTinTucs;
            TinTuc tin = db.TinTucs.SingleOrDefault(n => n.MaTin == maTin);
            return View(tin);
        }
        public ActionResult ListTintuc(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            var listTinTuc = db.TinTucs;
            return View(listTinTuc.OrderBy(n => n.MaTin).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult TinTucTheoLoai(int maLoaiTin, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            var loaiTin = db.TinTucs.Where(n => n.MaLoaiTin == maLoaiTin);
            ViewBag.TenLoaiTin = db.LoaiTinTucs.SingleOrDefault(n => n.MaLoaiTin == maLoaiTin).TenLoaiTin;
            return View(loaiTin.OrderBy(n => n.MaTin).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult _PartialTimkiem()
        {
            var loaiTins = db.LoaiTinTucs;
            return PartialView(loaiTins);
        }
    }
}