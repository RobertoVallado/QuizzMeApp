using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QuizzMe.Models
{
    public class TestModel
    {
        public int TestId { get; set; }
        public DataTable Questions { get; set; }
        public DataTable  Answers { get; set; }

    }

}