using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_11.Models.Tasks
{
    public class NewTaskViewModel
    {
        [Required]
        [StringLength(54, MinimumLength = 4, ErrorMessage = "Task text length must be 4-54 symbols")]
        public string Text { get; set; }
    }
}
