using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GetTravelApplication.Helpers.Attributes
{
    class BirthAttribute : ValidationAttribute
    {
        private int from;
        public int To { get; set; } = DateTime.Now.Year;
        public BirthAttribute(int year)
        {
            from = year;
        }

        public override bool IsValid(object value)
        {
            var val = (int)value;
            return val >= from && val <= To;
        } 
    }
}
