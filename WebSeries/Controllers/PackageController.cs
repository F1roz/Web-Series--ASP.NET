using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;
using WebSeriesApplication.Auth;

namespace WebSeries.Controllers
{
    public class PackageController : Controller
    {
        // GET: Package
        [Logged]
        public ActionResult Index()
        {
            var db = new WebSeriesDBEntities();
            List<Package> packages = db.Packages.ToList();
            var p = packages;
            return View(p);
        }
        [Logged]
        public ActionResult PackageCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PackageCreate(Package add)
        {
            if (ModelState.IsValid)
            {
                WebSeriesDBEntities db = new WebSeriesDBEntities();
                db.Packages.Add(add);
                db.SaveChanges();
                ViewBag.Msg = "Added Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Msg = "Error";
            return View();
        }

        [Logged]
        [HttpGet]
        public ActionResult PackageEdit(int id)
        {
            WebSeriesDBEntities db = new WebSeriesDBEntities();
            var package = (from p in db.Packages where p.PId == id select p).SingleOrDefault();
            return View(package);

        }

        [HttpPost]
        public ActionResult PackageEdit(Package add)
        {

            if (ModelState.IsValid)
            {
                WebSeriesDBEntities db = new WebSeriesDBEntities();
                var data = (from n in db.Packages where n.PId == add.PId select n).FirstOrDefault();
                db.Entry(data).CurrentValues.SetValues(add);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        public ActionResult PackageDelete(int id)
        {
            if (ModelState.IsValid)
            {
                WebSeriesDBEntities db = new WebSeriesDBEntities();
                var data = (from u in db.Packages
                            where u.PId == id
                            select u).FirstOrDefault();
                db.Packages.Remove(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}