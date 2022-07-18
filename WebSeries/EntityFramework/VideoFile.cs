using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSeries.EntityFramework
{
    public class VideoFile
    {
        public string VideoTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public HttpPostedFileBase VideoPath { get; set; }
    }
}