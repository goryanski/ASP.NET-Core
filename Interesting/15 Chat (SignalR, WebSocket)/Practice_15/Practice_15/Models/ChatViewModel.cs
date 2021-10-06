using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_15.Data.Entity;

namespace Practice_15.Models
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MessageViewModel> Messages { get; set; }
    }
}
