using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repository;
using Services.Abstractions.Dto.UseBook;
using Services.Abstractions.Service;

namespace Services
{
    internal sealed class UseBooksService : IUseBooksService
    {
        private readonly IUnitOfWork unitOfWork;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public UseBooksService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public async Task<UseBookDto> CreateAsync(CreateUseBookDto model, CancellationToken cancellationToken = default)
        {
            var entity = objectMapper.Mapper.Map<UseBook>(model);
            var result = await unitOfWork.UseBooksRepository.Create(entity);
            return objectMapper.Mapper.Map<UseBookDto>(result);
        }

        public async Task<IEnumerable<UseBookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await unitOfWork.UseBooksRepository.GetAllAsync();
            return objectMapper.Mapper.Map<IEnumerable<UseBookDto>>(result);
        }
    }
}
