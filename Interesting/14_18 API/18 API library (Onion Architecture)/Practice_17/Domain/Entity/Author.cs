using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Author : BaseEntity<Guid>
    {
        public string FullName { get; set; }
        public List<Book> Books { get; set; }
    }
}
