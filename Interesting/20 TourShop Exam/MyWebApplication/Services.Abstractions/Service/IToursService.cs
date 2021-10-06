using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions.Dto.Search;
using Services.Abstractions.Dto.Tour;

namespace Services.Abstractions.Service
{
    public interface IToursService
    {
        Task<TourFullInfoDto> GetTourAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<TourShortInfoDto>> GetFiveStarsTours(CancellationToken cancellationToken = default);
        Task<List<TourShortInfoDto>> GetFourStarsTours(CancellationToken cancellationToken = default);
        Task<List<TourShortInfoDto>> GetThreeStarsTours(CancellationToken cancellationToken = default);
        Task<List<TourShortInfoDto>> GetSrchTours(TourSrchParamsModelDto paramsModelDto, CancellationToken cancellationToken = default);
    }
}
