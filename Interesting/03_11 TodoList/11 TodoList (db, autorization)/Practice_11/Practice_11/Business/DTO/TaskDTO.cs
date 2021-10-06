using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_11.Business.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
