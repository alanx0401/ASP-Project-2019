using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class EventLogs
    {
        public int eventID { get; set; }
        public string eventDesc { get; set; }
        public DateTime dateTimeDetails { get; set; }
        public string UUID { get; set; }

    }
}