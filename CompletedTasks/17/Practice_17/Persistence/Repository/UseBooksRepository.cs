using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;

namespace Persistence.Repository
{
    internal sealed class UseBooksRepository : BaseRepository<Guid, UseBook>

    {
        public UseBooksRepository(ApplicationDbContext context) : base(context)
        {
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
