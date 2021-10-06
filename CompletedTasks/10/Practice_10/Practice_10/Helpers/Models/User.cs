using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_10.Helpers.Models
{
    public class User
    {
        // id is not necessary because Login will be unique
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretQuestionAnswer { get; set; }
    }
}
