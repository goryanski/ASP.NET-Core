using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Practice_07.Business.DTO;
using Practice_07.DB.Entities;

namespace Practice_07.Business.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsDTO>();
            CreateMap<NewsDTO, News>();
        }
    }
}
