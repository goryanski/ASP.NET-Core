using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
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

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto model, CancellationToken cancellationToken = default)
        {
            var entity = objectMapper.Mapper.Map<Author>(model);
            var result = await unitOfWork.AuthorsRepository.Create(entity);
            return objectMapper.Mapper.Map<AuthorDto>(result);
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await unitOfWork.AuthorsRepository.GetAllAsync();
            // to avoid cycle while getting info
            foreach (var i in result)
            {
                foreach (var j in i.Books)
                {
                    j.Authors = null;
                }
            }
            return objectMapper.Mapper.Map<IEnumerable<AuthorDto>>(result);
        }
    }
}
