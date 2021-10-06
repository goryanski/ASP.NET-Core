using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.Business.DTO.Books;

namespace Practice_13.Business.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
