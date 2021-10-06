using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_07.DB.Entities;

namespace Practice_07.DB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        DatabaseContext db;
        NewsRepository _newsRepository;
        public NewsRepository NewsRepository
            => _newsRepository ?? (_newsRepository = new NewsRepository(db));

        public UnitOfWork(DatabaseContext context)
        {
            db = context;
            DbInit();
        }

        private void DbInit()
        {
            News news1 = new News
            {
                Title = "Lorem",
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                Date = new DateTime(2021, 03, 11)
            };
            News news2 = new News
            {
                Title = "Ipsum",
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                Date = new DateTime(2021, 05, 07)
            };
            News news3 = new News
            {
                Title = "Dolor",
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                Date = new DateTime(2021, 06, 22)
            };

            db.News.Add(news1);
            db.News.Add(news2);
            db.News.Add(news3);

            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
