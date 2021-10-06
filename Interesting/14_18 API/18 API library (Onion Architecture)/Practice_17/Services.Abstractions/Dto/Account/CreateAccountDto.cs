using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstractions.Dto.Account
{
    public class CreateAccountDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
