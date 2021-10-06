using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions.Dto.Author;

namespace Services.Abstractions.Service
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AuthorDto> CreateAsync(CreateAuthorDto model, CancellationToken cancellationToken = default);
    }
}
