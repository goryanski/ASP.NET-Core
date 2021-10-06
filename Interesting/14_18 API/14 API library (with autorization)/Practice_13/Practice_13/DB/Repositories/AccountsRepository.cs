using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.DB.Entities;

namespace Practice_13.DB.Repositories
{
    public class AccountsRepository : BaseRepository<Account>
    {
        public AccountsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
