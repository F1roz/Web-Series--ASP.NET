using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            var db = new WebSeriesDBEntities();
            List<User> user = db.Users.ToList();
            var u = user;
            return View(u);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, PackageManager, VideoManager")]
        public ActionResult Account(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new WebSeriesDBEntities();
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, PackageManager, VideoManager")]
        public ActionResult AccountSetting(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new WebSeriesDBEntities();
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, User, PackageManager, VideoManager")]
        [ValidateAntiForgeryToken]
        public ActionResult AccountSetting([Bind(Include = "Id,Name,Email,Password,Phone,DOB,Role,Address1,Address2,Status,AccountCreateTime,LoginTime")] User user)
        {
            
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    var db = new WebSeriesDBEntities();
                    var u = (from lt in db.Logins where lt.Email == user.Email select lt).FirstOrDefault();
                    
                    u.Email = user.Email;
                    u.Name = user.Name;
                    u.Password = user.Password;
                    u.Role = user.Role;
                    db.Entry(user).State = EntityState.Modified;
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            var db = new WebSeriesDBEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var db = new WebSeriesDBEntities();
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        
    }
}