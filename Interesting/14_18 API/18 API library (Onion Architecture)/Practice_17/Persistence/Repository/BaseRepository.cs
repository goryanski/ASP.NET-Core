using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal abstract class BaseRepository<TKey, TValue> : IRepository<TKey, TValue>
        where TKey : struct
        where TValue : BaseEntity<TKey>
    {
        protected ApplicationDbContext db;
        DbSet<TValue> Table => db.Set<TValue>();

        public BaseRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public abstract Task<TValue> Create(TValue entity);

        public abstract Task<TValue> GetAsync(TKey id, CancellationToken cancellationToken = default);

        public abstract Task<IEnumerable<TValue>> GetAllAsync(CancellationToken cancellationToken = default);

        public abstract void Remove(TKey id);

        public abstract void Update(TValue entity);
    }
}
