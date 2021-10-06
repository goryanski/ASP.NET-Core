using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_11.DB.Entities
{
    public class User: BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<ToDoItem> Tasks { get; set; }
    }
}
