using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiKieuTien_2122110557_Asp_BanHang.Context;

namespace VoThiKieuTien_2122110557_Asp_BanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();

        // GET: Admin/Product
        public ActionResult Index()
        {
            var lstProduct = objwebsiteBanHangEntities.Products.ToList();
            return View(lstProduct);
        }
        public ActionResult Details(int Id)
        {
            var objProduct = objwebsiteBanHangEntities.Products.Where(n=>n.id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpLoad != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                    filename = filename + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) ;
                    objProduct.Avatar = filename;
                    objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                objwebsiteBanHangEntities.Products.Add(objProduct);
                objwebsiteBanHangEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objwebsiteBanHangEntities.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product product)
        {
            var objProduct = objwebsiteBanHangEntities.Products.Where(n => n.id == product.id).FirstOrDefault();
            objwebsiteBanHangEntities.Products.Remove(objProduct);
            objwebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objwebsiteBanHangEntities.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            if (product.ImageUpLoad != null)
            {
                string filename = Path.GetFileNameWithoutExtension(product.ImageUpLoad.FileName);
                string extension = Path.GetExtension(product.ImageUpLoad.FileName);
                filename = filename + extension + "_" + long.Parse(DateTime.Now.ToString("yyyMMddhhmmss"));
                product.Avatar = filename;
                product.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            objwebsiteBanHangEntities.Entry(product).State = EntityState.Modified;
            objwebsiteBanHangEntities.SaveChanges();
            return View(product);
        }
    }
    
}