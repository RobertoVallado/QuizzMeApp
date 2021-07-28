using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizzMe.Models
{
    public class ScoreModel
    {
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public string TestTime {get; set;}
        public string TestDate {get; set;}
        public string CorrectAnswers { get; set; }
        public string IncorrectAnswers { get; set; }
        public int Score { get; set; }
        public int HighestScore { get; set; }

    }
}