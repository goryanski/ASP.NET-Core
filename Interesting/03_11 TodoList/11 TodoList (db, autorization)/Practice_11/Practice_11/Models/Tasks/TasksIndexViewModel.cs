using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_11.Models.Tasks
{
    public class TasksIndexViewModel
    {
        public List<TaskViewModel> Tasks { get; set; }
        public int AuthUserId { get; set; }
    }
}
