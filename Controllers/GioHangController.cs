using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class GioHangController : Controller
    {
        BookStoreController db = new BookStoreController();
        
        // GET: GioHang
        List<GioHang> LayGioHang()
        {
            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            if(giohang == null)
            {
                giohang = new List<GioHang>();
                Session["giohang"] = giohang;
            }
            return giohang;
        }

        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            List<GioHang> giohang = LayGioHang();

            GioHang sanpham = giohang.Find(a => a.iMaSach == iMaSach);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                giohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        private int TinhTongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            if(giohang != null)
            {
                iTongSoLuong = giohang.Sum(a => a.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TinhTongTien()
        {
            double iTongTien = 0;
            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            if (giohang != null)
            {
                iTongTien = giohang.Sum(a => a.dThanhTien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> giohang = LayGioHang();

            if(giohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();

            return View(giohang);
        }
    }
}