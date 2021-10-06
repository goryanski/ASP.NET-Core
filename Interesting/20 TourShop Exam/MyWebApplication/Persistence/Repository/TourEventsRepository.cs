using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal sealed class TourEventsRepository : BaseRepository<Guid, TourEvent>

    {
        public TourEventsRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<TourEvent>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await db.TourEvents.Include(e => e.Hotel).Include(e => e.Location).ToListAsync();
        }

        public override Task<TourEvent> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return db.TourEvents.Include(e => e.Location)
                .Include(e => e.Hotel)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(TourEvent entity)
        {
            throw new NotImplementedException();
        }
    }
}
