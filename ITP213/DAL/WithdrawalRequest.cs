using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class WithdrawalRequest
    {
        public string tripName { set; get; }
        public int tripID { set; get; }
        public string tripType { set; get; }
        public string tripNameAndTripType { set; get; }
        public int withdrawTripRequestID { set; get; }
        public string withdrawalReason { set; get; }
        public string adminNo { set; get; }
        public string departureDate { set; get; } // on hold
        public string arrivalDate { set; get; } // on hold
        public string createdOn { set; get; }
        public string withdrawalTripRequestStatus { set; get; }
        public string country { set; get; }
    }
}