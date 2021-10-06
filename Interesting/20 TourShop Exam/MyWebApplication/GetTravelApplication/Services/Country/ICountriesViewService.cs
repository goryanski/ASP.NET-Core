using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GetTravelApplication.Models.Country;

namespace GetTravelApplication.Services.Country
{
    public interface ICountriesViewService
    {
        Task<List<CountryViewModel>> GelAll(CancellationToken cancellationToken = default);
    }
}
