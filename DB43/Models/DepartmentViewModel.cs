using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DB43.Models
{
    public class DepartmentViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
         public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}