using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
    public class PackageAdminController : Controller
    {
        // GET: PackageAdmin
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
                        where e.Name.Equals(c.Name) &&
                        e.Password.Equals(c.Password)
                        select e).SingleOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Name, true);
                Session["user_name"] = user.Name;
                Session["user_id"]= user.Id;
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