using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DB43.Models
{
	public class OptionViewModel
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Option Value")]
		public string OptionValue { get; set; }
		public int QuestionsId { get; set; }
	}
}