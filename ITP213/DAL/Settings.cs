using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Settings
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
        public string googleAuthEnabled { set; get; }
        public string otpEnabled { set; get; }
        public string accountStatus { set; get; }
        public DateTime banAccountDateTime { set; get; }
        public string changePasswordDate { set; get; }
        public string secretKey { set; get; }
        public string phoneVerified { set; get; }
        public string emailVerified { set; get; }
    }
}