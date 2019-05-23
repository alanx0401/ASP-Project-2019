using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Response
    {
        public int responseID { get; set; }
        public int feedbackID { get; set; }
        public string responseContent { get; set; }
        public int questionID { get; set; }
        public string adminNo { get; set; }
    }
}// many responses to one question