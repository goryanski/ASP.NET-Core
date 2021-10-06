using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions.Dto.UseBook;

namespace Services.Abstractions.Service
{
    public interface IUseBooksService
    {
        Task<IEnumerable<UseBookDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
