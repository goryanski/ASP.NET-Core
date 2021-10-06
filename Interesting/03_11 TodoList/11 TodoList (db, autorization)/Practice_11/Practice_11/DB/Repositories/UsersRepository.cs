using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.DB.Entities;

namespace Practice_11.DB.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(DatabaseContext context) : base(context)
        {
        }
        internal async Task<User> GetUserByEmail(string email)
        {
            var users = await GetAllAsync();
            return users.FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}
