using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB43.Models
{
    public class InstructorWithCourses
    {
        public int Id { get; set; }// course id 
        public string deptName { get; set; }
        public string CourseTitle { get; set; }
        public string InstName { get; set; }

    }
}