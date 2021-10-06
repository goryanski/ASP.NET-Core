using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Models.Tasks;

namespace Practice_11.Models.Auth
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<TaskViewModel> Tasks { get; set; }
    }
}
