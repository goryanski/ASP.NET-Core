using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_15.Data.Entity
{
    public class Message
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
        public Chat Chat { get; set; }
        public int ChatId { get; set; }
    }
}
