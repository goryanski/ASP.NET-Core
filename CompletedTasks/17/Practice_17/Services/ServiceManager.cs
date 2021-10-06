using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repository;
using Services.Abstractions.Service;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBooksService> _booksService;
        private readonly Lazy<IAuthorsService> _authorsService;
        private readonly Lazy<IUseBooksService> _useBooksService;
        private readonly Lazy<IAccountsService> _accountsService;

        public IBooksService BooksService => _booksService.Value;
        public IAuthorsService AuthorsService => _authorsService.Value;
        public IUseBooksService UseBooksService => _useBooksService.Value;
        public IAccountsService AccountsService => _accountsService.Value;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _booksService = new Lazy<IBooksService>(() => new BooksService(unitOfWork));
            _authorsService = new Lazy<IAuthorsService>(() => new AuthorsService(unitOfWork));
            _useBooksService = new Lazy<IUseBooksService>(() => new UseBooksService(unitOfWork));
            _accountsService = new Lazy<IAccountsService>(() => new AccountsService(unitOfWork));
        }
    }
}
