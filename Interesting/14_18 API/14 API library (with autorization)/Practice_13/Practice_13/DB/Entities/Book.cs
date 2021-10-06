using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_13.DB.Entities
{
    public class Book: BaseEntity
    {
        public string Name { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public int PublishYear { get; set; }
        public bool IsExist { get; set; }

        public List<Author> Authors { get; set; }
    }
}
