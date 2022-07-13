using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
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
            return View(u);
        }
    }
}