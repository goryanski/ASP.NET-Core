using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_11.DB.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        TasksRepository TasksRepository { get; }
        UsersRepository UsersRepository { get; }
    }
}
