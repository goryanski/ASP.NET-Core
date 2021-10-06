using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetTravelApplication.Models.Tour;

namespace GetTravelApplication.Models
{
    public class IndexViewModel
    {
        public IEnumerable<TourShortInfoViewModel> Tours { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
