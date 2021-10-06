using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;

namespace Persistence.Repository
{
    internal sealed class AccountsRepository : BaseRepository<Guid, Account>

    {
        public AccountsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<Account> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
