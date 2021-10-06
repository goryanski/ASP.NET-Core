using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Practice_07.Business.DTO;
using Practice_07.Models.News;

namespace Practice_07.UI.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsDTO, NewsViewModel>();
            CreateMap<NewsViewModel, NewsDTO>();
        }
    }
}
