using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class OverallReport
    {
        public int overallReportID { get; set; }
        public string reportName { get; set; }
        public int tripID { get; set; }
        public string staffID { get; set; }
        public int injuryReportID { get; set; }
        public DateTime overallReportDate { get; set; }
        public int feedbackID { get; set; }
    }
    // additional attributes needed for displaying of stats
}