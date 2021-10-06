using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice_19.Data.Entity;

namespace Practice_19.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<CarDescription> CarDescriptions { get; set; }
        public DbSet<Translation> Translations { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
    }
}
