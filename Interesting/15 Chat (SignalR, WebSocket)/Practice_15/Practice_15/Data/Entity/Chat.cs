using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_15.Data.Entity
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; } // many-to-many
        public List<Message> Messages { get; set; } // one-to-many
    }
}
