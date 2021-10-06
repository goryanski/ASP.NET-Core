using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstractions.Dto.Country;
using Services.Abstractions.Dto.TourEvent;

namespace Services.Abstractions.Dto.Tour
{
    public class TourFullInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DaysCount { get; set; }
        public int Rating { get; set; }
        public string Photo { get; set; }
        public string CountryName { get; set; }
        public List<TourEventFullInfoDto> TourEvents { get; set; }
    }
}
