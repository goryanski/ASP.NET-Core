using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice_19.Data.Entity;

namespace Practice_19.Data.Repository
{
    public class TranslationRepository
    {
        readonly ApplicationDbContext db;
        public TranslationRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task Create(Translation entity)
        {
            await db.Translations.AddAsync(entity);
            await db.SaveChangesAsync();
        }
        public async Task<List<Translation>> GetAll()
        {
            return await db.Translations.ToListAsync();
        }
    }
}
