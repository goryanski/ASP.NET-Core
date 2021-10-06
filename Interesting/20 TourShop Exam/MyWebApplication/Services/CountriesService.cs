using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repository;
using Services.Abstractions.Dto.Country;
using Services.Abstractions.Service;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly IUnitOfWork unitOfWork;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public CountriesService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public async Task<List<CountryDto>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await unitOfWork.CountriesRepository.GetAllAsync();
            return objectMapper.Mapper.Map<List<CountryDto>>(result);
        }
    }
}
