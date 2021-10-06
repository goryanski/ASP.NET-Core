using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions.Dto.Book;

namespace Services.Abstractions.Service
{
    public interface IBooksService
    {
        Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BookDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BookDto> CreateAsync(CreateBookDto model, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateBookDto model, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
