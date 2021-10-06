using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstractions.Service
{
    public interface IServiceManager
    {
        public IBooksService BooksService { get; }
        public IAuthorsService AuthorsService { get; }
        public IAccountsService AccountsService { get; }
        public IUseBooksService UseBooksService { get; }
    }
}
