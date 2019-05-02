using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DB43.Models
{
	public class StudentQuizViewModel
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Quiz Name")]
		public string Title { get; set; }

		[Required]
		[Display(Name = "Date Created")]
		public System.DateTime DateCreated { get; set; }

		public int TotalMarks { get; set; }
        public List<QuizQuestionsViewModel> Questions { get => questions; set => questions = value; }

        public int obtainedMarks { get; set; }
        private List<QuizQuestionsViewModel> questions = new List<QuizQuestionsViewModel>();

    }
}