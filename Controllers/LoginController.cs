using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Connect4_Web_Project.Models;
using System.Security.Cryptography;
using Connect4_Web_Project.Models.Database;


namespace Connect4_Web_Project.Controllers
{
    public class LoginController : Controller
    {
        private Entities _entities = new Entities();
        private UserLogin userLogin = new UserLogin();
        private User user = new User();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //REGISTER
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _entities.Users.FirstOrDefault(s => s.Username == _user.Username);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _entities.Configuration.ValidateOnSaveEnabled = false;
                    _entities.Users.Add(_user);
                    _entities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Username already exists";
                    return View();
                }
            }
            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        ///LOGIN
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin _userLogin)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(_userLogin.Password);
                var data = _entities.Users.Where(s => s.Username.Equals(_userLogin.Email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Username"] = data.FirstOrDefault().Username;
                    Session["idUser"] = data.FirstOrDefault().idUser;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}