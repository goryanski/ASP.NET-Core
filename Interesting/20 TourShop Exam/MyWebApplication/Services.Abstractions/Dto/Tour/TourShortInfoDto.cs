using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstractions.Dto.Country;

namespace Services.Abstractions.Dto.Tour
{
    public class TourShortInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DaysCount { get; set; }
        public int Rating { get; set; }
        public string Photo { get; set; }
        public CountryDto Country { get; set; }
        public string CountryName { get; set; }
    }
}
