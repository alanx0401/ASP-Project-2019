using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFunctions.DAL
{
    public class MedicalRecord
    {
        public string medicalRecordID { get; set; }
        public string adminNo { get; set; }
        public string bloodType { get; set; }
        public string allergies { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string insurances { get; set; }
    }
}