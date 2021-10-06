using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice_19.Data.Entity;

namespace Practice_19.Data.Repository
{
    public class CarDescriptionRepository
    {
        readonly ApplicationDbContext db;
        public CarDescriptionRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task Create(CarDescription entity)
        {
            await db.CarDescriptions.AddAsync(entity);
            await db.SaveChangesAsync();
        }
        public async Task<IEnumerable<CarDescription>> GetAll()
        {
            return await Task.FromResult(db.Set<CarDescription>().Include(c => c.Translations).ToList());
        }
    }
}
