using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetTravelApplication.Services.Country;
using GetTravelApplication.Services.Tour;

namespace GetTravelApplication.Services
{
    public interface IServiceViewManager
    {
        public IToursViewService ToursService { get; }
        public ICountriesViewService CountriesViewService { get; }
    }
}
