using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class AdminController : Controller
    {
        BookStoreDataContext db = new BookStoreDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QLSach()
        {
            return View(db.SACHes.ToList());
        }
    }
}