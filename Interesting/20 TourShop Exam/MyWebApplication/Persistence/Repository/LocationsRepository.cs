using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal sealed class LocationsRepository : BaseRepository<Guid, Location>

    {
        public LocationsRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Location>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await db.Locations.ToListAsync();
        }

        public override async Task<Location> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await db.Locations.FirstOrDefaultAsync(l => l.Id == id);
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Location entity)
        {
            throw new NotImplementedException();
        }
    }
}
