using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class ReportInjury
    {
        public string name { set; get; } // for staffName in reportInjury.cs
        public string adminNo { set; get; }
        // get injury record
        public int injuryReportID { set; get; }
        public string dateTimeOfInjury { set; get; }
        public string location { set; get; }
        public string description { set; get; }
        public string witnessName { set; get; }
        public string witnessPhone { set; get; }
        public string natureOfInjury { set; get; }
        public string causeOfInjury { set; get; }
        public string locationOnBody { set; get; }
        public string agency { set; get; }
        public string firstAidGiven { set; get; }
        public string firstAiderName { set; get; }
        public string treatment { set; get; }
        public string staffID { set; get; }
        public string studentName { set; get; }
        public string tripName { set; get; }
        public string tripType { set; get; }
        public string createdOn { set; get; }
        public int tripID { set; get; }
        public string image { set; get; }
        public string injurySeverity { set; get; }
        public string parentNum { set; get; }
        public string remark { set; get; }
        public string sendDate { set; get; }
    }
}