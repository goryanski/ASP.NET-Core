using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstractions.Dto.Book;

namespace Services.Abstractions.Dto.Author
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public List<BookDto> Books { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
