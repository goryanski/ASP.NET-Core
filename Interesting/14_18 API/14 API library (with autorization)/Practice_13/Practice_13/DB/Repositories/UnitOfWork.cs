using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.DB.Entities;
using Practice_13.DB.Repositories.Interfaces;

namespace Practice_13.DB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        DatabaseContext db;
        AuthorsRepository _authorsRepos;
        BooksRepository _booksRepos;
        AccountsRepository _accountsRepository;

        public AuthorsRepository AuthorsRepository =>
           _authorsRepos ?? (_authorsRepos = new AuthorsRepository(db));
        public BooksRepository BooksRepository =>
           _booksRepos ?? (_booksRepos = new BooksRepository(db));
        public AccountsRepository AccountsRepository =>
          _accountsRepository ?? (_accountsRepository = new AccountsRepository(db));

        public UnitOfWork(DatabaseContext context)
        {
            db = context;
            //DbInit();
        }

        private void DbInit()
        {
            Author author1 = new Author
            {
                FullName = "Grinenko Alexey Alekseevich"
            };
            Author author2 = new Author
            {
                FullName = "Gruntal Mark Albertovich"
            };
            Author author3 = new Author
            {
                FullName = "Gursky Georgy Valentinovich"
            };
            Author author4 = new Author
            {
                FullName = "Dzhemgirov Ochir Sandjievich"
            };
            Author author5 = new Author
            {
                FullName = "Dunaev Yaroslav Alexandrovich"
            };

            Book book1 = new Book
            {
                Name = "Sky",
                Genre = "Nature",
                IsExist = true,
                Pages = 440,
                PublishYear = 2010,
                Authors = new List<Author> { author1, author2 }
            };
            Book book2 = new Book
            {
                Name = "Bronze Key",
                Genre = "Psychology",
                IsExist = true,
                Pages = 520,
                PublishYear = 2016,
                Authors = new List<Author> { author3, author5 }
            };
            Book book3 = new Book
            {
                Name = "Thieves of dreams",
                Genre = "Drama",
                IsExist = true,
                Pages = 620,
                PublishYear = 1993,
                Authors = new List<Author> { author4 }
            };
            Book book4 = new Book
            {
                Name = "Red laugh",
                Genre = "Horror",
                IsExist = true,
                Pages = 670,
                PublishYear = 2021,
                Authors = new List<Author> { author5 }
            };
            Book book5 = new Book
            {
                Name = "Child of sun",
                Genre = "Science",
                IsExist = true,
                Pages = 470,
                PublishYear = 1998,
                Authors = new List<Author> { author2 }
            };

            db.Books.Add(book1);
            db.Books.Add(book2);
            db.Books.Add(book3);
            db.Books.Add(book4);
            db.Books.Add(book5);

            Account account1 = new Account
            {
                Password = "123456",
                Username = "vasya",
                Role = "ADMIN"
            };
            Account account2 = new Account
            {
                Password = "admin",
                Username = "admin",
                Role = "USER"
            };

            db.Accounts.Add(account1);
            db.Accounts.Add(account2);

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
