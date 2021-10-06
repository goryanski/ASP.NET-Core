using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetTravelApplication.Models.Country;
using GetTravelApplication.Models.Search;
using GetTravelApplication.Models.Tour;

namespace GetTravelApplication.Models
{
    public class ProSearchViewModel
    {
        public List<CountryViewModel> Countries { get; set; }
        public List<TourShortInfoViewModel> Tours { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public TourSrchParamsModel Filter { get; set; }
    }
}
