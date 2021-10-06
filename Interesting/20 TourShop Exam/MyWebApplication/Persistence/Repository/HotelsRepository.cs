using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal sealed class HotelsRepository : BaseRepository<Guid, Hotel>

    {
        public HotelsRepository(ApplicationContext context) : base(context)
        {
        }

        public override Task<IEnumerable<Hotel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override async Task<Hotel> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await db.Hotels.Include(h => h.Meals).FirstOrDefaultAsync(h => h.Id == id);
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Hotel entity)
        {
            throw new NotImplementedException();
        }
    }
}