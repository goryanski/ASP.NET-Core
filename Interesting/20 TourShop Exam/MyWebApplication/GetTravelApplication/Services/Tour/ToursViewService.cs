using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GetTravelApplication.Models.Search;
using GetTravelApplication.Models.Tour;
using Services.Abstractions.Service;
using Services.Abstractions.Dto.Search;
using Services.Abstractions.Dto.Tour;

namespace GetTravelApplication.Services.Tour
{
    public class ToursViewService : IToursViewService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        readonly IServiceManager serviceManager;
        public ToursViewService(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        public async Task<List<TourShortInfoViewModel>> GetToursByStarsCount(int countStars, CancellationToken cancellationToken = default)
        {
            return countStars switch
            {
                5 => await GetFiveStarsTours(),
                4 => await GetFourStarsTours(),
                _ => await GetThreeStarsTours()
            };
        }

        public async Task<List<TourShortInfoViewModel>> GetFiveStarsTours(CancellationToken cancellationToken = default)
        {
            var result = await serviceManager.ToursService.GetFiveStarsTours();
            return objectMapper.Mapper.Map<List<TourShortInfoViewModel>>(result);
        }

        public async Task<List<TourShortInfoViewModel>> GetFourStarsTours(CancellationToken cancellationToken = default)
        {
            var result = await serviceManager.ToursService.GetFourStarsTours();
            return objectMapper.Mapper.Map<List<TourShortInfoViewModel>>(result);
        }

        public async Task<List<TourShortInfoViewModel>> GetThreeStarsTours(CancellationToken cancellationToken = default)
        {
            var result = await serviceManager.ToursService.GetThreeStarsTours();
            return objectMapper.Mapper.Map<List<TourShortInfoViewModel>>(result);
        }

        public async Task<TourFullInfoViewModel> GetTour(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await serviceManager.ToursService.GetTourAsync(id);
            return objectMapper.Mapper.Map<TourFullInfoViewModel>(result);
        }

        public async Task<List<TourShortInfoViewModel>> GetSrchTours(TourSrchParamsModel srchParams, CancellationToken cancellationToken = default)
        {
            TourSrchParamsModelDto paramsModelDto = objectMapper.Mapper.Map<TourSrchParamsModelDto>(srchParams);
            List<TourShortInfoDto> result = await serviceManager.ToursService.GetSrchTours(paramsModelDto);
            return objectMapper.Mapper.Map<List<TourShortInfoViewModel>>(result);
        }
    }
}
