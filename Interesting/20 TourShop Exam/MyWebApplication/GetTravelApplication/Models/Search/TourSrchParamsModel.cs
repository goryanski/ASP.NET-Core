using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetTravelApplication.Models.Search
{
    public class TourSrchParamsModel
    {
        public string CountryId { get; set; }
        public bool ExcludeTour5stars { get; set; }
        public bool ExcludeTour4stars { get; set; }
        public bool ExcludeTour3stars { get; set; }
        public bool Include5starsHotelsOnly { get; set; }
        public int MaxPriceValue { get; set; }
        public int MaxDaysCountValue { get; set; }
    }
}
