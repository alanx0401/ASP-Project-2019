using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Blog
    {
        public int blogID { get; set; }
        public string studentName { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string blogTime { get; set; }
        public string country { get; set; }
    }
}