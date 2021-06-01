using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;

namespace DoAnDuLich.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        DoAnDuLichEntities db = new DoAnDuLichEntities();
        // GET: Admin/NhanVien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KhongCoQuyen()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string email = f["email"].ToString();
            string password = f["password"].ToString();

            NhanVien nv = db.NhanViens.SingleOrDefault(n => n.Email == email && n.MatKhau == password);

            if (nv != null)
            {
                if (nv.Quyen == -1)
                {
                    ViewBag.ThongBao = "Tài Khoản của bạn đã bị khóa bởi hệ thống.Bạn cần liên hệ Admin để có thể mở lại";
                    return View();
                }
                else
                {
                    Session["Admin"] = nv;
                    return RedirectToAction("Index", "HomeAdmin");
                }
                
            }
            else
            {
                ViewBag.ThongBao = "Tai Khoan or Mat Khau khong chinh xac.";
                return View();
            }
        }
        public ActionResult DangXuat()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login", "NhanVien");
        }
    }
}