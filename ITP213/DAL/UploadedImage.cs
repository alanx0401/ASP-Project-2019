using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class UploadedImage
    {
        public string imageID { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string user { get; set; }
        public string location { get; set; }
        public string tags { get; set; }
    }
}