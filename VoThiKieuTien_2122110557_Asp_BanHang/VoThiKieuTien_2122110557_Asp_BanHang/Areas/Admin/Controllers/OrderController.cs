using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiKieuTien_2122110557_Asp_BanHang.Context;

namespace VoThiKieuTien_2122110557_Asp_BanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();

        // GET: Admin/Order
        public ActionResult Index()
        {
            var lstOrder = objwebsiteBanHangEntities.Orders.ToList();
            return View(lstOrder);
        }
    }
}