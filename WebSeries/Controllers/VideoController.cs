using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VideoList()
        {
            var db = new WebSeriesDBEntities();
            List<Video> video = db.Videos.ToList();
            
            return View(video);
        }
        public ActionResult List()
        {
            var db = new WebSeriesDBEntities();
            List<Video> video = db.Videos.ToList();

            return View(video);
        }

    }
}