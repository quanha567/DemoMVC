using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVC.Models
{
    public class GioHang
    {
        BookStoreDataContext db = new BookStoreDataContext();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            SACH sach = db.SACHes.Single(a => a.Masach == iMaSach);
            sTenSach = sach.Tensach;
            sAnhBia = sach.Anhbia;
            dDonGia = Convert.ToDouble(sach.Giaban.ToString());
            iSoLuong = 1;
        }
    }
}