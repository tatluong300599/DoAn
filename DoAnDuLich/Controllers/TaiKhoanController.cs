using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;

namespace DoAnDuLich.Controllers
{
    public class TaiKhoanController : Controller
    {
        DoAnDuLichEntities db = new DoAnDuLichEntities();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string taiKhoan, string matKhau)
        {
            var khachHang = db.KhachHangs.SingleOrDefault(n => n.Email == taiKhoan && n.MatKhau == matKhau && n.TrangThai==true);
            if (khachHang != null)
            {
                Session["TaiKhoan"] = khachHang;
                //string filePath = @"D:\nguoitruycap.txt";
                //string testData = System.IO.File.ReadAllText(filePath);
                //System.IO.File.Delete(filePath);
                //int songuoituycap = Convert.ToInt32(testData) + 1;
                //System.IO.File.WriteAllText(filePath, songuoituycap.ToString());
                return Json(new { status = 1 });
            }
            else
            {
                return Json(new { status = -1 });
            }
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DangKyTest()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKyTest(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                KhachHang kh = new KhachHang();
                kh.MatKhau = f["MatKhau"];
                kh.HoTen = f["HoTen"];
                kh.SoDienThoai = f["SoDienThoai"];
                kh.Email = f["Email"];
                kh.DiaChi = f["DiaChi"];
                kh.TrangThai = true;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DangKy(string hoTen,string SDT,string email,string diaChi,string matKhau)
        {
            KhachHang kh = new KhachHang();
            kh.MatKhau = matKhau;
            kh.HoTen = hoTen;
            kh.SoDienThoai = SDT;
            kh.Email = email;
            kh.DiaChi = diaChi;
            kh.TrangThai = true;
            var checkKH = db.KhachHangs.SingleOrDefault(n => n.Email == kh.Email);
            if (checkKH == null)
            {
                try
                {
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                    return Json(new { status = 1,model= kh });
                }
                catch (Exception)
                {
                    return Json(new { status = 0, model = kh });
                }
                
            }
            else
            {
                ViewBag.TaiKhoanDaCo = "Email này đã tồn tại trong hệ thống!";
                return Json(new { status = -1, model = kh });
            }


        }

        public ActionResult ThongTinCaNhan()
        {
            if (Session["TaiKhoan"] != null)
            {
                KhachHang kh = Session["TaiKhoan"] as KhachHang;
                KhachHang khach = db.KhachHangs.SingleOrDefault(n => n.MaKhachHang == kh.MaKhachHang);
                return View(khach);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}