using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<UseBookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await unitOfWork.UseBooksRepository.GetAllAsync();
            ;
            return objectMapper.Mapper.Map<IEnumerable<UseBookDto>>(result);
        }
    }
}
