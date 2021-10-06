using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<TourEvent> TourEvents { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
    }
}
