using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstractions.Dto.UseBook
{
    public class UseBookDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime TakeAt { get; set; }
        public DateTime ReturnAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
