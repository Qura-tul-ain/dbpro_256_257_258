using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DB43.Models
{
	public class QuizQuestionsViewModel
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Question")]
		public string Name { get; set; }

		[Display(Name = "Marks")]
		public int Q_TotalMarks { get; set; }
		public int QuizId { get; set; }
	}
}