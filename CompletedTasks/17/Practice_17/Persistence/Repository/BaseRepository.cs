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

        public void Create(TValue entity)
        {
            db.Entry(entity).State = EntityState.Added;
        }

        public abstract Task<TValue> GetAsync(TKey id, CancellationToken cancellationToken = default);

        public async Task<IEnumerable<TValue>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Table.ToListAsync(cancellationToken);
        }

        public abstract void Remove(TKey id);

        public abstract void Update(TValue entity);
    }
}
