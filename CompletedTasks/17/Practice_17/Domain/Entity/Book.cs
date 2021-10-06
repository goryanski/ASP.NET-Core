using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Book : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public bool IsExist { get; set; }
        public int PublishYear { get; set; }

        public List<Author> Authors { get; set; }
    }
}
