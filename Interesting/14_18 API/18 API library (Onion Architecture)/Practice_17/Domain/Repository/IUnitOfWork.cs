using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Repository
{
    public interface IUnitOfWork
    {
        public IRepository<Guid, Account> AccountsRepository { get; }
        public IRepository<Guid, Book> BooksRepository { get; }
        public IRepository<Guid, Author> AuthorsRepository { get; }
        public IRepository<Guid, UseBook> UseBooksRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
