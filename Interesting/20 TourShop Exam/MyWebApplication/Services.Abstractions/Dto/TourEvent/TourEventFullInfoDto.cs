using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstractions.Dto.Hotel;
using Services.Abstractions.Dto.Location;

namespace Services.Abstractions.Dto.TourEvent
{
    public class TourEventFullInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DayNumber { get; set; }
        public string LocationName { get; set; }
        public HotelDto Hotel { get; set; }
    }
}
