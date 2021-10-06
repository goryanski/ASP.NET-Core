using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice_13.DB.Entities;

namespace Practice_13.DB.Repositories
{
    public class AuthorsRepository : BaseRepository<Author>
    {
        public AuthorsRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Author> GetFullAuthorInfo(int id)
        {
            return await Table.Include(a => a.Books).FirstOrDefaultAsync(entity => entity.Id == id);
        }
    }
}
