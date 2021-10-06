using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Entity;
using Services.Abstractions.Dto.Account;
using Services.Abstractions.Dto.Author;
using Services.Abstractions.Dto.Book;
using Services.Abstractions.Dto.UseBook;

namespace Services.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<UseBook, UseBookDto>();
            CreateMap<UseBookDto, UseBook>();
        }
    }
}
