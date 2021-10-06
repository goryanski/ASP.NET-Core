using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_13.Business.DTO.Books
{
    public class BooksInRangeDTO
    {
        [Required]
        [Range(1, 10_000)]
        public int From { get; set; }

        [Required]
        [Range(1, 10_000)]
        public int To { get; set; }
    }
}
