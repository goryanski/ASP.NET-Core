using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstractions.Dto.Search
{
    public class TourSrchParamsModelDto
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
