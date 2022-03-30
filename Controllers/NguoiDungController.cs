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
        
        BookStoreDataContext data = new BookStoreDataContext();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection col, KHACHHANG kh)
        {
            var HoTen = col["HotenKH"];
            var TaiKhoan = col["TenDN"];
            var Matkhau = col["Matkhau"];
            var Matkhaunhaplai = col["Matkhaunhaplai"];
            var DiachiKH = col["Diachi"];
            var Email = col["Email"];
            var DienthoaiKH = col["Dienthoai"];
            var Ngaysinh = String.Format("{0:dd/MM/yyyy}", col["Ngaysinh"]);

            if(String.IsNullOrEmpty(HoTen))
            {
                ViewData["loi1"] = "Tên khách hàng không được bỏ trống";
            } else if (String.IsNullOrEmpty(TaiKhoan))
            {
                ViewData["loi2"] = "Tên đăng nhập không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(Matkhau))
            {
                ViewData["loi3"] = "Mật khẩu không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(Matkhaunhaplai))
            {
                ViewData["loi4"] = "Nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(Email))
            {
                ViewData["loi5"] = "Tên khách hàng không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(DienthoaiKH))
            {
                ViewData["loi6"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(DiachiKH))
            {
                ViewData["loi7"] = "Địa chỉ không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(Ngaysinh))
            {
                Ngaysinh = "1/1/2000";
            }
            else
            {
                kh.HoTen = HoTen;
                kh.Taikhoan = TaiKhoan;
                kh.Matkhau = Matkhau;
                kh.Email = Email;
                kh.DiachiKH = DiachiKH;
                kh.DienthoaiKH = DienthoaiKH;
                kh.Ngaysinh = DateTime.Parse(Ngaysinh);

                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Dang Nhap");
            }
            return this.DangNhap();
        }
    }
}