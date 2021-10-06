using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_10.Models.Users
{
    public class NewUserViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9_]{3,15}$", ErrorMessage = "Incorrect entered login." +
                    "\nmust be 4-16 symbols, English language, first a letter, only letters, gigits, symbol _")]
        public string Login { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9_]{4,16}$", ErrorMessage = "Incorrect entered password." +
                    "\nmust be 4-16 symbols, English language, only letters, gigits, symbol _")]
        public string Password { get; set; }


        [Required]
        [Range(16, 104, ErrorMessage = "Age must be in the 16-104 age range.")]
        public int Age { get; set; }


        [Required]
        [StringLength(54, MinimumLength = 4, ErrorMessage = "Question length must be 4-54 symbols")]
        [Display(Name = "Secret question")]
        public string SecretQuestion { get; set; }


        [Required]
        [StringLength(24, MinimumLength = 4, ErrorMessage = "Answer length must be 4-24 symbols")]
        [Display(Name = "Answer for secret question")]
        public string SecretQuestionAnswer { get; set; }
    }
}
