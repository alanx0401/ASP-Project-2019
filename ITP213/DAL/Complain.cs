using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Complain
    {
        public int complainID { get; set; }
        public string subject { get; set; }
        public string comments { get; set; }
        public string studentame{ get; set; }
        public string adminNo { get; set; }
        public string image { get; set; }
        public string complainType { get; set; }
    }
}