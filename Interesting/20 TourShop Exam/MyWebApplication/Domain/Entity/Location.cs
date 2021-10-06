using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Location : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public List<TourEvent> TourEvents { get; set; }
    }
}
