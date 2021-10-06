using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstractions.Dto.Hotel
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public List<string> Meals { get; set; }
    }
}
