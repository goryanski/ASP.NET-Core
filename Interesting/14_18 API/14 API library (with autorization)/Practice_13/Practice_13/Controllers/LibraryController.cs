using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice_13.Business.DTO;
using Practice_13.Business.DTO.Books;
using Practice_13.Business.Services.Interfaces;

namespace Practice_13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibraryController : ControllerBase
    {
        IBooksService booksService;
        IAuthorsService authorsService;
        public LibraryController(IBooksService booksService, IAuthorsService authorsService)
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
        }


        [HttpGet]
        public async Task<IEnumerable<BookDTO>> GetAllBooks()
        {
            // send http://localhost:54496/api/library
            return await booksService.GetAllBooksWithAuthors();
        }

        [HttpPost]
        public async Task<BookDTO> Create(BookDTO book)
        {
            // send book like (authors will be created separately)
            /*
             {
                 "name": "Test book",
                 "pages": 670,
                 "genre": "Horror",
                 "publishYear": 2021,
                 "isExist": true
             }
             */
            await booksService.CreateBook(book);
            return await booksService.GetLastAddedBook();
        }


        [HttpPut]
        public async Task<ActionResult> Update(BookDTO book)
        {
            // send book like
            /*
             {
                "id": 3, // id of book what we want to update
                "name": "New Test book",
                "pages": 100,
                "genre": "Horror",
                "publishYear": 2021,
                "isExist": true
             }
             */
            if (await booksService.BookWasNotFound(book.Id))
            {
                return BadRequest("Book was not found");
            }

            await booksService.UpdateBook(book);
            return new JsonResult(await booksService.GetBookById(book.Id));
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            // send http://localhost:54496/api/library/2

            if (await booksService.BookWasNotFound(id))
            {
                return BadRequest("Book was not found");
            }

            await booksService.DeleteBook(id);
            return new JsonResult(new
            {
                Message = "Success",
                Code = 200
            });
        }


        [HttpGet("bookPagesInRange")]
        public async Task<IEnumerable<BookDTO>> GetProductsInPriceRange([FromQuery] BooksInRangeDTO range) 
        {
            // send http://localhost:54496/api/library/bookPagesInRange?from=100&to=500
            return await booksService.GetBooksInPagesRange(range.From, range.To);
        }


        [HttpGet("bookById")]
        [Route("bookById/{id:int}")]
        public async Task<BookDTO> GetBookById(int id)
        {
            // send http://localhost:54496/api/library/bookById/3
            if (await booksService.BookWasNotFound(id))
            {
                return new BookDTO { Name = "Error. Book not found" };
            }
            return await booksService.GetFullBookInfo(id);
        }


        [HttpPut("WriteOffBook")]
        [Route("WriteOffBook/{id:int}")]
        public async Task<ActionResult> WriteOffBook(int id)
        {
            // send http://localhost:54496/api/library/WriteOffBook/3
            if (await booksService.BookWasNotFound(id))
            {
                return BadRequest("Book was not found");
            }

            await booksService.WriteOffBook(id);
            return new JsonResult(await booksService.GetBookById(id));
        }


        [HttpGet("booksByGenre")]
        [Route("booksByGenre/{genre}")]
        public async Task<IEnumerable<BookDTO>> GetBooksByGenre(string genre)
        {
            // send http://localhost:54496/api/library/booksByGenre/Drama
            if (await booksService.GenreWasNotFound(genre))
            {
                return new List<BookDTO> { new BookDTO { Name = "Error. There're no books with such genre" } };
            }
            return await booksService.GetBooksByGenre(genre);
        }


        [HttpGet("authorBooks")]
        [Route("authorBooks/{id:int}")]
        public async Task<AuthorDTO> GetAuthorBooks(int id)
        {
            // send http://localhost:54496/api/library/authorBooks/1
            if (await authorsService.AuthorWasNotFound(id))
            {
                return new AuthorDTO { FullName = "Error. There're no authors with such id" };
            }
            return await authorsService.GetAuthorFullInfo(id);
        }
    }
}
