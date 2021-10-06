using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Entity;
using Services.Abstractions.Dto.Country;
using Services.Abstractions.Dto.Tour;

namespace Services.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tour, TourShortInfoDto>();
            CreateMap<TourShortInfoDto, Tour>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
        }
    }
}
