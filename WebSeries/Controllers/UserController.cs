using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult List()
        {
            var db = new WebSeriesDBEntities();
            List<User> user = db.Users.ToList();
            var u = user;
            return View(u);
        }
    }
}