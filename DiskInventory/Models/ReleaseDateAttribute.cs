using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DiskInventory.Models;

namespace DiskInventory.Models
{
    public class ReleaseDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;
            var max = DateTime.Now;
            var min = DateTime.Now.AddYears(-250);
            if(date > max || date < min)
            {
                return new ValidationResult("Date must be in the past 250 years.");
            } else
            {
                return ValidationResult.Success;
            }


        }
    }
}
