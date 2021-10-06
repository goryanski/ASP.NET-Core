using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstractions.Dto.Account;
using Services.Abstractions.Dto.Book;

namespace Services.Abstractions.Dto.UseBook
{
    public class CreateUseBookDto
    {
        public AccountDto User { get; set; }
        public BookDto Book { get; set; }
        public DateTime TakeAt { get; set; }
        public DateTime ReturnAt { get; set; }
    }
}
