using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFunctions.DAL
{
    public class AcademicResult
    {
        public string academicResultID { get; set; }
        public string adminNo { get; set; }
        public string cumulativeGPA { get; set; }
        public string sem1Year1GPA { get; set; }
        public string sem2Year1GPA { get; set; }
        public string sem1Year2GPA { get; set; }
        public string sem2Year2GPA { get; set; }
        public string sem1Year3GPA { get; set; }
        public string sem2Year3GPA { get; set; }
    }
}