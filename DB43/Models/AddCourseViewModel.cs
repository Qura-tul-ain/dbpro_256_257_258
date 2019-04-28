using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DB43.Models
{
    public class AddCourseViewModels
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Credits { get; set; }
        public decimal Fee { get; set; }

        public int DepartmentId { get; set; }
        public string CourseImage { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
}
