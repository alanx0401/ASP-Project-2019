using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Internship
    {
        public int overseasInternID { get; set; }
        public string reportContent { get; set; }
        public string reflectionContent { get; set; }
        public int reflectionID { get; set; }
        public DateTime internStartDate { get; set; }
        public DateTime internEndDate { get; set; }
        public string adminNo { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string staffID { get; set; }
        public string reportStatus { get; set; }
        public string reflectionStatus { get; set; }
        public string companyName { get; set; }
    }
    // edit attributes where neccessary
}