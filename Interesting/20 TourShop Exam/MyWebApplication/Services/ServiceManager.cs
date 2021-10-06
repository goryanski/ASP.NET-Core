using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repository;
using Services.Abstractions.Service;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IToursService> _toursService;
        private readonly Lazy<ICountriesService> _countriesService;
        public IToursService ToursService => _toursService.Value;
        public ICountriesService CountriesService => _countriesService.Value;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _toursService = new Lazy<IToursService>(() => new ToursService(unitOfWork));
            _countriesService = new Lazy<ICountriesService>(() => new CountriesService(unitOfWork));
        }
    }
}
