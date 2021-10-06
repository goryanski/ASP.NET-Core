using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GetTravelApplication.Models.Search;
using GetTravelApplication.Models.Tour;

namespace GetTravelApplication.Services.Tour
{
    public interface IToursViewService
    {
        Task<List<TourShortInfoViewModel>> GetToursByStarsCount(int countStars, CancellationToken cancellationToken = default);
        Task<TourFullInfoViewModel> GetTour(Guid id, CancellationToken cancellationToken = default);
        Task<List<TourShortInfoViewModel>> GetSrchTours(TourSrchParamsModel srchParams, CancellationToken cancellationToken = default);
    }
}
