using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.VaildationAttributes
{
    public class NightMailAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string pattern = @"^[0-9a-zA-Z]+@nightmail.com$";
            if (value is string str && Regex.IsMatch(str, pattern))
                return true;

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must match '[0-9a-z]@nightmail.com' format";
        }
    }
}
