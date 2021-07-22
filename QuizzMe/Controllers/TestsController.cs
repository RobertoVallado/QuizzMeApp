using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataLibrary;
using QuizzMe.Models;
using data_ = DataLibrary.DataAccess; // solution for static interface?

namespace QuizzMe.Controllers
{
    public class TestsController : ApiController
    {
        // GET api/<controller> 
        public HttpResponseMessage Get()
        {
            string sql = "SELECT DISTINCT TestId FROM questions";
            var tests_ = data_.LoadData<TestModel, dynamic>(sql, new {},
                ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);
            var tests = DataTableConvResponse.ToDataTable(tests_);

            return Request.CreateResponse(HttpStatusCode.OK, tests);
        }

        public HttpResponseMessage GetTest(int id)
        {
            //get questions array
            string sql = "SELECT * FROM questions WHERE TestId = @TestId";
            var questions_ = data_.LoadData<QuestionsModel, dynamic>(sql, new { TestId = $"{id}"},
                ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);
            var questionsTable = DataTableConvResponse.ToDataTable(questions_);

            //get answers array
            sql = "SELECT * FROM answers WHERE TestId = @TestId";
            var answers_ = data_.LoadData<AnswersModel, dynamic>(sql, new { TestId = $"{id}"},
                ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);
            var answersTable = DataTableConvResponse.ToDataTable(answers_);

            var newTest = new TestModel { TestId = id, Questions = questionsTable, Answers = answersTable };


            return Request.CreateResponse(HttpStatusCode.OK, newTest);

        }
/*
            // DELETE api/<controller>/5   //DELETE TEST??
            public string Delete()//int id)
            {
                try
                {
                    string sql = @"delete from users where LastName = @LastName;";

                    var users_ = data_.LoadData<UserModel, dynamic>(sql, new { LastName = "Bob" },
                        ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

                    var table = DataTableConvResponse.ToDataTable(users_);

                    return "Deleted succesfully";

                }
                catch (Exception)
            {
                    return "Failed to delete data.";
                }
            }*/
    }
}
