using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstractions.Dto.Book;

namespace Services.Abstractions.Dto.Author
{
    public class CreateAuthorDto
    {
        public string FullName { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
