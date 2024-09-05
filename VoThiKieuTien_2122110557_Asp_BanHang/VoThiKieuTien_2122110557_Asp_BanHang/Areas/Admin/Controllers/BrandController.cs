using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
//using PagedList;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiKieuTien_2122110557_Asp_BanHang.Context;

namespace VoThiKieuTien_2122110557_Asp_BanHang.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        //public ActionResult Index()
        //{
        //    return View();
        //}
        WebsiteBanHangEntities dbObj = new WebsiteBanHangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var brands = new List<Brand>();

            // Check if the search string is not null, then set the page number to 1
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                // Set the search string to the current filter if it's null
                searchString = currentFilter;
            }

            // If the search string is not empty, filter the products based on the search string
            if (!string.IsNullOrEmpty(searchString))
            {
                brands = dbObj.Brands
                              .Where(n => n.Name.Contains(searchString))
                              .ToList();
            }
            else
            {
                // If no search string is provided, get all products
                brands = dbObj.Brands.ToList();
            }

            // Set the current filter for the view
            ViewBag.CurrentFilter = searchString;

            // Define the page size and page number
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            // Order the products by descending ID and return paginated list
            brands = brands.OrderByDescending(n => n.Id).ToList();

            // Return the view with the paginated list of products
            return View(brands);
            //return View(brands.ToPagedList(pageNumber, pageSize));
        }


        // Set the current filter for the

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            try
            {
                if (brand.ImageUpLoad != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(brand.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(brand.ImageUpLoad.FileName);
                    filename = filename + extension + "_" + long.Parse(DateTime.Now.ToString("yyyMMddhhmmss"));
                    brand.Avatar = filename;
                    brand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                dbObj.Brands.Add(brand);
                dbObj.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = dbObj.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = dbObj.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand brand)
        {
            var objBrand = dbObj.Brands.Where(n => n.Id == brand.Id).FirstOrDefault();
            dbObj.Brands.Remove(objBrand);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = dbObj.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Edit(int id, Brand brand)
        {
            if (brand.ImageUpLoad != null)
            {
                string filename = Path.GetFileNameWithoutExtension(brand.ImageUpLoad.FileName);
                string extension = Path.GetExtension(brand.ImageUpLoad.FileName);
                filename = filename + extension + "_" + long.Parse(DateTime.Now.ToString("yyyMMddhhmmss"));
                brand.Avatar = filename;
                brand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            dbObj.Entry(brand).State = EntityState.Modified;
            dbObj.SaveChanges();
            return View(brand);
        }

    }
}