using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Practice_13.Business.DTO;
using Practice_13.Business.DTO.Books;
using Practice_13.DB.Entities;

namespace Practice_13.Business.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorDTO, Author>();

            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
            
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
        }
    }
}
