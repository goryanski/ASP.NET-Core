﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Country : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
