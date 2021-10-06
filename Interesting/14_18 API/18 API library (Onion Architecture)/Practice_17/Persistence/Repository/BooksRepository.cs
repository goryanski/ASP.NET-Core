using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal sealed class BooksRepository : BaseRepository<Guid, Book>

    {
        public BooksRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Book> Create(Book entity)
        {
            await db.Books.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public override async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await db.Books.Include(b => b.Authors).ToListAsync();
        }

        public override Task<Book> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
