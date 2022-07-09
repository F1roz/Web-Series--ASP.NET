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
        public ActionResult Login(Login loginData)
        {
            var db = new WebSeriesDBEntities();
            var isLoggin = db.Logins.FirstOrDefault(x => x.Email == loginData.Email && x.Password == loginData.Password);
            if (isLoggin != null)
            {
                var bc = Request.Browser;
                var BroswerName = bc.Browser;
                var MachineName = System.Net.Dns.GetHostName();
                var Platfrom = bc.Platform;
                CultureInfo en = new CultureInfo("en-US");
                DateTime dt = DateTime.Now;
                String format = "MM/dd/yyyy hh:mm:sszzz";
                String str = dt.ToString(format);
                DateTime localMechinTime = DateTime.ParseExact(str, format, en.DateTimeFormat);
                TimeZone localZone = TimeZone.CurrentTimeZone;
                var GMT = localZone.StandardName;
                string GMTSplitSrting = GMT.Split()[0];
                var osNameVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                User usr = new User();
                usr.Device = MachineName;
                usr.Platform = Platfrom;
                usr.Browser = BroswerName;
                usr.Time = localMechinTime;
                usr.GMT = GMTSplitSrting;
                usr.OS = osNameVersion;
                db.Users.Add(usr);
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(loginData.Email, false);
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
                var bc = Request.Browser;
                var BroswerName = bc.Browser;
                var MachineName = System.Net.Dns.GetHostName();
                var Platfrom = bc.Platform;
                CultureInfo en = new CultureInfo("en-US");
                DateTime dt = DateTime.Now;
                String format = "MM/dd/yyyy hh:mm:sszzz";
                String str = dt.ToString(format);
                DateTime localMechinTime = DateTime.ParseExact(str, format, en.DateTimeFormat);
                TimeZone localZone = TimeZone.CurrentTimeZone;
                var GMT = localZone.StandardName;
                string GMTSplitSrting = GMT.Split()[0];
                var osNameVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

                add.Role = "user";
                User usr = new User();
                usr.Name = add.Name;
                usr.Email = add.Email;
                usr.Password = add.Password;
                usr.Role = "User";
                usr.Device = MachineName;
                usr.Platform = Platfrom;
                usr.Browser = BroswerName;
                usr.Time = localMechinTime;
                usr.GMT = GMTSplitSrting;
                usr.OS = osNameVersion;
                usr.AccountCreateTime = localMechinTime;
                db.Logins.Add(add);
                db.Users.Add(usr);
                db.SaveChanges();
                ViewBag.Msg = "Account Create Successfully";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}