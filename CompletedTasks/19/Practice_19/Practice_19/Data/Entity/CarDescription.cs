using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_19.Data.Entity
{
    public class CarDescription
    {
        public int Id { get; set; }
        public List<Translation> Translations { get; set; }
    }
}
