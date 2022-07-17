using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult List()
        {
            var db = new WebSeriesDBEntities();
            List<Video> v = db.Videos.ToList();
            
            return View(v);
        }
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            /* // Verify that the user selected a file
             if (file != null && file.ContentLength > 0)
             {
                 // extract only the filename
                 var fileName = Path.GetFileName(file.FileName);
                 // store the file inside ~/App_Data/uploads folder
                 var path = Path.Combine(Server.MapPath("~/FTP"), fileName);
                 file.SaveAs(path);
             }
             // redirect back to the index action to show the form once again
             return RedirectToAction("Upload");*/
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/FTP/"), _FileName);
                    file.SaveAs(_path);
                    ViewBag.Message = _path;
                }
               
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}