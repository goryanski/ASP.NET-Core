using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity
{
    public class User : IdentityUser
    {
        // extra field
        public int Year { get; set; }
    }
}
