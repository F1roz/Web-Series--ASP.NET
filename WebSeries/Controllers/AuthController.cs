using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Login add)
        {
            var db = new WebSeriesDBEntities();
            var user = db.Logins.FirstOrDefault(u => u.Email == add.Email);
            if (user != null)
            {
                ViewBag.Msg = "Email has beed alredy registed";
            }
            else
            {
                add.Role = "user";
                User u = new User();
                u.Name = add.Name;
                u.Email = add.Email;
                u.Password = add.Password;
                db.Logins.Add(add);
                db.Users.Add(u);
                db.SaveChanges();
                ViewBag.Msg = "Account Create Successfully";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}