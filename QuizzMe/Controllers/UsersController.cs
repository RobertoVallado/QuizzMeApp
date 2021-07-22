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
    public class UsersController : ApiController
    {
        // GET: Users
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string sql = "SELECT * FROM users";
            var users_ = data_.LoadData<UserModel, dynamic>(sql, new { },
                ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

            var table = DataTableConvResponse.ToDataTable(users_);

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // POST api/<controller>
        //public void Post([FromBody] string value)
        public string Post(UserModel user)
        {
            try
            {
                string sql = @"INSERT INTO users (FirstName, LastName, USerEmail, CreatedAt)
                               VALUES(@FirstName, @LastName, @UserEmail, localtime());";

                var users_ = data_.SaveData<dynamic>(sql, new {
                    FirstName = $"{user.FirstName}",
                    LastName = $"{user.LastName}",
                    UserEmail = $"{user.UserEmail}"},
                    ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

                return "User Added succesfully!";
            }
            catch (Exception)
            {
                return "Failed to add data.";
            }
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        public string Put(UserModel user) //update user
        {
            try
            {
                string sql = @"UPDATE users 
                               SET FirstName = @FirstName, LastName = @LastName, UserEmail = @UserEmail
                               WHERE UserId = @UserId;";

                var users_ = data_.SaveData<dynamic>(sql, new {
                    UserId = $"{user.UserId}",
                    FirstName = $"{user.FirstName}",
                    LastName = $"{user.LastName}",
                    UserEmail = $"{user.UserEmail}"},
                    ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

                return "User Updated succesfully!";
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
                string sql = @"DELETE FROM users WHERE UserId = @UserId;";

                var users_ = data_.SaveData<dynamic>(sql, new { UserId = $"{id}" },
                    ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString);

                return "User Deleted succesfully!";
            }
            catch (Exception)
            {
                return "Failed to delete User.";
            }
        }
    }
}