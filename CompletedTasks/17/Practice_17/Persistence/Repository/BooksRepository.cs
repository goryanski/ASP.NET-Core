using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;

namespace Persistence.Repository
{
    internal sealed class BooksRepository : BaseRepository<Guid, Book>

    {
        public BooksRepository(ApplicationDbContext context) : base(context)
        {
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
