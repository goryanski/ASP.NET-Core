using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GetTravelApplication.Models.Country;
using GetTravelApplication.Models.Hotel;
using GetTravelApplication.Models.Search;
using GetTravelApplication.Models.Tour;
using GetTravelApplication.Models.TourEvent;
using Services.Abstractions.Dto.Country;
using Services.Abstractions.Dto.Hotel;
using Services.Abstractions.Dto.Search;
using Services.Abstractions.Dto.Tour;
using Services.Abstractions.Dto.TourEvent;

namespace GetTravelApplication.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TourShortInfoDto, TourShortInfoViewModel>();
            CreateMap<TourShortInfoViewModel, TourShortInfoDto>();

            CreateMap<TourFullInfoDto, TourFullInfoViewModel>();
            CreateMap<TourFullInfoViewModel, TourFullInfoDto>();

            CreateMap<TourEventFullInfoDto, TourEventFullInfoViewModel>();
            CreateMap<TourEventFullInfoViewModel, TourEventFullInfoDto>();

            CreateMap<HotelDto, HotelViewModel>();
            CreateMap<HotelViewModel, HotelDto>();

            CreateMap<CountryDto, CountryViewModel>();
            CreateMap<CountryViewModel, CountryDto>();

            CreateMap<TourSrchParamsModelDto, TourSrchParamsModel>();
            CreateMap<TourSrchParamsModel, TourSrchParamsModelDto>();
        }
    }
}
