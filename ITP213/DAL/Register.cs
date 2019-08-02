using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP213.DAL
{
    public class Register
    {
        // VerifyPhoneOTP table
        public DateTime dateTimeSend { set; get; }
        public string passwordHash { set; get; }
        public string passwordSalt { set; get; }

        // VerifyEmail table
        public string verificationToken { set; get; }
        public string UUID { set; get; }
        // public dateTime

        public string oldEmail { set; get; }
    }
}