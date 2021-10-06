using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repository;
using Services.Abstractions.Dto.Hotel;
using Services.Abstractions.Dto.Location;
using Services.Abstractions.Dto.Search;
using Services.Abstractions.Dto.Tour;
using Services.Abstractions.Dto.TourEvent;
using Services.Abstractions.Service;

namespace Services
{
    internal sealed class ToursService : IToursService
    {
        private readonly IUnitOfWork unitOfWork;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public ToursService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        // transform every tour to more convenient Dto object
        public async Task<TourFullInfoDto> GetTourAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tour = await unitOfWork.ToursRepository.GetAsync(id);

            List<TourEventFullInfoDto> tourEventFullInfoDtoList = new List<TourEventFullInfoDto>();
            foreach (var tourEvent in tour.TourEvents)
            {
                var hotel = await unitOfWork.HotelsRepository.GetAsync(tourEvent.HotelId);

                List<string> mealsList = new List<string>();
                foreach (var meal in hotel.Meals)
                {
                    mealsList.Add(meal.Name);
                }

                HotelDto hotelDto = new HotelDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Rating = hotel.Rating,
                    Meals = mealsList
                };

                var location = await unitOfWork.LocationsRepository.GetAsync(tourEvent.LocationId);

                TourEventFullInfoDto tourEventFullInfoDto = new TourEventFullInfoDto
                {
                    Id = tourEvent.Id,
                    Name = tourEvent.Name,
                    DayNumber = tourEvent.DayNumber,
                    Description = tourEvent.Description,
                    LocationName = location.Name,
                    Hotel = hotelDto
                };
                tourEventFullInfoDtoList.Add(tourEventFullInfoDto);
            }
            
            TourFullInfoDto tourFullInfoDto = new TourFullInfoDto
            {
                Id = tour.Id,
                Name = tour.Name,
                CountryName = tour.Country.Name,
                Photo = tour.Photo,
                Rating = tour.Rating,
                Price = tour.Price,
                DaysCount = tour.DaysCount,
                TourEvents = tourEventFullInfoDtoList
            };
            return tourFullInfoDto;
        }

        private async Task<IEnumerable<TourShortInfoDto>> GetAllShortInfoTours(CancellationToken cancellationToken = default)
        {
            var tours = await unitOfWork.ToursRepository.GetAllAsync();
            var toursDto = MapTours(tours);
            return toursDto;
        }

        private IEnumerable<TourShortInfoDto> MapTours(IEnumerable<Tour> tours)
        {
            var toursDto =  objectMapper.Mapper.Map<IEnumerable<TourShortInfoDto>>(tours);
            foreach (var tour in toursDto)
            {
                // get only name of country
                string tourName = tour.Country.Name;
                tour.CountryName = tourName;
                tour.Country = null;
            }
            return toursDto;
        }

        public async Task<List<TourShortInfoDto>> GetFiveStarsTours(CancellationToken cancellationToken = default)
        {
            return (await GetAllShortInfoTours()).Where(t => t.Rating == 5).ToList();
        }
        public async Task<List<TourShortInfoDto>> GetFourStarsTours(CancellationToken cancellationToken = default)
        {
            return (await GetAllShortInfoTours()).Where(t => t.Rating == 4).ToList();
        }
        public async Task<List<TourShortInfoDto>> GetThreeStarsTours(CancellationToken cancellationToken = default)
        {
            return (await GetAllShortInfoTours()).Where(t => t.Rating == 3).ToList();
        }

        public async Task<List<TourShortInfoDto>> GetSrchTours(TourSrchParamsModelDto filter, CancellationToken cancellationToken = default)
        {
            var allTours = (await unitOfWork.ToursRepository.GetAllAsync()).ToList();
            List<Tour> result = new List<Tour>();

            // first filter by country to significantly reduce the number of tours
            if (filter.CountryId is null)
            {
                // country was not selected
                result = allTours;
            }
            else
            {
                result = allTours.Where(t => t.Country.Id == Guid.Parse(filter.CountryId)).ToList();
            }

            // then filter by rating
            if (filter.ExcludeTour5stars)
            {
                result.RemoveAll(t => t.Rating == 5);
            }
            if (filter.ExcludeTour4stars)
            {
                result.RemoveAll(t => t.Rating == 4);
            }
            if (filter.ExcludeTour3stars)
            {
                result.RemoveAll(t => t.Rating == 3);
            }

            // price, days
            result.RemoveAll(t => t.Price > filter.MaxPriceValue);
            result.RemoveAll(t => t.DaysCount > filter.MaxDaysCountValue);


            // in the end the most resource-intensive task (when the number of tours is reduced to the maximum)
            if (filter.Include5starsHotelsOnly)
            {
                List<Tour> matched = new List<Tour>();
                foreach (var tour in result)
                {
                    //var tourId = tour.Id
                    var srchTour = await GetTourAsync(tour.Id);
                    if(srchTour.TourEvents.All(e => e.Hotel.Rating == 5))
                    {
                        matched.Add(tour);
                    } 
                }
                result = matched;
            }

            // use foreach from GetAllShortInfoTours 
            return MapTours(result).ToList();
        }
    }
}
