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
    public class UserController : Controller
    {
        WebsiteBanHangEntities dbObj = new WebsiteBanHangEntities();
        // GET: Admin/User
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var users = new List<User>();

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
                users = dbObj.Users
                              .Where(n => n.FirstName.Contains(searchString))
                              .ToList();
            }
            else
            {
                // If no search string is provided, get all products
                users = dbObj.Users.ToList();
            }

            // Set the current filter for the view
            ViewBag.CurrentFilter = searchString;

            // Define the page size and page number
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            // Order the products by descending ID and return paginated list
            users = users.OrderByDescending(n => n.FirstName).ToList();

            // Return the view with the paginated list of products
            return View(users);
            //return View(users.ToPagedList(pageNumber, pageSize));
        }
        //create
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (user.ImageUpLoad != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(user.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(user.ImageUpLoad.FileName);
                    filename = filename + extension + "_" + long.Parse(DateTime.Now.ToString("yyyMMddhhmmss"));
                    user.Email = filename;
                    user.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                dbObj.Users.Add(user);
                dbObj.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]


        //details
        public ActionResult Details(int id)
        {
            var objUser = dbObj.Users.Where(n => n.idUser == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]


        //delete
        public ActionResult Delete(int id)
        {
            var objUser = dbObj.Users.Where(n => n.idUser == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(User user)
        {
            var objUser = dbObj.Users.Where(n => n.idUser == user.idUser).FirstOrDefault();
            dbObj.Users.Remove(objUser);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]


        //edit
        public ActionResult Edit(int id)
        {
            var objUser = dbObj.Users.Where(n => n.idUser == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            if (user.ImageUpLoad != null)
            {
                string filename = Path.GetFileNameWithoutExtension(user.ImageUpLoad.FileName);
                string extension = Path.GetExtension(user.ImageUpLoad.FileName);
                filename = filename + extension + "_" + long.Parse(DateTime.Now.ToString("yyyMMddhhmmss"));
                user.Email = filename;
                user.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            dbObj.Entry(user).State = EntityState.Modified;
            dbObj.SaveChanges();
            return View(user);
        }
    }  
}