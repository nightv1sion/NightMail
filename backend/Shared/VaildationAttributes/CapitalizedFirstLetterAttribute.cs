using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VaildationAttributes
{
    public class CapitalizedFirstLetterAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value is string str && !String.IsNullOrEmpty(str) && str[0] == Char.ToUpper(str[0]))
                return true;

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must have capital first letter";
        }
    }
}
