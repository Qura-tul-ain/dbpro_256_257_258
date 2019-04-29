using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DB43.Models
{
	public class InstructorQuizViewModel
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Quiz Name")]
		public string Title { get; set; }
		public System.DateTime DateCreated { get; set; }
		[Required]
		[Display(Name = "Total Marks")]
		public int TotalMarks { get; set; }
		public int CourseId { get; set; }
	}
}