using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class NguoiDungController : Controller
    {
        BookStoreDataContext db = new BookStoreDataContext();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        // GET: NguoiDung/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDung/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                KHACHHANG user = new KHACHHANG();
                user.HoTen = collection["HoTen"];
                user.Taikhoan = collection["TaiKhoan"];
                user.Matkhau = collection["Matkhau"];

                db.KHACHHANGs.InsertOnSubmit(user);
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            { 
                return View();
            }
        }

        public ActionResult DangNhap(FormCollection col)
        {
            var tendn = col["tendangnhap"];
            var matkhau = col["matkhau"];

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Tên đăng nhập không được bỏ trống!";
            } else if(String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Mật khẩu không được bỏ trống!";
            } else
            {
                KHACHHANG user = db.KHACHHANGs.SingleOrDefault(a => a.Taikhoan == tendn && a.Matkhau == matkhau);
                if(user == null)
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
                else
                {
                    Session["UserInfo"] = user;
                    return RedirectToAction("Index", "BookStore");
                }
            }

            return View();
        }

        public ActionResult Avata()
        {
            return PartialView();
        }

        public ActionResult ThongTinKhachHang()
        {
            return View();
        }
    }
}
