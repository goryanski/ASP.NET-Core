using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.DB.Entities;

namespace Practice_13.DB.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task<T> GetAsync(int id);
    }
}
