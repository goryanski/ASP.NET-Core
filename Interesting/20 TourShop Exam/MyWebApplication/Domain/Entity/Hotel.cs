using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Hotel : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public List<Meal> Meals { get; set; }
        public List<TourEvent> TourEvents { get; set; }
    }
}
