using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetTravelApplication.Models.Hotel
{
    public class HotelViewModel
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public List<string> Meals { get; set; }
    }
}
