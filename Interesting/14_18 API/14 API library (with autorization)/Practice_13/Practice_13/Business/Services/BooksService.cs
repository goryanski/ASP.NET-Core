using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.Business.DTO.Books;
using Practice_13.Business.Services.Interfaces;
using Practice_13.DB.Entities;
using Practice_13.DB.Repositories.Interfaces;

namespace Practice_13.Business.Services
{
    public class BooksService : IBooksService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public BooksService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<bool> BookWasNotFound(int id)
        {
            return await GetBookById(id) == null;
        }

        public async Task CreateBook(BookDTO book)
        {
            var result = objectMapper.Mapper.Map<Book>(book);
            await uow.BooksRepository.CreateAsync(result);
        }

        public async Task DeleteBook(int id)
        {
            await uow.BooksRepository.DeleteAsync(id);
        }

        public async Task<bool> GenreWasNotFound(string genre)
        {
            var books = await uow.BooksRepository.GetAllAsync();
            return books.Where(b => b.Genre.Equals(genre)).ToList().Count == 0;
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
            var result = await uow.BooksRepository.GetAllAsync();
            return objectMapper.Mapper.Map<List<BookDTO>>(result);
        }

        public async Task<List<BookDTO>> GetAllBooksWithAuthors()
        {
            var result = await uow.BooksRepository.GetAllBooksWithAuthors();

            // to avoid queries cycle
            result.ForEach(b =>
            {
                b.Authors.ForEach(a =>
                {
                    a.Books = null;
                });
            });
            return objectMapper.Mapper.Map<List<BookDTO>>(result);
        }

        public async Task<BookDTO> GetBookById(int id)
        {
            var result = await uow.BooksRepository.GetAsync(id);
            return objectMapper.Mapper.Map<BookDTO>(result);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksByGenre(string genre)
        {
            var result = await uow.BooksRepository.GetAllAsync(b => b.Genre.Equals(genre));
            return objectMapper.Mapper.Map<List<BookDTO>>(result);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksInPagesRange(int from, int to)
        {
            var result = await uow.BooksRepository.GetAllAsync(b => b.Pages >= from && b.Pages <= to);
            return objectMapper.Mapper.Map<List<BookDTO>>(result);
        }

        public async Task<BookDTO> GetFullBookInfo(int id)
        {
            var book = await uow.BooksRepository.GetFullBookInfo(id);

            book.Authors.ForEach(a =>
            {
                a.Books = null;
            });
            return objectMapper.Mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO> GetLastAddedBook()
        {
            var result = await uow.BooksRepository.GetLastAddedBook();
            return objectMapper.Mapper.Map<BookDTO>(result);
        }

        public async Task UpdateBook(BookDTO book)
        {
            var result = objectMapper.Mapper.Map<Book>(book);
            await uow.BooksRepository.Update(result);
        }

        public async Task WriteOffBook(int id)
        {
            await uow.BooksRepository.SetToNonexistent(id);
        }
    }
}
