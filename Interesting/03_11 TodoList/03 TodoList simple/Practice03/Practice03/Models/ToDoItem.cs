using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice03.Models
{
    public class ToDoItem
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public override string ToString() => $"{Text}";
    }
}
