using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VoThiKieuTien_2122110557_Asp_BanHang.Context;

namespace VoThiKieuTien_2122110557_Asp_BanHang.Controllers
{
    public class UserController : Controller
    {


        // GET: User
        //[HttpGet]
        //public ActionResult Register()
        //{
        //    return View();
        //}
        //WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult Register(User _user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var check = objwebsiteBanHangEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
        //        if (check == null)
        //        {
        //            _user.Password = GetMD5((_user.Password));
        //            objwebsiteBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
        //            objwebsiteBanHangEntities.Users.Add(_user);
        //            objwebsiteBanHangEntities.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.error = "Email already exists";
        //            return View();
        //        }
        //    }

        //    //kiem tra va luu vao database
        //    return View("Index");
        //}
        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(str);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;
        //    for (int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");
        //    }
        //    return byte2String;
        //}
        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(int a)
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        WebsiteBanHangEntities obj = new WebsiteBanHangEntities();

        [HttpPost]
        public ActionResult Register(User users)
        {
            try
            {
                users.Password = CreateMD5(users.Password);
                obj.Users.Add(users);
                obj.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = CreateMD5(password);
                var data = obj.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().idUser;
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Index", "Home");
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}