using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<UseBook> UseBooks { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
    }
}
