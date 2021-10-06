using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_11.DB.Entities
{
    public class ToDoItem : BaseEntity
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
