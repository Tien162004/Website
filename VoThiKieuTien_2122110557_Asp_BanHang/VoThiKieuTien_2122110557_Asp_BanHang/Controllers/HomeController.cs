using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VoThiKieuTien_2122110557_Asp_BanHang.Context;
using VoThiKieuTien_2122110557_Asp_BanHang.Models;

namespace VoThiKieuTien_2122110557_Asp_BanHang.Controllers
{
    public class HomeController : Controller
    {
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            var lstProduct = objwebsiteBanHangEntities.Products.ToList();
            var lstCategory = objwebsiteBanHangEntities.Categories.ToList();
            objHomeModel.ListCategory = lstCategory;
            objHomeModel.ListProduct = lstProduct;
            return View(objHomeModel);
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ListingGrid()
        {
            return View();
        }
        public ActionResult ListingLarge()
        {
            return View();
        }
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult Content()
        {
            return View();
        }
    }
}