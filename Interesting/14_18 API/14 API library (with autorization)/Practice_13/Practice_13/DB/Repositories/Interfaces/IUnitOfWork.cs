using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_13.DB.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        BooksRepository BooksRepository { get; }
        AuthorsRepository AuthorsRepository { get; }
        AccountsRepository AccountsRepository { get; }
    }
}
