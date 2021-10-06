using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repository;
using Services.Abstractions.Dto.Book;
using Services.Abstractions.Service;

namespace Services
{
    internal sealed class BooksService : IBooksService
    {
        private readonly IUnitOfWork unitOfWork;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;


        public BooksService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public Task<BookDto> CreateAsync(CreateBookDto model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await unitOfWork.BooksRepository.GetAllAsync();
            return objectMapper.Mapper.Map<IEnumerable<BookDto>>(result);
        }

        public Task<BookDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateBookDto model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
