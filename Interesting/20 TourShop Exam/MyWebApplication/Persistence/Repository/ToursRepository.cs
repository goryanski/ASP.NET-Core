using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal sealed class ToursRepository : BaseRepository<Guid, Tour>

    {
        public ToursRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Tour>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await db.Tours.Include(t => t.Country).ToListAsync();
        }

        public override async Task<Tour> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await db.Tours.Include(t => t.Country)
                .Include(t => t.TourEvents)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Tour entity)
        {
            throw new NotImplementedException();
        }
    }
}
