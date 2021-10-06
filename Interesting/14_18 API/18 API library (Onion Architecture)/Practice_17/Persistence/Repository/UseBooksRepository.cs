using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal sealed class UseBooksRepository : BaseRepository<Guid, UseBook>

    {
        public UseBooksRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<UseBook> Create(UseBook entity)
        {
            await db.UseBooks.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public override async Task<IEnumerable<UseBook>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await db.UseBooks.Include(ub => ub.Book).Include(ub => ub.User).ToListAsync();
        }

        public override Task<UseBook> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(UseBook entity)
        {
            throw new NotImplementedException();
        }
    }
}
