using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;
using PagedList;
using PagedList.Mvc;

namespace DemoMVC.Controllers
{
    public class BookStoreController : Controller
    {
        // init database variable
        BookStoreDataContext data = new BookStoreDataContext();

        private List<SACH> GetNewProduct(int index)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(index).ToList();
        }

        // GET: BookStore
        public ActionResult Index(int ? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);

            var list_product = GetNewProduct(10);
            return View(list_product.ToPagedList(pageNum, pageSize));
        }

        public ActionResult ChuDe()
        {
            var chude = from TenChuDe in data.CHUDEs select TenChuDe;
            //var chude = data.CHUDEs.Select(a => a.TenChuDe).ToList();
            return PartialView(chude);
        }

        public ActionResult NhaXuatBan()
        {
            var nxb = from TenNXB in data.NHAXUATBANs select TenNXB;
            return PartialView(nxb);
        }

        public ActionResult SanPhamTheoChuDe(int id)
        {
            var sach = data.SACHes.Where(a => a.MaCD == id).ToList();
            return View(sach);
        }

        public ActionResult SanPhamTheoNXB(int id)
        {
            var sach = data.SACHes.Where(a => a.MaNXB == id).ToList();
            return View(sach);
        }

        public ActionResult ChiTietSach(int id)
        {
            var chitiet = data.SACHes.Where(a => a.Masach == id).First();
            return View(chitiet);
        }
    }
}