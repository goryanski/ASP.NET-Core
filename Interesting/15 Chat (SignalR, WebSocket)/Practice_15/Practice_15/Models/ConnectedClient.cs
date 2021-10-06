using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Practice_15.Models
{
    public class ConnectedClient
    {
        public string ConnectionId { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
    }
}
