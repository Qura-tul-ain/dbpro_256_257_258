using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DB43.Models
{
    public class AnnouncementViewModel
    {
        [Key]
        public int Id { get; set; }
         public string text { get; set; }
        public  int personId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }

    }
}