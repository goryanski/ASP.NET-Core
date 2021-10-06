using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.Business.DTO.Books;

namespace Practice_13.Business.Services.Interfaces
{
    public interface IBooksService
    {
        Task CreateBook(BookDTO book);
        Task<BookDTO> GetBookById(int id);
        Task<List<BookDTO>> GetAllBooksWithAuthors();
        Task<List<BookDTO>> GetAllBooks();
        Task<BookDTO> GetLastAddedBook();
        Task<bool> BookWasNotFound(int id);
        Task UpdateBook(BookDTO book);
        Task DeleteBook(int id);
        Task<IEnumerable<BookDTO>> GetBooksInPagesRange(int from, int to);
        Task<BookDTO> GetFullBookInfo(int id);
        Task WriteOffBook(int id);
        Task<bool> GenreWasNotFound(string genre);
        Task<IEnumerable<BookDTO>> GetBooksByGenre(string genre);
    }
}
