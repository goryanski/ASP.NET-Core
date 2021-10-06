using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_11.Models.Users.Auth
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9_]{4,16}$", ErrorMessage = "Incorrect entered password." +
                    "\nmust be 4-16 symbols, English language, only letters, gigits, symbol _")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm your password")]
        public string ConfirmPassword { get; set; }
    }
}
