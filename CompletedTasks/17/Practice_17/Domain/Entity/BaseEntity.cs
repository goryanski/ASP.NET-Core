using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public abstract class BaseEntity<T>
        where T : struct
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
