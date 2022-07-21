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
            HttpCookie myCookie = new HttpCookie("userInfo");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

                FormsAuthentication.SetAuthCookie(isLoggin.Name, true);
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["role"] = isLoggin.Role;
                userInfo["email"] = isLoggin.Email;
                userInfo.Expires.Add(new TimeSpan(1, 0, 0));
                Response.Cookies.Add(userInfo);

                String uEmail = Request.Cookies["userInfo"]["email"];
                String uRole = Request.Cookies["userInfo"]["role"];

                Session se = new Session();
                se.Name = uEmail;
                se.Device = MachineName;
                se.Platfrom = Platfrom;
                se.Browser = BroswerName;
                se.LoginTime = localMechinTime;
                se.GMT = GMTSplitSrting;
                se.OS = osNameVersion;
                se.LoginId = isLoggin.Id;

                var u = (from lt in db.Users where lt.Email == isLoggin.Email select lt).FirstOrDefault();
                u.LoginTime = localMechinTime;
                db.Sessions.Add(se);
                db.SaveChanges();

           
                if (u.Email.Equals(uEmail) && u.Role == "Admin")
                {
                    return RedirectToAction("List", "User");
                }
                if (u.Email.Equals(uEmail) && u.Role == "User")
                {
                    return RedirectToAction("Index", "PackageAdmin");
                }
                if (u.Email.Equals(uEmail) && u.Role == "PackageManager")
                {
                    return RedirectToAction("Index", "Package");
                }
                if (u.Email.Equals(uEmail) && u.Role == "VideoManager")
                {
                    return RedirectToAction("List", "Video");
                }

                //return RedirectToAction("List","User");
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
        [ValidateAntiForgeryToken]
        public ActionResult Register(User add)
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

                add.Role = "User";
                Login log = new Login();
                log.Name = add.Name;
                log.Email = add.Email;
                log.Password = add.Password;
                log.Role = "User";
                
                add.AccountCreateTime = localMechinTime;
                db.Logins.Add(log);
                db.Users.Add(add);
                db.SaveChanges();
                ViewBag.Msg = "Account Create Successfully";
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var db = new WebSeriesDBEntities();
            string email = Request.Cookies["userInfo"]["email"].ToString();
            var isLoggin = (from s in db.Sessions where s.Name == email select s).FirstOrDefault();
            db.Sessions.Remove(isLoggin);
            db.SaveChanges();
            HttpCookie myCookie = new HttpCookie("userInfo");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}