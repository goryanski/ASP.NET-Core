using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_07.DB.Entities;

namespace Practice_07.DB.Repositories
{
    public class NewsRepository : BaseRepository<News>
    {
        public NewsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
