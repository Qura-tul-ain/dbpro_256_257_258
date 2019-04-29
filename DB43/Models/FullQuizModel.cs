using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DB43;

namespace DB43.Models
{
	public class FullQuizModel
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Quiz Name")]
		public string Title { get; set; }

		[Required]
		[Display(Name = "Date Created")]
		public System.DateTime DateCreated { get; set; }

		[Required]
		[Display(Name = "Total Marks")]
		public int TotalMarks { get; set; }

		[Required]
		[Display(Name = "Course")]
		public int CourseId { get; set; }

		[Required]
		[Display(Name = "Email")]
		public int QuestionId { get; set; }

		[Required]
		[Display(Name = "Question")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Question Marks")]
		public int Q_TotalMarks { get; set; }

		[Required]
		[Display(Name = "Email")]
		public int QuizId { get; set; }

		[Required]
		[Display(Name = "Email")]
		public int Option_Id_1 { get; set; }

		[Required]
		[Display(Name = "Option A.")]
		public string OptionValue_1 { get; set; }

		[Required]
		[Display(Name = "Email")]
		public int QuestionsId_1 { get; set; }

		[Required]
		[Display(Name = "Email")]

		public int Option_Id_2 { get; set; }

		[Required]
		[Display(Name = "Option B.")]
		public string OptionValue_2 { get; set; }

		[Required]
		[Display(Name = "Email")]
		public int QuestionsId_2 { get; set; }

		[Required]
		[Display(Name = "Email")]
		public int Option_Id_3 { get; set; }

		[Required]
		[Display(Name = "Option C.")]
		public string OptionValue_3 { get; set; }

		[Required]
		[Display(Name = "Email")]
		public int QuestionsId_3 { get; set; }


		

	}
}