using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
<<<<<<< HEAD
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User u)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("User/Registration");
            }
=======
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        [Authorize(Roles = "User")]
        public ActionResult List()
        {
            var db = new WebSeriesDBEntities();
            List<User> user = db.Users.ToList();
            var u = user;
>>>>>>> master
            return View(u);
        }
    }
}