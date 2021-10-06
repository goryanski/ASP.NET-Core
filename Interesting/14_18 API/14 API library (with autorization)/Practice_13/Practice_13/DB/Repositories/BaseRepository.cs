﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice_13.DB.Entities;
using Practice_13.DB.Repositories.Interfaces;

namespace Practice_13.DB.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected DatabaseContext db;

        protected DbSet<T> Table => db.Set<T>();

        public BaseRepository(DatabaseContext context)
        {
            db = context;
        }
        public async Task CreateAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Added;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(Table.ToList());
        }
        public virtual async Task<List<T>> GetAllAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => Table.Where(predicate).ToList());
        }

        public async Task<T> GetAsync(int id)
        {
            return await Table.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            db.Entry(entity).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }
    }
}
