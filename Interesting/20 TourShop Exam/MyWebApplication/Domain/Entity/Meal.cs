using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Meal : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}
