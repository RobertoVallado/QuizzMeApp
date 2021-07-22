using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizzMe.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string CreatedAt { get; set; } //string datetime ??
    }
}