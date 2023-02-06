using System;

namespace WE_SECB_API.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseFee { get; set; }
        public string CreditHour { get; set; }
        public string CourseCode { get; set; }
        public int Status { get; set; }
    }
}
