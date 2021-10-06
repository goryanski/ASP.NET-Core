using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetTravelApplication.Models.Hotel;

namespace GetTravelApplication.Models.TourEvent
{
    public class TourEventFullInfoViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DayNumber { get; set; }
        public string LocationName { get; set; }
        public HotelViewModel Hotel { get; set; }
    }
}
