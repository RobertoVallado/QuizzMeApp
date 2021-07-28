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
//using data_2 = DataLibrary.DataAccess; 

namespace QuizzMe.Controllers
{
    public class ScoresController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            string sql = "SELECT * FROM scores WHERE UserId = @UserId";
    
            var board_ = data_.LoadData<ScoreModel, dynamic>(sql, new { UserId = $"{id}" },
                ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

            var table = DataTableConvResponse.ToDataTable(board_);

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public HttpResponseMessage GetScores(int userId, int testId)
        {
            string sql = "SELECT * FROM scores WHERE UserId = @UserId AND TestId = @TestId";

            var board_ = data_.LoadData<ScoreModel, dynamic>(sql, new { UserId = $"{userId}", TestId = $"{testId}" },
                ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

            var table = DataTableConvResponse.ToDataTable(board_);

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public async Task<string> Post(ScoreModel board)
        { 
            try
            { 
                string sql = @"INSERT INTO scores (UserId, TestId, TestTime, TestDate, CorrectAnswers, IncorrectAnswers, Score, HighestScore)
                                          VALUES (@UserId, @TestId, @TestTime, @TestDate, @CorrectAnswers, @IncorrectAnswers, @Score, @HighestScore);";

                await data_.SaveData<dynamic>(sql, new {
                        UserId = $"{board.UserId}",
                        TestId= $"{board.TestId}",
                        TestTime = $"{board.TestTime}",
                        TestDate = $"{board.TestDate}",
                        CorrectAnswers = $"{board.CorrectAnswers}",
                        IncorrectAnswers = $"{board.IncorrectAnswers}",
                        Score = $"{board.Score}",
                        HighestScore = $"{board.HighestScore}"}, ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

                return "New Score has been added successfully!";
            }
            catch (Exception ex)
            {
                return "Failed to add data.";
            }
        }

        //public void Put(int id, [FromBody] string value)
        public string Put(int userId, int testId) //update high scores boolean values
        {
            try
            {
                string sql = @"UPDATE scores 
                              SET HighestScore = 0
                              WHERE UserId = @UserId AND TestId = @TestId;";

                var board_= data_.SaveData<dynamic>(sql,new { UserId = $"{userId}", TestId = $"{testId}" },

                    ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

                return "Score Updated successfully!";
            }
            catch (Exception)
            {
                return "Failed to update data.";
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public string Delete(int id)
        {
            try
            {
                string sql = @"DELETE FROM scores WHERE ScoreId = @ScoreId;";

                var board_ = data_.SaveData<dynamic>(sql, new { ScoreId = $"{id}" },
                    ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

                return "Score Deleted successfully!";
            }
            catch (Exception)
            {
                return "Failed to delete User.";
            }
        }
    }
}