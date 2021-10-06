using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions.Dto.Country;

namespace Services.Abstractions.Service
{
    public interface ICountriesService
    {
        Task<List<CountryDto>> GetAll(CancellationToken cancellationToken = default);
    }
}
