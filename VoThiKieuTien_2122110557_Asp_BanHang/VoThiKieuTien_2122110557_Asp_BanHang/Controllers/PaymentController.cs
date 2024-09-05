using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiKieuTien_2122110557_Asp_BanHang.Context;
using VoThiKieuTien_2122110557_Asp_BanHang.Models;

namespace VoThiKieuTien_2122110557_Asp_BanHang.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        WebsiteBanHangEntities obj = new WebsiteBanHangEntities();

        // GET: Payment
        public ActionResult Index()
        {
            // Check if the user is logged in
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                //
                Order objOrder = new Order();


                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMdd");
                objOrder.Id = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1; // assuming 1 represents a valid status; adjust as needed
                obj.Orders.Add(objOrder);
                obj.SaveChanges();


                //
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;  // Correct property name
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.id;  // Ensure 'Id' is correct
                    lstOrderDetail.Add(obj);
                }

                obj.OrderDetails.AddRange(lstOrderDetail);
                obj.SaveChanges();

            }


            return View();
        }
    }
}