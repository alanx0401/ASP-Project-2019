using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Login
    {
        // account table
        public string accountID { set; get; } // --> UUID
        public string UUID { set; get; }
        public string accountType { set; get; }
        public string name { set; get; }
        public string email { set; get; }
        public string mobile { set; get; }
        public DateTime dateOfBirth { set; get; }
        public string password { set; get; }
        public string googleAuthEnabled { set; get;}
        public string otpEnabled { set; get; }
        public string accountStatus { set; get; }
        public DateTime banAccountDateTime { set; get; }
        // student table
        public string adminNo { set; get; }
        public string studentSchool { set; get; }
        public string course { set; get; }
        public string allergies { set; get; }
        public string dietaryNeeds { set; get; }
        // parent table
        // public string parentID { set; get; }
        // lecturer table
        public string staffID { set; get; }
        public string lecturerSchool { set; get; }
        public string staffRole { set; get; }

        // For login page
        public string changePasswordDate { set; get; }

        // AccountFailedAttempt Table
        public int AccountFailedAttemptCounter { set; get; }
        // public string AccountFailedAttemptCounter { set; get; }
    }
}