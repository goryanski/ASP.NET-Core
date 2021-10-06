using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_10.Models.Users
{
    public class RestoreUserViewModel
    {
        [Display(Name = "User")]
        public string Login { get; set; }

        [Display(Name = "Answer your secret question to get access the site")]
        public string SecretQuestion { get; set; }

        [Required]
        public string SecretQuestionAnswer { get; set; }
    }
}
