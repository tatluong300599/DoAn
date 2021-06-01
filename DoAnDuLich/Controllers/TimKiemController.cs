using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDuLich.Models;

namespace DoAnDuLich.Controllers
{
    public class TimKiemController : Controller
    {
        DoAnDuLichEntities db = new DoAnDuLichEntities();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KQTimKiem(FormCollection f)
        {
            int diemXP = int.Parse(f["DiemXP"].ToString());
            var diemDen = f["DiemDen"].ToString();
            var ngayXP = f["NgayXP"].ToString();
            var ngayKT = f["NgayKT"].ToString();
            if(ngayXP=="" || ngayKT=="")
            {
                if (diemDen == "")
                {
                    var listDuLich = db.Tours.Where(n => n.DiemXP.MaDiemXP == diemXP && n.TrangThai == true);
                    int kq = listDuLich.Count();
                    string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                    ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1}", kq, tenDiemXP);
                    return View(listDuLich);
                }
                else
                {
                    int diemKT = int.Parse(diemDen);
                    var listDuLich = db.Tours.Where(n => n.DiemXP.MaDiemXP == diemXP && n.DiemDen.MaDiemDen == diemKT && n.TrangThai == true);
                    int kq = listDuLich.Count();
                    string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                    string tenDiemDen = db.DiemDens.SingleOrDefault(n => n.MaDiemDen == diemKT).TenDiemDen;
                    ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1} đến {2}.", kq, tenDiemXP,tenDiemDen);
                    return View(listDuLich);
                }
            }
            else
            {
                DateTime ngay1 = DateTime.Parse(ngayXP);
                DateTime ngay2 = DateTime.Parse(ngayKT);
                if (diemDen == "")
                {
                    var listDuLich = (from t in db.Tours
                                       join ctt in db.CTTours on t.MaTour equals ctt.MaTour
                                       where ctt.NgayXP > ngay1 && ctt.NgayXP < ngay2 && t.MaDiemXP == diemXP && t.TrangThai == true
                                      select t);
                    int kq = listDuLich.Count();
                    string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                    ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1}", kq, tenDiemXP);
                    return View(listDuLich);
                }
                else
                {
                    int diemKT = int.Parse(diemDen);
                    var listDuLich = (from t in db.Tours
                                   join ctt in db.CTTours on t.MaTour equals ctt.MaTour
                                   where ctt.NgayXP > ngay1 && ctt.NgayXP < ngay2 && t.MaDiemXP == diemXP && t.MaDiemDen== diemKT && t.TrangThai == true
                                      select t);
                    int kq = listDuLich.Count();
                    string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                    string tenDiemDen = db.DiemDens.SingleOrDefault(n => n.MaDiemDen == diemKT).TenDiemDen;
                    ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1} đến {2}.", kq, tenDiemXP, tenDiemDen);
                    return View(listDuLich);
                }
            }
        }
        public ActionResult _PartialTimKiem()
        {
            ViewBag.DiemXP = new SelectList(db.DiemXPs, "MaDiemXP", "TenDiemXP");
            ViewBag.DiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen");
            //ViewBag.DiemXP = db.DiemXPs;
            //ViewBag.DiemKT = db.DiemDens;
            return PartialView();
        }
        public ActionResult KQTimKiemTuForm(FormCollection f)
        {
            int diemXP = int.Parse(f["DiemXP"].ToString());
            var diemDen = f["DiemDen"].ToString();
            var ngayXP = f["NgayXP1"].ToString();
            var ngayKT = f["NgayXP2"].ToString();
            var giaMin = f["GiaMin"].ToString();
            var giaMax = f["GiaMax"].ToString();
            if (ngayXP == "" || ngayKT == "")
            {
                if (diemDen == "")
                {
                    if (giaMin == "GiaMin" || giaMax == "GiaMax")
                    {
                        var listDuLich = db.Tours.Where(n => n.DiemXP.MaDiemXP == diemXP && n.TrangThai==true);
                        int kq = listDuLich.Count();
                        string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                        ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1}", kq, tenDiemXP);

                        return View(listDuLich);
                    }
                    else
                    {
                        int giaMn = int.Parse(giaMin);
                        int giaMx = int.Parse(giaMax);
                        var listDuLich = db.Tours.Where(n => n.DiemXP.MaDiemXP == diemXP && n.Gia >= giaMn && n.Gia <= giaMx && n.TrangThai == true);
                        int kq = listDuLich.Count();
                        string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                        ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1}", kq, tenDiemXP);

                        return View(listDuLich);
                    }
                }
                else
                {
                    if (giaMin == "GiaMin" || giaMax == "GiaMax")
                    {
                        int diemKT = int.Parse(diemDen);
                        var listDuLich = db.Tours.Where(n => n.DiemXP.MaDiemXP == diemXP && n.DiemDen.MaDiemDen == diemKT && n.TrangThai == true);
                        int kq = listDuLich.Count();
                        string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                        string tenDiemDen = db.DiemDens.SingleOrDefault(n => n.MaDiemDen == diemKT).TenDiemDen;
                        ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1} đến {2}.", kq, tenDiemXP, tenDiemDen);
                        return View(listDuLich);
                    }
                    else
                    {
                        int giaMn = int.Parse(giaMin);
                        int giaMx = int.Parse(giaMax);
                        int diemKT = int.Parse(diemDen);
                        var listDuLich = db.Tours.Where(n => n.DiemXP.MaDiemXP == diemXP && n.DiemDen.MaDiemDen == diemKT && n.Gia >= giaMn && n.Gia <= giaMx && n.TrangThai == true);
                        int kq = listDuLich.Count();
                        string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                        string tenDiemDen = db.DiemDens.SingleOrDefault(n => n.MaDiemDen == diemKT).TenDiemDen;
                        ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1} đến {2}.", kq, tenDiemXP, tenDiemDen);
                        return View(listDuLich);
                    }
                }
            }
            else
            {
                DateTime ngay1 = DateTime.Parse(ngayXP);
                DateTime ngay2 = DateTime.Parse(ngayKT);
                if (diemDen == "")
                {
                    if (giaMin == "GiaMin" || giaMax == "GiaMax")
                    {
                        var listDuLich = (from t in db.Tours
                                          join ctt in db.CTTours on t.MaTour equals ctt.MaTour
                                          where ctt.NgayXP > ngay1 && ctt.NgayXP < ngay2 && t.MaDiemXP == diemXP && t.TrangThai == true
                                          select t);
                        int kq = listDuLich.Count();
                        string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                        ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1}", kq, tenDiemXP);
                        return View(listDuLich);
                    }
                    else
                    {
                        int giaMn = int.Parse(giaMin);
                        int giaMx = int.Parse(giaMax);
                        var listDuLich = (from t in db.Tours
                                          join ctt in db.CTTours on t.MaTour equals ctt.MaTour
                                          where ctt.NgayXP > ngay1 && ctt.NgayXP < ngay2 && t.MaDiemXP == diemXP && t.Gia >= giaMn && t.Gia <= giaMx && t.TrangThai == true
                                          select t);
                        int kq = listDuLich.Count();
                        string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                        ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1}", kq, tenDiemXP);
                        return View(listDuLich);
                    }
                }
                else
                {
                    if(giaMin=="GiaMin" || giaMax == "GiaMax")
                    {
                        int diemKT = int.Parse(diemDen);
                        var listDuLich = (from t in db.Tours
                                          join ctt in db.CTTours on t.MaTour equals ctt.MaTour
                                          where ctt.NgayXP > ngay1 && ctt.NgayXP < ngay2 && t.MaDiemXP == diemXP && t.MaDiemDen == diemKT && t.TrangThai == true
                                          select t);
                        int kq = listDuLich.Count();
                        string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                        string tenDiemDen = db.DiemDens.SingleOrDefault(n => n.MaDiemDen == diemKT).TenDiemDen;
                        ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1} đến {2}.", kq, tenDiemXP, tenDiemDen);
                        return View(listDuLich);
                    }
                    else
                    {
                        int giaMn = int.Parse(giaMin);
                        int giaMx = int.Parse(giaMax);
                        int diemKT = int.Parse(diemDen);
                        var listDuLich = (from t in db.Tours
                                          join ctt in db.CTTours on t.MaTour equals ctt.MaTour
                                          where ctt.NgayXP > ngay1 && ctt.NgayXP < ngay2 && t.MaDiemXP == diemXP && t.MaDiemDen == diemKT && t.Gia >= giaMn && t.Gia <= giaMx && t.TrangThai == true
                                          select t);
                        int kq = listDuLich.Count();
                        string tenDiemXP = db.DiemXPs.SingleOrDefault(n => n.MaDiemXP == diemXP).TenDiemXP;
                        string tenDiemDen = db.DiemDens.SingleOrDefault(n => n.MaDiemDen == diemKT).TenDiemDen;
                        ViewBag.KetQua = string.Format("Tìm thấy {0} Tour Xuất phát từ {1} đến {2}.", kq, tenDiemXP, tenDiemDen);
                        return View(listDuLich);
                    }
                    
                }
            }
        }
    }
}