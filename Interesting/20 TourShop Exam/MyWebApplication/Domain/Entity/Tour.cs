using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Tour : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DaysCount { get; set; }
        public int Rating { get; set; }
        public string Photo { get; set; }
        public Country Country { get; set; }
        public List<TourEvent> TourEvents { get; set; }
    }
}
