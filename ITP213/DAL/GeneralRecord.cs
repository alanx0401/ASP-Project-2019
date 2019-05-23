using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFunctions.DAL
{
    public class GeneralRecord
    {
        public string generalRecordID { get; set; }
        public string adminNo { get; set; }
        public string studentName { get; set; }
        public string school { get; set; }
        public string course { get; set; }
        public string email { get; set; }
        public string dateOfBirth { get; set; }
        public string dietaryNeeds { get; set; }
        public string spokenLanguage { get; set; }
    }
}