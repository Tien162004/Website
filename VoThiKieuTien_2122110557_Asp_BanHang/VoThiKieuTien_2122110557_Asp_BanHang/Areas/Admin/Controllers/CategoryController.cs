using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
//using PagedList;
using System.Web;
using System.Web.Mvc;
using VoThiKieuTien_2122110557_Asp_BanHang.Context;

namespace VoThiKieuTien_2122110557_Asp_BanHang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        //public ActionResult Index()
        //{
        //    return View();
        //}
        WebsiteBanHangEntities obj = new WebsiteBanHangEntities();
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var categorys = new List<Category>();

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
                categorys = obj.Categories
                              .Where(n => n.Name.Contains(searchString))
                              .ToList();
            }
            else
            {
                // If no search string is provided, get all products
                categorys = obj.Categories.ToList();
            }

            // Set the current filter for the view
            ViewBag.CurrentFilter = searchString;

            // Define the page size and page number
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            // Order the products by descending ID and return paginated list
            categorys = categorys.OrderByDescending(n => n.Id).ToList();

            // Return the view with the paginated list of products
            return View(categorys);
            //return View(categorys.ToPagedList(pageNumber, pageSize));
        }



        //

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                if (category.ImageUpLoad != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(category.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(category.ImageUpLoad.FileName);
                    filename = filename + extension + "_" + long.Parse(DateTime.Now.ToString("yyyMMddhhmmss"));
                    category.Avatar = filename;
                    category.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                obj.Categories.Add(category);
                obj.SaveChanges();
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
            var objCategory = obj.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = obj.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            var objCategory = obj.Categories.Where(n => n.Id == category.Id).FirstOrDefault();
            obj.Categories.Remove(objCategory);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = obj.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            if (category.ImageUpLoad != null)
            {
                string filename = Path.GetFileNameWithoutExtension(category.ImageUpLoad.FileName);
                string extension = Path.GetExtension(category.ImageUpLoad.FileName);
                filename = filename + extension + "_" + long.Parse(DateTime.Now.ToString("yyyMMddhhmmss"));
                category.Avatar = filename;
                category.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            obj.Entry(category).State = EntityState.Modified;
            obj.SaveChanges();
            return View(category);
        }

    }
}
