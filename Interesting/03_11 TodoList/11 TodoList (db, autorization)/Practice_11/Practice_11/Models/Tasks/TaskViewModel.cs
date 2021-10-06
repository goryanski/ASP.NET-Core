using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Models.Auth;

namespace Practice_11.Models.Tasks
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }

        public string Color { get; set; }
    }
}
