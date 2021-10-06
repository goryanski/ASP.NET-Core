using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GetTravelApplication.Models.Country;
using Services.Abstractions.Service;

namespace GetTravelApplication.Services.Country
{
    public class CountriesViewService : ICountriesViewService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        readonly IServiceManager serviceManager;
        public CountriesViewService(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public async Task<List<CountryViewModel>> GelAll(CancellationToken cancellationToken = default)
        {
            var result = await serviceManager.CountriesService.GetAll();
            return objectMapper.Mapper.Map<List<CountryViewModel>>(result);
        }
    }
}
