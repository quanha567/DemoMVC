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
        BookStoreDataContext db = new BookStoreDataContext();
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

        public ActionResult ThongTinGioHang()
        {
            ViewBag.TongSoLuong = TinhTongSoLuong();
            return PartialView();
        }

        public ActionResult XoaSanPham(int iMaSach)
        {
            List<GioHang> giohang = LayGioHang();

            GioHang sanpham = giohang.SingleOrDefault(a => a.iMaSach == iMaSach);
            if(sanpham != null)
            {
                giohang.RemoveAll(a => a.iMaSach == iMaSach);
                return RedirectToAction("GioHang");
            }
            if(giohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }

            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGiohang(int iMaSach, FormCollection col)
        {
            List<GioHang> giohang = LayGioHang();
            GioHang sanpham = giohang.SingleOrDefault(a => a.iMaSach == iMaSach);
            if(sanpham != null)
            {
                sanpham.iSoLuong = Convert.ToInt32(col["soluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> giohang = LayGioHang();
            giohang.Clear();

            return RedirectToAction("Index", "BookStore");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if(Session["UserInfo"] == null || Session["UserInfo"].ToString() == null)
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "BookStore");
            }

            List<GioHang> giohang = LayGioHang();
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();

            return View(giohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection col)
        {
            DONDATHANG dondat = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["UserInfo"];
            List<GioHang> giohang = LayGioHang();
            dondat.MaKH = kh.MaKH;
            dondat.Ngaydat = DateTime.Now;
            var ngaygiao = String.Format("{0:dd/MM/yyyy}", col["ngaygiao"]);
            dondat.Ngaygiao = DateTime.Parse(ngaygiao);
            dondat.Tinhtranggiaohang = false;
            dondat.Dathanhtoan = false;
            db.DONDATHANGs.InsertOnSubmit(dondat);
            db.SubmitChanges();

            foreach(var item in giohang)
            {
                CHITIETDONTHANG chitiet = new CHITIETDONTHANG();
                chitiet.MaDonHang = dondat.MaDonHang;
                chitiet.Masach = item.iMaSach;
                chitiet.Soluong = item.iSoLuong;
                chitiet.Dongia = (decimal)item.dDonGia;
                db.CHITIETDONTHANGs.InsertOnSubmit(chitiet);
            }
            db.SubmitChanges();
            Session["giohang"] = null;

            return RedirectToAction("XacNhanDonhang", "Giohang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}