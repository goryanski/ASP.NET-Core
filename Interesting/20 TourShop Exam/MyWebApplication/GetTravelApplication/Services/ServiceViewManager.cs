using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetTravelApplication.Services.Country;
using GetTravelApplication.Services.Tour;
using Services.Abstractions.Service;

namespace GetTravelApplication.Services
{
    public class ServiceViewManager : IServiceViewManager
    {
        private readonly Lazy<IToursViewService> _toursService;
        private readonly Lazy<ICountriesViewService> _countriesViewService;

        public IToursViewService ToursService => _toursService.Value;
        public ICountriesViewService CountriesViewService => _countriesViewService.Value;

        public ServiceViewManager(IServiceManager serviceManager)
        {
            _toursService = new Lazy<IToursViewService>(() => new ToursViewService(serviceManager));
            _countriesViewService = new Lazy<ICountriesViewService>(() => new CountriesViewService(serviceManager));
        }
    }
}
