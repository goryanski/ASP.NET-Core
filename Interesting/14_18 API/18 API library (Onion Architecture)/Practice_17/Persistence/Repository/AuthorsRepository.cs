using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal sealed class AuthorsRepository : BaseRepository<Guid, Author>

    {
        public AuthorsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Author> Create(Author entity)
        {
            await db.Authors.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public override async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await db.Authors.Include(a => a.Books).ToListAsync();
        }

        public override Task<Author> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
