﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_19.Data.Entity
{
    public class Translation
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Lang { get; set; }
    }
}
