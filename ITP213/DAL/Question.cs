using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Question
    {
        public int questionID { get; set; }
        public int feedbackID { get; set; }
        public string questionContent { get; set; }
    }
}