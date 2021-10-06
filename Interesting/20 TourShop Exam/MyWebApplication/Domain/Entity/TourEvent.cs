using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class TourEvent : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DayNumber { get; set; }
        public Location Location { get; set; }
        public Guid LocationId { get; set; }
        public Hotel Hotel { get; set; }
        public Guid HotelId { get; set; }
        public Tour Tour { get; set; }
    }
}
