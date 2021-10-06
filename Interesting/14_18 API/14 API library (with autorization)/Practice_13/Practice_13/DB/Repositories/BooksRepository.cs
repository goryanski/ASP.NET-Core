using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice_13.DB.Entities;

namespace Practice_13.DB.Repositories
{
    public class BooksRepository : BaseRepository<Book>
    {
        public BooksRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task Update(Book entity)
        {
            var book = await GetAsync(entity.Id);
            book.Name = entity.Name;
            book.Pages = entity.Pages;
            book.Genre = entity.Genre;
            book.IsExist = entity.IsExist;
            book.PublishYear = entity.PublishYear;
            book.Authors = entity.Authors;
            db.Entry(book).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllBooksWithAuthors()
        {
            return await Task.FromResult(Table.Include(b => b.Authors).ToList());
        }

        public async Task SetToNonexistent(int id)
        {
            var book = await GetAsync(id);
            book.IsExist = false;
            db.Entry(book).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Book> GetLastAddedBook()
        {
            return await Task.FromResult(Table.ToList().Last());
        }

        public async Task<Book> GetFullBookInfo(int id)
        {
            return await Table.Include(b => b.Authors).FirstOrDefaultAsync(entity => entity.Id == id);
        }
    }
}
