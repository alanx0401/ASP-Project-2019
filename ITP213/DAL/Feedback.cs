using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Feedback
    {
        public int feedbackID { get; set; }
        public string enjoy { get; set; }
        public string lodging { get; set; }
        public string affordable { get; set; }
        public string interaction { get; set; }
        public string companyVisit { get; set; }
        public string transport { get; set; }
        public string improvement { get; set; }
        public int tripID { get; set; }
        public string adminNo { get; set; }
    }// hopefully this is it
}