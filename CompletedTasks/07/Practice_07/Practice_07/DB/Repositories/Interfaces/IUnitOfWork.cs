using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_07.DB.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        NewsRepository NewsRepository { get; }
    }
}
