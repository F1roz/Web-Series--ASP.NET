using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        [HttpPost]
        public ActionResult Login(Login log)
        {
            var db = new WebSeriesDBEntities();
            var isLoggin = db.Logins.Any(x => x.Email == log.Email && x.Password == log.Password);
            if (isLoggin)
            {
                var bc = Request.Browser;
                var BroswersName = bc.Browser;
                var Platfrom = bc.Platform;
                var device = Environment.MachineName;
                string MachineName2 = System.Net.Dns.GetHostName();
                //HttpBrowserCapabilities bc = Request.Browser;

                CultureInfo en = new CultureInfo("en-US");
                DateTime dt = DateTime.Now;
                
                String format = "MM/dd/yyyy hh:mm:sszzz";
                String str = dt.ToString(format);
                DateTime localMechinTime = DateTime.ParseExact(str, format, en.DateTimeFormat);
                DateTime utcMechinTime = DateTime.ParseExact(str, format, en.DateTimeFormat);
                TimeZone localZone = TimeZone.CurrentTimeZone;
                var GMT = localZone.StandardName;
                var osNameAndVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

                log.BrowserName = BroswersName;
                db.Logins.Add(log);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(log.Email, false);
                return RedirectToAction("List","User");
            }
            else
            {
                ViewBag.Msg = "Please provide correct email & password";

            }
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