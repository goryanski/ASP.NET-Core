using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repository;
using Services.Abstractions.Dto.Author;
using Services.Abstractions.Service;

namespace Services
{
    internal sealed class AuthorsService : IAuthorsService
    {
        private readonly IUnitOfWork unitOfWork;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;


        public AuthorsService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await unitOfWork.AuthorsRepository.GetAllAsync();
            return objectMapper.Mapper.Map<IEnumerable<AuthorDto>>(result);
        }
    }
}
