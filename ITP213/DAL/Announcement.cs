using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Announcement
    {
        public int announcementID { set; get; } //
        public string announcementTitle { set; get; } //
        public string announcementMessage { set; get; } //
        public string staffID {set; get;} //
        public string staffName { set; get; }
        public int tripID { set; get; }
        public string tripNameAndTripType { set; get; } 
        public string tripName { set; get; } //
        public string tripType { set; get; }
        public string createdOn { set; get; } //
        public string timeDue { set; get; }
        public string studentView { set; get; }
        public string lecturerView { set; get; }
        // For site.master
        public string unreadCount { set; get; }
        // ViewAnnouncement.aspx
        public bool announcementVisible { set; get; }
    }
}