using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB43.Models
{
	public class CourseViewModels
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Credits { get; set; }
		public decimal Fee { get; set; }
	}
}