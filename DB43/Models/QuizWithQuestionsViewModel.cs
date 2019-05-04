using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB43.Models
{
    public class QuizWithQuestionsViewModel
    {
        public int quizId;
        public List<QuizQuestionsViewModel> questions { get; set; }
        public List<OptionViewModel> options { get; set; }
    }
}