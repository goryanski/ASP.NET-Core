using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetTravelApplication.Models.TourEvent;

namespace GetTravelApplication.Models.Tour
{
    public class TourFullInfoViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DaysCount { get; set; }
        public int Rating { get; set; }
        public string Photo { get; set; }
        public string CountryName { get; set; }
        public List<TourEventFullInfoViewModel> TourEvents { get; set; }
    }
}
