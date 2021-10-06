using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Practice_11.Business.DTO;
using Practice_11.DB.Entities;

namespace Practice_11.Business.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItem, TaskDTO>();
            CreateMap<TaskDTO, ToDoItem>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
