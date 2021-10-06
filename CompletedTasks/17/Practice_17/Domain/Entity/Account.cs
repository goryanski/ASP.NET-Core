using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Account : BaseEntity<Guid>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
