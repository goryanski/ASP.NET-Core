using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Practice_11.Business.DTO;
using Practice_11.Models.Auth;
using Practice_11.Models.Tasks;

namespace Practice_11.UI.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskDTO, TaskViewModel>();
            CreateMap<TaskViewModel, TaskDTO>();

            CreateMap<UserDTO, UserViewModel>();
            CreateMap<UserViewModel, UserDTO>();
        }
    }
}
