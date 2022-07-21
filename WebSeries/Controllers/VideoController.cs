using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSeries.EntityFramework;

namespace WebSeries.Controllers
{
    [Authorize]

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
        public ActionResult Upload(VideoFile v)
        {

            try{ 

            using (WebSeriesDBEntities db = new WebSeriesDBEntities())
            {

                Video video = new Video();
                video.VideoTitle = v.VideoTitle;
                video.Description = v.Description;
                video.Status = "Ap";
                video.UserId = 2;
                    video.UploadDate = DateTime.Now;
                video.VideoPath = SaveToPhysicalLocation(v.VideoPath);
                db.Videos.Add(video);
                db.SaveChanges();

            }
            ModelState.Clear();
            ViewBag.Message = "Video Uploaded Successfully";
            //ViewBag.Message =video;
            return View();
        }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ViewBag.Message = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" +
                        eve.Entry.Entity.GetType().Name+ eve.Entry.State;
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ViewBag.Message= "- Property: \"{0}\", Error: \"{1}\""+ ve.PropertyName+ ve.ErrorMessage;
                    }
                }
                return View();
            }
            
        }
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/FTP/"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }
    }
}