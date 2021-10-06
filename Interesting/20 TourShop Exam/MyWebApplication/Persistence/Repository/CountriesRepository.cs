using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    internal sealed class CountriesRepository : BaseRepository<Guid, Country>

    {
        public CountriesRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await db.Countries.ToListAsync();
        }

        public override Task<Country> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
