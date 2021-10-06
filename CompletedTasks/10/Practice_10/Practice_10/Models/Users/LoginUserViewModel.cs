using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_10.Models.Users
{
    public class LoginUserViewModel
    {
        // login validation will be on backend side, because we need to check if the login is unique or not. For validation will be used UsersService
        [Required]
        public string Login { get; set; }


        // password validation will be on backend side, because if checkbox PasswordHasBeenForgotten will be true we don't need to check the password. For validation will be used UsersService
        public string Password { get; set; }


        [Display(Name = "I forgot my password")]
        public bool PasswordHasBeenForgotten { get; set; }
    }
}
