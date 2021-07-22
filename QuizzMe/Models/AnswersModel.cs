using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizzMe.Models
{
    public class AnswersModel
    {
        public int AnswerId { get; set; }
        public string AnswerString { get; set; }
        public int IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}