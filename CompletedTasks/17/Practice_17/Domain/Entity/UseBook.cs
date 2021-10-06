using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class UseBook : BaseEntity<Guid>
    {
        public Account User { get; set; }
        public Book Book { get; set; }
        public DateTime TakeAt { get; set; }
        public DateTime ReturnAt { get; set; }
    }
}
