using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
    public class PALoginController : Controller
    {
        // GET: PALogin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(PackageAdmin c)
        {
            var db = new WebSeriesDBEntities();
            var user = (from e in db.PackageAdmins
                        where e.PAName.Equals(c.PAName) &&
                        e.PAPassword.Equals(c.PAPassword)
                        select e).SingleOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie("data", true);
                Session["user_name"] = user.PAName;
                return RedirectToAction("Index", "Package");
            }
            TempData["Msg"] = "Username and Password is invalid";
            return View();

        }
        public ActionResult Logout()
        {
            Session.Remove("user_name");
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}