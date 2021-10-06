using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstractions.Dto.Author;

namespace Services.Abstractions.Dto.Book
{
    public class UpdateBookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public bool IsExist { get; set; }
        public int PublishYear { get; set; }

        public List<AuthorDto> Authors { get; set; }
    }
}
