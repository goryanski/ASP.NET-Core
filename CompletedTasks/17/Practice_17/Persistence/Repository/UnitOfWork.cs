using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repository;

namespace Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext db;
        readonly Lazy<IRepository<Guid, Book>> _booksRepository;
        readonly Lazy<IRepository<Guid, Author>> _authorsRepository;
        readonly Lazy<IRepository<Guid, UseBook>> _useBooksRepository;
        readonly Lazy<IRepository<Guid, Account>> _accountsRepository;

        public IRepository<Guid, Book> BooksRepository => _booksRepository.Value;
        public IRepository<Guid, Author> AuthorsRepository => _authorsRepository.Value;
        public IRepository<Guid, UseBook> UseBooksRepository => _useBooksRepository.Value;
        public IRepository<Guid, Account> AccountsRepository => _accountsRepository.Value;

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
            _booksRepository = new Lazy<IRepository<Guid, Book>>(() => new BooksRepository(context));
            _accountsRepository = new Lazy<IRepository<Guid, Account>>(() => new AccountsRepository(context));
            _useBooksRepository = new Lazy<IRepository<Guid, UseBook>>(() => new UseBooksRepository(context));
            _authorsRepository = new Lazy<IRepository<Guid, Author>>(() => new AuthorsRepository(context));

            //DbInit();
        }

        private void DbInit()
        {
            Author author1 = new Author
            {
                FullName = "Grinenko Alexey Alekseevich",
                CreatedAt = DateTime.Now
            };
            Author author2 = new Author
            {
                FullName = "Gruntal Mark Albertovich",
                CreatedAt = DateTime.Now
            };
            Author author3 = new Author
            {
                FullName = "Gursky Georgy Valentinovich",
                CreatedAt = DateTime.Now
            };
            Author author4 = new Author
            {
                FullName = "Dzhemgirov Ochir Sandjievich",
                CreatedAt = DateTime.Now
            };

            Book book1 = new Book
            {
                Name = "Sky",
                Genre = "Nature",
                IsExist = true,
                Pages = 440,
                PublishYear = 2010,
                Authors = new List<Author> { author1, author2 },
                CreatedAt = DateTime.Now
            };
            Book book2 = new Book
            {
                Name = "Bronze Key",
                Genre = "Psychology",
                IsExist = true,
                Pages = 520,
                PublishYear = 2016,
                Authors = new List<Author> { author3, author4 },
                CreatedAt = DateTime.Now
            };
            Book book3 = new Book
            {
                Name = "Thieves of dreams",
                Genre = "Drama",
                IsExist = true,
                Pages = 620,
                PublishYear = 1993,
                Authors = new List<Author> { author4 },
                CreatedAt = DateTime.Now
            };
            Book book4 = new Book
            {
                Name = "Red laugh",
                Genre = "Horror",
                IsExist = true,
                Pages = 670,
                PublishYear = 2021,
                Authors = new List<Author> { author1 },
                CreatedAt = DateTime.Now
            };
            db.Books.Add(book1);
            db.Books.Add(book2);
            db.Books.Add(book3);
            db.Books.Add(book4);
            //SaveChangesAsync();

            Account account1 = new Account
            {
                Email = "nina",
                Password = "1111",
                CreatedAt = DateTime.Now
            };
            Account account2 = new Account
            {
                Email = "rita",
                Password = "1111",
                CreatedAt = DateTime.Now
            };
            db.Accounts.Add(account1);
            db.Accounts.Add(account2);
            //SaveChangesAsync();
            db.SaveChanges();

            UseBook useBook1 = new UseBook
            {
                Book = book1,
                User = account1,
                TakeAt = DateTime.Now.AddDays(-10),
                ReturnAt = DateTime.Now.AddDays(+20),
                CreatedAt = DateTime.Now
            };
            UseBook useBook2 = new UseBook
            {
                Book = book2,
                User = account2,
                TakeAt = DateTime.Now.AddDays(-5),
                ReturnAt = DateTime.Now.AddDays(+17),
                CreatedAt = DateTime.Now
            };
            UseBook useBook3 = new UseBook
            {
                Book = book3,
                User = account1,
                TakeAt = DateTime.Now.AddDays(-1),
                ReturnAt = DateTime.Now.AddDays(+24),
                CreatedAt = DateTime.Now
            };
            db.UseBooks.Add(useBook1);
            db.UseBooks.Add(useBook2);
            db.UseBooks.Add(useBook3);
            SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return db.SaveChangesAsync();
        }
    }
}
